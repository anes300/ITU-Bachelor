using Model.Queries.Enums;
using Model.Queries.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Variables
{
    public class WhereVariable
    {
        public List<WhereExpression> Expressions { get; set; } = new List<WhereExpression>(); // < > = 
        public List<WhereOperator> Operators { get; set; } = new List<WhereOperator>(); // AND OR

    }
}
