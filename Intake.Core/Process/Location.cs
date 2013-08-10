using System;
using MPRV.Process;

namespace Intake.Core.Process
{
	public class Location
	{
		public static ProcessResult<bool> GetOrCreateLocation(Model.User user, string name, out Model.Location location)
		{
			location = null;
			return new ProcessResult<bool>(false);
		}
	}
}

