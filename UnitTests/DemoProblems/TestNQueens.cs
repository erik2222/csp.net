using Csp.UnitTests.DemoProblems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.UnitTests.DemoProblems
{
    public class TestNQueens
    {
        [Test]
        public void Solve_BacktrackSolver()
        {
            var problem = NQueens.CreateProblem();
            var solver = new RecursiveBacktrackSolver<int, int>();
            var solution = solver.Solve(problem, CancellationToken.None);

            var expected = new Dictionary<int, int>
            {
                {1, 1}, {2, 5}, {3, 8}, {4, 6}, {5, 3}, {6, 7}, {7, 2}, {8, 4},
            };
            CollectionAssert.AreEquivalent(expected, solution.AsReadOnlyDictionary().OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value));
        }

        [Test]
        public void Solve_BacktrackSolver_MaximumDegreeVariableSelectionStrategy()
        {
            var problem = NQueens.CreateProblem();
            var solver = new RecursiveBacktrackSolver<int, int>(variableSelectionStrategy: new MaximumDegreeVariableSelectionStrategy<int, int>());
            var solution = solver.Solve(problem, CancellationToken.None);

            var expected = new Dictionary<int, int>
            {
                {1, 1}, {2, 5}, {3, 8}, {4, 6}, {5, 3}, {6, 7}, {7, 2}, {8, 4},
            };
            CollectionAssert.AreEquivalent(expected, solution.AsReadOnlyDictionary().OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value));
        }

        [Test]
        public void Solve_BacktrackSolver_MinimumRemainingValueVariableSelectionStrategy()
        {
            var problem = NQueens.CreateProblem();
            var solver = new RecursiveBacktrackSolver<int, int>(variableSelectionStrategy: new MinimumRemainingValueVariableSelectionStrategy<int, int>());
            var solution = solver.Solve(problem, CancellationToken.None);

            var expected = new Dictionary<int, int>
            {
                {1, 1}, {2, 5}, {3, 8}, {4, 6}, {5, 3}, {6, 7}, {7, 2}, {8, 4},
            };
            CollectionAssert.AreEquivalent(expected, solution.AsReadOnlyDictionary().OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value));
        }

        [Test]
        public void Solve_BacktrackSolver_DomainReverseOrder()
        {
            var problem = NQueens.CreateProblem();
            var solver = new RecursiveBacktrackSolver<int, int>(domainSortStrategy: new ComparerDomainSortStrategy<int, int>((a, b) => b.CompareTo(a)));
            var solution = solver.Solve(problem, CancellationToken.None);

            var expected = new Dictionary<int, int>
            {
                {1, 8}, {2, 4}, {3, 1}, {4, 3}, {5, 6}, {6, 2}, {7, 7}, {8, 5},
            };
            CollectionAssert.AreEquivalent(expected, solution.AsReadOnlyDictionary().OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value));
        }

        [Test]
        public void Solve_BacktrackSolver_VariablesReverseOrder()
        {
            var problem = NQueens.CreateProblem();
            var solver = new RecursiveBacktrackSolver<int, int>(variableSelectionStrategy: new ComparerVariableSelectionStrategy<int, int>((a, b) => b.UserObject.CompareTo(a.UserObject)));
            var solution = solver.Solve(problem, CancellationToken.None);

            var expected = new Dictionary<int, int>
            {
                {1, 4}, {2, 2}, {3, 7}, {4, 3}, {5, 6}, {6, 8}, {7, 5}, {8, 1},
            };
            CollectionAssert.AreEquivalent(expected, solution.AsReadOnlyDictionary().OrderBy(kv => kv.Key.UserObject).ToDictionary(kv => kv.Key.UserObject, kv => kv.Value));
        }

        [TestCase(2)]
        [TestCase(3)]
        public void Solve_BacktrackSolver_NoSolution(int n)
        {
            var problem = NQueens.CreateProblem(n);
            var solver = new RecursiveBacktrackSolver<int, int>();
            var solution = solver.Solve(problem, CancellationToken.None);
            Assert.IsNull(solution);
        }
    }
}
