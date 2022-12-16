internal class Day15
{
    private static void Day(string[] args)
    {
        var size = 4000001;
        var beacons = File.ReadLines(args[0])
            .Select(s => s.Split('=').Skip(1)
                .Select(p => int.Parse(new string(p.TakeWhile(c => char.IsDigit(c) || c == '-')
                .ToArray()))).ToArray()).ToArray();

        int row = 0;
        var distress = -1;
        for (row = 0; row < size; row++)
        {
            var cleared = new List<(int, int)>();
            for (int x = 0; x < beacons.Length; x++)
            {
                var coords = beacons[x];
                var distOnRow = Math.Abs(coords[0] - coords[2]) + Math.Abs(coords[1] - coords[3]) - Math.Abs(coords[1] - row);
                if (distOnRow >= 0)
                {
                    var start = Math.Max(coords[0] - distOnRow, 0);
                    var end = Math.Min(start + distOnRow * 2, size);
                    cleared.Add((start, end));
                }
            }
            foreach (var range in cleared)
            {
                if (!cleared.Any(c => range.Item1 - 1 >= c.Item1 && range.Item1 - 1 < c.Item2))
                    distress = range.Item1 - 1;
            }
            if (distress != -1) break;
        }
        Console.WriteLine(distress * 4000000L + row);
    }
}