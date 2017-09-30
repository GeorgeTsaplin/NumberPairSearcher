using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPairSearcher
{
    /// <summary> Number pairs searcher. This class not Thread Safe! 
    /// </summary>
    public class NumberPairSearcher
    {
        private int[] raw;
        private IReadOnlyDictionary<int, uint> preparedSource = null;

        public NumberPairSearcher(int[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.raw = source;
        }

        public IEnumerable<Tuple<int, int>> Search(int target)
        {
            this.Prepare();

            var source = this.preparedSource.ToDictionary(k => k.Key, i => i.Value);

            foreach (var key in this.preparedSource.Keys)
            {
                do
                {
                    source[key] = --source[key];

                    var searchFor = target - key;

                    uint searchForAmount;
                    if (source.TryGetValue(searchFor, out searchForAmount) && searchForAmount > 0)
                    {
                        source[searchFor] = searchForAmount - 1;

                        yield return CreateOrderedTuple(key, searchFor);
                    }
                    else
                    {
                        break;
                    }
                } while (source[key] > 0);
            }
        }

        public void Prepare()
        {
            if (this.preparedSource != null)
            {
                return;
            }

            var result = new Dictionary<int, uint>(this.raw.Length);

            foreach (var number in this.raw)
            {
                uint currentAmount;
                if (result.TryGetValue(number, out currentAmount))
                {
                    result[number] = ++currentAmount;
                }
                else
                {
                    result.Add(number, 1);
                }
            }

            this.preparedSource = result;
            this.raw = null;
        }

        private static Tuple<int, int> CreateOrderedTuple(int arg1, int arg2)
        {
            return arg1 < arg2 ? Tuple.Create(arg1, arg2) : Tuple.Create(arg2, arg1);
        }
    }
}
