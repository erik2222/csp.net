using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp.UnitTests.DemoProblems
{
    public static class NQueens
    {
        public static Problem<int, int> CreateProblem(int n = 8)
        {
            var columns = Enumerable.Range(1, n).ToList();
            var variables = Enumerable.Range(1, n).Select(row => new Variable<int, int>(row, columns)).ToList();
            var constraints = (
                from rowVar1 in variables
                from rowVar2 in variables
                where rowVar1.UserObject < rowVar2.UserObject
                select new EightQueensConstraint(rowVar1, rowVar2)
            ).ToList();
            return new Problem<int,int>(variables, constraints);
        }

        private class EightQueensConstraint : IConstraint<int, int> 
        {
            private readonly Variable<int, int> rowVariableA;
            private readonly Variable<int, int> rowVariableB;

            public EightQueensConstraint(Variable<int, int> rowVariableA, Variable<int, int> rowVariableB) 
            {
                if (rowVariableA == null) throw new ArgumentNullException("rowVariableA");
                if (rowVariableB == null) throw new ArgumentNullException("rowVariableB");
                if (rowVariableA.UserObject == rowVariableB.UserObject) throw new ArgumentException("row variables a and b must be different");

			    this.rowVariableA = rowVariableA;
                this.rowVariableB = rowVariableB;
		    }

            public IReadOnlyCollection<Variable<int, int>> Variables
            {
                get { return new[] { rowVariableA, rowVariableB }; }
            }

            public bool IsViolated(Assignment<int, int> assignment)
            {
                // If either value isn't assigned yet, then there can be no violation
                if (!assignment.HasValue(rowVariableA) || !assignment.HasValue(rowVariableB))
                {
                    return false;
                }

                var columnValueA = assignment.GetValue(rowVariableA);
                var columnValueB = assignment.GetValue(rowVariableB);

                // Check for orthogonal violations
                if (columnValueA == columnValueB) {
                    return true;
                }

                // Check for diagonal violations
                int rowDiff = Math.Abs(rowVariableA.UserObject - rowVariableB.UserObject);
                int colDiff = Math.Abs(columnValueA - columnValueB);
                if (rowDiff == colDiff) {
                    return true;
                }

                return false;
            }
        }
    }
}
