using Model.Nodes.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEngine.Services
{
    public interface ISensorManager
    {
        public string GetSensorData(DataType type);
    }
}
