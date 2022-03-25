using Model.Nodes.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Nodes
{
    public interface INode
    {

        public string? Parent { get; set; }
        public int? ParentPort { get; set; }

        public string Address { get; set; }
        public int AddressPort { get; set; }

        public NodeType Type { get; set; }
        public Status Status { get; set; }
        public DataType DataType { get; set; }


    }
}
