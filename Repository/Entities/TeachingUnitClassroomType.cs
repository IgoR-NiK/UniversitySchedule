using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Учебная единица - Тип аудитории по назначению".
	/// </summary>
	public class TeachingUnitClassroomType
	{
		public int TeachingUnitId { get; set; }
		public TeachingUnit TeachingUnit { get; set; }

		public int ClassroomTypeId { get; set; }
		public ClassroomType ClassroomType { get; set; }
	}
}
