using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbBuilding = Repository.Entities.Building;

namespace DataLayer.Converters
{
	public static class BuildingConverter
	{
		public static Building Convert(DbBuilding dbBuilding)
		{
			if (dbBuilding == null) return null;

			return new Building()
			{
				Id = dbBuilding.Id,
				Name = dbBuilding.Name,
				ShortName = dbBuilding.ShortName
			};
		}

		public static DbBuilding Convert(Building building)
		{
			if (building == null) return null;

			return new DbBuilding()
			{
				Id = building.Id,
				Name = building.Name,
				ShortName = building.ShortName
			};
		}
	}
}
