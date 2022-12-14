var grid = new int[1000, 1000];
var coords = File.ReadLines(args[0])
                .Select(l => l.Split(" -> ")
                    .Select(s => (int.Parse(s.Split(",").First()), int.Parse(s.Split(',').Last()))).ToArray());
foreach (var r in coords)
{
    for (int i = 0; i < r.Length - 1; i++)
    {
        if (r[i].Item1 != r[i + 1].Item1)
        {
            var start = Math.Min(r[i].Item1, r[i + 1].Item1);
            var end = Math.Max(r[i].Item1, r[i + 1].Item1);
            var y = r[i].Item2;
            for (int x = start; x <= end; x++)
                grid[x, y] = 1;
        }
        else if (r[i].Item2 != r[i + 1].Item2)
        {
            var start = Math.Min(r[i].Item2, r[i + 1].Item2);
            var end = Math.Max(r[i].Item2, r[i + 1].Item2);
            var x = r[i].Item1;
            for (int y = start; y <= end; y++)
                grid[x, y] = 1;
        }
    }
}

for (int i = 0; i < 10; i++)
{
    for (int j = 494; j < 504; j++)
        Console.Write(grid[j, i]);
    Console.WriteLine();
}
