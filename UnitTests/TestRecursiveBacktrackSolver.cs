using Csp.Constraints;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.UnitTests
{
    public class TestRecursiveBacktrackSolver
    {
        [Test]
        public void Demo()
        {
            const int n = 8;
            IReadOnlyCollection<int> values = Enumerable.Range(1, n).ToList();
            IReadOnlyCollection<Variable<string, int>> variables = Enumerable.Range(1, n).Select(i => new Variable<string, int>(i.ToString(), values)).ToList();
            IConstraint<string, int>[] constraints = new[] { new AllDifferentConstraint<string, int>(variables) };
            Problem<string, int> problem = new Problem<string, int>(variables, constraints);

            ISolver<string, int> solver = new RecursiveBacktrackSolver<string, int>();
            Assignment<string, int> solution = solver.Solve(problem, CancellationToken.None);
            IReadOnlyDictionary<Variable<string, int>, int> solutionDictionary = solution.AsReadOnlyDictionary();
       
            var expectedDictionary = Enumerable.Range(1, n).ToDictionary(i => i.ToString(), i => i);
            var actualDictionary = solutionDictionary.OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value);
            CollectionAssert.AreEqual(expectedDictionary, actualDictionary);
        }
    }
}
