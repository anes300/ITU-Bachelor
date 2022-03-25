using Model.Queries.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Messages
{
    public class SelectVariableResult : SelectVariable
    {
        public double? Value { get; set; }
    }
}
