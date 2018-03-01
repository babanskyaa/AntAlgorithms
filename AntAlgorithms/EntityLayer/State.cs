using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Техническое состояние
    /// </summary>
    public class State
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Порядковый номер в последовательности состояний
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает значение поля Value (Index)
        /// </summary>
        public override string ToString()
        {
            return Value;
        }
    }
}
