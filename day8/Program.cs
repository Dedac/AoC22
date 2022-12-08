internal class Day8
{
    private static void Day(string[] args)
    {
        var treeMatrix = File.ReadLines(args[0]) //read the trees into an int 'matrix'
            .Select(l => l.Select(c => c - '0').ToArray()).ToArray();
        int visibleTrees = 0;
        int maxScenicScore = 0;
        for (int i = 0; i < treeMatrix.Length; i++) //rows
            for (int j = 0; j < treeMatrix[i].Length; j++) //columns
            {   //look out from this tree
                var viewsFromTree = new List<IEnumerable<int>>() {
                            treeMatrix[..i].Select(a => a[j]).Reverse(), //up
                            treeMatrix[(i + 1)..].Select(a => a[j]), //down
                            treeMatrix[i][..j].Reverse(), //left
                            treeMatrix[i][(j + 1)..] }; //right
                                                        // Check each direction to see if there are not any same or taller trees
                if (viewsFromTree.Any(v => !v.Any(t => t >= treeMatrix[i][j]))) visibleTrees++;
                // Multiply views from each direction, and select the max
                maxScenicScore = Math.Max(maxScenicScore,
                    viewsFromTree.Aggregate(1, (c, v) => c * v.TakeUntil(t => t < treeMatrix[i][j]).Count()));
            }
        Console.WriteLine(visibleTrees);
        Console.WriteLine(maxScenicScore);
    }
}