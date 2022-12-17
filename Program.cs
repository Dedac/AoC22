var rooms = File.ReadLines(args[0])
    .Select(s =>
    {
        var p = s.Split(' ');
        return new
        {
            name = p[1],
            rate = int.Parse(p[4].Split(new char[] { '=', ';' })[1]),
            tunnels = p[9..].Select(x => x.Split(",")[0]).ToArray()
        };
    }).ToDictionary((r) => r.name);

var score = new Dictionary<(string, bool), (int, int)>();
foreach (var r in rooms)
{
    score[(r.Key, false)] = (-1,-1);
    score[(r.Key, true)] = (-1,-1);
}

//TODO: heuristic function should be all unopened valves at 2 minutes per?
var distanceGuess = (string s) => 0;

var from = new Dictionary<(string, bool), (string, bool)>();
var consider = new List<(string, bool)>() { ("AA", false) };
score[("AA", false)] = (0, distanceGuess("AA"));
var path = new List<(string, bool)>();
//Do A*
while (consider.Count() != 0)
{
    var curr = consider.OrderBy(c => score[c].Item2).First();
    if (from.Sum(f => f.Value.Item2 ? 2 : 1) == 30)  //TODO: fix end condition?
    {
        while (from.Keys.Contains(curr))
        {
            path.Add(curr);
            curr = from[curr];
        }
        break;
    }
    consider.Remove(curr);
    foreach (var tunnel in rooms[curr.Item1].tunnels)
    {
        var dirs = new List<(string, bool)> { (tunnel, false) };
        if (rooms[tunnel].rate > 0) dirs.Add((tunnel, true));
        foreach (var dir in dirs)
        {
            //Score should be rate added * time remaining over time taken?
            var newScore = score[curr].Item1 + 1;
            if (newScore > score[dir].Item1)
            {
                from[dir] = curr; 
                score[dir] = (newScore, newScore + distanceGuess(tunnel));
                if (!consider.Contains(dir)) consider.Add(dir);
            }
        }
    }
}
var i = path.GetEnumerator();
var totalFlow = 0;
for (int t = 30; t == 0; t--)
{
    //TODO bug on when the flow starts? (t-1?)
    i.MoveNext();
    if (i.Current.Item2) totalFlow += rooms[i.Current.Item1].rate * t;
}
Console.WriteLine(totalFlow);