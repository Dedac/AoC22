/* You are a landscape architect. You are designing a new park. You are given a matrix of numbers representing trees of different sizes. You are asked to find the number of trees that can be seen from any location in the park. Trees can be seen from any location if there are no same or taller trees between them. Also, find the maximum scenic score of the park. The scenic score of the park is the product of the number of trees visible from each location. */
        var treeMatrix = File.ReadLines(args[0])
            .Select(l => l.Select(c => c - '0').ToArray()).ToArray();
        int visibleTrees = 0;
        int maxScenicScore = 0;
        for (int i = 0; i < treeMatrix.Length; i++)
            for (int j = 0; j < treeMatrix[i].Length; j++)
            {
                var viewsFromTree = new List<IEnumerable<int>>() {
                                    treeMatrix[..i].Select(a => a[j]).Reverse(),
                                    treeMatrix[(i + 1)..].Select(a => a[j]),
                                    treeMatrix[i][..j].Reverse(),
                                    treeMatrix[i][(j + 1)..] };
                if (viewsFromTree.Any(v => !v.Any(t => t >= treeMatrix[i][j]))) visibleTrees++;
                maxScenicScore = Math.Max(maxScenicScore, 
                    viewsFromTree.Aggregate(1, (c, v) => c * v.TakeUntil(t => t < treeMatrix[i][j]).Count()));
            }
        Console.WriteLine(visibleTrees);
        Console.WriteLine(maxScenicScore);