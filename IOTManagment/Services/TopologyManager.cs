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

        public Dictionary<IPAddress, INode> IPAdresses { get; set; }
        
        public TopologyManager() {
            IPAdresses = new Dictionary<IPAddress, INode>();
        }

        public void AddNode(IPAddress nodeAdd, INode node)
        {
           IPAdresses.Add(nodeAdd, node); 
        }

        public void UpdateNode(IPAddress nodeAdd, INode node) 
        {
            IPAdresses[nodeAdd] = node;
        }
    }
}
