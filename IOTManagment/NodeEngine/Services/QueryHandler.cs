using Model.Nodes.Enum;
using Model.Queries.Enums;
using Model.Queries.Expressions;
using Model.Queries.Statements;
using Model.Queries.Variables;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NodeEngine.Services
{
    public class QueryHandler : IQueryHandler
    {
        private readonly ISensorManager sensorManager;
        public QueryHandler(SensorManager sensorManager)
        {
            this.sensorManager = sensorManager;
        }


        public bool CheckWhereStatement(WhereStatement statement)
        {
            try
            {        
                switch (statement.Operator)
                {
                    case WhereOperator.None:
                        return CheckVariable(statement.Variables[0]);
                    case WhereOperator.And:
                        return CheckVariable(statement.Variables[0]) && CheckVariable(statement.Variables[0]);
                    case WhereOperator.Or:
                        return CheckVariable(statement.Variables[0]) || CheckVariable(statement.Variables[0]);
                    default:
                        break;     
                }
                return false;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        private bool CheckVariable(WhereVariable variable)
        {
            if (variable == null) return false;
            bool result = CheckExpression(variable.Expressions.First());
            variable.Expressions.RemoveAt(0);
            
            foreach (var op in variable.Operators)
            {
                switch (op)
                {
                    case WhereOperator.And:
                        bool res = CheckExpression(variable.Expressions.First());
                        variable.Expressions.RemoveAt(0);
                        if(res) result = true;
                        break;
                    case WhereOperator.Or:
                        if (result == false) return false;
                        result = CheckExpression(variable.Expressions.First());
                        variable.Expressions.RemoveAt(0);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        // checks and evals the expression and returns if the expression is true or not
        private bool CheckExpression(WhereExpression exp)
        {
            try
            {       
                switch (exp.Operator)
                {
                    case WhereExpOperator.GreaterThan:
                        return GetExpValue(exp.exp1) > GetExpValue(exp.exp2);
                    case WhereExpOperator.LessThan:
                        return GetExpValue(exp.exp1) < GetExpValue(exp.exp2);
                    case WhereExpOperator.LessThanOrEqual:
                        return GetExpValue(exp.exp1) <= GetExpValue(exp.exp2);
                    case WhereExpOperator.GreaterThenOrEqual:
                        return GetExpValue(exp.exp1) >= GetExpValue(exp.exp2);           
                    case WhereExpOperator.NotEqual:
                        return GetExpValue(exp.exp1) != GetExpValue(exp.exp2);
                    case WhereExpOperator.Equal:
                        return GetExpValue(exp.exp1) == GetExpValue(exp.exp2);
                    default:
                        throw new ArgumentException("Unkown Operator");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when checking expression with message: " + ex.Message);
                throw;
            }
        }

        private double GetExpValue(string exp)
        {
            try
            {           
            // check if the exp is a datatype, if yes then gets the data from sensor
            if (Enum.GetNames(typeof(DataType)).Any(x => x.ToLower() == exp))
            {
                DataType dataType = Enum.Parse<DataType>(exp,true);
                string data = sensorManager.GetSensorData(dataType);
                Double result = -1;

                if (dataType == DataType.TEMPERATURE_CPU)
                    {
                        result = double.Parse(data) / 1000;
                    }

                if (dataType == DataType.TEMPERATURE_GPU)
                    {
                        data.Replace("temp=", "");
                        data.Replace("''C", "");
                        result = double.Parse(data);
                    }

                result = double.Parse(data);
                return result;
            }

            return double.Parse(exp);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("A Error happened, when trying to get expressions value with message: " + ex.Message);
                throw;
            }
        }
    }
}
