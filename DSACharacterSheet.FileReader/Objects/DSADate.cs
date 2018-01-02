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
    public class DSADate : BindableBase, IComparable<DSADate>, IFormattable, ISerializable, IEquatable<DSADate>
    {
        #region Properties

        private int _day = -1;
        public int Day
        {
            get { return _day; }
            private set
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
            private set
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
            private set
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


        #region Operators

        #region Equals

        public override bool Equals(object obj)
        {
            if (!(obj is DSADate))
                return false;

            return this.Equals((DSADate)obj);
        }

        public bool Equals(DSADate other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(this, null))
            {
                return false;
            }
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return (this.Day == other.Day
                    && this.Month == other.Month
                    && this.Year == other.Year);
        }

        public override int GetHashCode()
        {
            return int.Parse(Year.ToString() + ((int)Month).ToString() + Day.ToString());
        }

        public static bool operator!=(DSADate obj1, DSADate obj2)
        {
            return !(obj1 == obj2);
        }

        public static bool operator==(DSADate obj1, DSADate obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator>(DSADate obj1, DSADate obj2)
        {
            return obj1.CompareTo(obj2) > 0;
        }

        public static bool operator<(DSADate obj1, DSADate obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        #endregion Equals

        #region Add

        public static DSADate operator+(DSADate date, int value)
        {
            if (value < 0)
                date.SubtractDays((uint)value);
            else if (value > 0)
                date.AddDays((uint)value);
            return date;
        }

        public static DSADate operator+(DSADate date, DSADate date2)
        {
            date.Add(date2);
            return date;
        }

        public void Add(DSADate date)
        {
            this.AddDays((uint)date.Day);
            this.AddMonths((uint)date.Month);
            this.AddYears((uint)date.Year);
        }

        public void AddDays(uint days)
        {
            if (days <= 0)
                throw new ArgumentException("Days can not be smaller than or equal to 0");

            long newDay = Day + days;

            while (newDay > Month.AllowedDays())
            {
                newDay -= Month.AllowedDays();
                this.AddMonths(1);
            }

            this.Day = (int)newDay;
        }

        public void AddMonths(uint months)
        {
            int newMonth = (int)Month + (int)months;

            var maxMonths = (int)Enum.GetValues(typeof(DSAMonth)).Cast<DSAMonth>().Max();

            while (newMonth > maxMonths)
            {
                newMonth -= maxMonths;
                this.AddYears(1);
            }

            this.Month = (DSAMonth)newMonth;
        }

        public void AddYears(uint years)
        {
            this.Year += (int)years;
        }

        #endregion Add

        #region Subtract

        public static DSADate operator-(DSADate date, int value)
        {
            if (value < 0)
                date.AddDays((uint)value);
            else if (value > 0)
                date.SubtractDays((uint)value);
            return date;
        }

        public static DSADate operator-(DSADate date, DSADate date2)
        {
            date.Subtract(date2);
            return date;
        }

        public void Subtract(DSADate date)
        {
            this.SubtractDays((uint)date.Day);
            this.SubtractMonths((uint)date.Month);
            this.SubtractYears((uint)date.Year);
        }

        public void SubtractDays(uint days)
        {
            long newDay = Day - days;

            while (newDay <= 0)
            {
                newDay += Month.AllowedDays();
                this.SubtractMonths(1);
            }

            this.Day = (int)newDay;
        }

        public void SubtractMonths(uint months)
        {
            int newMonth = (int)Month - (int)months;

            var maxMonths = (int)Enum.GetValues(typeof(DSAMonth)).Cast<DSAMonth>().Max();

            while (newMonth <= 0)
            {
                newMonth += maxMonths;
                this.SubtractYears(1);
            }

            this.Month = (DSAMonth)newMonth;
        }

        public void SubtractYears(uint years)
        {
            this.Year -= (int)years;
        }

        #endregion Subtract

        #endregion Operators


        #region Interfaces

        public int CompareTo(DSADate other)
        {
            if (this == other)
                return 0;

            if (this.Year > other.Year)
                return 1;
            else if (this.Year < other.Year)
                return -1;

            if (this.Month > other.Month)
                return 1;
            else if (this.Month < this.Month)
                return -1;

            if (this.Day > other.Day)
                return 1;
            else
                return -1;
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
