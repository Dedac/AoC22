var grid = new int[1000, 1000];
var coords = File.ReadLines(args[0])
                .Select(l => l.Split(" -> ")
                    .Select(s => (int.Parse(s.Split(",").First()), int.Parse(s.Split(',').Last()))).ToArray());
var rockBottom = 0;
foreach (var r in coords)
{
    for (int i = 0; i < r.Length - 1; i++)
    {
        if (r[i].Item1 != r[i + 1].Item1)
        {
            var start = Math.Min(r[i].Item1, r[i + 1].Item1);
            var end = Math.Max(r[i].Item1, r[i + 1].Item1);
            for (int x = start; x <= end; x++)
                grid[x, r[i].Item2] = 1;
        }
        else
        {
            var start = Math.Min(r[i].Item2, r[i + 1].Item2);
            var end = Math.Max(r[i].Item2, r[i + 1].Item2);
            for (int y = start; y <= end; y++)
                grid[r[i].Item1, y] = 1;
        }
            rockBottom = Math.Max(rockBottom, r[i].Item2 + 2);
    }
}

int grains = 0;
var lastGrain = false;
while (!lastGrain)
{
    grains++;
    var x = 500;
    var y = 0;
    while (true)
    {
        if (y >= rockBottom)
        {
            lastGrain = true;
            break;
        }
        else if (grid[x, y + 1] == 0 && y+1 != rockBottom)
        {
            y += 1;
        }
        else if (grid[x - 1, y + 1] == 0 && y+1 != rockBottom)
        {
            x -= 1;
            y += 1;
        }
        else if (grid[x + 1, y + 1] == 0 && y+1 != rockBottom)
        {
            x += 1;
            y += 1;
        }
        else
        {
            grid[x, y] = 2;
            if (x == 500 && y == 0) lastGrain = true;
            break;
        }
    }
}

Console.WriteLine(grains);
for (int i = 0; i <= rockBottom+3; i++)
{
    for (int j = 480; j < 650; j++)
        Console.Write( grid[j, i] == 0 ? ' ' : grid[j,i] == 1 ? 'R' : 'o');
    Console.WriteLine();
}
