var range = (string s) => { 
    var n = s.Split('-').Select(a => int.Parse(a)).ToArray();
    return Enumerable.Range(n[0], n[1] - n[0] + 1); 
    };

//pt1
var contained = (string[] sectionPair) => {
    var ranges = sectionPair.Select(p => range(p)).ToArray();
    var i = ranges[0].Intersect(ranges[1]);
    return i.SequenceEqual(ranges[0]) || i.SequenceEqual(ranges[1]);
};

//pt2 
var overlaps = (string[] sectionPair) => {
    var ranges = sectionPair.Select(p => range(p)).ToArray();
    return ranges[0].Intersect(ranges[1]).Any();
};

Console.WriteLine(
    File.ReadLines(args[0]).Select(s => overlaps(s.Split(",")) ? 1:0).Sum()
);