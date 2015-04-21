using Csp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Csp
{
    /// <summary>
    /// Represents a constraint-satisfaction problem.
    /// </summary>
    /// <typeparam name="TVar">type that variables represent</typeparam>
    /// <typeparam name="TVal">type of value to assign to variables </typeparam>
    public sealed class Problem<TVar, TVal>
    {
        private readonly IImmutableList<Variable<TVar, TVal>> variables;
        private readonly IImmutableList<IConstraint<TVar, TVal>> constraints;
        private readonly Assignment<TVar, TVal> initialAssignment;

        /// <summary>
        /// Constructs a problem with the given variables and constraints and 
        /// an empty initial assignment.
        /// </summary>
        /// 
        /// <param name="variables">the varibles</param>
        /// 
        /// <param name="constraints">the constraints</param>
        /// 
        /// <exception cref="System.ArgumentNullException">if any of the parameters are null</exception>
        public Problem(IEnumerable<Variable<TVar, TVal>> variables, IEnumerable<IConstraint<TVar, TVal>> constraints)
            : this(variables, constraints, initialAssignment: null) { }

        /// <summary>
        /// Constructs a problem with the given variables, constraints, 
        /// and initial assignment.
        /// </summary>
        /// 
        /// <param name="variables">the variables</param>
        /// 
        /// <param name="constraints">the constraints</param>
        /// 
        /// <param name="initialAssignment">the initial assignment</param>
        /// 
        /// <exception cref="System.ArgumentNullException">if the variables or constraints are null</exception>
        public Problem(IEnumerable<Variable<TVar, TVal>> variables,
                IEnumerable<IConstraint<TVar, TVal>> constraints,
                Assignment<TVar, TVal> initialAssignment)
            : this(
                variables: ImmutableCollectionUtils.AsImmutableList(variables, throwExceptionIfNull: true),
                constraints: ImmutableCollectionUtils.AsImmutableList(constraints, throwExceptionIfNull: true),
                initialAssignment: initialAssignment
            ) { }

        private Problem(IImmutableList<Variable<TVar, TVal>> variables,
            IImmutableList<IConstraint<TVar, TVal>> constraints,
            Assignment<TVar, TVal> initialAssignment)
        {
            if (variables == null) throw new ArgumentNullException("variables");
            if (constraints == null) throw new ArgumentNullException("constraints");

            this.variables = variables;
            this.constraints = constraints;
            this.initialAssignment = initialAssignment ?? new Assignment<TVar, TVal>();
        }

        /// <summary>
        /// The variables in this problem.
        /// </summary>
        public IReadOnlyCollection<Variable<TVar, TVal>> Variables { get { return variables; } }

        /// <summary>
        /// The constraints in this problem.
        /// </summary>
        public IReadOnlyCollection<IConstraint<TVar, TVal>> Constraints { get { return constraints; } }

        /// <summary>
        /// The initial assignment of the problem.  
        /// </summary>
        public Assignment<TVar, TVal> InitialAssignment { get { return initialAssignment; } }
    }
}
