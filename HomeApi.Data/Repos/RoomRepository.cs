using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        
        public RoomRepository (HomeApiContext context)
        {
            _context = context;
        }
        
        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
        
        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект Room по Id
        /// </summary>
        /// <param name="id">Уникальный Guid объекта</param>
        /// <returns>Объект Room</returns>
        public Task<Room> GetRoomByIdAsync(Guid id)
        {
            return _context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
        }

        /// <summary>
        /// Обновить существующую комнату
        /// </summary>
        /// <param name="room">Обновляемая комната</param>
        /// <param name="editData">Параметры обновления</param>
        public async Task Edit(Room room, UpdateRoomQuery editData)
        {
            // Проверяем на null новые значения и записываем в свойства изменяемого объекта Room
            room.Name = string.IsNullOrEmpty(editData.NewName) ? room.Name : editData.NewName;
            
            room.Area = editData.NewArea ?? room.Area;
            
            room.GasConnected = editData.NewGasConnected ?? room.GasConnected;

            room.Voltage = editData.NewVoltage ?? room.Voltage;

            EntityEntry entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(room);

            await _context.SaveChangesAsync(); 
        }
    }
}