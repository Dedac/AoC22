internal class Day3
{
    private static void Day(string[] args)
    {
        int sum = 0;
        int sumBadges = 0;
        var priority = (char c) => (c - 'a' + 1 > 0) ? 
                                        (c - 'a' + 1) 
                                      : (c - 'A' + 27);

        var lines = File.ReadLines(args[0])
                        .Select(l => l.ToCharArray())
                        .ToArray();
        for (int i = 0; i < lines.Length; i++)
        {
            sum += priority(lines[i][00..(lines[i].Length / 2)]
                    .Intersect(lines[i][(lines[i].Length / 2)..])
                    .First());
            if (i % 3 == 2)
                sumBadges += priority(lines[i]
                    .Intersect(lines[i - 1])
                    .Intersect(lines[i - 2])
                    .First());
        }
        Console.WriteLine(sum);
        Console.WriteLine(sumBadges);
    }
}