using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Значение элементарной проверки
    /// </summary>
    public class ValueCheck
    {
        /// <summary>
        /// Конструктор,
        /// принимает на вход значения первого и второго битов проверки
        /// </summary>
        public ValueCheck(bool first, bool second)
        {
            First = first;
            Second = second;
        }

        /// <summary>
        /// Конструктор,
        /// принимает на вход строковое значение первого и второго бита
        /// </summary>
        public ValueCheck(string value)
        {
            First = (value[0] == '1') ? true : false;
            Second = (value[1] == '1') ? true : false;
        }

        /// <summary>
        /// Первый бит
        /// </summary>
        private bool First { get; set; }
        /// <summary>
        /// Второй бит
        /// </summary>
        private bool Second { get; set; }

        /// <summary>
        /// Возвращает значение первого или второго бита
        /// </summary>
        public bool GetValue(Bit bit)
        {
            return (bit == Bit.First) ? First : Second;
        }

        /// <summary> 
        /// Переопределенный метод, 
        /// возвращает строку в формате: [First/Second]
        /// </summary>
        public string ToString(Bit bit)
        {
            bool value = (bit == Bit.First) ? First : Second;
            return (value) ? "1" : "0";
        }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает строку в формате: [First][Second]
        /// </summary>
        public override string ToString()
        {
            return this.ToString(Bit.First) + this.ToString(Bit.Second);
        }
    }
}
