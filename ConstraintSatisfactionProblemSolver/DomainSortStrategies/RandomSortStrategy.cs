using Csp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.DomainSortStrategies
{
    /// <summary>
    /// A strategy that puts the elements in a random order.
    /// </summary>
    /// <typeparam name="TVar"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public class RandomSortStrategy<TVar, TVal> : IDomainSortStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the values from the variable's domain in a random order.
        /// The order may be different each time this method is called.
        /// </summary>
        /// 
        /// <param name="variable">the variable from which to get the ordered domain</param>
        /// 
        /// <param name="assignment">the current assigment</param>
        /// 
        /// <param name="problem">the current problem</param>
        /// 
        /// <returns>the domain of the variable in random order</returns>
        public IEnumerable<TVal> GetOrderedDomain(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            var domain = variable.Domain.ToList();
            RandomUtils.Shuffle(variable.Domain.ToList());
            return domain;
        }
    }
}
