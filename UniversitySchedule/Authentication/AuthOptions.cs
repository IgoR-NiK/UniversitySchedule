using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySchedule.Authentication
{
	public class AuthOptions
	{
		public const string Issuer = "UniversityScheduleServer";

		public const string Audience = "UniversityScheduleClients";

		const string Key= "ryF3Djk4r1e!i$jijfW1";

		public const int LifeTime = 1;

		public static SymmetricSecurityKey SymmetricSecurityKey { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
	}
}
