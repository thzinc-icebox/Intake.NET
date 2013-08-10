using System;
using System.Data;
using MPRV.Model.Data;
using System.Linq;

namespace Intake.Core.Model.Data
{
	/// <summary>
	/// Data accessor for <see cref="Model.User"/>
	/// </summary>
	public class User : MPRV.Common.Factory<User>
	{
		#region Queries
		/// <summary>
		/// Query part to select records from "User"
		/// </summary>
		public const string GET_BASE = @"
			SELECT
				CAST(u.UserId AS bigint) AS UserId,
				u.Handle,
				u.Name,
				u.PasswordDigest
			FROM ""User"" u
		";
		/// <summary>
		/// Query to return a result set for populating a <see cref="Model.User"/> by @Handle
		/// </summary>
		private const string GET_BY_HANDLE = @"
			" + GET_BASE + @"
			WHERE u.Handle = @Handle
		";
		/// <summary>
		/// Query to return a result set for populating a <see cref="Model.User"/> by @Handle and @PasswordDigest
		/// </summary>
		private const string GET_BY_HANDLE_AND_PASSWORD_DIGEST = @"
			" + GET_BY_HANDLE + @"
			AND u.PasswordDigest = @PasswordDigest
		";
		/// <summary>
		/// Query to commit a User record to the database
		/// </summary>
		private const string COMMIT = @"
			INSERT INTO ""User"" (Handle, Name, PasswordDigest)
			VALUES (@Handle, @Name, @PasswordDigest)
			RETURNING CAST(UserId AS bigint) AS UserId
		";
		#endregion
		#region Public Methods
		/// <summary>
		/// Commits parameters for a User record to the data store
		/// </summary>
		/// <param name="parameters">Parameters for commiting a User record</param>
		public long Commit(ParameterList parameters)
		{
			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, COMMIT, parameters, "User");

			return ds.Tables["User"].Rows
				.Cast<DataRow>()
					.Select(r => r.Field<long>("UserId"))
					.FirstOrDefault();
		}

		/// <summary>
		/// Gets a <see cref="DataRow"/> for populating a <see cref="Model.User"/>
		/// </summary>
		/// <param name="handle">Handle</param>
		/// <param name="passwordDigest">Password digest</param>
		public DataRow Get(string handle, string passwordDigest)
		{
			ParameterList parameters = new ParameterList();
			parameters.Add("@Handle", handle);
			parameters.Add("@PasswordDigest", passwordDigest);

			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, GET_BY_HANDLE_AND_PASSWORD_DIGEST, parameters, "User");

			return ds.Tables["User"].Rows.Cast<DataRow>().FirstOrDefault();
		}

		/// <summary>
		/// Gets a <see cref="DataRow"/> for populating a <see cref="Model.User"/>
		/// </summary>
		/// <param name="handle">Handle</param>
		public DataRow Get(string handle)
		{
			ParameterList parameters = new ParameterList();
			parameters.Add("@Handle", handle);

			var ds = IDbCommandExecutor.ExecuteQuery("Intake", CommandType.Text, GET_BY_HANDLE, parameters, "User");

			return ds.Tables["User"].Rows.Cast<DataRow>().FirstOrDefault();
		}
		#endregion
	}
}

