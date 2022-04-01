using Model.Nodes.Enum;

namespace NodeEngine.Services
{
    public interface ISensorManager
    {
        public string GetSensorData(DataType type);
    }
}
