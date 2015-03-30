using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace StockWatch.DataAccess
{
	public class SqlRoutine
	{
		public enum RoutineType
		{
			FUNCTION,
			PROCEDURE,
			VIEW,
			DIRECT //just run the SQL statement
		}
		const string _TAG_SCRIPT = "script";
		const string _TAG_ORDER = "order";
		const string _TAG_TYPE = "type";
		const string _TAG_SCHEMA = "schema";
		const string _TAG_NAME = "name";
		const string _TAG_TEXT = "text";
		const string _TAG_BOOTSTRAP = "bootstrap";


		public int Order { get; set; }
		public string Text { get; set; }
		public RoutineType RType { get; set; }
		public string Schema { get; set; }
		public string Name { get; set; }
		public string Bootstrap { get; set; }

		public string BootStrapText
		{
			get
			{
				switch (this.RType)
				{
				case RoutineType.FUNCTION:
					return string.Format(
						@"IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = '{1}' 
    AND ROUTINE_SCHEMA = '{0}'
    AND ROUTINE_TYPE = 'FUNCTION'
) 
BEGIN
    EXEC ('{2}')
END", this.Schema, this.Name, this.Bootstrap);
				case RoutineType.PROCEDURE:
					return string.Format(
						@"IF OBJECT_ID('[{0}].[{1}]', 'P') IS NULL 
	EXEC ('{2}')", this.Schema, this.Name, this.Bootstrap);
				case RoutineType.VIEW:
					return string.Format(
						@"IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.VIEWS 
    WHERE TABLE_SCHEMA ='{0}'
    AND TABLE_NAME = '{1}')
BEGIN
    EXEC ('{2}')
END", this.Schema, this.Name, this.Bootstrap);
				case RoutineType.DIRECT:
					return string.Empty;
				default:
					throw new Exception("Routine type not supported");
				}
			}
		}

		public static List<SqlRoutine> FromFile(string file)
		{
			XElement elem = XElement.Load(file);
			return elem.Elements(_TAG_SCRIPT)
				.Select(e => FromElement(e))
				.OrderBy(s => s.Order)
				.ToList();
		}

		private static SqlRoutine FromElement(XElement elem)
		{
			SqlRoutine value = new SqlRoutine
			{
				Order = int.Parse(elem.Attribute(_TAG_ORDER).Value),
				Text = elem.Element(_TAG_TEXT).Value,
				RType = EnumUtils.Parse<RoutineType>(elem.Attribute(_TAG_TYPE).Value).Value,
				Schema = elem.Attribute(_TAG_SCHEMA).Value,
				Name = elem.Attribute(_TAG_NAME).Value,
				Bootstrap = elem.Element(_TAG_BOOTSTRAP).Value,
			};
			return value;
		}

	}
}

