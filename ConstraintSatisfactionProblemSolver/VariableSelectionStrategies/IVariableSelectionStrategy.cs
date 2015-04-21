using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// Selects a variable for use in a backtracking constraint satisfaction problem solver 
    /// (or any solver that needs to select variables one at a time).
    /// </summary>
    public interface IVariableSelectionStrategy<TVar, TVal>
    {
        /// <summary>
        /// Selects the next unassigned variable.
        /// </summary>
        /// 
        /// <param name="variables">the variables to sort</param>
        /// 
        /// <param name="assignment">the current assignment</param>
        /// 
        /// <param name="problem">the problem that is being solved</param>
        /// 
        /// <returns>an unassigned variable</returns>
        Variable<TVar, TVal> SelectUnassignedVariable(IEnumerable<Variable<TVar, TVal>> variables, Assignment<TVar, TVal> assignment, Problem<TVar, TVal> problem);
    }
}
