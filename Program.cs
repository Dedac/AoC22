var lines = File.ReadLines(args[0]);
var sum = 0;
foreach (var line in lines)
{
    var g = line.Split(" ");
    if (g[1] == "X") {
        if (g[0] == "A") sum += 3;
        else if (g[0] == "B") sum += 1;
        else sum += 2; //C
    }
    else if (g[1] == "Y") { 
        sum += 3 + g[0].First() - 64; // Yay char math!
    }
    else { //"Z"
        if (g[0] == "A") sum += 2;
        else if (g[0] == "B") sum += 3;
        else sum += 1; //C
        sum += 6;
    }
}
Console.WriteLine(sum);