using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using static HomeApi.Contracts.Validation.Values;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов обновления устройства
    /// </summary>
    public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public EditDeviceRequestValidator() 
        {
            RuleFor(x => x.NewName).NotEmpty(); 
            RuleFor(x => x.NewRoom).NotEmpty().Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }
    }
}