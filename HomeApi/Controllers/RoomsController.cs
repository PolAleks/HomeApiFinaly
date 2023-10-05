using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        /// <summary>
        /// Редактирование комнаты
        /// </summary>
        /// <param name="id">Guid id комнаты</param>
        /// <param name="value">Новые параметры комнаты</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRoomAsync(
            [FromRoute] Guid id,
            [FromBody] EditRoomRequest value)
        {
            try
            {
                Room editableRoom = await _repository.GetRoomByIdAsync(id);

                if (editableRoom != null)
                    await _repository.Edit(editableRoom,
                        new UpdateRoomQuery(
                            value.NewName,
                            value.NewArea,
                            value.NewGasConnected,
                            value.NewVoltage
                            ));
                else
                    return StatusCode(400, $"Комнаты с Id: {id} нет в базе данных.");
            }
            catch
            {
                return StatusCode(400, $"Не удалось обновить комнату с Id: {id}");
            }            
            return StatusCode(200, $"Комната с Id: {id} отредактирована");
        }
    }
}