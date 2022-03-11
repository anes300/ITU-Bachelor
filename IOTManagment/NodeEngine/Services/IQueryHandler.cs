using Model.Queries.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEngine.Services
{
    public interface IQueryHandler
    {
        public bool CheckWhereStatement(WhereStatement statement);
    }
}
