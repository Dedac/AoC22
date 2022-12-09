var H = (0, 0);
var T = (0,0);
var T2 = new (int,int)[9];
var visits = new List<(int, int)> { (0, 0) };
var move = new Dictionary<string, Func<(int, int), (int, int)>>(){
    {"R", (t) => (t.Item1 + 1, t.Item2)},
    {"L", (t) => (t.Item1 - 1, t.Item2)},
    {"U", (t) => (t.Item1, t.Item2 + 1)},
    {"D", (t) => (t.Item1, t.Item2 - 1)}
};

foreach (var line in File.ReadLines(args[0]).Select(l => l.Split(' ')))
{
    for (int i = 0; i < int.Parse(line[1]); i++)
    {
        H = move[line[0]](H);

        var d = (H.Item1 - T.Item1, H.Item2 - T.Item2);
        if (Math.Abs(d.Item1) > 1 || Math.Abs(d.Item2) > 1)
        {
            visits.Add(T);
            if (d.Item1 != 0) T = move[H.Item1 < T.Item1 ? "L" : "R"](T);
            if (d.Item2 != 0) T = move[H.Item2 > T.Item2 ? "U" : "D"](T);
        }
    }
}
visits.Add(T);
Console.WriteLine(visits.Distinct().Count());