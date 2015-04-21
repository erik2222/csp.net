using Csp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.VariableSelectionStrategies
{
    /// <summary>
    /// A variable selection strategy that selects a random variable.
    /// </summary>
    public class RandomVariableSelectionStrategy<TVar, TVal> : IVariableSelectionStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns a random unassigned variable from the list.
        /// This may return a different variable each time it is called even if all of the inputs are the same.
        /// </summary>
        public Variable<TVar, TVal> SelectUnassignedVariable(IEnumerable<Variable<TVar, TVal>> variables,
            Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            var variableList = variables.ToList();
            RandomUtils.Shuffle(variableList);
            return variableList.FirstOrDefault(v => !assignment.HasValue(v));
        }
    }
}
