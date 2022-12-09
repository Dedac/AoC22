internal class Day9
{
    private static void Day(string[] args)
    {
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

        var distance = (int length) =>
        {
            var rope = new (int, int)[length];
            var visits = File.ReadLines(args[0]).Select(l => l.Split(' '))
                .SelectMany(l => Enumerable.Repeat(l[0], int.Parse(l[1])))
                .Aggregate(new List<(int, int)>(), (vis, dir) =>
            {
                rope[0] = move[dir](rope[0]);
                for (int i = 1; i < length; i++) rope[i] = follow(rope[i - 1], rope[i]);
                if (!vis.Contains(rope.Last())) vis.Add(rope.Last());
                return vis;
            });
            return visits.Count();
        };

        Console.WriteLine(distance(2));
        Console.WriteLine(distance(10));
    }
}