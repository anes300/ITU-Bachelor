using Model.Nodes.Enum;
using Model.Queries.Statements;
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

            return true;
        }
    }
}
