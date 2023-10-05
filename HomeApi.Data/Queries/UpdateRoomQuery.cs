using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Data.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров при обновлении комнаты
    /// </summary>
    public class UpdateRoomQuery
    {
        public string NewName { get; }
        public int? NewArea { get; }
        public bool? NewGasConnected { get; }
        public int? NewVoltage { get; }

        public UpdateRoomQuery(
            string name = null,
            int? area = null,
            bool? gasConnection = null,
            int? voltage = null)
        {
            NewName = name;
            NewArea = area; 
            NewGasConnected = gasConnection;
            NewVoltage = voltage;
        }
    }
}
