using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// A default implementation of the domain sort strategy.
    /// </summary>
    public sealed class DefaultDomainSortStrategy<TVar, TVal> : IDomainSortStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the variable's domain in the order that they are given from the variable's <c>Domain</c> property.
        /// </summary>
        public IEnumerable<TVal> GetOrderedDomain(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return variable.Domain;
        }
    }
}
