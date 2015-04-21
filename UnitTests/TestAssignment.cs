using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Csp.UnitTests
{
    [TestFixture]
    public class TestAssignment
    {
        protected Variable<int, int> v1;
        protected Variable<int, int> v2;

        [SetUp]
        public void SetUp()
        {
            v1 = new Variable<int, int>(1, new List<int>());
            v2 = new Variable<int, int>(2, new List<int>());
        }

        [Test]
        public void Test_Constructor()
        {
            var a = new Assignment<int, int>();

            CollectionAssert.IsEmpty(a.AssignedVariables);
        }

        [Test]
        public void Test_Constructor_Null()
        {
            var a = new Assignment<string, int>((IDictionary<Variable<string, int>, int>)null);

            CollectionAssert.IsEmpty(a.AssignedVariables);
        }

        [Test]
        public void Test_Constructor_With_Values()
        {
            IDictionary<Variable<int, int>, int> d = new Dictionary<Variable<int, int>, int>
            { 
                {v1, 1}, 
                {v2, 3}
            };
            var a = new Assignment<int, int>(d);

            var assignmentDictionary = a.AsReadOnlyDictionary();
            Assert.AreEqual(1, assignmentDictionary[v1]);
            Assert.AreEqual(3, assignmentDictionary[v2]);
        }

        [Test]
        public void Test_Empty_Assignment_Is_Complete_Without_Variables()
        {
            var a = new Assignment<int, int>();
 
            Assert.IsTrue(a.IsComplete(new List<Variable<int, int>>()));
        }

        [Test]
        public void Test_Empty_Assignment_Is_Complete_With_Variable()
        {
            var a = new Assignment<int, int>();

            Assert.IsFalse(a.IsComplete(new List<Variable<int, int>>() {v1}));
        }

        [Test]
        public void Test_Empty_Assignment_Is_Consistent_With_True_Constraint()
        {
            var a = new Assignment<int, int>();
            var c1 = new Mock<IConstraint<int, int>>();
            c1.Setup(c => c.IsViolated(It.IsAny<Assignment<int, int>>())).Returns(true);

            Assert.IsTrue(c1.Object.IsViolated(null));
            Assert.IsFalse(a.IsConsistent(new List<IConstraint<int, int>> { c1.Object }));
        }

        [Test]
        public void Test_Empty_Assignment_Is_Consistent_With_False_Constraint()
        {
            var a = new Assignment<int, int>();
            var c1 = new Mock<IConstraint<int, int>>();
            c1.Setup(c => c.IsViolated(It.IsAny<Assignment<int, int>>())).Returns(false);

            Assert.IsFalse(c1.Object.IsViolated(null));
            Assert.IsTrue(a.IsConsistent(new List<IConstraint<int, int>> { c1.Object }));
        }

        [Test]
        public void Test_HashValue_True()
        {
            var d = new Dictionary<Variable<int, int>, int>
            { 
                {v1, 1}, 
                {v2, 3}
            };
            var a = new Assignment<int, int>(d);

            Assert.IsTrue(a.HasValue(v1));
        }

        [Test]
        public void Test_HashValue_False()
        {
            var d = new Dictionary<Variable<int, int>, int>
            { 
                {v2, 3}
            };
            var a = new Assignment<int, int>(d);

            Assert.IsFalse(a.HasValue(v1));
        }

        [Test]
        public void Test_GetValue()
        {
            var d = new Dictionary<Variable<int, int>, int>
            { 
                {v1, 1}, 
                {v2, 3}
            };
            var a = new Assignment<int, int>(d);

            Assert.AreEqual(3, a.GetValue(v2));
        }

        [Test]
        public void Test_GetValue_Primitive_Missing()
        {
            var d = new Dictionary<Variable<int, int>, int>
            { 
                {v1, 1}
            };
            var a = new Assignment<int, int>(d);

            Assert.AreEqual(default(int), a.GetValue(v2));
        }

        [Test]
        public void Test_GetValue_Object_Missing()
        {
            var vs1 = new Variable<int, string>(1, new List<string>());
            var vs2 = new Variable<int, string>(2, new List<string>());
            var d = new Dictionary<Variable<int, string>, string>
            { 
                {vs1, "hi"}
            };
            var a = new Assignment<int, string>(d);

            Assert.AreEqual(null, a.GetValue(vs2));
        }

        [Test]
        public void Test_Dictionary_Not_Updates_After_Assignment()
        {
            var a = new Assignment<int, int>();
            var d = a.AsReadOnlyDictionary();

            var newA = a.Assign(v1, 2);
            var newD = newA.AsReadOnlyDictionary();

            Assert.AreEqual(2, newD[v1]);
            Assert.IsFalse(d.ContainsKey(v1));
        }
    }
}
