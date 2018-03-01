using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    /// <summary>
    /// Запись в СПР
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Состояние
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Значения битов
        /// </summary>
        public string Values { get; set; }

        /// <summary>
        /// Переопределенный метод, 
        /// возвращает строку в формате: [State] [Values]
        /// </summary>
        public override string ToString()
        {
            return State.ToString() + " " + Values;
        }

        /// <summary>
        /// Добавляет значение value в поле Values
        /// </summary>
        public void AppendValue(string value)
        {
            Values += value;
        }

        /// <summary>
        /// Сравнивает две записи в СПР по полю Values
        /// </summary>
        public static bool IsEqualsValueInRecords(Record r1, Record r2)
        {
            return r1.Values == r2.Values;
        }

        /// <summary>
        /// Возвращает индекс записи
        /// если набор записей уже содержит запись с совпадающим полем Values
        /// </summary>
        public static int IsContainsValueInRecords(List<Record> records, Record r1)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (IsEqualsValueInRecords(r1, records[i])) return i;
            }
            return -1;
        }
    }
}
