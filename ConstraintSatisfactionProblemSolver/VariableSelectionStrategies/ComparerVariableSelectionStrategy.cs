using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csp
{
    /// <summary>
    /// A variable selection strategy that uses a specified comparer.
    /// </summary>
    public class ComparerVariableSelectionStrategy<TVar, TVal> : ComparerVariableSelectionStrategyBase<TVar, TVal>
    {
        private readonly IComparer<Variable<TVar, TVal>> comparer;

        /// <summary>
        /// Constructs a variable selection strategy using the specified comparison.
        /// </summary>
        /// <param name="comparison">the comparison to use for sorting</param>
        public ComparerVariableSelectionStrategy(Comparison<Variable<TVar, TVal>> comparison)
            : this(new ComparisonComparer<Variable<TVar, TVal>>(comparison)) { }

        /// <summary>
        /// Constructs a variable selection strategy using the specified comparer.
        /// </summary>
        /// <param name="comparer">the comparer to use for sorting</param>
        public ComparerVariableSelectionStrategy(IComparer<Variable<TVar, TVal>> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            this.comparer = comparer;
        }

        /// <summary>
        /// Returns the comparer that was used to construct this object.  This does not use the assignment or problem.
        /// </summary>
        protected override IComparer<Variable<TVar, TVal>> GetComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
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
    /// A base class for a variable selection strategy that uses a comparer.
    /// </summary>
    public abstract class ComparerVariableSelectionStrategyBase<TVar, TVal> : IVariableSelectionStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the first variable in the order specified by the comparer.
        /// </summary>
        public Variable<TVar, TVal> SelectUnassignedVariable(IEnumerable<Variable<TVar, TVal>> variables, 
            Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return OrderVariables(variables, assignment, problem).FirstOrDefault(v => !assignment.HasValue(v));
        }

        /// <summary>
        /// Returns all of the variables in the order specified by the comparer.
        /// </summary>
        public IEnumerable<Variable<TVar, TVal>> OrderVariables(IEnumerable<Variable<TVar, TVal>> variables, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            var comparer = GetComparer(assignment, problem);

            var variableList = variables.ToList();
            variableList.Sort(comparer);
            return variableList;
        }

        /// <summary>
        /// Returns a comparer for the specified assignment and problem.
        /// </summary>
        protected abstract IComparer<Variable<TVar, TVal>> GetComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem);
    }
}
