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

var score = new Dictionary<Visit, int>();

//TODO: heuristic function should be all unopened valves in value order at 2 minutes per?
//Function required to be lower is better for priority queue
var distanceGuess = (string s) => 0;

var from = new Dictionary<Visit, Visit>();
var consider = new PriorityQueue<Visit, int>();
consider.Enqueue(new("AA", false, 30, ""), distanceGuess(""));

score[new("AA", false, 30, "")] = 0;

while (consider.Count != 0)
{
    var curr = consider.Dequeue();
    if (curr.timeRemaining == 0) continue;
    
    foreach (var tunnel in rooms[curr.name].tunnels)
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
                //Enque should be current score - guess?
                if (!consider.UnorderedItems.Any(ci => ci.Element == dir)) consider.Enqueue(dir, distanceGuess(dir.openedValves));
            }
        }
    }
}
var lastStop = score.Aggregate(score.First(),(m, s) => m = s.Value > m.Value ? s : m );

Console.WriteLine(lastStop.Value);

record Visit ( string name, bool open, int timeRemaining, string openedValves);