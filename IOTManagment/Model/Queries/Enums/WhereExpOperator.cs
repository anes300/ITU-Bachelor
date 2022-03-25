using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Enums
{
    public enum WhereExpOperator
    {
        GreaterThan,
        LessThan,
        LessThanOrEqual,
        GreaterThenOrEqual,
        NotEqual, 
        Equal
    }
}
