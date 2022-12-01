var lines = File.ReadAllLines(args[0]).AsEnumerable();
var elves = lines.SplitBy((s) => s != "");
var maxCals = elves.Max(elf => elf.Select((s) => int.Parse(s)).Sum());

Console.WriteLine(maxCals);