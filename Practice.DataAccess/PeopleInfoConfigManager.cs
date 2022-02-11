using System;
using Microsoft.Extensions.Configuration;

namespace Practice.DataAccess
{
	public class PeopleInfoConfigManager : IPeopleInfoConfigManager
	{
		private readonly IConfiguration _configuration;

		public PeopleInfoConfigManager(IConfiguration configuration)
        {
			_configuration = configuration;
        }

		public string PeopleInfoConnection
        {
            get
            {
                return _configuration["ConnectionStrings:People_Info"];
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
	}
}

