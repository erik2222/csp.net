using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.Utils
{
    /// <summary>
    /// Helper methods for immutable collections.
    /// </summary>
    internal static class ImmutableCollectionUtils
    {
        /// <summary>
        /// Returns an immutable dictionary representation of the specified items, only creating a new immutable dictionary if necessary.
        /// </summary>
        public static IImmutableDictionary<TKey, TValue> AsImmutableDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (items == null)
            {
                return ImmutableDictionary<TKey, TValue>.Empty;
            }
            if (items is IImmutableDictionary<TKey, TValue>)
            {
                return (IImmutableDictionary<TKey, TValue>)items;
            }
            return ImmutableDictionary.CreateRange(items);
        }

        /// <summary>
        /// Returns an immutable list representation of the specified items, only creating a new immutable list if necessary.
        /// </summary>
        public static IImmutableList<T> AsImmutableList<T>(IEnumerable<T> items, bool throwExceptionIfNull = false)
        {
            if (items == null)
            {
                if (throwExceptionIfNull) throw new ArgumentNullException("items");
                return ImmutableList<T>.Empty;
            }
            if (items is IImmutableList<T>)
            {
                return (IImmutableList<T>)items;
            }
            return ImmutableList.CreateRange(items);
        }
    }
}
