using Model.Queries.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Variables
{
    public class SelectVariable
    {
        public string Variable { get; set; }
        public SelectOperator Operator { get; set; }
    }
}
