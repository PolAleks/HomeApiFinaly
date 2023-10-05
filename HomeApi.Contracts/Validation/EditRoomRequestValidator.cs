using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HomeApi.Contracts.Validation.Values;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор на изменение устройства
    /// </summary>
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator() 
        {
            When(_ => string.IsNullOrEmpty(_.NewName), () =>
            {
                
            }).Otherwise(() => 
            {
                RuleFor(room => room.NewName).Must(BeSupported)
                    .WithMessage(($"Выберите одну из доступных локаций: {string.Join(", ", Values.ValidRooms)}"));
            });
            
            RuleFor(_ => _.NewVoltage).InclusiveBetween(120, 220);
        }
    }
}
