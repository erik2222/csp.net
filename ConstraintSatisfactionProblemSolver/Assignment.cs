using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using Csp.Utils;

namespace Csp
{
    /// <summary>
    /// Represents an assignment of variables to values for a constraint-satisfaction problem.
    /// </summary>
    public class Assignment<TVar, TVal>
    {
        private readonly IImmutableDictionary<Variable<TVar, TVal>, TVal> assignments;

        /// <summary>
        /// Constructs an empty assignment.
        /// </summary>
        public Assignment()
            : this(ImmutableDictionary<Variable<TVar, TVal>, TVal>.Empty) { }

        /// <summary>
        /// Constructs an assignment with the specified variable/value assignments.
        /// </summary>
        /// <param name="assignments">the assignments</param>
        public Assignment(IEnumerable<KeyValuePair<Variable<TVar, TVal>, TVal>> assignments)
            : this(ImmutableCollectionUtils.AsImmutableDictionary(assignments)) { }

        private Assignment(IImmutableDictionary<Variable<TVar, TVal>, TVal> assignments)
        {
            //if (assignments == null) throw new ArgumentNullException("assignments");
            this.assignments = assignments ?? ImmutableDictionary<Variable<TVar, TVal>, TVal>.Empty;
        }

        /// <summary>
        /// Returns true if there is a value assigned to the specified variable.
        /// </summary>
        /// 
        /// <param name="variable">the variable</param>
        /// 
        /// <returns>true if the variable has been assigned a value</returns>
        public bool HasValue(Variable<TVar, TVal> variable)
        {
            return assignments.ContainsKey(variable);
        }

        /// <summary>
        /// Returns the value assigned to the given variable 
        /// or the default value for the type if the variable 
        /// does not have an assigned value.
        /// </summary>
        /// 
        /// <param name="variable">the variable</param>
        /// 
        /// <returns>the assigned value or the default type value</returns>
        public TVal GetValue(Variable<TVar, TVal> variable)
        {
            TVal value;
            if (!assignments.TryGetValue(variable, out value))
            {
                return default(TVal);
            }
            return value;
        }

        /// <summary>
        ///  Returns true if all variables in this assignment have been assigned a value.
        ///  A complete assignment may not necessarily be consistent.
        /// </summary>
        /// 
        /// <param name="variables">all of the variables in a csp</param>
        /// 
        /// <returns>true if the assignment is complete</returns>
        /// 
        /// <exception cref="System.ArgumentNullException">if <code>variables</code> is null</exception>
        /// 
        public bool IsComplete(IEnumerable<Variable<TVar, TVal>> variables)
        {
            return variables.All(v => assignments.ContainsKey(v));
        }

        /// <summary>
        /// Returns true if this none of the assignments violate any constraints.
        /// A consistent assignment may not necessarily be complete.
        /// </summary>
        /// 
        /// <param name="constraints">all of the constraints in a csp</param>
        /// 
        /// <returns>true if this assignment is consistent</returns>
        /// 
        /// <exception cref="System.ArgumentNullException">if <code>constraints</code> is null</exception>
        public bool IsConsistent(IEnumerable<IConstraint<TVar, TVal>> constraints)
        {
            return constraints.All(c => !c.IsViolated(this));
        }

        /// <summary>
        /// The variables that have an assigned value.
        /// </summary>
        public IEnumerable<Variable<TVar, TVal>> AssignedVariables
        {
            get { return assignments.Keys; }
        }

        /// <summary>
        /// Returns an immutable dictionary representation of this assignment.
        /// </summary>
        /// <returns>a dictionary representation of this assignment</returns>
        public IReadOnlyDictionary<Variable<TVar, TVal>, TVal> AsReadOnlyDictionary() { return assignments; }

        #region Modify

        /// <summary>
        /// Returns an assignment when the specified variable is assigned the specified value, 
        /// and all other assignments are the same as in this one.  
        /// This method will never modify the current assignment; it will return a new assignment.
        /// </summary>
        /// 
        /// <param name="variable">the variable to be assigned</param>
        /// 
        /// <param name="value">the value to assign</param>
        /// 
        /// <returns>the new assignment</returns>
        /// 
        /// <exception cref="System.ArgumentNullException">if variable or value is null</exception>
        public Assignment<TVar, TVal> Assign(Variable<TVar, TVal> variable, TVal value)
        {
            if (variable == null)
            {
                throw new ArgumentNullException("variable");
            }
            if (value == null)
            {
                //return Unassign(variable);
                throw new ArgumentNullException("value");
            }
            return new Assignment<TVar, TVal>(assignments.SetItem(variable, value));
        }

        /// <summary>
        /// Returns an assignment when the specified variable is unassigned, 
        /// and all other assignments are the same as in this one.
        /// This method will never modify the current assignment; it will return a new assignment.
        /// </summary>
        /// 
        /// <param name="variable">the variable to unassign</param>
        /// 
        /// <returns>the new assignment</returns>
        /// 
        /// <exception cref="System.ArgumentNullException">if variable is null</exception>
        public Assignment<TVar, TVal> Unassign(Variable<TVar, TVal> variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException("variable");
            }

            // Don't need to do anything if the variable is not currently assigned.
            if (!HasValue(variable))
            {
                return this;
            }

            return new Assignment<TVar, TVal>(assignments.Remove(variable));
        }

        #endregion
    }
}
