var area = Enumerable.Range(0, 21);
var cleared = area.Select(s => area).ToArray();

var distress = File.ReadLines(args[0])
    .Select(s => s.Split('=').Skip(1)
        .Select(p => int.Parse(new string(p.TakeWhile(c => char.IsDigit(c) || c == '-').ToArray()))).ToArray())
    .Select(coords =>
        area.Aggregate((0,0),(c, row) =>
        {
            var distOnRow = Math.Abs(coords[0] - coords[2]) + Math.Abs(coords[1] - coords[3]) - Math.Abs(coords[1] - row);
            if (distOnRow >= 0)
                cleared[row] = cleared[row].Except(Enumerable.Range(coords[0] - distOnRow, distOnRow * 2 + 1));
            if (cleared[row].Count() == 1) 
                c = (row,cleared[row].First());
            return c;
        })
    ).Last();

Console.WriteLine(distress.Item2 * 4000000 + distress.Item1);