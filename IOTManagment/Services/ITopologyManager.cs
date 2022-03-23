using Model.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITopologyManager 
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

        //TODO: Maybe change the Key-type from object to something comparable?
        Dictionary<IPEndPoint, INode> IPAdresses { get; } 
        public void AddNode(IPEndPoint nodeAdd, INode Node);

        public void UpdateNode(IPEndPoint nodeAdd, INode node);
    }
}
