using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Диагностическая информация
    /// </summary>
    public class DiagnosticInfo
    {
        /// <summary>
        /// Значение проверки
        /// </summary>
        public ValueCheck ValueCheck { get; set; }
        /// <summary>
        /// Элементарная проверка
        /// </summary>
        public Check Check { get; set; }
        /// <summary>
        /// Техническое состояние
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает значение поля Value
        /// </summary>
        public override string ToString()
        {
            return ValueCheck.ToString();
        }
    }
}
