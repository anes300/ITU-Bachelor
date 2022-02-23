using Model.Enum;
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
        public IPAddress? Parent { get;}

        public IPAddress Address { get; set; }

        public NodeType Type { get; set; }
        public Status Status { get; set; }

        public DataType DataType { get; set; }


        //Root-Constructor
        public Node(IPAddress address, Status status, NodeType nodeType)
        {
            this.Address = address;
            this.Status = status;
            this.Type = nodeType;
           

        }
        public Node(IPAddress address, IPAddress parent, Status status, NodeType nodeType, DataType dataType) 
        { 
            this.Address = address;
            this.Parent = parent;
            this.Status = status;
            this.Type = nodeType;
            this.DataType = dataType;
            
        }

    }
}
