using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Роль - Страница" для ограничения страниц определенным ролям.
	/// </summary>
	public class BanRoleWebPage
	{
		public int RoleId { get; set; }
		public Role Role { get; set; }

		public int WebPageId { get; set; }
		public WebPage WebPage { get; set; }
	}
}
