namespace Drachenhorn.Xml.Objects
{
    /// <summary>
    /// List of DSA Months
    /// </summary>
    public enum DSAMonth
    {
        /// <summary>
        /// 1st month of a year.
        /// </summary>
        Praios = 1,
        /// <summary>
        /// 2nd month of a year.
        /// </summary>
        Rondra = 2,
        /// <summary>
        /// 3rd month of a year.
        /// </summary>
        Efferd = 3,
        /// <summary>
        /// 4th month of a year.
        /// </summary>
        Travia = 4,
        /// <summary>
        /// 5th month of a year.
        /// </summary>
        Boron = 5,
        /// <summary>
        /// 6th month of a year.
        /// </summary>
        Hesinde = 6,
        /// <summary>
        /// 7th month of a year.
        /// </summary>
        Firun = 7,
        /// <summary>
        /// 8th month of a year.
        /// </summary>
        Tsa = 8,
        /// <summary>
        /// 9th month of a year.
        /// </summary>
        Phex = 9,
        /// <summary>
        /// 10th month of a year.
        /// </summary>
        Peraine = 10,
        /// <summary>
        /// 11th month of a year.
        /// </summary>
        Ingerimm = 11,
        /// <summary>
        /// 12th month of a year.
        /// </summary>
        Rahja = 12,
        /// <summary>
        /// Last 5 days of a year.
        /// </summary>
        Nameless = 13
    }

    /// <summary>
    /// Extensions for DSAMonth
    /// </summary>
    public static class DSAMonthExtensions
    {
        /// <summary>
        /// Gets the AllowedDays for a Month
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static int GetAllowedDays(this DSAMonth month)
        {
            switch (month)
            {
                case DSAMonth.Praios:
                case DSAMonth.Rondra:
                case DSAMonth.Efferd:
                case DSAMonth.Travia:
                case DSAMonth.Boron:
                case DSAMonth.Hesinde:
                case DSAMonth.Firun:
                case DSAMonth.Tsa:
                case DSAMonth.Phex:
                case DSAMonth.Peraine:
                case DSAMonth.Ingerimm:
                case DSAMonth.Rahja:
                    return 30;

                case DSAMonth.Nameless:
                    return 5;

                default:
                    return -1;
            }
        }
    }
}