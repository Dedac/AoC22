var size = 4000001;
var cleared = new bool[size];
var beacons = File.ReadLines(args[0])
    .Select(s => s.Split('=').Skip(1)
        .Select(p => int.Parse(new string(p.TakeWhile(c => char.IsDigit(c) || c == '-')
        .ToArray()))).ToArray()).ToArray();

int row = 0;
int col = 0;
var flipper = true;
for (row = 0; row < size; row++)
{
    for (int x = 0; x< beacons.Length; x++) 
    {
        var coords = beacons[x];
        var distance = Math.Abs(coords[0] - coords[2]) + Math.Abs(coords[1] - coords[3]);
        var distOnRow = distance - Math.Abs(coords[1] - row);
        var start = Math.Max(coords[0] - distOnRow, 0);
        var end = Math.Min(start + distOnRow * 2 + 1, size);
        for (col = start; col < end; col++)
            cleared[col] = flipper;
    }
    if (!cleared.All(c => flipper == c)) break;
    flipper = !flipper;
}
var i = 0;
while (cleared[i] == flipper && i<size) i++;
Console.WriteLine(i * 4000000 + row);