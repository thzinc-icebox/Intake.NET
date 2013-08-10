using System;
using System.Collections.Generic;
using MPRV.Common;
using MPRV.Model;
using MPRV.Model.Data;
using System.Linq;

namespace Intake.Core.Model
{
	/// <summary>
	/// Represents a single piece of information entered by a <see cref="User"/>
	/// </summary>
	public class Datum : MPRV.Model.WritableModel<Datum>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Model.Datum"/> class
		/// </summary>
		public Datum()
			:base()
		{
			Tags = new List<string>();
		}
		#endregion
		#region Public Properties
		/// <summary>
		/// Datum identifier
		/// </summary>
		[DataRowPopulator.Column("DatumId")]
		[Equatable]
		[ParameterList.Parameter("@DatumId")]
		public long? DatumId { get; protected set; }

		/// <summary>
		/// Value
		/// </summary>
		[DataRowPopulator.Column("Value")]
		[Equatable]
		[ParameterList.Parameter("@Value")]
		public string Value { get; set; }

		/// <summary>
		/// UTC date corresponding to the <see cref="Value"/>
		/// </summary>
		[DataRowPopulator.Column("Date")]
		[Equatable]
		[ParameterList.Parameter("@Date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Optional description of the <see cref="Value"/>
		/// </summary>
		[DataRowPopulator.Column("Description")]
		[Equatable]
		[ParameterList.Parameter("@Description")]
		public string Description { get; set; }

		/// <summary>
		/// <see cref="User"/> who entered the <see cref="Value"/>
		/// </summary>
		public User User
		{ 
			get{ return _user.Value;}
			set
			{
				_user = new LazyReadableModel<User>(() => value);
			}
		}

		/// <summary>
		/// List of textual tags that apply to the <see cref="Value"/>
		/// </summary>
		[DataRowPopulator.Column("Tags")]
		[Equatable]
		[ParameterList.Parameter("@Tags")]
		public ICollection<string> Tags{ get; set; }
		#region Coordinates
		/// <summary>
		/// Latitude corresponding to the <see cref="Value"/>
		/// </summary>
		[DataRowPopulator.Column("Latitude")]
		[Equatable]
		[ParameterList.Parameter("@Latitude")]
		public double? Latitude { get; set; }

		/// <summary>
		/// Longitude corresponding to the <see cref="Value"/>
		/// </summary>
		[DataRowPopulator.Column("Longitude")]
		[Equatable]
		[ParameterList.Parameter("@Longitude")]
		public double? Longitude { get; set; }

		/// <summary>
		/// Accuracy (in meters) of the <see cref="Latitude"/> and <see cref="Longitude"/> coordinates
		/// </summary>
		[DataRowPopulator.Column("Accuracy")]
		[Equatable]
		[ParameterList.Parameter("@Accuracy")]
		public double? Accuracy { get; set; }
		#endregion
		#endregion
		#region Public Methods
		/// <summary>
		/// Commits this instance to the data store
		/// </summary>
		public override bool Commit()
		{
			var row = Data.Datum.Current.Commit(this.BuildParameterList());
			var populator = new MPRV.Model.DataRowPopulator(row);

			this.Populate(populator.Populator);

			return this.IsPopulated;
		}
		#endregion
		#region Private Properties
		/// <summary>
		/// Private backing for <see cref="User"/>
		/// </summary>
		[DataRowPopulator.ParentRelationship("User_UserId_Datum_UserId")]
		private LazyReadableModel<User> _user;

		/// <summary>
		/// User identifier
		/// </summary>
		/// <remarks>This exists to ensure that the UserId is associated to this Datum upon <see cref="Commit()"/></remarks>
		[Equatable]
		[ParameterList.Parameter("@UserId")]
		private long? _userId
		{
			get{ return User == null ? null : User.UserId;}
		}
		#endregion
	}
}

