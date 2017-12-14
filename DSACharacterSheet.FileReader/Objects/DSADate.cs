using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.FileReader.Objects
{
    [SerializableAttribute]
    public class DSADate : BindableBase, IComparable<DSADate>, IFormattable, ISerializable
    {
        #region Properties

        private int _day = -1;
        public int Day
        {
            get { return _day; }
            set
            {
                if (_day == value)
                    return;
                _day = value;
                OnPropertyChanged();
            }
        }

        private DSAMonth _month = DSAMonth.None;
        public DSAMonth Month
        {
            get { return _month; }
            set
            {
                if (_month == value)
                    return;
                _month = value;
                OnPropertyChanged();
            }
        }

        private int _year = 0;
        public int Year
        {
            get { return _year; }
            set
            {
                if (_year == value)
                    return;
                _year = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        public DSADate() { }

        public DSADate(int day, int month, int year) : this(day, (DSAMonth)month, year) { }

        public DSADate(int day, DSAMonth month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        #endregion c'tor


        #region Interfaces

        public int CompareTo(DSADate other)
        {
            throw new NotImplementedException();
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("SerializationInfo can not be null.");

            info.AddValue("Value", this.ToString(), typeof(string));
        }

        #region ToString

        public override string ToString()
        {
            return ToString("d.MMMM.yyyy g");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var parameterList = new List<string>();

            foreach (var c in format)
            {
                if (parameterList.Count > 0 && parameterList.Last().First() == c)
                    parameterList[parameterList.Count - 1] += c;
                else
                    parameterList.Add("" + c);
            }

            var result = "";

            foreach (var param in parameterList)
            {
                switch (param)
                {
                    case "d":
                        result += Day;
                        break;
                    case "dd":
                        result += Day.ToString("D2");
                        break;
                    case "g":
                    case "gg":
                        result += "BF";
                        break;
                    case "M":
                        result += (int)Month;
                        break;
                    case "MM":
                        result += ((int)Month).ToString("D2");
                        break;
                    case "MMMM":
                        result += Month.ToString();
                        break;
                    case "y":
                        result += Year % 100;
                        break;
                    case "yy":
                        result += (Year % 100).ToString("D2");
                        break;
                    case "yyy":
                        result += (Year % 1000).ToString("D3");
                        break;
                    case "yyyy":
                        result += Year;
                        break;

                    default:
                        result += param;
                        break;
                }
            }

            return result;
        }

        #endregion ToString

        #endregion Interfaces
    }
}
