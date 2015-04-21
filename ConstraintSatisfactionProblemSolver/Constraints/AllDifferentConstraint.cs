using Csp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.Constraints
{
    /// <summary>
    /// A constraint that is violated if any two variables have the same value.
    /// </summary>
    public class AllDifferentConstraint<TVar, TVal> : IConstraint<TVar, TVal>
    {
        private readonly IImmutableList<Variable<TVar, TVal>> variables;

        /// <summary>
        /// Constructs a constraint for ensuring that all of the specified variable have different values.
        /// </summary>
        /// <param name="variables">the variables</param>
        /// <exception cref="ArgumentException">if fewer than two variables are provided</exception>
        public AllDifferentConstraint(params Variable<TVar, TVal>[] variables)
            : this(ImmutableCollectionUtils.AsImmutableList(variables)) { }

        /// <summary>
        /// Constructs a constraint for ensuring that all of the specified variable have different values.
        /// </summary>
        /// <param name="variables">the variables</param>
        /// <exception cref="ArgumentException">if fewer than two variables are provided</exception>
        public AllDifferentConstraint(IEnumerable<Variable<TVar, TVal>> variables)
            : this(ImmutableCollectionUtils.AsImmutableList(variables)) { }

        private AllDifferentConstraint(IImmutableList<Variable<TVar, TVal>> variables)
        {
            if (variables == null) throw new ArgumentNullException("variables");
            if (variables.Count < 2) throw new ArgumentException("Must have at least two variables");
            this.variables = variables;
        }

        /// <summary>
        /// The variables that must all have different values.
        /// </summary>
        public IReadOnlyCollection<Variable<TVar, TVal>> Variables
        {
            get { return variables; }
        }

        /// <summary>
        /// Returns true if any two of the variables included in this constraint have the same values in the specified assignment.
        /// </summary>
        public bool IsViolated(Assignment<TVar, TVal> assignment)
        {
            var assignedVariables = variables.Where(v => assignment.HasValue(v));
            var assignedValues = assignedVariables.Select(v => assignment.GetValue(v));
            return assignedValues.Count() != assignedValues.Distinct().Count();
        }
    }
}
