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


       

        public string? Parent { get; }

        //Works as ID
        public string Address { get; set; }

        


    }
}
