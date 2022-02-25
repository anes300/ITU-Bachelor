using Model.Queries.Enums;
using Model.Queries.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Statements
{
    public class WhereStatement
    {
        public List<WhereVariable> Variables { get; set; } = new List<WhereVariable>();
        public WhereOperator Operator { get; set; }

        
    }
}
