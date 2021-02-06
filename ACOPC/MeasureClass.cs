using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using VisaInstrClasses;
using System.Collections;

namespace ACOPC
{
    public class MeasConfigurator
    {
        public static string Name;
        private static int m_StepCount;
        public static int SweepCount { get { return m_StepCount; } set { if ((value > 0) && (value < 20)) m_StepCount = value; } }
        public static bool NewMeas;
        public static bool ShowFS;
        public static InputCoupling InputCoupling;
        private static int m_BandWidth;
        public static int BandWidth { get { return m_BandWidth; } set { if ((value == 9) || (value == 120)) m_BandWidth = value; } }
        private static int m_Span;
        public static int SpanPercentage { get { return m_Span; } set { if ((value > 1) && (value < 100)) m_Span = value; } }
        public static VisaMonitor.MonitorChannels MonitorChannel;
        public static Impedance CorrectionImpedance;
        private static int m_CurrIndex;
        public static int CurrentIndex { get { return m_CurrIndex; } }
        public static bool TrackingGenerator;
        public static VisaAmplifier Amplifier;
        public static VisaGenerator Generator;
        public static VisaAnalyzer Analyzer;
        public static VisaMonitor Monitor;
        private static List<MeasPoint> m_Points;
        public static MeasPoint[] Points { get { return m_Points.ToArray(); } }
        public static int PointsCount { get { return m_Points.Count; } }
        public static int RangePointsCount { get { return m_StopPoint - m_StartPoint + 1; } }
        public static long[] FrequencyList
        {
            get
            {
                return m_Points.FindAll(i => (i.Index >= m_StartPoint) && (i.Index <= m_StopPoint)).Select(x => x.Frequency).ToArray();
            }
        }
        public static bool ReadyToStart { get { return m_Points.Count > 1; } }
        public static VisaInstrClasses.UnitsAmplitude UnitsAnalyzer;
        public static VisaInstrClasses.UnitsTime UnitsTime;

        public static int m_StartPoint;
        public static int m_StopPoint;
        
        /// <summary>
        /// Параметры точки измерения
        /// </summary>
        public class MeasPoint
        {
            private long m_Freq;
            public long Frequency { get { return m_Freq; } set { if (value > 9000) m_Freq = value; } }
            private int m_Ampl;
            //public int Amplitude { get { return m_Ampl; } set { if (value <= 0) m_Ampl = value; } }
            public int Amplitude { get { return m_Ampl; } set { m_Ampl = value; } }
            private int m_Time;
            public int Time_ms { get { return m_Time; } set { if ((value > 99) && (value < 10000)) m_Time = value; } }
            private int m_Index;
            public int Index { get { return m_Index; } }
            public InstrBands Band;

            public MeasPoint(int _index)
            {
                m_Freq = 9000;
                m_Ampl = 0;
                m_Time = 1000;
                Band = InstrBands.NAN;
                m_Index = _index;
            }
        }

        public MeasConfigurator()
        {
            m_Points = new List<MeasPoint>();
            Name = "Measure1";
            NewMeas = true;
            ShowFS = false;
            m_StepCount = 1;
            InputCoupling = InputCoupling.AC;
            m_BandWidth = 120;
            m_Span = 10;
            MonitorChannel = VisaMonitor.MonitorChannels.CH1;
            CorrectionImpedance = Impedance.R50;
            m_CurrIndex = -1;
            Amplifier = null;
            Generator = null;
            Analyzer = null;
            Monitor = null;
        }

        /// <summary>
        /// Создает таблицу точек
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            dt.Columns.Add("Частота, МГц");
            dt.Columns.Add("Амплитуда, дБм");
            dt.Columns.Add("Время измерения, с");
            dt.Columns.Add("Диапазоны усилителя");
            foreach (var point in Points)
            {
                dt.Rows.Add(point.Index + 1, Converter.Transform(point.Frequency, UnitsFrequency.Hz, UnitsFrequency.MHz),
                    point.Amplitude, point.Time_ms / 1000.0, Converter.ToString<InstrBands>(point.Band));
            }
            return dt;
        }

        /// <summary>
        /// Сброс измерения к начальным значениям
        /// </summary>
        public static void ResetMeas()
        {
            m_CurrIndex = m_StartPoint - 1;
        }
        
        /// <summary>
        /// Получить следующую точку
        /// </summary>
        /// <param name="_point">Параметры точки измерения</param>
        /// <returns>Возвращает false если точки закончились</returns>
        public static MeasPoint NextPoint()
        {
            if (++m_CurrIndex > m_StopPoint) return null;
            return m_Points[m_CurrIndex];
        }
        
        /// <summary>
        /// Пересчет диапазона из процентного в частотный (Гц)
        /// </summary>
        /// <returns></returns>
        public static long GetSpan()
        {
            long result;
            try
            {
                result = m_Span * m_Points[m_CurrIndex].Frequency / 100;
            }catch(Exception) { result = 0; }
            return result;
        }

        public static void EditPoint(int _index, long _freq, int _ampl, int _time, InstrBands _band)
        {
            if (_freq < long.MaxValue) m_Points[_index].Frequency = _freq;
            if (_ampl < int.MaxValue) m_Points[_index].Amplitude = _ampl;
            if (_time < int.MaxValue) m_Points[_index].Time_ms = _time;
            if (_band != InstrBands.Null) m_Points[_index].Band = _band;
        }

        public static void DeletePoints(IEnumerable<int> _IndexList)
        {
            foreach(var index in _IndexList)
            {
                m_Points.RemoveAt(index);
            }
        }

        public static void DeletePoint(int _Index)
        {
            m_Points.RemoveAt(_Index);
        }

        public static void ClearPoints()
        {
            try
            {
                m_Points.Clear();
            }
            catch (Exception) { }
            ResetMeas();
        }

        public static void AddPoint(long _freq, int _ampl, int _time_ms, InstrBands _band)
        {
            MeasPoint mp = new MeasPoint(m_Points.Count);
            mp.Frequency = _freq;
            mp.Amplitude = _ampl;
            mp.Time_ms = _time_ms;
            mp.Band = _band;

            m_Points.Add(mp);
        }

        public static double GetStartFreq(UnitsFrequency _units)
        {
            double res;
            try
            {
                res = Converter.Transform(Points[m_StartPoint].Frequency, UnitsFrequency.Hz, _units);
            }
            catch (Exception) { return double.MinValue; }
            return res;
        }

        public static double GetEndFreq(UnitsFrequency _units)
        {
            double res;
            try
            {
                res = Converter.Transform(Points[m_StopPoint].Frequency, UnitsFrequency.Hz, _units);
            }
            catch (Exception) { return double.MaxValue; }
            return res;
        }
    }

    public class Meas
    {
        public string Name;
        private List<double> m_AnYValues;
        private List<double> m_MonYValues;
        public List<double> AnYValues { get { return m_AnYValues; } }
        public List<double> MonYValues { get { return m_MonYValues; } }
        public double AnMin { get { return m_AnYValues.Min(); } }
        public double AnMax { get { return m_AnYValues.Max(); } }
        public double MonMin { get { return m_MonYValues.Min(); } }
        public double MonMax { get { return m_MonYValues.Max(); } }
        private bool m_CanBeSaved = true;
        public bool CanBeSaved { get { return m_CanBeSaved; } }
        private bool m_FromFile;
        public bool FromFile { get { return m_FromFile; } }

        public Meas(string _name, bool _CanBeSaved = true, bool _FromFile = false)
        {
            Name = _name;
            m_AnYValues = new List<double>();
            m_MonYValues = new List<double>();
            m_CanBeSaved = _CanBeSaved;
            m_FromFile = _FromFile;
        }

        public void ReName(string _NewName)
        {
            Name = _NewName;
        }

        public void Clear()
        {
            m_AnYValues.Clear();
            m_MonYValues.Clear();
        }

        public void AddToAnalyzer(double _value)
        {
            m_AnYValues.Add(_value);
        }

        public void AddToMonitor(double _value)
        {
            m_MonYValues.Add(_value);
        }

        public double GetYAn(int _index)
        {
            return m_MonYValues[_index];
        }

        public double GetYMon(int _index)
        {
            return m_MonYValues[_index];
        }
    }

    public class MeasList : IEnumerator<Meas>, IEnumerable<Meas>
    {
        private List<long> m_XValues;
        /// <summary>
        /// Количество точек в измерениях
        /// </summary>
        public int PointsCount { get { return m_XValues.Count; } }
        /// <summary>
        /// Массив частот (Гц)
        /// </summary>
        public long[] FrequencyList { get { return m_XValues.ToArray(); } } 
        /// <summary>
        /// Коллекция измерений (Meas)
        /// </summary>
        private List<Meas> m_Measures;
        public bool IsEmpty { get { return m_Measures.Count == 0; } }
        public int Count { get { return m_Measures.Count; } }
        public bool HaveToSave { get { return m_Measures.Exists(x => x.CanBeSaved == true); } }

        private int m_DefaultIndex;

        public long MinFrequency { get { return m_XValues[0]; } }
        public long MaxFrequency { get { return m_XValues[m_XValues.Count - 1]; } }

        public bool AnIsEmpty { get { return this.IsEmpty || (m_Measures[0].AnYValues.Count == 0); } }
        public double AnYMin
        {
            get
            {
                double min = double.NaN;
                foreach (var meas in m_Measures)
                {
                    if (double.IsNaN(min) || (min > meas.AnMin)) min = meas.AnMin;
                }
                return min;
            }
        }
        public double AnYMax
        {
            get
            {
                double max = double.NaN;
                foreach (var meas in m_Measures)
                {
                    if (double.IsNaN(max) || (max < meas.AnMax)) max = meas.AnMax;
                }
                return max;
            }
        }

        public bool MonIsEmpty { get { return this.IsEmpty || (m_Measures[0].MonYValues.Count == 0); } }
        public double MonYMin
        {
            get
            {
                double min = double.NaN;
                foreach (var meas in m_Measures)
                {
                    if ((min == double.NaN) || (min > meas.MonMin)) min = meas.MonMin;
                }
                return min;
            }
        }
        public double MonYMax
        {
            get
            {
                double max = double.NaN;
                foreach (var meas in m_Measures)
                {
                    if ((max == double.NaN) || (max < meas.MonMax)) max = meas.MonMax;
                }
                return max;
            }
        }

        private VisaInstrClasses.UnitsAmplitude m_AmplitudeUnits;
        public VisaInstrClasses.UnitsAmplitude AmplitudeUnits { get { return m_AmplitudeUnits; } set { m_AmplitudeUnits = value; } }

        //-----------------------------------------------------------------------------
        //                  IEnumerator<Meas>, IEnumerable<Meas>
        //-----------------------------------------------------------------------------
        private int m_CurrentIndex;
        private Meas m_CurrMeas;
        public Meas Current { get { return m_CurrMeas; } }
        object IEnumerator.Current { get { return Current; } }

        public Meas this[int index] { get { return m_Measures[index]; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Meas>)m_Measures).GetEnumerator();
        }

        public IEnumerator<Meas> GetEnumerator()
        {
            foreach (var meas in m_Measures)
                yield return meas;
        }

        public bool MoveNext()
        {
            if (++m_CurrentIndex >= m_Measures.Count)
            {
                Reset();
                return false;
            }
            m_CurrMeas = m_Measures[m_CurrentIndex];
            return true;
        }

        public void Reset()
        {
            m_CurrentIndex = -1;
        }

        void IDisposable.Dispose() { }
        //-----------------------------------------------------------------------------
        public MeasList()
        {
            m_XValues = new List<long>();
            m_Measures = new List<Meas>();
            m_CurrentIndex = -1;
            m_DefaultIndex = -1;
        }
        
        /// <summary>
        /// Задает список частот для измерений
        /// </summary>
        /// <param name="_collection"></param>
        public void SetFrequency(IEnumerable<long> _collection)
        {
            m_XValues.AddRange(_collection);
        }

        public void Clear()
        {
            m_XValues.Clear();
            m_Measures.Clear();
            m_CurrentIndex = -1;
            m_DefaultIndex = -1;
        }

        /// <summary>
        /// Создает новый элемент в колекции измерений. Если измерение с указанным именем существует, возвращает имя выбранное автоматически
        /// </summary>
        /// <param name="_name">Имя нового измерения</param>
        /// <returns>Имя, присвоенное измерению</returns>
        public string AddMeasure(string _name, bool _AddIfExists = false, bool _CanBeSaved = true, bool _FromFile = false)
        {
            if (m_XValues.Count == 0)
                throw new System.InvalidOperationException("Не допускается добавление измерений при пустом списке частот.");
            int namecnt = 1;
            string tempname = _name;
            if (!_AddIfExists || (m_Measures.Find(x => x.Name == tempname) == null))
            {
                while (m_Measures.Find(x => x.Name == tempname) != null)
                {
                    namecnt++;
                    tempname = _name + namecnt.ToString();
                }
                m_Measures.Add(new Meas(tempname, _CanBeSaved, _FromFile));
                m_DefaultIndex = m_Measures.Count - 1;
            }
            else
            {
                m_DefaultIndex = m_Measures.IndexOf(m_Measures.Find(x => x.Name == tempname));
            }

            return tempname;
        }

        public bool IsCompatible(MeasList _ml)
        {
            if (this.IsEmpty) return true;
            // Единицы амплитуды анализатора не совпадают
            if (this.AmplitudeUnits != _ml.AmplitudeUnits) return false;
            // Начальные частоты не совпадают
            if (this.MinFrequency != _ml.MinFrequency) return false;
            // Конечные частоты не совпадают
            if (this.MaxFrequency != _ml.MaxFrequency) return false;
            // Количество точек не совпадает
            if (this.PointsCount != _ml.PointsCount) return false;
            // 
            bool A, M, a, m, r;
            A = !this.AnIsEmpty;
            M = !this.MonIsEmpty;
            a = !_ml.AnIsEmpty;
            m = !_ml.MonIsEmpty;
            r = a && !m && !M || !a && m && !A || a && m && (!A && !M || A && M);
            if (!r) return false;

            return true;
        }

        public bool Merge(MeasList _ml)
        {
            if (!IsCompatible(_ml)) return false;
            if (m_XValues.Count == 0) m_XValues.AddRange(_ml.FrequencyList);

            m_Measures.AddRange(_ml.m_Measures);

            m_DefaultIndex = PointsCount - 1;

            return true;
        }

        public Meas GetCurrentMeasure()
        {
            if (this.IsEmpty)
                throw new System.InvalidOperationException("Коллекция пуста");
            return m_Measures[m_DefaultIndex];
        }

        public void Remove(int _index)
        {
            if (this.IsEmpty)
                throw new System.InvalidOperationException("Коллекция пуста");
            m_Measures.RemoveAt(_index);
            m_DefaultIndex = m_Measures.Count - 1;
        }

        public void RemoveLast()
        {
            if (this.IsEmpty)
                throw new System.InvalidOperationException("Коллекция пуста");
            Remove(m_DefaultIndex);
        }

        public void AddToAnalyzer(double _y)
        {
            AddToAnalyzer(m_DefaultIndex, _y);
        }

        public void AddToMonitor(double _y)
        {
            AddToMonitor(m_DefaultIndex, _y);
        }

        public void AddToAnalyzer(int _meas_index, double _y)
        {
            if (this.IsEmpty)
                throw new System.InvalidOperationException("Коллекция пуста");
            m_Measures[_meas_index].AddToAnalyzer(_y);
        }

        public void AddToMonitor(int _meas_index, double _y)
        {
            if (this.IsEmpty)
                throw new System.InvalidOperationException("Коллекция пуста");
            m_Measures[_meas_index].AddToMonitor(_y);
        }
    }
}
