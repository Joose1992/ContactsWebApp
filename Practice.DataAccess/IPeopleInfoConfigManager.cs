using System;
namespace Practice.DataAccess
{
	public interface IPeopleInfoConfigManager
	{
		string PeopleInfoConnection { get; }

		string GetConnectionString(string connectionName);
	}
}

