using System.Text.RegularExpressions;

var p1Data = File.ReadAllText("d3_data.txt");
var p1Result = Part1(p1Data);
var p2Result = Part2(p1Data);

Console.WriteLine(p1Result);
Console.WriteLine(p2Result);

long Part1(string input)
{
	long result = 0;
	
	var regex = new Regex(@"mul\(\d+,\d+\)");

	var matches = regex.Matches(input);
	
	foreach (Match match in matches)
	{
		var numbers = match.Value
			.Replace("mul(", "")
			.Replace(")", "")
			.Split(",")
			.Select(t => long.TryParse(t, out var n) ? n : 1)
			.ToArray();

		result = result + (numbers[0] * numbers[1]);
	}
	
	return result;
}

long Part2(string input)
{
	long result = 0;
	
	var regex = new Regex(@"mul\(\d+,\d+\)");
	var doRegex = new Regex(@"do\(\)");
	var dontRegex = new Regex(@"don't\(\)");

	var matches = regex.Matches(input);
	var doIndexes = doRegex.Matches(input).Select(m => m.Index).ToList();
	doIndexes = doIndexes.Prepend(0).ToList();
	var dontIndexes = dontRegex.Matches(input).Select(m => m.Index).ToList();
	
	foreach (Match match in matches)
	{
		var index = match.Index;
		var numbers = match.Value
			.Replace("mul(", "")
			.Replace(")", "")
			.Split(",")
			.Select(t => long.TryParse(t, out var n) ? n : 1)
			.ToArray();

		if (CanDo(index, doIndexes, dontIndexes))
			result = result + (numbers[0] * numbers[1]);
	}
	
	return result;
}

bool CanDo(int multIndex, List<int> doIndexes, List<int> dontIndexes)
{
	var validDo = doIndexes
		.Where(i => i < multIndex)
		.OrderDescending()
		.FirstOrDefault();
	
	var validDont = dontIndexes
		.Where(i => i < multIndex)
		.OrderDescending()
		.FirstOrDefault();

	return validDo >= validDont;
}