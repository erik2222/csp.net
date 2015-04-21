using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp
{
    /// <summary>
    /// Solves a constraint satisfaction problem.
    /// </summary>
    /// <typeparam name="TVar">type that variables represent</typeparam>
    /// <typeparam name="TVal">type of value to assign to variables </typeparam>
    public interface ISolver<TVar, TVal>
    {
        /// <summary>
        /// Attempts to solve the specified constraint satisfaction problem.
        /// If the problem could not be solved then this will return null.
        /// </summary>
        /// <param name="problem">the problem to solve</param>
        /// <param name="cancellationToken">a token that can be used to cancel the solve operation</param>
        /// <returns>a complete assignment or null</returns>
        /// <exception cref="OperationCanceledException">if the token is cancelled</exception>
        Assignment<TVar, TVal> Solve(Problem<TVar, TVal> problem, CancellationToken cancellationToken);
    }
}
