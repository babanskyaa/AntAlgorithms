using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Элементарная проверка
    /// </summary>
    public class Check
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Порядковый номер в последовательности проверок
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает значение поля Index (Value)
        /// </summary>
        public override string ToString()
        {
            return Index.ToString();
        }
    }
}
