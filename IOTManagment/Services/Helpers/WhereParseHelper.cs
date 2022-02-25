using Model.Queries.Enums;
using Model.Queries.Expressions;
using Model.Queries.Statements;
using Model.Queries.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class WhereParseHelper
    {
        public WhereStatement ParseWhere(string whereStatment)
        {
            whereStatment = whereStatment.Replace(" ", string.Empty);     
            var variables = whereStatment.Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            WhereStatement statement = new WhereStatement();
            
            foreach (string expr in variables)
            {
                if(!CheckIfOperator(expr))
                {
                    statement.Variables.Add(ParseVariable(expr));                
                    // the expr is a variable

                } else
                {
                    statement.Operator = ParseOperator(expr);
                }
            }
        
            return statement;
        }


        private bool CheckIfOperator(string expr)
        {
            switch (expr)
            {
                case "&&":
                    return true;
                    break;
                case "||":
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private WhereVariable ParseVariable(string expr)
        {
            var variable = new WhereVariable();

            string pattern = @"(&&|\|\|)";      
            string[] operators = Regex.Matches(expr, pattern).Select(x => x.Value).ToArray();

            foreach (var ops in operators)
            {
                variable.Operators.Add(ParseOperator(ops));
            }

            var exprs = expr.Split(new[] { "&&", "||" }, StringSplitOptions.TrimEntries);
            foreach (var item in exprs)
            {
                var x = item.Split(new[] { "<", ">","<=",">=","=","!=" }, StringSplitOptions.TrimEntries);

                variable.Expressions.Add(new WhereExpression
                {
                    exp1 = x[0],
                    exp2 = x[1],
                    Operator = ParseExpOperator(item.Replace(x[0], "").Replace(x[1], ""))
                });
            }

            return variable;
        }

        private WhereExpOperator ParseExpOperator(string ops)
        {
            switch (ops)
            {
                case "<":
                    return WhereExpOperator.LessThan;
                    break;
                case ">":
                    return WhereExpOperator.GreaterThan;
                    break;
                case "<=":
                    return WhereExpOperator.LessThanOrEqual;
                    break;
                case">=":
                    return WhereExpOperator.GreaterThenOrEqual;
                    break;
                case"=":
                    return WhereExpOperator.Equal;
                    break;
                case"!=":
                    return WhereExpOperator.NotEqual;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ops), ops);
                    break;
            }
        }

        private WhereOperator ParseOperator(string ops)
        {
            switch (ops)
            {
                case "||":
                    return WhereOperator.Or;
                    break;
                case "&&":
                    return WhereOperator.And;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ops), ops);
                    break;
            }
        }
    }
}
