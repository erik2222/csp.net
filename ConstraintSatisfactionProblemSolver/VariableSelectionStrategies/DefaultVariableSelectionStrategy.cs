using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// A default implementation of the variable selection strategy.
    /// </summary>
    public sealed class DefaultVariableSelectionStrategy<TVar, TVal> : IVariableSelectionStrategy<TVar, TVal>
    {
        /// <summary>
        /// Returns the first unassigned variable in the specified variable list, without applying any ordering.
        /// </summary>
        /// <param name="variables">the first variable from this enumerable will be selected</param>
        /// <param name="assignment">not used</param>
        /// <param name="problem">not used</param>
        /// <returns>the first unassigned variable</returns>
        public Variable<TVar, TVal> SelectUnassignedVariable(IEnumerable<Variable<TVar, TVal>> variables, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem)
        {
            return variables.First(v => !assignment.HasValue(v));
        }
    }
}
