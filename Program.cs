var rooms = File.ReadLines(args[0])
    .Select(s =>
    {
        var p = s.Split(' ');
        return new
        {
            name = p[1],
            rate = int.Parse(p[4].Split(new char[] { '=', ';' })[1]),
            tunnels = p[9..].Select(x => x.Split(",")[0]).ToList()
        };
    }).ToDictionary((r) => r.name);

var score = new Dictionary<Visit, int>();
var from = new Dictionary<Visit, Visit>();
var consider = new List<Visit>();
consider.Add(new("AA", false, 30, ""));

score[new("AA", false, 30, "")] = 0;

while (consider.Count != 0)
{
    var curr = consider.Last();
    consider.Remove(curr);

    //out of time
    if (curr.timeRemaining == 0) continue;
    //can't open more valves
    if (curr.openedValves.Split(',').Count() > rooms.Count(s => s.Value.rate > 0)) continue;

    //Don't add tunnels that take us immediately backward
    foreach (var tunnel in rooms[curr.name].tunnels.Where(t => t != curr.name))
    {
        var dirs = new List<Visit> { new(tunnel, false, curr.timeRemaining - 1, curr.openedValves) };
        if (rooms[tunnel].rate > 0 && curr.timeRemaining > 1 &&
         !curr.openedValves.Split(',').Any(p => p == tunnel))
            dirs.Add(new(tunnel, true, curr.timeRemaining - 2, curr.openedValves + "," + tunnel));

        foreach (var dir in dirs)
        {
            var newScore = score[curr] + (dir.open ? rooms[dir.name].rate * dir.timeRemaining : 0);

            if (!score.ContainsKey(dir) || newScore > score[dir])
            {
                from[dir] = curr;
                score[dir] = newScore;
                if (!consider.Contains(dir)) consider.Add(dir);
            }
        }
    }
}
var lastStop = score.Aggregate(score.First(), (m, s) => m = s.Value > m.Value ? s : m);

Console.WriteLine(lastStop.Value);

record Visit(string name, bool open, int timeRemaining, string openedValves);