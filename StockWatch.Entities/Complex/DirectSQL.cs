using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockWatch.Utility;

namespace StockWatch.Entities.Complex
{
    public class DirectSQL : ResearchDTO
    {
        public static string Type = "Direct SQL";

        public string SqlStmt { get; set; }

        public override Research ToResearch()
        {
            return new Research
            {
                Name = this.Name,
                Type = DirectSQL.Type,
                Data = (new XElement("Data",
                    new XElement("SqlStmt",
                        TextParser.Base64Encode(
                            this.SqlStmt)))).ToString()
            };
        }
    }
}
