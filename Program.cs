var treeMatrix = File.ReadLines(args[0])
    .Select(l => l.Select(c => c - '0').ToArray()).ToArray();

var treeIsVisible = (int i, int j) =>
            i == 0 || j == 0
            || i == treeMatrix.Length - 1 || j == treeMatrix[i].Length - 1
            || !treeMatrix[..i].Select(a => a[j]).Any(t => t >= treeMatrix[i][j])
            || !treeMatrix[(i + 1)..].Select(a => a[j]).Any(t => t >= treeMatrix[i][j])
            || !treeMatrix[i][..j].Any(t => t >= treeMatrix[i][j])
            || !treeMatrix[i][(j + 1)..].Any(t => t >= treeMatrix[i][j]);

var ScenicScore = (int i, int j) =>
    treeMatrix[..i].Select(a => a[j]).Reverse().TakeUntil(t => t < treeMatrix[i][j]).Count() *
    treeMatrix[(i + 1)..].Select(a => a[j]).TakeUntil(t => t < treeMatrix[i][j]).Count() *
    treeMatrix[i][..j].Reverse().TakeUntil(t => t < treeMatrix[i][j]).Count() *
    treeMatrix[i][(j + 1)..].TakeUntil(t => t < treeMatrix[i][j]).Count();

int visibleTrees = 0;
int maxScenicScore = 0;
for (int i = 0; i < treeMatrix.Length; i++)
    for (int j = 0; j < treeMatrix[i].Length; j++)
    {
        if (treeIsVisible(i, j)) visibleTrees++;
        maxScenicScore = Math.Max(maxScenicScore, ScenicScore(i, j));
    }

Console.WriteLine(visibleTrees);
Console.WriteLine(maxScenicScore);
