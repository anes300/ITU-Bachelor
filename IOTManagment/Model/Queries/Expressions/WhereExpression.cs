﻿using Model.Queries.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Queries.Expressions
{
    public class WhereExpression
    {
        public decimal exp1 { get; set; }
        public decimal exp2 { get; set; }
        public WhereExpOperator Operator { get; set; }
    }
}
