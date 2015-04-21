using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Csp.UnitTests
{
    [TestFixture]
    public class TestProblem
    {
        [Test]
        public void Test_Constructor_Variables_Null()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Problem<int, int>(null, new List<IConstraint<int, int>>()));

            Assert.Throws<ArgumentNullException>(() => 
                new Problem<int, int>(null, new List<IConstraint<int, int>>(), new Assignment<int, int>()));
        }

        [Test]
        public void Test_Constructor_Constraints_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Problem<int, int>(new List<Variable<int, int>>(), null));

            Assert.Throws<ArgumentNullException>(() =>
                new Problem<int, int>(new List<Variable<int, int>>(), null, new Assignment<int, int>()));
        }

        [Test]
        public void Test_Constructor_Assignment_Null()
        {
            var p = new Problem<int, int>(new List<Variable<int, int>>(), new List<IConstraint<int, int>>(), null);

            Assert.IsNotNull(p.InitialAssignment);
        }

        [Test]
        public void Test_Constructor_Values()
        {
            var v = new Variable<int, int>(1, new List<int>());
            var variables = new List<Variable<int, int>> { v };

            var c = new Mock<IConstraint<int, int>>().Object;
            var constraints = new List<IConstraint<int, int>> { c };

            var a = new Assignment<int, int>();
            a.Assign(v, 1);

            var p = new Problem<int, int>(variables, constraints, a);

            Assert.AreEqual(v, p.Variables.ToArray()[0]);
            Assert.AreEqual(c, p.Constraints.ToArray()[0]);
            Assert.AreEqual(a.GetValue(v), p.InitialAssignment.GetValue(v));
        }

        [Test]
        public void Test_Variables_Immutable()
        {
            var p = new Problem<int, int>(new List<Variable<int, int>>(), new List<IConstraint<int, int>>(), null);

            var l = (IList<Variable<int, int>>)p.Variables;
            var v = new Variable<int, int>(1, new List<int>());

            Assert.Throws<NotSupportedException>(() => l.Add(v));
        }

        [Test]
        public void Test_Constraints_Immutable()
        {
            var p = new Problem<int, int>(new List<Variable<int, int>>(), new List<IConstraint<int, int>>(), null);

            var l = (IList<IConstraint<int, int>>)p.Constraints;
            var c = new Mock<IConstraint<int, int>>().Object;

            Assert.Throws<NotSupportedException>(() => l.Add(c));
        }
    }
}
