var priority = (char c) => (c - 'a' + 1 > 0) ? (c - 'a' + 1) : (c - 'A' + 27);
//pt 1
var common = (char[] sack) => sack[00..(sack.Length / 2)]
                                .Intersect(sack[(sack.Length / 2)..])
                                .First();
Console.WriteLine(File.ReadLines(args[0])
                        .Select(line => priority(common(line.ToCharArray())))
                        .Sum());

//pt 2
var badge = (IEnumerable<char[]> group) => group.First()
                                            .Intersect(group.Skip(1).First())
                                            .Intersect(group.Skip(2).First())
                                            .First() ;
Console.WriteLine(File.ReadLines(args[0]).Chunk(3)
                        .Select(lines => priority(badge(lines.Select(l => l.ToCharArray()))))
                        .Sum()); 