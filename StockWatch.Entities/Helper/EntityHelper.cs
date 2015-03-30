using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Helper
{
	public static class EntityHelper
	{
		public static string GetTableName(Type entityType)
		{
			TableAttribute tableAttrib =
				(TableAttribute) Attribute.GetCustomAttribute(entityType, typeof (TableAttribute));
			return (tableAttrib == null) ? string.Empty : tableAttrib.Name;
		}
	}
}

