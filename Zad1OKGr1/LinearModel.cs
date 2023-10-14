using System.Collections.Immutable;
using Google.OrTools.LinearSolver;

namespace Zad1OKGr1;

public static class LinearModel
{
    public static void BuildAndSolve(int n, int m, List<ImmutableHashSet<int>> C)
    {
        Solver solver = Solver.CreateSolver("SCIP");
        if (solver is null)
        {
            Console.WriteLine("Could not initiate solver SCIP!");
            return;
        }

        int j, s1, s2;
        Variable[] x = new Variable[m];
        for (j = 0; j < m; j++)
        {
            x[j] = solver.MakeBoolVar($"x_{j}");
        }
        Console.WriteLine("Number of variables = " + solver.NumVariables());
        Dictionary<(int, int, int), int> k = new();
        for (j = 0; j < m; j++)
        {
            for (s1 = 0; s1 < n - 1; ++s1)
            {
                for (s2 = s1 + 1; s2 < n; ++s2)
                {
                    if (C[j].Contains(s1) && !C[j].Contains(s2)
                        || C[j].Contains(s2) && !C[j].Contains(s1)) k[(j, s1, s2)] = 1;
                    else k[(j, s1, s2)] = 0;
                }
            }
        }
        for (s1 = 0; s1 < n-1; ++s1)
        {
            for (s2 = s1 + 1; s2 < n; ++s2)
            {
                Constraint constraint = solver.MakeConstraint(1, m, "");
                for (j = 0; j < m; ++j)
                {
                    constraint.SetCoefficient(x[j], k[(j, s1, s2)]);
                }
            }
        }
        Console.WriteLine("Number of constraints = " + solver.NumConstraints());
        Objective objective = solver.Objective();
        for (j = 0; j < m; ++j)
        {
            objective.SetCoefficient(x[j], 1);
        }
        objective.SetMinimization();
        Solver.ResultStatus resultStatus = solver.Solve();
        if (resultStatus != Solver.ResultStatus.OPTIMAL)
        {
            Console.WriteLine("The problem does not have a solution!");
            return;
        }

        Console.WriteLine("Solution:");
        Console.WriteLine("Optimal objective value = " + solver.Objective().Value());

        for (j = 0; j < m; ++j)
        {
            Console.WriteLine("x[" + j + "] = " + x[j].SolutionValue());
        }
    }
}