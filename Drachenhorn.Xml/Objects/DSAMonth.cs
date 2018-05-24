namespace Drachenhorn.Xml.Objects
{
    public enum DSAMonth
    {
        Praios = 1,
        Rondra = 2,
        Efferd = 3,
        Travia = 4,
        Boron = 5,
        Hesinde = 6,
        Firun = 7,
        Tsa = 8,
        Phex = 9,
        Peraine = 10,
        Ingerimm = 11,
        Rahja = 12,
        Nameless = 13
    }

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