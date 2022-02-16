using Model.Nodes;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TopologyManager : ITopologyManager
    {

        public Dictionary<IPAddress, IPAddress> IPAdresses { get; set; }
        
        public TopologyManager() {
            IPAdresses = new Dictionary<IPAddress, IPAddress>();
        }

        public void AddNode(IPAddress NewNode, IPAddress ParentNode)
        {
           IPAdresses.Add(NewNode, ParentNode ); 
        }
    }
}
