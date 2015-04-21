# CSP.NET
A .NET constraint satisfaction problem solver.

## Use

Here's an example of setting up and solving a very basic problem:

```c#
const int n = 8;
IReadOnlyCollection<int> values = Enumerable.Range(1, n).ToList();
IReadOnlyCollection<Variable<string, int>> variables = Enumerable.Range(1, n).Select(i => new Variable<string, int>(i.ToString(), values)).ToList();
IConstraint<string, int>[] constraints = new[] { new AllDifferentConstraint<string, int>(variables) };
Problem<string, int> problem = new Problem<string, int>(variables, constraints);

ISolver<string, int> solver = new RecursiveBacktrackSolver<string, int>();
Assignment<string, int> solution = solver.Solve(problem, CancellationToken.None);
IReadOnlyDictionary<Variable<string, int>, int> solutionDictionary = solution.AsReadOnlyDictionary();
```