using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NationalInstruments.VisaNS;

namespace VisaInstrClasses
{
    public struct TMarkXY
    {
        public float X;
        public float Y;
    };


    public enum UnitsFrequency { Hz, kHz, MHz, GHz }
    public enum UnitsAmplitude { dBm, dBmV, dBuV, V, W }
    public enum UnitsTime { ms, s }
    public enum Statuses { ASK, ON, OFF }
    public enum Modes { ASK, FIX, STEP, PULS, LIST }
    public enum Impedance { R50 = 50, R75 = 75 }
    public enum InputCoupling { AC, DC, ACDC }
    public enum InstrBands { NAN = 0, TWT = 3, SSA = 4, BAND1 = 1, BAND2 = 2, Null = 8 }
    public enum InstrumentType { Analyser, Generator, Amplifier, Monitor }

    public static class Converter
    {
        private static string[] FU = { "Hz", "kHz", "MHz", "GHz" };
        private static string[] FUR = { "Гц", "кГц", "МГц", "ГГц" };
        private static string[] PU = { "dBm", "dBmV", "dBuV", "V", "W" };
        private static string[] PUR = { "дБм", "дБмВ", "дБмкВ", "В", "Вт" };
        private static string[] TE = { "ms", "s" };
        private static string[] TR = { "мс", "с" };

        public static long DoubleToLong(double _param, string _units)
        {
            for (int i = 0; i < FUR.Length; i++)
            {
                if (FUR[i].ToUpper() == _units.ToUpper())
                {
                    return (long)(_param * (Int64)Math.Pow(1000, i));
                }
            }
            for (int i = 0; i < FU.Length; i++)
            {
                if (FU[i].ToUpper() == _units.ToUpper())
                {
                    return (long)(_param * (Int64)Math.Pow(1000, i));
                }
            }
            return -1;
        }

        public static double LongToDouble(long _param, UnitsFrequency _units)
        {
            return (double)_param / Math.Pow(1000, (int)_units);
        }

        public static void Trim(long _param, out double _result, out UnitsFrequency _units)
        {
            double value = _param;
            int k = 0;
            while (value > 1000)
            {
                value /= 1000;
                k++;
            }

            _result = (double)value;
            _units = (UnitsFrequency)k;
        }

        public static double Transform(double _param, UnitsFrequency _from, UnitsFrequency _to)
        {
            if (_from > _to)
            {
                return _param * Math.Pow(1000, (int)_from);
            }
            else if (_from < _to)
            {
                return _param / Math.Pow(1000, (int)_to);
            }
            else return _param;
        }

        public static long DoubleToLong(double _param, UnitsFrequency _units)
        {
            return (long)(_param * (Int64)Math.Pow(1000, (int)_units));
        }

        public static string ToString<T>(object _obj, bool RU = false) where T : Enum
        {
            string[] U = null;
            Type objType = typeof(T);

            if (objType == typeof(UnitsAmplitude))
            {
                if (RU) U = PUR; else U = PU;

            }
            else if (objType == typeof(UnitsFrequency))
            {
                if (RU) U = FUR; else U = FU;
            }
            else if (objType == typeof(UnitsTime))
            {
                if (RU) U = TR; else U = TE;
            }
            else if (objType == typeof(InstrBands))
            {
                if ((InstrBands)_obj == InstrBands.NAN) return "";
                return Enum.GetName(typeof(InstrBands), (InstrBands)_obj);
            }
            else throw new ArgumentException("Не допустимы тип");

            return U[(int)_obj];
            //for (int i = 0; i < U.Length; i++)
            //{
            //    if (U[(int)_obj].ToUpper() == Enum.GetName(objType, _obj).ToUpper()) return U[(int)_obj];
            //}

            throw new ArgumentException("Не допустимое значение параметра");
        }

        public static T ToUnits<T>(string _str) where T : Enum
        {
            if (_str == null) return default(T);
            _str = _str.ToUpper();
            Type objType = typeof(T);

            if (objType == typeof(UnitsAmplitude))
            {
                for (int i = 0; i < PU.Length; i++)
                {
                    if (PU[i].ToUpper() == _str) return (T)(object)i;
                    if (PUR[i].ToUpper() == _str) return (T)(object)i;
                }
            }
            else if (objType == typeof(UnitsFrequency))
            {
                for (int i = 0; i < FU.Length; i++)
                {
                    if (FU[i].ToUpper() == _str) return (T)(object)i;
                    if (FUR[i].ToUpper() == _str) return (T)(object)i;
                }
            }
            else if (objType == typeof(UnitsTime))
            {
                for (int i = 0; i < TE.Length; i++)
                {
                    if (TE[i].ToUpper() == _str) return (T)(object)i;
                    if (TR[i].ToUpper() == _str) return (T)(object)i;
                }
            }

            return default(T);
        }

        public static InstrBands StrToBand(string _param)
        {
            InstrBands result;
            try
            {
                result = (InstrBands)Enum.Parse(typeof(InstrBands), _param);
            }
            catch (Exception) { return InstrBands.NAN; }
            return result;
        }
    }

    public class VisaHW
    {
        private MessageBasedSession mbSession;

        public int Connect(string _addr, string _type)
        {
            mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_addr);
            return 
        }
    }

    public class VisaInstrument
    {
        [DllImport(".\\visa_dll.dll")]
        public static extern int Connect([MarshalAs(UnmanagedType.LPStr)] string _addr, [MarshalAs(UnmanagedType.LPStr)] string _type);
        [DllImport(".\\visa_dll.dll")]
        public static extern void DisConnect(int _index);
        [DllImport(".\\visa_dll.dll")]
        public static extern void Send(int _index, [MarshalAs(UnmanagedType.LPStr)] string _cmd);
        [DllImport(".\\visa_dll.dll")]
        public static extern IntPtr Query(int _index, [MarshalAs(UnmanagedType.LPStr)] string _cmd);
        //---------------------------------------------------------------------------

        protected int ConnectionID = -1;

        public string Name { get; protected set; }
        public bool Connected { get { return ConnectionID > -1; } }
        //---------------------------------------------------------------------------
        public bool Connect(string _addr)
        {
            if (ConnectionID > -1) return false;

            //Random rnd = new Random();
            //if (rnd.NextDouble() > 0.2) return false;

            int res = -1;
            if (int.TryParse(_addr, out res)) //адрес GPIB
            {
                ConnectionID = Connect(_addr, "GPIB");
            }
            else                             //адрес IP
            {
                if (_addr.Split('.').Length == 4)
                {
                    foreach (string n in _addr.Split('.'))
                    {
                        if (!int.TryParse(n, out res) || (res > 255) || (res < 0)) return false;
                    }
                    ConnectionID = Connect(_addr, "IP");
                }
            }
            if (ConnectionID >= 0) Name = Command("*IDN ?");
            return ConnectionID >= 0;
        }
        //---------------------------------------------------------------------------
        public void Disconnect()
        {
            if (ConnectionID > -1) DisConnect(ConnectionID);
        }
        //---------------------------------------------------------------------------
        public string Command(string _cmd, bool _forcequery = false)
        {
            if (ConnectionID < 0) return "";
            if (_cmd.Contains("?") || _forcequery) //Query
            {
                return Marshal.PtrToStringAnsi(Query(ConnectionID, _cmd));
            }
            else
            {
                Send(ConnectionID, _cmd);
                return "";
            }
        }
        //---------------------------------------------------------------------------
        public void Reset()
        {
            Command("*RST");
        }
        //---------------------------------------------------------------------------
        public void ClearStatus()
        {
            Command("*CLS");
        }
        //---------------------------------------------------------------------------
        public string SelfTest()
        {
            return Command("*TST?");
        }
        //---------------------------------------------------------------------------
        protected string GetValue(string _str)
        {
            string pattern = @"\b(\d+\W?\d)";
            //Regex regex = new Regex(pattern);
            Match match = Regex.Match(_str, pattern);
            if (match.Success) return match.Groups[1].Value;
            return "";
        }
        public bool GetErrors(out string _errs)
        {
            _errs = "";
            return false;
        }
    }
    //===========================================================================================================
    public class VisaAnalyzer : VisaGenerator
    {
        public VisaAnalyzer()
        {
            //m_CanTracking = true;
        }

        /// <summary>
        /// Устанавливает частоту для генератора.
        /// </summary>
        /// <param name="_freq">Частота, Int64</param>
        /// <param name="_units">Единицы измерения, FrequencyUnits</param>
        public new void Set_FREQuency(long _freq, UnitsFrequency _units = UnitsFrequency.Hz)
        {
            Set_FREQuency_CENTer(_freq, _units);
        }

        /// <summary>
        /// Устанавливает центральную частоту анализатора
        /// </summary>
        /// <param name="_freq">Частота, Int64</param>
        /// <param name="_units">Единицы измерения, FrequencyUnits</param>
        public void Set_FREQuency_CENTer(long _freq, UnitsFrequency _units = UnitsFrequency.Hz)
        {
            Command("SENS:FREQ:CENT " + _freq + Enum.GetName(typeof(UnitsFrequency), _units));
        }

        /// <summary>
        /// Устанавливает ширину диапазона сканирования
        /// </summary>
        /// <param name="_span">Ширина диапазона, Int64</param>
        /// <param name="_units">Единицы измерения, FrequencyUnits</param>
        public void Set_FREQuency_SPAN(long _span, UnitsFrequency _units = UnitsFrequency.Hz)
        {
            Command("SENS:FREQ:SPAN " + _span + Enum.GetName(typeof(UnitsFrequency), _units));
        }

        /// <summary>
        /// Устанавливает единицы измерения
        /// </summary>
        /// <param name="_units">Единицы измерения, PowerUnits</param>
        public void Set_UNIT_POWer(UnitsAmplitude _units)
        {
            Command("UNIT:POW " + Enum.GetName(typeof(UnitsAmplitude), _units));
        }

        /// <summary>
        /// Устанавливает полосу пропускания
        /// </summary>
        /// <param name="_bw">Полоса пропускания (9 или 120 кГц), Int64</param>
        /// <param name="_units">Единицы измерения (kHz), PowerUnits</param>
        public void Set_BANDwidth(long _bw, UnitsFrequency _units = UnitsFrequency.kHz)
        {
            Command("BAND " + _bw + Enum.GetName(typeof(UnitsFrequency), _units));
        }

        /// <summary>
        /// Устанавливает время свипирования
        /// </summary>
        /// <param name="_time">Время, Int64</param>
        /// <param name="_units">Единицы времени (s или ms), string</param>
        public void Set_SWEep_TIME(long _time, string _units = "ms")
        {
            Command("SENS:SWEEP:TIME " + _time + _units);
        }

        /// <summary>
        /// Количество проходов на точку
        /// </summary>
        /// <param name="_count">Количество, Int64</param>
        public void Set_SWEep_COUNt(long _count)
        {
            Command("SENS:SWEEP:COUNT " + _count);
        }

        /// <summary>
        /// Возвращает максимальное значение измеренной амплитуды и частоту, на которой оно измерено
        /// </summary>
        /// <returns>Структура, TMarkXY</returns>
        public TMarkXY MARKer_MAXimum()
        {
            String val;
            TMarkXY retv;

            retv.X = 0;
            retv.Y = 0;

            Command("CALC:MARK:MAX");
            val = Command("CALC:MARK:X?");
            if (float.TryParse(val, out retv.X))
            {
                val = Command("CALC:MARK:Y?");
                float.TryParse(val, out retv.Y);
            }
            return retv;
        }

        /// <summary>
        /// Запуск однократного сканирования
        /// </summary>
        public void StartSinglSweep()
        {
            Command("CALC:MARK:AOFF");
            Command("INIT:CONT OFF");
            Command("INIT:IMM;*WAI");
        }

        /// <summary>
        /// Чтение ошибок анализатора
        /// </summary>
        /// <returns>Строка со списком ошибок</returns>
        public new bool GetError(out string _errs)
        {
            _errs = Command("SYST:ERR?");
            if (_errs.Contains("NO ERROR")) return false;
            return true;
        }

        /// <summary>
        /// Корректирующее сопротивление 50 или 75 Ом
        /// </summary>
        /// <param name="_prm"></param>
        public void Set_CORRection_IMPedance(Impedance _prm)
        {
            Command("CORR:IMP " + _prm);
        }

        public void Set_INPut_COUPling(InputCoupling _prm)
        {
            if (Name.Contains("FSP-30")) return;
            switch (_prm)
            {
                case InputCoupling.AC: { Command("INP:COUP AC"); break; }
                case InputCoupling.DC: { Command("INP:COUP DC"); break; }
                case InputCoupling.ACDC: { Command("INP:COUP ACDC"); break; }
            }
        }
        //---------------------------------------------------------------------------
        public void Set_RefLevel(string _prm)
        {
            if (!Name.Contains("FSP-30")) return;
            Command("DISP:WIND:TRAC:Y:RLEV " + _prm);
        }
        //---------------------------------------------------------------------------
        public string Get_RefLevel()
        {
            if (!Name.Contains("FSP-30")) return "";
            return Command("DISP:WIND:TRAC:Y:RLEV?");
        }
    }
    //===========================================================================================================
    public class VisaGenerator : VisaInstrument
    {
        private bool Emission;
        public bool EmissionEnabled { get { return Emission; } }
        //protected bool m_CanTracking;
        //private bool m_Tracking;
        //public bool Tracking { get { return m_Tracking; } set { m_Tracking = value && m_CanTracking; } }

        public VisaGenerator()
        {
            //m_CanTracking = false;
        }

        public void Start()
        {
            Emission = true;
            Command("OUTP:STAT ON");
        }
        //---------------------------------------------------------------------------
        public void Stop()
        {
            Emission = false;
            Command("OUTP:STAT OFF");
        }
        //---------------------------------------------------------------------------
        public string OUTPut_STATe(Statuses _prm = Statuses.ASK)
        {
            switch (_prm)
            {
                case Statuses.ASK: { return Command("OUTP:STAT?"); }
                case Statuses.ON: { Command("OUTP:STAT ON"); break; }
                case Statuses.OFF: { Command("OUTP:STAT OFF"); break; }
            }
            return "";
        }
        //---------------------------------------------------------------------------
        public string FREQuency_MODE(Modes _mode = Modes.ASK)
        {
            if (_mode == Modes.ASK) return Command("SOUR:FREQ:MODE?");
            else Command("OUTP:STAT " + Enum.GetName(typeof(Modes), _mode));
            return "";
        }
        //---------------------------------------------------------------------------
        public void Set_SOURce_POWer(long _pow, UnitsAmplitude _units = UnitsAmplitude.dBm)
        {
            //Command("SOUR:POW " + _pow + PU[(int)_units]);
            Command("SOUR:POW " + _pow + Enum.GetName(typeof(UnitsAmplitude), _units));
        }
        //---------------------------------------------------------------------------
        public void Set_FREQuency(long _freq, UnitsFrequency _units = UnitsFrequency.Hz)
        {
            //Command("FREQ " + _freq + FU[(int)_units]);
            Command("FREQ " + _freq + Enum.GetName(typeof(UnitsFrequency), _units));
        }
        //---------------------------------------------------------------------------
        public bool GetError(out string _errs)
        {
            _errs = Command("SYST:ERR?");
            if (_errs.Contains("NO ERROR")) return false;
            return true;
        }
    }
    //===========================================================================================================
    public class VisaAmplifier : VisaInstrument
    {
        public enum AmpIDs { NAN, AR, Bonn0101, Bonn0125 }
        public AmpIDs AmpID { get; private set; }
        private bool Emission;
        public bool EmissionEnabled { get { return Emission; } }
        //---------------------------------------------------------------------------
        public new bool Connect(string _addr)
        {
            bool res = base.Connect(_addr);
            if (!res) return false;
            if (Name.Contains("20ST1G18")) AmpID = AmpIDs.AR;
            else if (Name.Contains("0101")) AmpID = AmpIDs.Bonn0101;
            else if (Name.Contains("0125")) AmpID = AmpIDs.Bonn0125;
            else AmpID = AmpIDs.NAN;
            return res;
        }
        //---------------------------------------------------------------------------
        public void StartEmission()
        {
            Emission = true;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        Command("OPERATE;");
                        break;
                    }
                case AmpIDs.Bonn0101:
                    {
                        Command("AMP_ON");
                        break;
                    }
                case AmpIDs.Bonn0125:
                    {
                        Command("AMP_ON");
                        break;
                    }
                default:
                    {
                        Emission = false;
                        break;
                    }
            }
        }
        //---------------------------------------------------------------------------
        public void StopEmission()
        {
            Emission = false;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        Command("STANDBY;");
                        break;
                    }
                case AmpIDs.Bonn0101:
                    {
                        Command("AMP_OFF");
                        break;
                    }
                case AmpIDs.Bonn0125:
                    {
                        Command("AMP_OFF");
                        break;
                    }
            }
        }
        //---------------------------------------------------------------------------
        //						Запрос текущего режима (диапазона)
        //---------------------------------------------------------------------------
        public InstrBands GetBand()
        {
            string band;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        band = Command("RDBAND", true);
                        if (band[5] == '4') return InstrBands.SSA;
                        return InstrBands.TWT;
                    }
                case AmpIDs.Bonn0101:
                    {
                        band = Command("SW01?");
                        if (band[5] == '2') return InstrBands.BAND2;
                        return InstrBands.BAND1;
                    }
                case AmpIDs.Bonn0125:
                    {
                        return InstrBands.BAND1;
                    }
                default: return InstrBands.NAN;
            }
        }
        //---------------------------------------------------------------------------
        //						Запрос коэффициента усиления (в активном режиме)
        //---------------------------------------------------------------------------
        public int GetGain()
        {
            int gain = -1;
            string val = "";
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        val = base.GetValue(Command("RDA", true));
                        break;
                    }
                default: return -1;
            }
            int.TryParse(val, out gain);
            return gain;
        }
        //---------------------------------------------------------------------------
        //						Запрос прямой мощности
        //---------------------------------------------------------------------------
        public float GetFwdPower()
        {
            float pow = -1;
            string val = "";
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {  // Возвращает значение в dBm
                        val = GetValue(Command("RDPOD", true));
                        break;
                    }
                case AmpIDs.Bonn0125:
                    { // Возвращает значение в %
                        val = GetValue(Command("P_FWD?"));
                        break;
                    }
                default: return -1;
            }
            val = val.Replace('.', ',');
            float.TryParse(val, out pow);
            return pow;
        }
        //---------------------------------------------------------------------------
        //						Запрос обратной мощности
        //---------------------------------------------------------------------------
        public float GetRefPower()
        {
            String val = "";
            float pow = -1;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {  // Возвращает значение в dBm
                        val = GetValue(Command("RDPRD", true));
                        break;
                    }
                case AmpIDs.Bonn0125:
                    { // Возвращает значение в %
                        val = GetValue(Command("P_REF?"));
                        break;
                    }
                default: return -1;
            }
            float.TryParse(val, out pow);
            return pow;
        }
        //---------------------------------------------------------------------------
        //						Проверка нагрева
        //---------------------------------------------------------------------------
        public int HeaterTimer()
        {
            string status = "";
            int time = 0;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {  // Возвращает значение в dBm
                        status = Command("*STA?;");
                        if (status != "WARM-UP") return 0; // возвращаем 0 если нет нагрева
                        status = GetValue(Command("RDHTDREM", true));
                        break;
                    }
                default: return 0; // возвращаем 0 если нет такой функции 
            }
            int.TryParse(status, out time);
            return time;
        }
        //---------------------------------------------------------------------------
        //						Установка режима (диапазона)
        //---------------------------------------------------------------------------
        public bool SetBand(string _bandname)
        {
            return SetBand((InstrBands)System.Enum.Parse(typeof(InstrBands), _bandname));
        }
        //---------------------------------------------------------------------------
        //						Установка режима (диапазона)
        //---------------------------------------------------------------------------
        public bool SetBand(InstrBands _band)
        {
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        Command("SBAND " + _band);
                        break;
                    }
                case AmpIDs.Bonn0101:
                    {
                        Command("SW01_ " + _band);
                        break;
                    }
                case AmpIDs.Bonn0125:
                    {
                        return true;
                    }
                default: return false;
            }
            if (GetBand() == _band) return true;
            return false;
        }
        //---------------------------------------------------------------------------
        //					Установка коэффициента усиления (в активном режиме)
        //---------------------------------------------------------------------------
        public bool SetGain(long _gain)
        {
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        Command("SA " + _gain);
                        break;
                    }
                default: return false;
            }
            if (GetGain() == _gain) return true;
            return false;
        }

        /// <summary>
        /// Чтение ошибок анализатора
        /// </summary>
        /// <returns>Строка со списком ошибок</returns>
        public bool GetError(out string _errs)
        {
            _errs = null;
            switch (AmpID)
            {
                case AmpIDs.AR:
                    {
                        _errs = Command("RDFLT", true);
                        int code;
                        if (int.TryParse(base.GetValue(_errs), out code))
                        {
                            switch (code)
                            {
                                case 7: { _errs = "System Fault"; break; }
                                case 8: { _errs = "High Line"; break; }
                                case 9: { _errs = "Low Line"; break; }
                                case 10: { _errs = "Cathode overvoltage"; break; }
                                case 11: { _errs = "Body overcurrent"; break; }
                                case 12: { _errs = "Cathode undervoltage"; break; }
                                case 13: { _errs = "Heater undervoltage"; break; }
                                case 14: { _errs = "Heater overvoltage"; break; }
                                case 15: { _errs = "Collector undervoltage"; break; }
                                case 16: { _errs = "Inverter fault"; break; }
                                case 17: { _errs = "Internal interlock open"; break; }
                                case 18: { _errs = "Tube arc"; break; }
                                case 19: { _errs = "TWT (hardware) overtemperature"; break; }
                                case 20: { _errs = "Cabinet (hardware) overtemperature"; break; }
                                case 23: { _errs = "Over reflected power"; break; }
                                case 49: { _errs = "TWT (software) overtemperature"; break; }
                                case 50: { _errs = "Cabinet (software) overtemperature"; break; }
                                default: { _errs = "Unknown error"; break; }
                            }
                        }
                        break;
                    }
                case AmpIDs.Bonn0101:
                    {
                        _errs = Command("STATUS?");
                        if (_errs.Contains("SYSTEM_OK")) return false;
                        break;
                    }
                case AmpIDs.Bonn0125:
                    {
                        _errs = Command("STATUS?");
                        if (_errs.Contains("SYSTEM_OK")) return false;
                        break;
                    }
                default: return false;
            }
            return true;
        }
    }
    //===========================================================================================================
    public class VisaMonitor : VisaInstrument
    {
        public enum MonitorChannels { Default = 0, CH1 = 1, CH2 = 2, CH3 = 3, CH4 = 4 }
        private MonitorChannels DefaultChannel = MonitorChannels.CH1;

        public void Start()
        {
            Command("MMH,RUN");
        }
        //---------------------------------------------------------------------------
        public void Stop()
        {
            Command("MMH,STOP");
        }
        //---------------------------------------------------------------------------
        public float GetValue()
        {
            string answer = "";
            try
            {
                answer = Command("RD?").Trim().Split(',')[2].Replace('.', ',');
            }
            catch (Exception) { }

            float val;
            if (float.TryParse(answer, out val)) return val;
            return 0;
        }
        //---------------------------------------------------------------------------
        public bool ChannelReady(MonitorChannels _channel = MonitorChannels.Default)
        {
            if (_channel == MonitorChannels.Default) _channel = DefaultChannel;
            if (Command("CH," + _channel + ",ST?")[9] == 'N') return false;
            return true;
        }
        //---------------------------------------------------------------------------
        public void SetChannel(MonitorChannels _channel = MonitorChannels.CH1)
        {
            if (_channel == MonitorChannels.Default) _channel = MonitorChannels.CH1;
            DefaultChannel = _channel;
            if (Command("CH," + _channel + ",ST?")[9] == 'N')
            {
                Command("CH," + _channel + ",ST,ON");
            }
            Command("DISP,MMH");
        }
        //---------------------------------------------------------------------------
        public float GetFS(MonitorChannels _channel = MonitorChannels.Default)
        {
            if (_channel == MonitorChannels.Default) _channel = DefaultChannel;
            string answer = "";
            try
            {
                answer = Command("D," + _channel + "?").Split(',')[5].Replace('.', ',');
            }
            catch (Exception) { }
            float val = 0;
            if (!float.TryParse(answer, out val)) val = float.NaN;
            return val;
        }
        //---------------------------------------------------------------------------
        public string GetErrors()
        {
            string answer = Command("ERR?");
            if (answer.Contains("NONE")) return "";
            return answer;
        }

        /// <summary>
        /// Чтение ошибок анализатора
        /// </summary>
        /// <returns>Строка со списком ошибок</returns>
        public bool GetError(out string _errs)
        {
            _errs = Command("ERR?");
            if (_errs.Contains("NONE")) return false;
            return true;
        }
    }
    //===========================================================================================================
}
