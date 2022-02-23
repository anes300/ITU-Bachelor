using Model.Queries.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries
{
    public class Query
    {
        public Guid Id { get; set; }
        public SelectStatement SelectStatement { get; set; }
        public IntervalStatement IntervalStatement { get; set; }
        public WhereStatement? WhereStatement { get; set; }
    }
}
