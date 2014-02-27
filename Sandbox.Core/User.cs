using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Core
{
	public class User
	{
		public User()
		{
			Data = new List<Datum>();
		}

		public virtual IList<Datum> Data { get; protected set; }

		public virtual string DisplayName { get; set; }

		public virtual int UserId { get; protected set; }
		public virtual string Username { get; set; }
	}
}
