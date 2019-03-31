using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
	public class ConnectionOptions
	{
		public string ConnectionString { get; }

		public ConnectionOptions(string connectionString)
		{
			ConnectionString = connectionString;
		}
	}
}
