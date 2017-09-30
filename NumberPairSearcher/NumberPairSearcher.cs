using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPairSearcher
{
    public class NumberPairSearcher
    {
        private readonly double[] source;

        public NumberPairSearcher(double[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.source = source;
        }

        public IEnumerable<Tuple<double, double>> Search(double target)
        {
            throw new NotImplementedException();
        }
    }
}
