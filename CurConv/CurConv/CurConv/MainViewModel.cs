using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace CurConv
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MainViewModel : BaseViewModel
    {
        HttpClient http = new HttpClient();
        private string _messageStatus;
        public string MessageStatus { get { return _messageStatus; } set { _messageStatus = value; OnPropertyChanged(); } }
        public Command SetCurCommand { get; set; }
        string _date;
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged(); SetCurCommand.Execute(_date); } }
        private ObservableCollection<Cur> _curNames;
        public ObservableCollection<Cur> curNames { get { return _curNames; } set { _curNames = value; OnPropertyChanged(); } }
        private Cur _selectedCur1;
        public Cur selectedCur1
        {
            get { return _selectedCur1; }
            set
            {
                if (value != _selectedCur1)
                {
                    _selectedCur1 = value;
                    curRes1 = "1";
                }
                if (value == null)
                    curRes1 = "";
                OnPropertyChanged();
            }
        }
        private Cur _selectedCur2;
        public Cur selectedCur2
        {
            get { return _selectedCur2; }
            set
            {
                if (value != _selectedCur2)
                {
                    _selectedCur2 = value;
                    curRes2 = "1";
                }
                if (value == null)
                    curRes2 = "";
                OnPropertyChanged();
            }
        }

        private bool Flag = true;

        private string _curRes1;
        public string curRes1
        {
            get { return _curRes1; }
            set
            {
                _curRes1 = value;
                if (!string.IsNullOrEmpty(value) && selectedCur1 != null && selectedCur2 != null && Flag)
                {
                    Flag = false;
                    setCur2();
                }
                else
                    Flag = true;

                OnPropertyChanged(nameof(curRes1));
            }
        }
        private string _curRes2;
        public string curRes2
        {
            get { return _curRes2; }
            set
            {
                _curRes2 = value;
                if (!string.IsNullOrEmpty(value) && selectedCur1 != null && selectedCur2 != null && Flag)
                {
                    Flag = false;
                    setCur1();
                }
                else
                    Flag = true;

                OnPropertyChanged(nameof(curRes2));
            }
        }
        public Root result { get; set; }
        public MainViewModel()
        {
            SetCurCommand = new Command(async obj => await setCur(obj));
            var d = DateTime.Now;
            curNames = new ObservableCollection<Cur>();
            curRes1 = "";
            curRes2 = "";
            Date = d.Month + "/" + d.Day + "/" + d.Year;
        }

        private async Task setCur(object obj)
        {
            string date = obj as string;
            var arrDate = date.Substring(0, 10).Split('/');
            var d = new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[0]), int.Parse(arrDate[1]));
            var uri = "https://www.cbr-xml-daily.ru/archive/" + arrDate[2] + "/" + arrDate[0] + "/" + arrDate[1] + "/daily_json.js";
            var res = await http.GetAsync(uri);

            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                result = jObject.ToObject<Root>();
                setCurNameSelector();

                //List<Valute> list = jArray["Valute"]["AUD"].ToObject<List<Valute>>();
                //var result = await System.Text.Json.JsonSerializer.DeserializeAsync<DailyCur>(content);
                MessageStatus = "Курс на " + d.ToString("dd MMMM yyyy");
            }
            else
            {
                d = d.AddDays(-1);
                Date = d.Month + "/" + d.Day + "/" + d.Year;
            }
        }
        public void setCur2()
        {
            if (!string.IsNullOrEmpty(curRes1))
            {
                curRes2 = Math.Round(((selectedCur1.Value * Convert.ToDouble(curRes1)) / selectedCur2.Value), 4).ToString();
            }
        }
        public void setCur1()
        {
            if (!string.IsNullOrEmpty(curRes2))
            {
                curRes1 = Math.Round(((selectedCur2.Value * Convert.ToDouble(curRes2)) / selectedCur1.Value), 4).ToString();
            }
        }

        public void setCurNameSelector()
        {
            Cur sc1 = null;
            Cur sc2 = null;
            if (selectedCur1 != null && selectedCur2 != null)
            {
                sc1 = selectedCur1;
                sc2 = selectedCur2;
            }
            curNames.Clear();
            result.Valute.RUB = new Cur();
            result.Valute.RUB.CharCode="RUB";
            result.Valute.RUB.Value = 1;
            curNames.Add(result.Valute.RUB);
            curNames.Add(result.Valute.AUD);
            curNames.Add(result.Valute.AZN);
            curNames.Add(result.Valute.GBP);
            curNames.Add(result.Valute.AMD);
            curNames.Add(result.Valute.BYN);
            curNames.Add(result.Valute.BGN);
            curNames.Add(result.Valute.BRL);
            curNames.Add(result.Valute.AMD);
            curNames.Add(result.Valute.HUF);
            curNames.Add(result.Valute.HKD);
            curNames.Add(result.Valute.DKK);
            curNames.Add(result.Valute.USD);
            curNames.Add(result.Valute.EUR);
            curNames.Add(result.Valute.INR);
            curNames.Add(result.Valute.KZT);
            curNames.Add(result.Valute.CAD);
            curNames.Add(result.Valute.KGS);
            curNames.Add(result.Valute.CNY);
            curNames.Add(result.Valute.MDL);
            curNames.Add(result.Valute.NOK);
            curNames.Add(result.Valute.PLN);
            curNames.Add(result.Valute.RON);
            curNames.Add(result.Valute.XDR);
            curNames.Add(result.Valute.SGD);
            curNames.Add(result.Valute.TJS);
            curNames.Add(result.Valute.TRY);
            curNames.Add(result.Valute.TMT);
            curNames.Add(result.Valute.UZS);
            curNames.Add(result.Valute.UAH);
            curNames.Add(result.Valute.CZK);
            curNames.Add(result.Valute.SEK);
            curNames.Add(result.Valute.CHF);
            curNames.Add(result.Valute.ZAR);
            curNames.Add(result.Valute.KRW);
            curNames.Add(result.Valute.JPY);
            if (sc1 != null && sc2 != null)
            {
                foreach (Cur c in curNames)
                {
                    if (c.CharCode == sc1.CharCode)
                    {
                        selectedCur1 = c;
                    }
                    if (c.CharCode == sc2.CharCode)
                    {
                        selectedCur2 = c;
                    }
                }
            }
            
        }

        public class Root
        {
            public DateTime Date { get; set; }
            public DateTime PreviousDate { get; set; }
            public string PreviousURL { get; set; }
            public DateTime Timestamp { get; set; }
            public Valute Valute { get; set; }
        }
        public class Valute
        {
            public Cur RUB { get; set; }
            public Cur AUD { get; set; }
            public Cur AZN { get; set; }
            public Cur GBP { get; set; }
            public Cur AMD { get; set; }
            public Cur BYN { get; set; }
            public Cur BGN { get; set; }
            public Cur BRL { get; set; }
            public Cur HUF { get; set; }
            public Cur HKD { get; set; }
            public Cur DKK { get; set; }
            public Cur USD { get; set; }
            public Cur EUR { get; set; }
            public Cur INR { get; set; }
            public Cur KZT { get; set; }
            public Cur CAD { get; set; }
            public Cur KGS { get; set; }
            public Cur CNY { get; set; }
            public Cur MDL { get; set; }
            public Cur NOK { get; set; }
            public Cur PLN { get; set; }
            public Cur RON { get; set; }
            public Cur XDR { get; set; }
            public Cur SGD { get; set; }
            public Cur TJS { get; set; }
            public Cur TRY { get; set; }
            public Cur TMT { get; set; }
            public Cur UZS { get; set; }
            public Cur UAH { get; set; }
            public Cur CZK { get; set; }
            public Cur SEK { get; set; }
            public Cur CHF { get; set; }
            public Cur ZAR { get; set; }
            public Cur KRW { get; set; }
            public Cur JPY { get; set; }
        }
        public class Cur
        {
            public string ID { get; set; }
            public string NumCode { get; set; }
            public string CharCode { get; set; }
            public int Nominal { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public double Previous { get; set; }
        }
    }
}
