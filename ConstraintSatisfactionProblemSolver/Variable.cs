using Csp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Csp
{
    /// <summary>
    /// Represents a variable with a discrete domain of valid values in a constraint-satisfaction problem. 
    /// </summary>
    /// <typeparam name="TVar">type that variables represent</typeparam>
    /// <typeparam name="TVal">type of value to assign to variables </typeparam>
    public sealed class Variable<TVar, TVal>
    {
        private readonly TVar userObject;
        private readonly IImmutableList<TVal> domain;

        /// <summary>
        /// Constructs a variable with the given user object and the given discrete domain.
        /// </summary>
        /// <param name="userObject">the object that this variable represents</param>
        /// <param name="domain">the domain of valid values that can be assigned to this variable</param>
        /// <exception cref="ArgumentNullException">if either the user object or domain is null</exception>
        public Variable(TVar userObject, IEnumerable<TVal> domain)
            : this(userObject, ImmutableCollectionUtils.AsImmutableList(domain, throwExceptionIfNull: true)) { }

        private Variable(TVar userObject, IImmutableList<TVal> domain)
        {
            if (userObject == null) throw new ArgumentNullException("userObject");
            if (domain == null) throw new ArgumentNullException("domain");

            this.userObject = userObject;
            this.domain = domain;
        }

        /// <summary>
        /// The user object represented by this variable.
        /// </summary>
        /// <remarks>
        /// This name is based on what the Java DefaultMutableTreeNode calls its equivalent property.
        /// </remarks>
        public TVar UserObject 
        {
            get { return userObject; }
        }

        /// <summary>
        /// The discrete domain of values that can be assigned to 
        /// this variable.  This collection is immutable and will never be 
        /// null.
        /// </summary>
        public IReadOnlyCollection<TVal> Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// Returns the string representation of the user object.
        /// </summary>
        public override string ToString()
        {
            return userObject.ToString();
        }
    }
}
