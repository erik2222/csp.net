using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csp
{
    /// <summary>
    /// A variable selection strategy that returns the unassigned variable with the fewest allowed remaining values in its domain.
    /// </summary>
    public class MinimumRemainingValueVariableSelectionStrategy<TVar, TVal> : ComparerVariableSelectionStrategyBase<TVar, TVal>
    {
        /// <summary>
        /// Returns a comparer that orders variables by the smallest number of remaining values that can be assigned to it.
        /// </summary>
        protected override IComparer<Variable<TVar, TVal>> GetComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return new MinimumRemainingValueVariableComparer(assignment, problem);
        }

        private sealed class MinimumRemainingValueVariableComparer : IComparer<Variable<TVar, TVal>>
        {   
	        private readonly Assignment<TVar, TVal> assignment;
            private readonly Problem<TVar, TVal> problem;
            private readonly IDictionary<Variable<TVar, TVal>, int> remaingValuesCache = new Dictionary<Variable<TVar, TVal>, int>();

            public MinimumRemainingValueVariableComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
            {
                if (assignment == null) throw new ArgumentNullException("assignment");
                if (problem == null) throw new ArgumentNullException("problem");
                this.assignment = assignment;
                this.problem = problem;
	        }

	        public int Compare(Variable<TVar, TVal> a, Variable<TVar, TVal> b) {
                int aCount = GetRemainingValueCount(a);
                int bCount = GetRemainingValueCount(b);
                return aCount.CompareTo(bCount);
	        }

            private int GetRemainingValueCount(Variable<TVar, TVal> variable)
            {
                int remainingValueCount;
                if (!remaingValuesCache.TryGetValue(variable, out remainingValueCount))
                {
                    remainingValueCount = 0;
                    foreach (var value in variable.Domain)
                    {
                        if (assignment.Assign(variable, value).IsConsistent(problem.Constraints))
                        {
                            remainingValueCount++;
                        }
                    }
                    remaingValuesCache[variable] = remainingValueCount;
                }
                return remainingValueCount;
            }
        }
    }
}
