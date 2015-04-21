using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// A strategy for sorting the values in the domain of a variable.
    /// </summary>
    public interface IDomainSortStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the values from the given variable's domain in the order 
        /// with which they should be assigned.
        /// </summary>
        /// 
        /// <param name="variable">the variable from which to get the ordered domain</param>
        /// 
        /// <param name="assignment">the current assigment</param>
        /// 
        /// <param name="problem">the current problem</param>
        /// 
        /// <returns>the ordered values in the domain of the variable</returns>
        IEnumerable<TVal> GetOrderedDomain(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem);
    }
}
