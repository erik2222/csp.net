using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csp
{
    /// <summary>
    /// A variable selection strategy that returns the unassigned variable involved in the fewest constraints.
    /// </summary>
    /// <typeparam name="TVar">type that variables represent</typeparam>
    /// <typeparam name="TVal">type of value to assign to variables </typeparam>
    public class MaximumDegreeVariableSelectionStrategy<TVar, TVal> : ComparerVariableSelectionStrategyBase<TVar, TVal>
    {
        /// <summary>
        /// Returns a comparer for ordering variables by the degrees of freedom a solver would have for value assignment.
        /// </summary>
        protected override IComparer<Variable<TVar, TVal>> GetComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return new MaximumDegreeVariableComparer(assignment, problem);
        }

        private sealed class MaximumDegreeVariableComparer : IComparer<Variable<TVar, TVal>>
        {
            private readonly IDictionary<Variable<TVar, TVal>, int> variableDegreeDictionary;

            public MaximumDegreeVariableComparer(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
            {
                this.variableDegreeDictionary = CreateVariableDegreeDictionary(assignment, problem);
            }

            private static IDictionary<Variable<TVar, TVal>, int> CreateVariableDegreeDictionary(Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
            {
                var variableDegreeDictionary = new Dictionary<Variable<TVar, TVal>, int>();

                var assignedVariables = assignment.AssignedVariables;
                foreach (var c in problem.Constraints)
                {
                    var variables = c.Variables;
                    var unassignedVariables = variables.Except(assignedVariables);

                    foreach (var v in unassignedVariables)
                    {
                        int degree;
                        if (!variableDegreeDictionary.TryGetValue(v, out degree))
                        {
                            degree = 0;
                        }
                        variableDegreeDictionary[v] = degree + 1;
                    }
                }

                return variableDegreeDictionary;
            }

            public int Compare(Variable<TVar, TVal> x, Variable<TVar, TVal> y)
            {
                int degreeX;
                if (!variableDegreeDictionary.TryGetValue(x, out degreeX))
                {
                    degreeX = 0;
                }

                int degreeY;
                if (!variableDegreeDictionary.TryGetValue(y, out degreeY))
                {
                    degreeY = 0;
                }

                // The order is reversed because we want the maximum degree
                return degreeY.CompareTo(degreeX);
            }
        }
    }
}
