using Model.Queries.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class IntervalParseHelper
    {
        public IntervalStatement ParseInterval(string statement)
        {
            int interval = int.Parse(statement);

            return new IntervalStatement
            {
                Interval = interval,
            };
        }
    }
}
