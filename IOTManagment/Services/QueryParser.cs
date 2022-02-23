using Model.Queries;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QueryParser
    {
        SelectParseHelper SelectParser;
        IntervalParseHelper IntervalParser;
        public QueryParser()
        {
            SelectParser = new SelectParseHelper();
            IntervalParser = new IntervalParseHelper();
        }     
        public Query ParserQuery(string query)
        {
            // split the different statement parts
           string[] statmenets = query.ToLower().Split(new []{"select","interval","where"},StringSplitOptions.TrimEntries);

           var selectStatement = SelectParser.ParseSelect(statmenets[1]);
           var intervalStatement = IntervalParser.ParseInterval(statmenets[2]);
            return null;
        }
    }
}
