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
    /// Solves a constraint-satisfaction problem using a recursive backtracking algorithm.
    /// </summary>
    public sealed class RecursiveBacktrackSolver<TVar, TVal> : ISolver<TVar, TVal>
    {
        private readonly IVariableSelectionStrategy<TVar, TVal> variableSelectionStrategy;
        private readonly IDomainSortStrategy<TVar, TVal> domainSortStrategy;

        /// <summary>
        /// Constructs a backtrack solver using the given strategies.
        /// </summary>
        /// 
        /// <param name="variableSelectionStrategy">the strategy for selecting the next unassigned variable, or null to use the default strategy</param>
        /// 
        /// <param name="domainSortStrategy">the strategy for ordering the domain before assignment values, or null to use the default strategy</param>
        public RecursiveBacktrackSolver(IVariableSelectionStrategy<TVar, TVal> variableSelectionStrategy = null, IDomainSortStrategy<TVar, TVal> domainSortStrategy = null)
        {
            this.variableSelectionStrategy = variableSelectionStrategy ?? new DefaultVariableSelectionStrategy<TVar, TVal>();
            this.domainSortStrategy = domainSortStrategy ?? new DefaultDomainSortStrategy<TVar, TVal>();
        }

        /// <summary>
        /// The stragy used by this solver for selecting unassigned variables.
        /// </summary>
        public IVariableSelectionStrategy<TVar, TVal> VariableSelectionStrategy { get { return variableSelectionStrategy; } }

        /// <summary>
        /// The strategy used by this solver for ordering the domain of a variable.
        /// </summary>
        public IDomainSortStrategy<TVar, TVal> DomainSortStrategy { get { return domainSortStrategy; } }

        /// <summary>
        /// Attempts to solve the specified constraint satisfaction problem.
        /// If the problem could not be solved then this will return null.
        /// </summary>
        /// <param name="problem">the problem to solve</param>
        /// <returns>a complete assignment or null</returns>
        /// <exception cref="ArgumentNullException">if the problem is null</exception>
        public Assignment<TVar, TVal> Solve(Problem<TVar, TVal> problem)
        {
            return Solve(problem, CancellationToken.None);
        }

        /// <summary>
        /// Attempts to solve the specified constraint satisfaction problem.
        /// If the problem could not be solved then this will return null.
        /// </summary>
        /// <param name="problem">the problem to solve</param>
        /// <param name="cancellationToken">a token that can be used to cancel the solve operation</param>
        /// <returns>a complete assignment or null</returns>
        /// <exception cref="OperationCanceledException">if the token is cancelled</exception>
        /// <exception cref="ArgumentNullException">if the problem is null</exception>
        public Assignment<TVar, TVal> Solve(Problem<TVar, TVal> problem, CancellationToken cancellationToken)
        {
            if (problem == null) throw new ArgumentNullException("problem");
            return RecursiveBacktrack(problem.InitialAssignment, problem, cancellationToken);
        }

        private Assignment<TVar, TVal> RecursiveBacktrack(Assignment<TVar, TVal> currentAssignment, Problem<TVar, TVal> problem, CancellationToken cancellationToken)
        {
            // This is based on an algorithm from 
            // "Artificial Intelligence: A Modern Approach" 2nd Edition by Stuart Russel and Perter Norvig (page 142).
 
            if (currentAssignment.IsComplete(problem.Variables))
            {
                return currentAssignment;
            }

            cancellationToken.ThrowIfCancellationRequested();

            Variable<TVar, TVal> variable = VariableSelectionStrategy.SelectUnassignedVariable(problem.Variables, currentAssignment, problem);
            IEnumerable<TVal> domain = DomainSortStrategy.GetOrderedDomain(variable, currentAssignment, problem);
            foreach (var value in domain)
            {
                var nextAssignment = currentAssignment.Assign(variable, value);
                if (nextAssignment.IsConsistent(problem.Constraints))
                {
                    var result = RecursiveBacktrack(nextAssignment, problem, cancellationToken);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
