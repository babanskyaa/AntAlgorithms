using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EntityLayer;
using System.Diagnostics;

namespace LogicLayer
{
    public class DILogic
    {
        /// <summary>
        /// Конструктор класса логики,
        /// принимает на вход путь к файлу с данными
        /// </summary>
        public DILogic(string fileName)
        {
            ReadFromFile(fileName);
        }

        public DILogic()
        {
        }

        #region Fields
        /// <summary>
        /// Флаг для записи параметра c в файл
        /// </summary>
        private bool FlagHybrid;

        /// <summary>
        /// Исходное количество элементов, результативное количество элементов, разница
        /// </summary>
        private double from;
        private double to;
        private double diff;

        /// <summary>
        /// Диагностическая информация
        /// </summary>
        private List<DiagnosticInfo> ListDI { get; set; }

        /// <summary>
        /// Возвращает диагностическую информацию
        /// </summary>
        public List<DiagnosticInfo> GetListDI()
        {
            return ListDI;
        }

        /// <summary>
        /// Состояния
        /// </summary>
        private List<State> States { get; set; }

        /// <summary>
        /// Возвращает набор всех проверок
        /// </summary>
        public List<State> GetStates()
        {
            return States;
        }

        /// <summary>
        /// Элементарные проверки
        /// </summary>
        private List<Check> Checks { get; set; }

        /// <summary>
        /// Возвращает набор всех проверок
        /// </summary>
        public List<Check> GetChecks()
        {
            return Checks;
        }

        /// <summary>
        /// Множество записей в СПР
        /// </summary>
        private List<Record> Records { get; set; }

        /// <summary>
        /// Возвращает набор записей в СПР для заданной маски
        /// </summary>
        public List<Record> GetRecords(Mask mask)
        {
            return GetListRecord(mask);
        }

        /// <summary>
        /// Множество масок
        /// </summary>
        private List<Mask> Masks { get; set; }

        /// <summary>
        /// Возвращает множество масок
        /// </summary>
        public List<Mask> GetMasks()
        {
            return Masks;
        }

        /// <summary>
        /// Ожидаемая глубина диагностирования
        /// </summary>
        private double p1;

        public double GetP1()
        {
            return p1;
        }

        /// <summary>
        /// Диагностическое разрешение
        /// </summary>
        private double p2;

        public double GetP2()
        {
            return p2;
        }

        /// <summary>
        /// Количество муравьев
        /// </summary>
        private int ants;

        public int GetAnts()
        {
            return ants;
        }

        public void SetAnts(int count)
        {
            this.ants = count;
        }

        /// <summary>
        /// Влияние феромонов
        /// </summary>
        private double pherEffect;

        public double GetPherEffect()
        {
            return pherEffect;
        }

        public void SetPherEffect(double count)
        {
            this.pherEffect = count;
        }

        /// <summary>
        /// Скорость испарения феромонов
        /// </summary>
        private double evaporRate;

        public double GetEvaporRate()
        {
            return evaporRate;
        }

        public void SetEvaporRate(double count)
        {
            this.evaporRate = count;
        }

        /// <summary>
        /// Параметр c
        /// </summary>
        private int c;

        public int GetC()
        {
            return c;
        }

        public void SetC(int count)
        {
            this.c = count;
        }

        /// <summary>
        /// Путь для записи отчета
        /// </summary>
        private string fileName = "C:\\temp\\output.txt";

        public string GetFileName()
        {
            return fileName;
        }
        #endregion

        Stopwatch stopWatch = new Stopwatch();
        Random random = new Random();

        /// <summary>
        /// Возвращает DiagnosticInfo по Check и State
        /// </summary>
        public DiagnosticInfo GetDIByCheckState(Check check, State state)
        {
            return ListDI[(state.Index - 1) * Checks.Count + (check.Index - 1)];
        }

        /// <summary>
        /// Возвращает набор состояний,
        /// для которых значение проверки равно параметру value
        /// </summary>
        private List<State> GetListStateByCheckPointValue(List<State> listState, CheckPoint checkPoint, bool value)
        {
            return listState.Where(state =>
                GetDIByCheckState(checkPoint.Check, state)
                    .ValueCheck
                    .GetValue(checkPoint.Bit) == value).ToList();
        }

        /// <summary>
        /// Проверяет содержит ли набор записей СПР запись с состоянием state
        /// </summary>
        private Record GetRecordByState(List<Record> records, State state)
        {
            foreach (var record in records)
            {
                if (record.State == state) return record;
            }
            return null;
        }

        /// <summary>
        /// Возвращает набор ДИ, содержащий только состояния из маски
        /// </summary>
        private List<DiagnosticInfo> FilterDIByMask(Mask mask)
        {
            List<DiagnosticInfo> result = new List<DiagnosticInfo>();
            List<Check> checks = mask.GetChecks();
            foreach (var di in ListDI)
            {
                if (checks.Contains(di.Check)) result.Add(di);
            }
            return result;
        }

        /// <summary>
        /// Возвращает массив записей СПР
        /// </summary>
        private List<Record> GetListRecord(Mask mask)
        {
            List<Record> records = new List<Record>();
            List<DiagnosticInfo> listDIByMask = FilterDIByMask(mask);
            Record record;
            foreach (var di in listDIByMask)
            {
                //если записи с текущим состоянием еще нет в наборе, то создаем ее
                if ((record = GetRecordByState(records, di.State)) == null)
                {
                    records.Add(new Record() { State = di.State, Values = "" });
                    record = GetRecordByState(records, di.State);
                }
                //добавляем бит к записи
                foreach (var checkPoint in mask.GetCheckPoints(di.Check))
                {
                    Bit bit = checkPoint.Bit;
                    record.AppendValue(di.ValueCheck.ToString(bit));
                }
            }
            return records;
        }

        /// <summary>
        /// Чтение из файла
        /// </summary>
        private void ReadFromFile(string fileName)
        {
            ListDI = new List<DiagnosticInfo>();
            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            // Считываем все элементарные проверки из первой строки файла
            string[] strChecks = sr.ReadLine().Split(' ');
            Checks = new List<Check>();
            for (int i = 0; i < strChecks.Length; i++)
            {
                Checks.Add(new Check()
                {
                    Index = i + 1, //индекс с 1 и далее
                    Value = strChecks[i]
                });
            }
            // Считываем остальные данные из файла
            States = new List<State>();
            string[] strArr;
            int ind = 1; //индекс с 1 и далее
            while (sr.Peek() > 0)
            {
                strArr = sr.ReadLine().Split(' ');
                // Считываем состояние
                State state = new State() { Index = ind, Value = strArr[0] };
                States.Add(state);
                ind++;
                for (int j = 1; j < strArr.Length; j++)
                {
                    ListDI.Add(new DiagnosticInfo()
                    {
                        Check = Checks[j - 1],
                        State = state,
                        ValueCheck = new ValueCheck(strArr[j])
                    });
                }
            }
            sr.Close();
            from = Checks.Count * 2;
        }

        /// <summary>
        /// Запись в файл
        /// </summary>
        private void WriteToFile(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
            sw.WriteLine("Входные данные: " + Checks.Count + " элементарных проверок и " + States.Count + " технических состояний");
            sw.Write(Environment.NewLine);
            sw.WriteLine("Параметры муравьиного алгоритма:");
            sw.WriteLine("Количество муравьев = " + ants.ToString());
            sw.WriteLine("Влияние феромонов = " + pherEffect.ToString());
            sw.WriteLine("Скорость испарения феромонов = " + evaporRate.ToString());
            if (FlagHybrid)
                sw.WriteLine("Параметр c = " + c.ToString());
            sw.Write(Environment.NewLine);
            foreach (Mask m in Masks)
            {
                foreach (CheckPoint cp in m.CheckPoints)
                {
                    sw.Write(cp.ToString() + " ");
                }
                sw.WriteLine();
                sw.WriteLine("Маска состоит из " + m.CheckPoints.Count + " точек проверки");
                sw.Write(Environment.NewLine);
            }
            sw.WriteLine("Словарь полной реакции:");
            foreach (Record rc in Records)
            {
                sw.WriteLine(rc);
            }
            sw.Write(Environment.NewLine);
            sw.WriteLine("Исходная диагностическая информация сокращена на " + diff + "%");
            sw.WriteLine("Ожидаемая глубина диагностирования (p1): " + p1.ToString());
            sw.WriteLine("Диагностическое разрешение (p2): " + p2.ToString());
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            sw.Write("Время работы алгоритма: " + elapsedTime);
            sw.Close();
        }

        /// <summary>
        /// Выбор точки проверки (по вероятности)
        /// </summary>
        private CheckPoint SelectCheckPoint(List<CheckPoint> numbersMinI, double pherEffect)
        {
            List<Double> p = new List<Double>();

            // Считаем вероятности выбора для каждой точки проверки
            double summPheromones = 0;

            for (int i = 0; i < numbersMinI.Count; i++)
            {
                summPheromones += Math.Pow(numbersMinI.ElementAt(i).Pheromone, pherEffect);
            }

            for (int j = 0; j < numbersMinI.Count; j++)
            {
                p.Add((Math.Pow(numbersMinI.ElementAt(j).Pheromone, pherEffect) / summPheromones));
            }
            // Осуществляем выбор точки проверки исходя из вероятностей выбора
            double summ = 0;

            int k = -1;
            double rand = random.NextDouble();
            while (summ < rand && k < p.Count - 1)
            {
                k++;
                summ += p.ElementAt(k);
            }
            return numbersMinI.ElementAt(k);
        }

        /// <summary>
        /// Возвращает J для текущего шага (Гибридный муравьиный алгоритм)
        /// </summary>
        private double FindHybridJ(List<List<State>> S, CheckPoint numberMinI)
        {
            // Множество минимумов для unusedI
            List<double> minForUnused = new List<double>();
            //идем по всем подмножествам State
            for (int subS = 0; subS < S.Count; subS++)
            {
                // Счетчики нулей и единиц
                int count0 = 0;
                int count1 = 0;
                // Идем по всем State
                for (int k = 0; k < States.Count; k++)
                {
                    // Если в подмножестве технических состояний содержится нужное
                    if (S[subS].Contains(States[k]))
                    {
                        if (GetDIByCheckState(numberMinI.Check, States[k])
                            .ValueCheck
                            .GetValue(numberMinI.Bit)) count1++;
                        else count0++;
                    }
                }
                // Вычисляем для текущего подмножества State минимум
                // Проверка на 0, иначе логарифмы не существуют
                if (count0 == 0 || count1 == 0)
                {
                    if (count0 == 0)
                    {
                        minForUnused.Add(((double)count1 / S[subS].Count * Math.Log(count1, 2))
                       * (double)S[subS].Count / (States.Count));
                    }
                    if (count1 == 0)
                    {
                        minForUnused.Add(((double)count0 / S[subS].Count * Math.Log(count0, 2))
                       * (double)S[subS].Count / (States.Count));
                    }
                }
                else
                {
                    minForUnused.Add(((double)count0 / S[subS].Count * Math.Log(count0, 2)
                       + (double)count1 / S[subS].Count * Math.Log(count1, 2))
                       * (double)S[subS].Count / (States.Count));
                }
            }
            double result = 0;
            for (int t = 0; t < minForUnused.Count; t++)
            {
                result += minForUnused[t];
            }
            return result;
        }

        /// <summary>
        /// Запуск гибридного муравьиного алгоритма
        /// </summary>
        public void FindHybridAntMasks()
        {
            stopWatch.Start();
            List<CheckPoint> allCheckPoints = new List<CheckPoint>();
            List<CheckPoint> resultCheckPoints = new List<CheckPoint>();
            List<CheckPoint> numbersMinI = new List<CheckPoint>();
            List<List<State>> Si = new List<List<State>>();
            Masks = new List<Mask>();
            int ant = 0;
            FlagHybrid = true;
            for (int i = 0; i < Checks.Count; i++)
            {
                allCheckPoints.Add(new CheckPoint() { Bit = Bit.First, Check = Checks[i], Pheromone = 0.1 });
                allCheckPoints.Add(new CheckPoint() { Bit = Bit.Second, Check = Checks[i], Pheromone = 0.1 });
            }
            while (ant < ants)
            {
                Si.Clear();
                // Заполняем массив S0 всеми State
                Si.Add(States);
                // Заполняем массив неиспользованных столбцов всеми столбцами
                List<CheckPoint> unusedI = new List<CheckPoint>(allCheckPoints);
                CheckPoint numberMinI = new CheckPoint();
                double jCur = Math.Log(States.Count, 2);
                int countJ = 0;
                double difference = 0.000001;
                // Вычисляем в цикле jNext
                for (int k = 0; k < States.Count - 1; k++)
                {
                    numberMinI = SelectCheckPoint(unusedI, pherEffect);
                    double jNext = -1;
                    jNext = FindHybridJ(Si, numberMinI);
                    if (Math.Abs(jCur - jNext) <= difference)
                        countJ++;
                    else
                        countJ = 0;
                    List<List<State>> Stemp = new List<List<State>>();
                    for (int i = 0; i < Si.Count; i++)
                    {
                        List<State> newS0 = GetListStateByCheckPointValue(Si[i], numberMinI, false);
                        List<State> newS1 = GetListStateByCheckPointValue(Si[i], numberMinI, true);
                        if (newS1.Count > 0) Stemp.Add(newS1);
                        if (newS0.Count > 0) Stemp.Add(newS0);
                    }
                    Si = Stemp;
                    numberMinI.Pheromone += 1.0 / jNext;
                    resultCheckPoints.Add(numberMinI);
                    unusedI.Remove(numberMinI);
                    jCur = jNext;
                    if (countJ == c)
                    {
                        for (int i = 0; i < c; i++)
                        {
                            resultCheckPoints[resultCheckPoints.Count - 1].Pheromone -= 1.0 / jNext;
                            resultCheckPoints.RemoveAt(resultCheckPoints.Count - 1);
                        }
                        break;
                    }
                    if (unusedI.Count == 0) break;
                }
                Masks.Add(new Mask() { CheckPoints = resultCheckPoints });
                foreach (var cp in resultCheckPoints)
                {
                    if (cp.Pheromone > 0.1)
                    {
                        cp.Pheromone *= (1 - evaporRate);
                        if (cp.Pheromone < 0.1)
                            cp.Pheromone = 0.1;
                    }
                }
                resultCheckPoints = new List<CheckPoint>();
                ant++;
                ProcessChanged(ant);
            }
            Records = GetListRecord(Masks[Masks.Count - 1]);
            to = Masks[Masks.Count - 1].Count();
            diff = Math.Round(100.0 - (to * 100 / from), 2);
            p1 = FindP1(Records);
            p2 = FindP2(p1);
            stopWatch.Stop();
            WriteToFile(fileName);
            WorkCompleted(true);
        }

        /// <summary>
        /// Возвращает J для текущего шага (Простой муравьиный алгоритм)
        /// </summary>
        private double FindSimpleJ(List<List<State>> S, List<CheckPoint> unusedI, List<CheckPoint> numbersMinI)
        {
            double min = Double.MaxValue;
            // Идем по всем неиспользованным CheckPoint
            for (int i = 0; i < unusedI.Count; i++)
            {
                // Множество минимумов для unusedI
                List<double> minForUnused = new List<double>();
                //идем по всем подмножествам State
                for (int subS = 0; subS < S.Count; subS++)
                {
                    // Счетчики нулей и единиц
                    int count0 = 0;
                    int count1 = 0;
                    // Идем по всем State
                    for (int k = 0; k < States.Count; k++)
                    {
                        // Если в подмножестве технических состояний содержится нужное
                        if (S[subS].Contains(States[k]))
                        {
                            if (GetDIByCheckState(unusedI[i].Check, States[k])
                                .ValueCheck
                                .GetValue(unusedI[i].Bit)) count1++;
                            else count0++;
                        }
                    }
                    // Вычисляем для текущего подмножества State минимум
                    // Проверка на 0, иначе логарифмы не существуют
                    if (count0 == 0 || count1 == 0)
                    {
                        if (count0 == 0)
                        {
                            minForUnused.Add(((double)count1 / S[subS].Count * Math.Log(count1, 2))
                           * (double)S[subS].Count / (States.Count));
                        }
                        if (count1 == 0)
                        {
                            minForUnused.Add(((double)count0 / S[subS].Count * Math.Log(count0, 2))
                           * (double)S[subS].Count / (States.Count));
                        }
                    }
                    else
                    {
                        minForUnused.Add(((double)count0 / S[subS].Count * Math.Log(count0, 2)
                           + (double)count1 / S[subS].Count * Math.Log(count1, 2))
                           * (double)S[subS].Count / (States.Count));
                    }
                }
                double totalMin = 0;
                for (int t = 0; t < minForUnused.Count; t++)
                {
                    totalMin += minForUnused[t];
                }
                if (min > totalMin)
                {
                    min = totalMin;
                    numbersMinI.Clear();
                    numbersMinI.Add(unusedI[i]);
                }
                else if (min == totalMin)
                {
                    numbersMinI.Add(unusedI[i]);
                }
            }
            return min;
        }

        /// <summary>
        /// Запуск простого муравьиного алгоритма
        /// </summary>
        public void FindSimpleAntMasks()
        {
            stopWatch.Start();
            List<CheckPoint> allCheckPoints = new List<CheckPoint>();
            List<CheckPoint> resultCheckPoints = new List<CheckPoint>();
            List<CheckPoint> numbersMinI = new List<CheckPoint>();
            List<List<State>> Si = new List<List<State>>();
            Masks = new List<Mask>();
            int ant = 0;
            FlagHybrid = false;
            for (int i = 0; i < Checks.Count; i++)
            {
                allCheckPoints.Add(new CheckPoint() { Bit = Bit.First, Check = Checks[i], Pheromone = 0.1 });
                allCheckPoints.Add(new CheckPoint() { Bit = Bit.Second, Check = Checks[i], Pheromone = 0.1 });
            }
            while (ant < ants)
            {
                Si.Clear();
                // Заполняем массив S0 всеми State
                Si.Add(States);
                // Заполняем массив неиспользованных столбцов всеми столбцами
                List<CheckPoint> unusedI = new List<CheckPoint>(allCheckPoints);
                double jCur = Math.Round(Math.Log(States.Count, 2), 5);
                // Вычисляем в цикле jNext
                for (int k = 0; k < States.Count - 1; k++)
                {
                    double jNext = -1;
                    jNext = Math.Round(FindSimpleJ(Si, unusedI, numbersMinI), 5);
                    if (jCur == jNext)
                        break;
                    CheckPoint numberMinI = SelectCheckPoint(numbersMinI, pherEffect);
                    // Создаем новый массив технических состояний и разбиваем его
                    List<List<State>> Stemp = new List<List<State>>();
                    for (int i = 0; i < Si.Count; i++)
                    {
                        List<State> newS0 = GetListStateByCheckPointValue(Si[i], numberMinI, false);
                        List<State> newS1 = GetListStateByCheckPointValue(Si[i], numberMinI, true);
                        if (newS1.Count > 0) Stemp.Add(newS1);
                        if (newS0.Count > 0) Stemp.Add(newS0);
                    }
                    // Запоминаем новое разбиение
                    Si = Stemp;
                    // Записываем в маску
                    resultCheckPoints.Add(numberMinI);
                    // Удаляем из неиспользованных
                    unusedI.Remove(numberMinI);
                    // Запоминаем новое значение
                    jCur = jNext;
                }
                foreach (var cp in resultCheckPoints)
                {
                    cp.Pheromone += 1.0 / resultCheckPoints.Count;
                }
                Masks.Add(new Mask() { CheckPoints = resultCheckPoints });
                foreach (var cp in resultCheckPoints)
                {
                    if (cp.Pheromone > 0.1)
                    {
                        cp.Pheromone *= (1 - evaporRate);
                        if (cp.Pheromone < 0.1)
                            cp.Pheromone = 0.1;
                    }
                }
                resultCheckPoints = new List<CheckPoint>();
                ant++;
                ProcessChanged(ant);
            }
            Records = GetListRecord(Masks[Masks.Count - 1]);
            to = Masks[Masks.Count - 1].Count();
            diff = Math.Round(100.0 - (to * 100 / from), 2);
            p1 = FindP1(Records);
            p2 = FindP2(p1);
            stopWatch.Stop();
            WriteToFile(fileName);
            WorkCompleted(true);
        }

        /// <summary>
        /// Возвращает значение ожидаемой глубины диагностирования: [P1]
        /// </summary>
        private double FindP1(List<Record> records)
        {
            double p1 = 1.0 / States.Count();
            List<Record> uniques = new List<Record>();
            List<int> listCount = new List<int>();
            int pos;
            for (int i = 0; i < records.Count; i++)
            {
                if ((pos = Record.IsContainsValueInRecords(uniques, records[i])) != -1)
                {
                    listCount[pos]++;
                }
                else
                {
                    uniques.Add(records[i]);
                    listCount.Add(1);
                }
            }
            double summ = 0.0;
            for (int i = 0; i < listCount.Count; i++)
            {
                summ += listCount[i] * listCount[i];
            }
            return Math.Round(p1 * summ, 5);
        }

        /// <summary>
        /// Возвращает значение диагностического разрешения: [P2]
        /// </summary>
        private double FindP2(double p1)
        {
            return Math.Round((double)(States.Count() - p1) / (double)(States.Count() - 1), 5);
        }

        public event Action<int> ProcessChanged;
        public event Action<bool> WorkCompleted;
    }
}
