using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Helper
{
	public static class EntityHelper
	{
		public static string GetTableName(Type entityType)
		{
            return entityType.Name;
		}
	}
}

