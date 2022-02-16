using Model.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal interface ITopologyManager 
    {

        Dictionary<IPAddress, IPAddress> IPAdresses { get; set;} 
        public void AddNode(IPAddress NewNode, IPAddress ParentNode);
    }
}
