using System.Collections;
using System.Collections.Generic;

namespace Drachenhorn.Core.Objects
{
    public class LimitedList<T> : IList<T>
    {
        #region c'tor

        public LimitedList(uint limit)
        {
            Limit = limit;
        }

        #endregion

        #region Properties

        private uint _limit;

        public uint Limit
        {
            get => _limit;
            private set
            {
                if (_limit == value)
                    return;
                _limit = value;
            }
        }

        public List<T> List { get; } = new List<T>();

        #endregion

        public int Count => List.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        public void Add(T item)
        {
            if (Count >= Limit)
                RemoveAt(0);

            List.Add(item);
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

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
                Add(item);
        }
    }
}