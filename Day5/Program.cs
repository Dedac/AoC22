internal class Day5
{
    private static void Day(string[] args)
    {
        var boxRows = File.ReadLines(args[0])
                .TakeWhile(s => !s.StartsWith(" "))
                .Select(s => s.Chunk(4).Select(s => s[1]).ToArray());

        var stacks = new string[boxRows.Last().Length];
        for (int i = 0; i < boxRows.Last().Length; i++)
            stacks[i] = new string(boxRows.Select(b => b[i]).ToArray()).Trim();

        File.ReadLines(args[0]).SkipWhile(s => !s.StartsWith("move"))
                .Select(s => s.Split(' ').Chunk(2).Select(w => int.Parse(w[1]) - 1).ToArray())
                .All(m =>
                {
                    var boxes = stacks[m[1]].Substring(0, m[0] + 1);
                    stacks[m[1]] = stacks[m[1]].Substring(m[0] + 1);
                    stacks[m[2]] = boxes + stacks[m[2]];
                    return true;
                });

        Console.WriteLine(string.Join('\n', stacks));
    }
}