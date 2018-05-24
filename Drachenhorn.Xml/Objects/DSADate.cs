using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Drachenhorn.Xml.Objects
{
    /// <summary>
    /// Represents a Date in the world of DSA
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.BindableBase" />
    /// <seealso cref="System.IComparable{Drachenhorn.Xml.Objects.DSADate}" />
    /// <seealso cref="System.IFormattable" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    /// <seealso cref="System.IEquatable{Drachenhorn.Xml.Objects.DSADate}" />
    [Serializable]
    public class DSADate : BindableBase, IComparable<DSADate>, IFormattable, ISerializable, IEquatable<DSADate>
    {
        #region Properties

        private int _day = 1;
        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
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

        private DSAMonth _month = DSAMonth.Praios;
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
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

        private int _year = 1;
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DSADate"/> class.
        /// </summary>
        public DSADate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DSADate"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        public DSADate(int day, int month, int year) : this(day, (DSAMonth)month, year)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DSADate"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        public DSADate(int day, DSAMonth month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        #endregion c'tor

        #region Operators

        #region Equals

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DSADate))
                return false;

            return this.Equals((DSADate)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(DSADate other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            if (other is null)
            {
                return false;
            }

            return (this.Day == other.Day
                    && this.Month == other.Month
                    && this.Year == other.Year);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return int.Parse(Year.ToString() + ((int)Month).ToString() + Day.ToString());
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(DSADate obj1, DSADate obj2)
        {
            return obj1.CompareTo(obj2) > 0;
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(DSADate obj1, DSADate obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        #endregion Equals

        #region Add

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DSADate operator +(DSADate date, int value)
        {
            if (value < 0)
                date.SubtractDays((uint)value);
            else if (value > 0)
                date.AddDays((uint)value);
            return date;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="date2">The date2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DSADate operator +(DSADate date, DSADate date2)
        {
            date.Add(date2);
            return date;
        }

        /// <summary>
        /// Adds the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        public void Add(DSADate date)
        {
            this.AddDays((uint)date.Day);
            this.AddMonths((uint)date.Month);
            this.AddYears((uint)date.Year);
        }

        /// <summary>
        /// Adds the days.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <exception cref="System.ArgumentException">Days can not be smaller than or equal to 0</exception>
        public void AddDays(uint days)
        {
            if (days <= 0)
                throw new ArgumentException("Days can not be smaller than or equal to 0");

            long newDay = Day + days;

            while (newDay > Month.GetAllowedDays())
            {
                newDay -= Month.GetAllowedDays();
                this.AddMonths(1);
            }

            this.Day = (int)newDay;
        }

        /// <summary>
        /// Adds the months.
        /// </summary>
        /// <param name="months">The months.</param>
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

        /// <summary>
        /// Adds the years.
        /// </summary>
        /// <param name="years">The years.</param>
        public void AddYears(uint years)
        {
            this.Year += (int)years;
        }

        #endregion Add

        #region Subtract

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DSADate operator -(DSADate date, int value)
        {
            if (value < 0)
                date.AddDays((uint)value);
            else if (value > 0)
                date.SubtractDays((uint)value);
            return date;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="date2">The date2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DSADate operator -(DSADate date, DSADate date2)
        {
            date.Subtract(date2);
            return date;
        }

        /// <summary>
        /// Subtracts the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        public void Subtract(DSADate date)
        {
            this.SubtractDays((uint)date.Day);
            this.SubtractMonths((uint)date.Month);
            this.SubtractYears((uint)date.Year);
        }

        /// <summary>
        /// Subtracts the days.
        /// </summary>
        /// <param name="days">The days.</param>
        public void SubtractDays(uint days)
        {
            long newDay = Day - days;

            while (newDay <= 0)
            {
                newDay += Month.GetAllowedDays();
                this.SubtractMonths(1);
            }

            this.Day = (int)newDay;
        }

        /// <summary>
        /// Subtracts the months.
        /// </summary>
        /// <param name="months">The months.</param>
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

        /// <summary>
        /// Subtracts the years.
        /// </summary>
        /// <param name="years">The years.</param>
        public void SubtractYears(uint years)
        {
            this.Year -= (int)years;
        }

        #endregion Subtract

        #endregion Operators

        #region Interfaces

        /// <summary>
        /// Compares to other Date.
        /// </summary>
        /// <param name="other">The other Date.</param>
        /// <returns>1 if later. 0 if Equal. -1 if earlyer.</returns>
        public int CompareTo(DSADate other)
        {
            if (Equals(this, other))
                return 0;

            if (this.Year > other.Year)
                return 1;
            else if (this.Year < other.Year)
                return -1;

            if (this.Month > other.Month)
                return 1;
            else if (this.Month < other.Month)
                return -1;

            if (this.Day > other.Day)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"></see>) for this serialization.</param>
        /// <exception cref="System.ArgumentNullException">SerializationInfo can not be null.</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("SerializationInfo can not be null.");

            info.AddValue("Value", this.ToString(), typeof(string));
        }

        #region ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("d.MMMM.yyyy g");
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
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