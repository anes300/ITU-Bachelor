using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Nodes
{
    public class Node : INode
    {
        public INode Parent { get;}

        public IPAddress Address { get; set; }
    }
}
