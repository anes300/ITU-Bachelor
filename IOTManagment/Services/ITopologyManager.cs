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
        /*
       ˄          0
       |       /  |  \
       |      /   |   \
       |     0    0    0               Communication needs to go both ways (missing child atm) How should the proper data-structure look/build like.
       |    / \  / \  / \
       |    0 0  0 0  0 0
       ˅
       */

        Dictionary<IPAddress, INode> IPAdresses { get; set;} 
        public void AddNode(IPAddress nodeAdd, INode Node);

        public void UpdateNode(IPAddress nodeAdd, INode node);
    }
}
