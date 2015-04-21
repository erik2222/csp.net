using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.Utils
{
    /// <summary>
    /// Helper methods for random number generation.
    /// </summary>
    internal static class RandomUtils
    {
        [ThreadStatic]
        private static Random Local;

        /// <summary>
        /// Returns an instance of a random number generator.
        /// One instance will be created per thread.
        /// </summary>
        public static Random Instance
        {
            get
            {
                if (Local == null)
                {
                    int seed;
                    unchecked
                    {
                        seed = -842606958;
                        seed = (seed * -1521134295) + Environment.TickCount;
                        seed = (seed * -1521134295) + System.Threading.Thread.CurrentThread.ManagedThreadId;
                    }
                    Local = new Random(seed);
                }
                return Local;
            }
        }

        /// <summary>
        /// Shuffles the elements in the list into a random order.
        /// </summary>
        public static void Shuffle<T>(IList<T> a)
        {
            var random = Instance;

            // Fisher–Yates shuffle
            for (var i = a.Count - 1; i > 0; i--)
            {
                var j = random.Next(i + 1);
                var temp = a[j];
                a[j] = a[i];
                a[i] = temp;
            }
        }
    }
}
