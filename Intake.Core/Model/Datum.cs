using System;
using System.Collections.Generic;
using MPRV.Common;
using MPRV.Model;
using MPRV.Model.Data;
using System.Linq;

namespace Intake.Core.Model
{
	public class Datum : MPRV.Model.WritableModel<Datum>
	{
		#region Constructors
		public Datum()
			:base()
		{
			Tags = new List<string>();
		}
		#endregion
		#region Public Properties
		[DataRowPopulator.Column("DatumId")]
		[Equatable]
		[ParameterList.Parameter("@DatumId")]
		public long? DatumId { get; protected set; }

		[DataRowPopulator.Column("Value")]
		[Equatable]
		[ParameterList.Parameter("@Value")]
		public string Value { get; set; }

		[DataRowPopulator.Column("Date")]
		[Equatable]
		[ParameterList.Parameter("@Date")]
		public DateTime Date { get; set; }

		[DataRowPopulator.Column("Description")]
		[Equatable]
		[ParameterList.Parameter("@Description")]
		public string Description { get; set; }

		public User User
		{ 
			get{ return _user.Value;}
			set
			{
				_user = new LazyReadableModel<User>(() => value);
			}
		}

		[DataRowPopulator.Column("Latitude")]
		[Equatable]
		[ParameterList.Parameter("@Latitude")]
		public decimal? Latitude { get; set; }

		[DataRowPopulator.Column("Longitude")]
		[Equatable]
		[ParameterList.Parameter("@Longitude")]
		public decimal? Longitude { get; set; }

		[DataRowPopulator.Column("Accuracy")]
		[Equatable]
		[ParameterList.Parameter("@Accuracy")]
		public decimal? Accuracy { get; set; }

		[DataRowPopulator.Column("Tags")]
		[Equatable]
		[ParameterList.Parameter("@Tags")]
		public ICollection<string> Tags{ get; set; }
		#endregion
		#region Public Methods
		public override bool Commit()
		{
			var row = Data.Datum.Current.Commit(this.BuildParameterList());
			var populator = new MPRV.Model.DataRowPopulator(row);

			this.Populate(populator.Populator);

			return this.IsPopulated;
		}
		#endregion
		#region Private Properties
		[DataRowPopulator.ParentRelationship("User_UserId_Datum_UserId")]
		private LazyReadableModel<User> _user;

		[Equatable]
		[ParameterList.Parameter("@UserId")]
		private long? _userId
		{
			get{ return User == null ? null : User.UserId;}
		}
		#endregion
	}
}

