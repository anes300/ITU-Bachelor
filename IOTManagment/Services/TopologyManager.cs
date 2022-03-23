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

        public Dictionary<IPEndPoint, INode> IPAdresses { get;}
        
        public TopologyManager() {
            IPAdresses = new Dictionary<IPEndPoint, INode>();
        }

        public void AddNode(IPEndPoint nodeAdd, INode node)
        {
           IPAdresses.Add(nodeAdd, node); 
        }

        public void UpdateNode(IPEndPoint nodeAdd, INode node) 
        {
            IPAdresses[nodeAdd] = node;
        }
    }
}
