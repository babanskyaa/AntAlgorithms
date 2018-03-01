using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Точка проверки
    /// </summary>
    public class CheckPoint : IComparable<CheckPoint>
    {
        /// <summary>
        /// Элементарная проверка
        /// </summary>
        public Check Check { get; set; }
        /// <summary>
        /// Номер бита
        /// </summary>
        public Bit Bit { get; set; }
        /// <summary>
        /// Количество феромонов
        /// </summary>
        public Double Pheromone { get; set; }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает строку в формате: [Check]:[Bit]
        /// </summary>
        public override string ToString()
        {
            return Check.ToString() + ":" + ((Bit == Bit.First) ? 1 : 2);
        }

        /// <summary>
        /// Переопределенный метод, 
        /// сравнивает два экземпляра CheckPoint
        /// </summary>
        public int CompareTo(CheckPoint checkPoint)
        {
            if (this.Check.Index <= checkPoint.Check.Index)
            {
                if (this.Check.Index < checkPoint.Check.Index) return -1;
                else
                {
                    if (this.Bit == checkPoint.Bit) return 0;
                    else if (this.Bit == Bit.Second) return 1;
                    else return -1;
                }
            }
            else return 1;
        }

        public CheckPoint Clone()
        {
            return (CheckPoint)this.MemberwiseClone();
        }
    }
}
