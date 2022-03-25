using Model.Queries.Enums;
using Model.Queries.Statements;
using Model.Queries.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class SelectParseHelper
    {

        public SelectStatement ParseSelect (string selectStatement)
        {
            SelectStatement ResultStatment = new SelectStatement ();

            string[] statements = selectStatement.Split (',');

            foreach (string statement in statements)
            {
               if(statement.Contains('(')) // if contains ( then an operator is present
                {
                    ResultStatment.Variables.Add(new SelectVariable
                    {
                        Variable = statement.Split('(', ')')[1],
                        Operator = GetOperator (statement),
                    });
                } else
                {
                    ResultStatment.Variables.Add(new SelectVariable
                    {
                        Variable = statement,
                        Operator = SelectOperator.None
                    });
                }
            }

            return ResultStatment;
        }


        public SelectOperator GetOperator (string statement)
        {
            string ops = (statement.Split('(')[0]).ToLower();

            return Enum.Parse<SelectOperator>(ops,true);
        }
    }
}
