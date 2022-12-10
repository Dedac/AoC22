var cycle = 0;
var incCycle = ((int,int) acc) => 
{
    if (cycle % 40 == 0) Console.WriteLine();
    Console.Write((Enumerable.Range(acc.Item1-1,3)).Contains(cycle % 40) ? '#' : '.');
    cycle++;
    if ((cycle-20) % 40 == 0) acc.Item2 += acc.Item1*cycle;
    return acc;
};
var val = File.ReadLines(args[0]).Select(l => l.Split(" "))
        .Aggregate((1,0), (acc, line) => {
    acc = incCycle(acc);
    if (line[0]=="addx")
    {
        acc = incCycle(acc);
        acc.Item1 += int.Parse(line[1]);
    }
    return acc;
});
Console.WriteLine(val.Item2);