using System.Collections.Immutable;
using Zad1OKGr1;

List<ImmutableHashSet<int>> subsets = new();
subsets.Add(ImmutableHashSet.Create(0, 1, 3));
subsets.Add(ImmutableHashSet.Create(1, 2, 3));
subsets.Add(ImmutableHashSet.Create(2, 3));

LinearModel.BuildAndSolve(4, 3, subsets);
