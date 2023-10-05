using System.Linq;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-хранилище допустымых значений для валидаторов
    /// </summary>
    public static class Values
    {
        public static string [] ValidRooms = new  []
        {
            "Кухня",
            "Ванная",
            "Гостиная",
            "Туалет"
        };

        /// <summary>
        ///  Метод кастомной валидации для свойства location
        /// </summary>
        public static bool BeSupported(string location)
        {
            return ValidRooms.Any(e => e == location);
        }
    }
}