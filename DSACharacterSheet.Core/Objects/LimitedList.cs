using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Core.Objects
{
    public class LimitedList<T> : List<T>
    {
        private uint _limit;
        public uint Limit
        {
            get { return _limit; }
            private set
            {
                if (_limit == value)
                    return;
                _limit = value;
            }
        }

        public LimitedList(uint limit) : base()
        {
            this.Limit = limit;
        }


        public new void Add(T item)
        {
            if (this.Count >= Limit)
                this.RemoveAt(0);

            base.Add(item);
        }
    }
}
