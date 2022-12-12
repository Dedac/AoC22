var start = (0, 0);
var end = (0, 0);
var routes = new Dictionary<(int, int), List<(int, int)>>();
var score = new Dictionary<(int, int), (int, int)>();
var starts = new List<(int, int)>();
var map = File.ReadLines(args[0]).Select(l => l.Select(c => c - 'a').ToArray()).ToArray();

var directions = ((int, int) p) => new List<(int, int)>
            { (p.Item1 + 1, p.Item2),
              (p.Item1 - 1, p.Item2),
              (p.Item1, p.Item2 + 1),
              (p.Item1, p.Item2 - 1)}
              .Where(d => d.Item1 >= 0 && d.Item1 < map.Length && d.Item2 >= 0 && d.Item2 < map[0].Length);

for (int i = 0; i < map.Length; i++)
{
    for (int j = 0; j < map[0].Length; j++)
    {
        if (map[i][j] == 'S' - 'a')
        {
            start = (i, j);
            map[i][j] = 0;
        }
        else if (map[i][j] == 'E' - 'a')
        {
            end = (i, j);
            map[i][j] = 'z' - 'a';
        }
        if (map[i][j] == 0) starts.Add((i, j));

        routes.Add((i, j), directions((i, j)).Where(d => map[d.Item1][d.Item2] <= map[i][j] + 1).ToList());
        score.Add((i, j), (int.MaxValue, int.MaxValue));
    }
}
var distanceGuess = ((int, int) n) => Math.Abs(end.Item1 - n.Item1) + Math.Abs(end.Item2 - n.Item2);

var getBestPathLength = ((int, int) start) =>
{
    foreach (var s in score) score[s.Key] = (int.MaxValue, int.MaxValue);
    var from = new Dictionary<(int, int), (int, int)>();

    var consider = new List<(int, int)>() { start };
    score[start] = (0, distanceGuess(start));

    //Do A*
    while (consider.Count() != 0)
    {
        var curr = consider.OrderBy(c => score[c].Item2).First();
        if (curr == end) // we made it
        {
            var length = 1;
            while (from.Keys.Contains(curr))
            {
                curr = from[curr];
                length++;
            }
            return length + 1;
        }
        consider.Remove(curr);
        foreach (var dir in routes[curr])
        {
            var newScore = score[curr].Item1 + 1;
            if (newScore < score[dir].Item1)
            {
                from[dir] = curr;
                score[dir] = (newScore, newScore + distanceGuess(dir));
                if (!consider.Contains(dir)) consider.Add(dir);
            }
        }
    }
    return int.MaxValue;
};
Console.WriteLine(getBestPathLength(start));
Console.WriteLine(starts.Select(s => getBestPathLength(s)).OrderBy(a => a).First());