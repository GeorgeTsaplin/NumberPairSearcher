using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPairSearcher
{
    /// <summary> Number pairs searcher
    /// </summary>
    public class NumberPairSearcher
    {
        private readonly IReadOnlyDictionary<int, uint> preparedSource;

        /// <summary> Creates new instance of <see cref="NumberPairSearcher"/>.
        /// May be long running operation because of preprocessing of specified <paramref name="source"/>
        /// </summary>
        /// <param name="source">array of numbers</param>
        public NumberPairSearcher(int[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.preparedSource = Prepare(source);
        }

        public IEnumerable<Tuple<int, int>> Search(int target)
        {
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

        private static Dictionary<int, uint> Prepare(int[] source)
        {
            var result = new Dictionary<int, uint>(source.Length);

            foreach (var number in source)
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

            return result;
        }

        private static Tuple<int, int> CreateOrderedTuple(int arg1, int arg2)
        {
            return arg1 < arg2 ? Tuple.Create(arg1, arg2) : Tuple.Create(arg2, arg1);
        }
    }
}
