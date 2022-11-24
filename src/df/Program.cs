using df;

var suffixes = new[] { "B", "K", "M", "G", "T", "P" };

var options = ProgramOptions.From(args);

var step = options.StandardInternationalUnits
    ? 1000
    : 1024;
Func<long, string> sizeFormatter = options.HumanReadable
    ? HumanReadableSize
    : PassThroughSize;

var collected = options.Verbose
    ? new List<string[]>
    {
        new[] { "Drive", "Size", "Used", "Avail", "Use%", "Label" }
    }
    : new List<string[]>
    {
        new[] { "Drive", "Size", "Used", "Avail", "Use%" }
    };

foreach (var drive in DriveInfo.GetDrives())
{
    collected.Add(RowFor(drive));
}

string[] RowFor(DriveInfo drive)
{
    var used = drive.TotalSize - drive.AvailableFreeSpace;
    var result = new List<string>
    {
        drive.Name,
        sizeFormatter(drive.TotalSize),
        sizeFormatter(used),
        sizeFormatter(drive.AvailableFreeSpace),
        $"{Math.Round((used * 100) / (decimal) drive.TotalSize)}%"
    };
    if (options!.Verbose)
    {
        result.Add(drive.VolumeLabel);
    }
    return result.ToArray();
}

var colwidths = new List<int>();
for (var i = 0; i < collected[0].Length; i++)
{
    colwidths.Add(0);
}

foreach (var item in collected)
{
    for (var i = 0; i < colwidths.Count; i++)
    {
        colwidths[i] = Math.Max(item[i].Length, colwidths[i]);
    }
}

foreach (var item in collected)
{
    Console.WriteLine(FormatLine(item, colwidths));
}

// -- helpers

string FormatLine(string[] fields, List<int> widths)
{
    var parts = new List<string>();
    for (var i = 0; i < fields.Length; i++)
    {
        parts.Add(RightPad(fields[i], widths[i]));
    }

    return string.Join("  ", parts);
}

string RightPad(string str, int required)
{
    if (str.Length >= required)
    {
        return str;
    }

    var toAdd = new string(' ', required - str.Length);
    return $"{str}{toAdd}";
}


string PassThroughSize(long value)
{
    return $"{value}";
}

string HumanReadableSize(long value)
{
    var dec = (decimal) value;
    var idx = 0;
    while (dec > step && idx < suffixes.Length - 1)
    {
        idx++;
        dec /= step;
    }

    return $"{dec:0.0}{suffixes[idx]}";
}