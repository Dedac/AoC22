var rooms = File.ReadLines(args[0])
    .Select(s =>
    {
        var p = s.Split(' ');
        return new
        {
            name = p[1],
            rate = int.Parse(p[4].Split(new char[] { '=', ';' })[1]),
            tunnels = p[9..].Select(x => x.Split(",")[0])
        };
    }).ToDictionary((r) => r.name);

var score = new Dictionary<(string, bool, int), (int, int)>();
foreach (var r in rooms)
{
    for (int m = 0; m < 30; m++)
    {
        score[(r.Key, false, m)] = (-1, -1);
        score[(r.Key, true, m)] = (-1, -1);
    }
}

//TODO: heuristic function should be all unopened valves at 2 minutes per?
var distanceGuess = (string s) => 0;

var from = new Dictionary<(string, bool, int), (string, bool, int)>();
var consider = new List<(string, bool, int)>() { ("AA", false, 30) };
score[("AA", false, 30)] = (0, distanceGuess("AA"));
var path = new List<(string, bool, int)>();

while (consider.Count() != 0)
{
    var curr = consider.OrderByDescending(c => score[c].Item2).First();
    
    path = new List<(string, bool, int)>();
    consider.Remove(curr);
    if (curr.Item3 == 0) continue;
    var pather = curr;
    path.Add(curr);
    while (curr != ("AA", false, 30) && from[path.LastOrDefault()] != ("AA", false, 30))
    {
        pather = from[pather];
        path.Add(pather);
    }
    foreach (var tunnel in rooms[curr.Item1].tunnels)
    {
        var dirs = new List<(string, bool, int)> { (tunnel, false, curr.Item3 - 1) };
        if (rooms[tunnel].rate > 0 && curr.Item3 > 1 &&
         !path.Any(p => p.Item1 == tunnel && p.Item2)) 
            dirs.Add((tunnel, true, curr.Item3 - 2));
        foreach (var dir in dirs)
        {
            var newScore = score[curr].Item1 + (dir.Item2 ? rooms[dir.Item1].rate * dir.Item3 : 0);
            if (newScore > score[dir].Item1)
            {
                from[dir] = curr;
                score[dir] = (newScore, newScore + distanceGuess(tunnel));
                if (!consider.Contains(dir)) consider.Add(dir);
            }
        }
    }
}
var lastStop = score.Aggregate(score.First(),(m, s) => m = s.Value.Item1 > m.Value.Item1 ? s : m );

var finalPath = new List<KeyValuePair<(string, bool, int), (int, int)>>();
finalPath.Add(lastStop);
while (from[finalPath.LastOrDefault().Key] != ("AA", false, 30))
    {
        lastStop = new (from[lastStop.Key], score[from[lastStop.Key]]);
        finalPath.Add(lastStop);
    }
Console.WriteLine(lastStop.Value.Item1);