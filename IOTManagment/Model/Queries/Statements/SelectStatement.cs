using Model.Queries.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Statements
{
    public class SelectStatement
    {
        public List<SelectVariable> Variables { get; set; } = new List<SelectVariable>();
    }
}
