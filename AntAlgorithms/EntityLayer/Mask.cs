using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Маска диагностической информации
    /// </summary>
    public class Mask : System.Collections.IEnumerable, IEquatable<Mask>
    {
        /// <summary>
        /// Набор точек проверки
        /// </summary>
        public List<CheckPoint> CheckPoints { get; set; }

        /// <summary>
        /// Возвращает CheckPoint если совпадает с проверкой 
        /// </summary>
        public List<CheckPoint> GetCheckPoints(Check check)
        {
            return CheckPoints.Where(cp => cp.Check == check).ToList();
        }

        /// <summary>
        /// Возвращает Check из всех CheckPoints
        /// </summary>
        public List<Check> GetChecks()
        {
            return CheckPoints.Select(cp => cp.Check).ToList();
        }

        /// <summary>
        /// Сортирует по индексу
        /// </summary>
        public void Sort()
        {
            CheckPoints.Sort();
        }

        /// <summary>
        /// Содержится ли маска в коллекции масок
        /// </summary>
        public bool ContainsIn(List<Mask> masks)
        {
            foreach (var mask in masks)
            {
                if (this.ToString() == mask.ToString())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает строку в формате: [CheckPoint], [CheckPoint], [...]
        /// </summary>
        public override string ToString()
        {
            string result = "";
            foreach (var checkPoint in CheckPoints)
            {
                result += checkPoint.ToString() + ", ";
            }
            return result.Substring(0, result.Length - 2);
        }

        public int Count()
        {
            int count = 0;
            try
            {
                foreach (var checkPoint in CheckPoints)
                {
                    count++;
                }
            }
            catch (NullReferenceException)
            {
            }
            return count;
        }

        public bool Contains(CheckPoint checkpoint)
        {
            foreach (var cp in CheckPoints)
            {
                if (cp.Check == checkpoint.Check && cp.Bit == checkpoint.Bit)
                    return true;
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)CheckPoints).GetEnumerator();
        }

        public bool Equals(Mask other)
        {
            if (this.CheckPoints == other.CheckPoints)
                return true;
            else
                return false;
        }
    }
}
