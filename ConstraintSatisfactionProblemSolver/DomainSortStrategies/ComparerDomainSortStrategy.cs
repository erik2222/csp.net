using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// A strategy that sorts a variable's domain using a comparer.
    /// </summary>
    public class ComparerDomainSortStrategy<TVar, TVal> : ComparerDomainSortStrategyBase<TVar, TVal>
    {
        private readonly IComparer<TVal> comparer;

        /// <summary>
        /// Constructs a sort strategy using the specified comparison.
        /// </summary>
        /// <param name="comparison">the comparison to use for sorting</param>
        public ComparerDomainSortStrategy(Comparison<TVal> comparison)
            : this(new ComparisonComparer<TVal>(comparison)) { }

        /// <summary>
        /// Constructs a sort strategy using the specified comparer.
        /// </summary>
        /// <param name="comparer">the comparer to use for sorting</param>
        public ComparerDomainSortStrategy(IComparer<TVal> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            this.comparer = comparer;
        }

        /// <summary>
        /// Returns the comparer that was used to construct this object.
        /// </summary>
        protected override IComparer<TVal> GetComparer(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return comparer;
        }

        private class ComparisonComparer<T> : IComparer<T>  
        {  
            private readonly Comparison<T> comparison;  
  
            public ComparisonComparer(Comparison<T> comparison)  
            {  
                if (comparison == null) throw new ArgumentNullException("comparison");
                this.comparison = comparison;  
            }  
  
            public int Compare(T x, T y)  
            {  
                return comparison(x, y);  
            }  
        }
    }

    /// <summary>
    /// The base class for a strategy that sorts a variable's domain using a comparer.
    /// </summary>
    /// <typeparam name="TVar">type that variables represent</typeparam>
    /// <typeparam name="TVal">type of value to assign to variables </typeparam>
    public abstract class ComparerDomainSortStrategyBase<TVar, TVal> : IDomainSortStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the values from the variable's domain in the order specified by this strategy's comparer.
        /// </summary>
        /// 
        /// <param name="variable">the variable from which to get the ordered domain</param>
        /// 
        /// <param name="assignment">the current assigment</param>
        /// 
        /// <param name="problem">the current problem</param>
        /// 
        /// <returns>the ordered values in the domain of the variable</returns>
        public IEnumerable<TVal> GetOrderedDomain(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return OrderValues(variable.Domain, variable, assignment, problem);
        }

        /// <summary>
        /// Returns the specified values in the order specified by this strategy's comparer.
        /// </summary>
        /// 
        /// <param name="values">the values to sort</param>
        /// 
        /// <param name="variable">the current variable</param>
        /// 
        /// <param name="assignment">the current assigment</param>
        /// 
        /// <param name="problem">the current problem</param>
        /// 
        /// <returns>the specified values in sorted order</returns>
        public IEnumerable<TVal> OrderValues(IEnumerable<TVal> values, Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            var comparer = GetComparer(variable, assignment, problem);

            var valueList = values.ToList();
            valueList.Sort(comparer);
            return valueList;
        }

        /// <summary>
        /// Returns a comparer for the specified variable, assignment, and problem.
        /// </summary>
        /// 
        /// <param name="variable">the current variable</param>
        /// 
        /// <param name="assignment">the current assigment</param>
        /// 
        /// <param name="problem">the current problem</param>
        /// 
        /// <returns>the comparer</returns>
        protected abstract IComparer<TVal> GetComparer(Variable<TVar, TVal> variable, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem);
    }
}
