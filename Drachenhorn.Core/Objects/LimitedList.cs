using System.Collections;
using System.Collections.Generic;

namespace Drachenhorn.Core.Objects
{
    public class LimitedList<T> : IList<T>
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

        private List<T> _list = new List<T>();

        public List<T> List
        {
            get { return _list; }
        }

        public int Count { get { return List.Count; } }

        public bool IsReadOnly { get { return false; } }

        public T this[int index]
        {
            get { return List[index]; }
            set { List[index] = value; }
        }

        public LimitedList(uint limit) : base()
        {
            this.Limit = limit;
        }

        public void Add(T item)
        {
            if (this.Count >= Limit)
                this.RemoveAt(0);

            List.Add(item);
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
                Add(item);
        }

        public int IndexOf(T item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        public void Clear()
        {
            List.Clear();
        }

        public bool Contains(T item)
        {
            return List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return List.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}