using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using VisaInstrClasses;

namespace ACOPC
{
    class ExcelService
    {
        private _Worksheet worksheet;
        
        class ComObject<TType> : IDisposable
        {
            public TType Instance { get; set; }

            public ComObject(TType instance)
            {
                this.Instance = instance;
            }

            public void Dispose()
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(this.Instance);
            }
        }

        public void Load(string _FileName, string _NamePrefix, out MeasList _ml)
        {
            string tmpStr;
            int CurrentRow;
            int CurrentColumn;
            double tmpValue;
            string measname;

            bool AnData = false;
            bool MonData = false;
            _ml = null;
            
            using (var comApplication = new ComObject<Application>(new Application()))
            {
                var excelInstance = comApplication.Instance;
                excelInstance.Visible = false;
                excelInstance.DisplayAlerts = false;

                try
                {
                    using (var workbooks = new ComObject<Workbooks>(excelInstance.Workbooks))
                    using (var workbook = new ComObject<_Workbook>(workbooks.Instance.Open(_FileName)))
                    using (var comSheets = new ComObject<Sheets>(workbook.Instance.Sheets))
                    {
                        foreach(_Worksheet sh in comSheets.Instance)
                        {
                            if(sh.Name == "Анализатор.Данные")
                            {
                                AnData = true;
                            }else if (sh.Name == "Монитор.Данные")
                            {
                                MonData = true;
                            }
                            Marshal.ReleaseComObject(sh);
                        }

                        if(AnData || MonData)
                        {
                            _ml = new MeasList();
                            List<long> freqs = new List<long>();
                            UnitsFrequency frequnits = UnitsFrequency.Hz;

                            if (AnData)
                            {
                                this.worksheet = (Worksheet)comSheets.Instance.get_Item("Анализатор.Данные");
                                Range rangeCells = this.worksheet.Cells;

                                var currentCell = (Range)rangeCells[1, 1];
                                tmpStr = currentCell.Text;
                                Marshal.ReleaseComObject(currentCell);
                                if (tmpStr.Split(',').Count() > 1)
                                    frequnits = Converter.ToUnits<UnitsFrequency>(tmpStr.Split(',')[1].Trim());

                                currentCell = (Range)rangeCells[1, 2];
                                tmpStr = currentCell.Text;
                                Marshal.ReleaseComObject(currentCell);
                                if (tmpStr.Split(',').Count() > 1)
                                    _ml.AmplitudeUnits = Converter.ToUnits<UnitsAmplitude>(tmpStr.Split(',')[1].Trim());
                                else
                                    _ml.AmplitudeUnits = UnitsAmplitude.dBm;


                                CurrentRow = 2;
                                do      // Читаем частоты
                                {
                                    currentCell = (Range)rangeCells[CurrentRow, 1];
                                    tmpStr = currentCell.Text;
                                    Marshal.ReleaseComObject(currentCell);
                                    if (double.TryParse(tmpStr, out tmpValue))
                                    {
                                        freqs.Add((long)Converter.Transform(tmpValue, frequnits, UnitsFrequency.Hz));
                                    }
                                    else break;
                                    CurrentRow++;
                                } while (tmpStr != "");
                                _ml.SetFrequency(freqs);

                                CurrentColumn = 2;

                                currentCell = (Range)rangeCells[1, CurrentColumn];
                                measname = currentCell.Text;
                                Marshal.ReleaseComObject(currentCell);

                                try
                                {
                                    _ml.AmplitudeUnits = Converter.ToUnits<UnitsAmplitude>(measname.Split(',')[1].Trim());
                                }
                                catch (Exception)
                                {
                                    _ml.AmplitudeUnits = UnitsAmplitude.dBm;
                                }
                                
                                while (measname != "")  // пока в заголовке измерения не пусто
                                {
                                    CurrentRow = 2;
                                    _ml.AddMeasure(_NamePrefix + measname.Split(',')[0], false, false, true);// создаем измерение
                                    do      // Читаем измерение
                                    {
                                        currentCell = (Range)rangeCells[CurrentRow, CurrentColumn];
                                        tmpStr = currentCell.Text;
                                        Marshal.ReleaseComObject(currentCell);

                                        if (double.TryParse(tmpStr, out tmpValue))  // если парсится, добавляем точку
                                        {
                                            _ml.AddToAnalyzer(tmpValue);
                                        }
                                        else break;
                                        CurrentRow++;
                                    } while (tmpStr != "");
                                    CurrentColumn++;

                                    currentCell = (Range)rangeCells[1, CurrentColumn];
                                    measname = currentCell.Text;
                                    Marshal.ReleaseComObject(currentCell);
                                }

                                Marshal.ReleaseComObject(rangeCells);
                                Marshal.ReleaseComObject(this.worksheet);
                            }



                            if (MonData)
                            {
                                this.worksheet = (Worksheet)comSheets.Instance.get_Item("Монитор.Данные");
                                Range rangeCells = this.worksheet.Cells;

                                Range currentCell;

                                if (!AnData)
                                {
                                    currentCell = (Range)rangeCells[1, 1];
                                    tmpStr = currentCell.Text;
                                    Marshal.ReleaseComObject(currentCell);
                                    if (tmpStr.Split(',').Count() > 1)
                                        frequnits = Converter.ToUnits<UnitsFrequency>(tmpStr.Split(',')[1].Trim());

                                    _ml.AmplitudeUnits = UnitsAmplitude.dBm;

                                    CurrentRow = 2;
                                    do      // Читаем частоты
                                    {
                                        currentCell = (Range)rangeCells[CurrentRow, 1];
                                        tmpStr = currentCell.Text;
                                        Marshal.ReleaseComObject(currentCell);
                                        if (double.TryParse(tmpStr, out tmpValue))
                                        {
                                            freqs.Add((long)Converter.Transform(tmpValue, frequnits, UnitsFrequency.Hz));
                                        }
                                        else break;
                                        CurrentRow++;
                                    } while (tmpStr != "");
                                    _ml.SetFrequency(freqs);
                                }

                                CurrentColumn = 2;

                                currentCell = (Range)rangeCells[1, CurrentColumn];
                                measname = currentCell.Text;
                                Marshal.ReleaseComObject(currentCell);

                                while (measname != "")  // пока в заголовке измерения не пусто
                                {
                                    CurrentRow = 2;
                                    _ml.AddMeasure(_NamePrefix + measname.Split(',')[0], true, false, true);  // создаем измерение
                                    do      // Читаем измерение
                                    {
                                        currentCell = (Range)rangeCells[CurrentRow, CurrentColumn];
                                        tmpStr = currentCell.Text;
                                        Marshal.ReleaseComObject(currentCell);

                                        if (double.TryParse(tmpStr, out tmpValue))  // если парсится, добавляем точку
                                        {
                                            _ml.AddToMonitor(tmpValue);
                                        }
                                        else break;
                                        CurrentRow++;
                                    } while (tmpStr != "");
                                    CurrentColumn++;

                                    currentCell = (Range)rangeCells[1, CurrentColumn];
                                    measname = currentCell.Text;
                                    Marshal.ReleaseComObject(currentCell);
                                }

                                Marshal.ReleaseComObject(rangeCells);
                                Marshal.ReleaseComObject(this.worksheet);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    // Close Excel instance
                    excelInstance.Quit();
                }
            }
        }

        public bool Save(string _FileName, MeasList _ml)
        {
            if ((_ml == null) || (_FileName == "") || (_ml.Count == 0) || !_ml.HaveToSave) return false;

            bool AnData = _ml[0].AnYValues.Count > 0;
            bool MonData = _ml[0].MonYValues.Count > 0;

            using (var comApplication = new ComObject<Application>(new Application()))
            {
                var excelInstance = comApplication.Instance;
                excelInstance.Visible = false;
                excelInstance.DisplayAlerts = false;

                try
                {
                    using (var workbooks = new ComObject<Workbooks>(excelInstance.Workbooks))
                    using (var workbook = new ComObject<_Workbook>(workbooks.Instance.Add()))
                    using (var comSheets = new ComObject<Sheets>(workbook.Instance.Sheets))
                    {
                        if (MonData)
                        {
                            using (var comSheet = new ComObject<_Worksheet>(comSheets.Instance.Add()))
                            {
                                this.worksheet = comSheet.Instance;
                                this.worksheet.Name = "Монитор.Данные";

                                Range rangeCells = this.worksheet.Cells;
                                Range currentCell;
                                Range entireColumn;

                                

                                //  Добавление частот
                                int row = 2;
                                //double freq;
                                //UnitsFrequency frequnits;
                                //Converter.Trim(_ml.MaxFrequency, out freq, out frequnits);
                                rangeCells[1, 1] = "Частота, МГц";// + Converter.ToString<UnitsFrequency>(frequnits, true);
                                foreach (var p in _ml.FrequencyList)
                                {
                                    rangeCells[row, 1] = Converter.Transform((long)p, UnitsFrequency.Hz, UnitsFrequency.MHz).ToString("F4").Replace(',', '.');
                                    row++;
                                }

                                currentCell = (Range)rangeCells[1, 1];
                                entireColumn = currentCell.EntireColumn;
                                entireColumn.AutoFit();
                                Marshal.ReleaseComObject(currentCell);
                                Marshal.ReleaseComObject(entireColumn);

                                //  Добавление измерений
                                int CurrentCol = 2;
                                foreach (var measure in _ml)
                                {
                                    if (!measure.CanBeSaved) continue;
                                    row = 2;
                                    rangeCells[1, CurrentCol] = measure.Name + ", В/м";

                                    currentCell = (Range)rangeCells[1, CurrentCol];
                                    entireColumn = currentCell.EntireColumn;
                                    entireColumn.AutoFit();
                                    Marshal.ReleaseComObject(currentCell);
                                    Marshal.ReleaseComObject(entireColumn);

                                    foreach (var y in measure.MonYValues)
                                    {
                                        rangeCells[row, CurrentCol] = y.ToString("F4").Replace(',', '.');
                                        row++;
                                    }
                                    CurrentCol++;
                                }
                                Marshal.ReleaseComObject(rangeCells);
                            }

                            using (var comSheetData = new ComObject<_Worksheet>(comSheets.Instance["Монитор.Данные"]))
                            using (var comSheet = new ComObject<_Worksheet>(comSheets.Instance.Add()))
                            {
                                var DataInstance = comSheetData.Instance;
                                this.worksheet = comSheet.Instance;
                                this.worksheet.Name = "Монитор.График";

                                Range rangeCells = comSheetData.Instance.Cells;

                                //  Добавление графика
                                string range;
                                int ColCount = _ml.Count + 1;
                                int RowCount = _ml.PointsCount + 1;
                                if (ColCount > 26)
                                {
                                    range = "B1:" + (char)(ColCount / 26 + 64) + (char)(((float)ColCount / 26 - ColCount / 26) * 26 + 64) + RowCount.ToString();
                                }
                                else
                                {
                                    range = "B1:" + (char)(ColCount + 64) + RowCount.ToString();
                                }

                                using (var chartobjects = new ComObject<ChartObjects>(comSheet.Instance.ChartObjects()))
                                using (var chartobject = new ComObject<ChartObject>(chartobjects.Instance.Add(0, 0, 1000, 500)))
                                using (var comChart = new ComObject<Chart>(chartobject.Instance.Chart))
                                {
                                    var ChartInstance = comChart.Instance;
                                    using (var comRange = new ComObject<Range>(DataInstance.Range[range]))
                                    {
                                        ChartInstance.ChartType = XlChartType.xlLine;
                                        ChartInstance.SetSourceData(comRange.Instance, XlRowCol.xlColumns);
                                        using (var comAxis = new ComObject<Axis>(comChart.Instance.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary)))
                                        {
                                            var AxisInstance = comAxis.Instance;
                                            AxisInstance.HasTitle = true;
                                            var Title = AxisInstance.AxisTitle;
                                            var Cell = rangeCells[1, 1];
                                            Title.Text = Cell.Text;
                                            Marshal.FinalReleaseComObject(Cell);

                                            var DataRange = DataInstance.get_Range("A2", "A" + RowCount.ToString());
                                            AxisInstance.CategoryNames = DataRange;
                                            Marshal.FinalReleaseComObject(Title);
                                            Marshal.ReleaseComObject(DataRange);
                                        }
                                    }
                                }
                                Marshal.ReleaseComObject(rangeCells);
                            }
                        }

                        if (AnData)
                        {
                            using (var comSheet = new ComObject<_Worksheet>(comSheets.Instance.Add()))
                            {
                                this.worksheet = comSheet.Instance;
                                this.worksheet.Name = "Анализатор.Данные";

                                Range rangeCells = this.worksheet.Cells;

                                

                                //  Добавление частот
                                int row = 2;
                                //double freq;
                                //UnitsFrequency frequnits;
                                //Converter.Trim(_ml.MaxFrequency, out freq, out frequnits);
                                rangeCells[1, 1] = "Частота, МГц";// + Converter.ToString<UnitsFrequency>(frequnits, true);
                                foreach (var p in _ml.FrequencyList)
                                {
                                    rangeCells[row, 1] = Converter.Transform((long)p, UnitsFrequency.Hz, UnitsFrequency.MHz).ToString("F4").Replace(',', '.');
                                    row++;
                                }

                                var currentCell = (Range)rangeCells[1, 1];
                                Range entireColumn = currentCell.EntireColumn;
                                entireColumn.AutoFit();
                                Marshal.ReleaseComObject(currentCell);
                                Marshal.ReleaseComObject(entireColumn);

                                //  Добавление измерений
                                int CurrentCol = 2;
                                foreach (var measure in _ml)
                                {
                                    if (!measure.CanBeSaved) continue;
                                    row = 2;
                                    rangeCells[1, CurrentCol] = measure.Name + ", " + _ml.AmplitudeUnits;

                                    currentCell = (Range)rangeCells[1, CurrentCol];
                                    entireColumn = currentCell.EntireColumn;
                                    entireColumn.AutoFit();
                                    Marshal.ReleaseComObject(currentCell);
                                    Marshal.ReleaseComObject(entireColumn);

                                    foreach (var y in measure.AnYValues)
                                    {
                                        rangeCells[row, CurrentCol] = y.ToString("F4").Replace(',', '.');
                                        row++;
                                    }
                                    CurrentCol++;
                                }
                                Marshal.ReleaseComObject(rangeCells);
                            }
                            
                            using (var comSheetData = new ComObject<_Worksheet>(comSheets.Instance["Анализатор.Данные"]))
                            using (var comSheet = new ComObject<_Worksheet>(comSheets.Instance.Add()))
                            {
                                var DataInstance = comSheetData.Instance;
                                this.worksheet = comSheet.Instance;
                                this.worksheet.Name = "Анализатор.График";
                                
                                Range rangeCells = comSheetData.Instance.Cells;

                                //  Добавление графика
                                string range;
                                int ColCount = _ml.Count + 1;
                                int RowCount = _ml.PointsCount + 1;
                                if (ColCount > 26)
                                {
                                    range = "B1:" + (char)(ColCount / 26 + 64) + (char)(((float)ColCount / 26 - ColCount / 26) * 26 + 64) + RowCount.ToString();
                                }
                                else
                                {
                                    range = "B1:" + (char)(ColCount + 64) + RowCount.ToString();
                                }

                                using (var chartobjects = new ComObject<ChartObjects>(comSheet.Instance.ChartObjects()))
                                using (var chartobject = new ComObject<ChartObject>(chartobjects.Instance.Add(0, 0, 1000, 500)))
                                using (var comChart = new ComObject<Chart>(chartobject.Instance.Chart))
                                {
                                    var ChartInstance = comChart.Instance;
                                    using (var comRange = new ComObject<Range>(DataInstance.Range[range]))
                                    {
                                        ChartInstance.ChartType = XlChartType.xlLine;
                                        ChartInstance.SetSourceData(comRange.Instance, XlRowCol.xlColumns);
                                        using (var comAxis = new ComObject<Axis>(comChart.Instance.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary)))
                                        {
                                            var AxisInstance = comAxis.Instance;
                                            AxisInstance.HasTitle = true;
                                            var Title = AxisInstance.AxisTitle;
                                            var Cell = rangeCells[1, 1];
                                            Title.Text = Cell.Text;
                                            Marshal.FinalReleaseComObject(Cell);

                                            var DataRange = DataInstance.get_Range("A2", "A" + RowCount.ToString());
                                            AxisInstance.CategoryNames = DataRange;
                                            Marshal.FinalReleaseComObject(Title);
                                            Marshal.ReleaseComObject(DataRange);
                                        }
                                    }
                                }
                                Marshal.ReleaseComObject(rangeCells);
                            }
                        }

                        if (_FileName != null)
                        {
                            var currentWorkbook = (workbook.Instance as _Workbook);
                            currentWorkbook.SaveAs(_FileName, XlFileFormat.xlWorkbookNormal);
                            currentWorkbook.Close(false);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    // Close Excel instance
                    excelInstance.Quit();
                }
            }
        }
    }
}
