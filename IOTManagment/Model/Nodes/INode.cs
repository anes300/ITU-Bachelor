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

        /*
        ˄          0
        |       /  |  \
        |      /   |   \
        |     0    0    0               Communication needs to go both ways
        |    / \  / \  / \
        |    0 0  0 0  0 0
        ˅
        */

        INode Parent { get; }

        //Works as ID
        public IPAddress Address { get; set; }

        enum Status
        {
            Active,
            Inactive
        }

        enum Type
        {
            Sensor = 0,
            Node = 1,
            RouteNode = 2,
        }


    }
}
