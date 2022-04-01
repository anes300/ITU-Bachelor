using Model.Messages;
using Model.Queries.Statements;

namespace NodeEngine.Services
{
    public interface IQueryHandler
    {
        public bool CheckWhereStatement(WhereStatement statement);
        
        public List<SelectVariableResult>GetSelectResults(SelectStatement statement);
    }
}
