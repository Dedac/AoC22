var Rope = new (int, int)[10];
var visits = new List<(int, int)> { (0, 0) };
var visits10 = new List<(int, int)> { (0, 0) };

var move = new Dictionary<string, Func<(int, int), (int, int)>>(){
    {"R", (t) => (t.Item1 + 1, t.Item2)},
    {"L", (t) => (t.Item1 - 1, t.Item2)},
    {"U", (t) => (t.Item1, t.Item2 + 1)},
    {"D", (t) => (t.Item1, t.Item2 - 1)}
};

var follow = ((int, int) Leader, (int, int) Follows) =>
{
    var d = (Leader.Item1 - Follows.Item1, Leader.Item2 - Follows.Item2);
    if (Math.Abs(d.Item1) > 1 || Math.Abs(d.Item2) > 1)
    {
        if (d.Item1 != 0) Follows = move[Leader.Item1 < Follows.Item1 ? "L" : "R"](Follows);
        if (d.Item2 != 0) Follows = move[Leader.Item2 > Follows.Item2 ? "U" : "D"](Follows);
    }
    return Follows;
};

foreach (var line in File.ReadLines(args[0]).Select(l => l.Split(' ')))
{
    foreach (var _ in Enumerable.Range(1,int.Parse(line[1])))
    {
        Rope[0] = move[line[0]](Rope[0]);
        for (int i = 1; i<10; i++) Rope[i] = follow(Rope[i-1],Rope[i]);
        if (!visits.Contains(Rope[1])) visits.Add(Rope[1]);
        if (!visits10.Contains(Rope[9])) visits10.Add(Rope[9]);
    }
}

Console.WriteLine(visits.Count());
Console.WriteLine(visits10.Count());
