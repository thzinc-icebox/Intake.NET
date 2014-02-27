using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Sandbox.Core
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Id(u => u.UserId);
			HasMany(u => u.Data)
				.Inverse()
				.Cascade.All();
			Map(u => u.DisplayName);
			Map(u => u.Username);
		}
	}
}
