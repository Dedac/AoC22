var getIndex = (int size, StreamReader signal) =>
{
    var buff = new char[size];
    for (int i = 0; true; i++)
    {
        buff[i % size] = (char)signal.Read();
        if (i > size && buff.Distinct().Count() == size)
            return i + 1;
    }
};
Console.WriteLine(getIndex(4, File.OpenText(args[0])));
Console.WriteLine(getIndex(14, File.OpenText(args[0])));