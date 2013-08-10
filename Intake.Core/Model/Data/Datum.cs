using System;
using System.Data;
using MPRV.Model.Data;
using System.Linq;
using System.Collections.Generic;

namespace Intake.Core.Model.Data
{
	/// <summary>
	/// Data accessor for <see cref="Model.Datum"/>
	/// </summary>
	public class Datum : MPRV.Common.Factory<Datum>
	{
		#region Queries
		/// <summary>
		/// Query part to select a standard set of columns from "Datum"
		/// </summary>
		private const string SELECT_BASE = @"
			SELECT
				CAST(d.DatumId AS bigint) AS DatumId,
				d.UserId,
				d.""Date"",
				d.Description,
				d.Latitude,
				d.Longitude,
				d.Accuracy,
				d.""Value"",
				d.Tags
		";
		/// <summary>
		/// Query part to select records from "Datum"
		/// </summary>
		public const string GET_BASE = @"
			" + SELECT_BASE + @"
			FROM ""Datum"" d
		";
		/// <summary>
		/// Query to return correlated result sets containing the latest data without being filtered
		/// </summary>
		private const string GET_LATEST_PAGED = @"
 			" + SELECT_BASE + @",
				COUNT(*) OVER() AS TotalItems
			INTO TEMPORARY SelectedDatum
			FROM ""Datum"" d
			ORDER BY ""Date"" DESC
			LIMIT @PerPage OFFSET @Offset;

			SELECT * FROM SelectedDatum;

			" + User.GET_BASE + @"
			WHERE EXISTS (
				SELECT *
				FROM SelectedDatum d
				WHERE d.UserId = u.UserId
			);

			DROP TABLE SelectedDatum;
		";
		/// <summary>
		/// Query to return correlated result sets containing the latest data filtered by @UserId
		/// </summary>
		private const string GET_LATEST_PAGED_BY_USER_ID = @"
 			" + SELECT_BASE + @",
				COUNT(*) OVER() AS TotalItems
			INTO TEMPORARY SelectedDatum
			FROM ""Datum"" d
			WHERE d.UserId = @UserId
			ORDER BY ""Date"" DESC
			LIMIT @PerPage OFFSET @Offset;

			SELECT * FROM SelectedDatum;

			" + User.GET_BASE + @"
			WHERE EXISTS (
				SELECT *
				FROM SelectedDatum d
				WHERE d.UserId = u.UserId
			);

			DROP TABLE SelectedDatum;
		";
		/// <summary>
		/// Query to return correlated result sets containing the latest data filtered by @Tags
		/// </summary>
		private const string GET_LATEST_PAGED_BY_TAGS = @"
 			" + SELECT_BASE + @",
				COUNT(*) OVER() AS TotalItems
			INTO TEMPORARY SelectedDatum
			FROM ""Datum"" d
			WHERE d.Tags @> CAST(@Tags AS varchar(64)[])
			ORDER BY ""Date"" DESC
			LIMIT @PerPage OFFSET @Offset;

			SELECT * FROM SelectedDatum;

			" + User.GET_BASE + @"
			WHERE EXISTS (
				SELECT *
				FROM SelectedDatum d
				WHERE d.UserId = u.UserId
			);

			DROP TABLE SelectedDatum;
		";
		/// <summary>
		/// Query to return correlated result sets containing the latest data filtered by @Tags and @UserId
		/// </summary>
		private const string GET_LATEST_PAGED_BY_TAGS_AND_USER_ID = @"
 			" + SELECT_BASE + @",
				COUNT(*) OVER() AS TotalItems
			INTO TEMPORARY SelectedDatum
			FROM ""Datum"" d
			WHERE d.Tags @> CAST(@Tags AS varchar(64)[])
			AND d.UserId = @UserId
			ORDER BY ""Date"" DESC
			LIMIT @PerPage OFFSET @Offset;

			SELECT * FROM SelectedDatum;

			" + User.GET_BASE + @"
			WHERE EXISTS (
				SELECT *
				FROM SelectedDatum d
				WHERE d.UserId = u.UserId
			);

			DROP TABLE SelectedDatum;
		";
		/// <summary>
		/// Query to return correlated result sets for a given @DatumId
		/// </summary>
		private const string GET_BY_DATUM_ID = @"
			" + GET_BASE + @"
			WHERE d.DatumId = @DatumId;

			" + User.GET_BASE + @"
			JOIN ""Datum"" d ON (u.UserId = d.UserId)
			WHERE d.DatumId = @DatumId;
		";
		/// <summary>
		/// Query to commit a Datum record to the database
		/// </summary>
		private const string COMMIT = @"
			INSERT INTO ""Datum"" (UserId, ""Date"", Description, Latitude, Longitude, Accuracy, ""Value"", Tags)
			VALUES (@UserId, @Date, @Description, @Latitude, @Longitude, @Accuracy, @Value, @Tags)
			RETURNING CAST(DatumId AS bigint) AS DatumId;
		";
		#endregion
		#region Public Methods
		/// <summary>
		/// Commits parameters for a Datum record to the data store
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		public DataRow Commit(ParameterList parameters)
		{
			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, COMMIT, parameters, "Datum");

			return ds.Tables["Datum"].Rows
				.Cast<DataRow>()
				.FirstOrDefault();
		}

		/// <summary>
		/// Gets a <see cref="DataRow"/> for populating a <see cref="Model.Datum"/>
		/// </summary>
		/// <param name="datumId">Datum identifier</param>
		public DataRow Get(long datumId)
		{
			ParameterList parameters = new ParameterList();
			parameters.Add("@DatumId", datumId);

			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, GET_BY_DATUM_ID, parameters, "Datum", "User");

			ds.Relations.Add(
				"User_UserId_Datum_UserId",
				ds.Tables["User"].Columns["UserId"],
				ds.Tables["Datum"].Columns["UserId"]
			);

			return ds.Tables["Datum"].Rows.Cast<DataRow>().FirstOrDefault();
		}

		/// <summary>
		/// Gets an enumerable of <see cref="DataRow"/> for populating <see cref="Model.Datum"/> models
		/// </summary>
		/// <returns>Enumerable representing a page of Datum records</returns>
		/// <param name="userId">Optional user identifier to filter by</param>
		/// <param name="tags">Optional enumerable of tags to filter by</param>
		/// <param name="page">Page of results to return</param>
		/// <param name="perPage">Number of results to return per page</param>
		/// <param name="totalItems">Total items</param>
		/// <param name="totalPages">Total pages</param>
		public IEnumerable<DataRow> GetLatestPaged(long? userId, IEnumerable<string> tags, long page, long perPage, out long totalItems, out long totalPages)
		{
			ParameterList parameters = new ParameterList();
			parameters.Add("@PerPage", perPage);
			parameters.Add("@Offset", Math.Max(0, (page - 1) * perPage));

			var anyTags = tags != null && tags.Any();
			var tagsArray = anyTags ? tags.ToArray() : new string[] { };

			string query;
			if (userId.HasValue && anyTags)
			{
				parameters.Add("@Tags", tagsArray);
				parameters.Add("@UserId", userId);
				query = GET_LATEST_PAGED_BY_TAGS_AND_USER_ID;
			}
			else if (userId.HasValue)
			{
				parameters.Add("@UserId", userId);
				query = GET_LATEST_PAGED_BY_USER_ID;
			}
			else if (anyTags)
			{
				parameters.Add("@Tags", tagsArray);
				query = GET_LATEST_PAGED_BY_TAGS;
			}
			else
			{
				query = GET_LATEST_PAGED;
			}

			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, query, parameters, "Datum", "User");

			ds.Relations.Add(
				"User_UserId_Datum_UserId",
				ds.Tables["User"].Columns["UserId"],
				ds.Tables["Datum"].Columns["UserId"]
			);

			var rows = ds.Tables["Datum"].Rows.Cast<DataRow>();

			if (rows.Any())
			{
				var firstRow = rows.First();
				totalItems = firstRow.Field<long>("TotalItems");
				totalPages = (long)Math.Ceiling(totalItems / (double)perPage);
			}
			else
			{
				totalItems = 0;
				totalPages = 0;
			}
			return rows;
		}
		#endregion
	}
}

