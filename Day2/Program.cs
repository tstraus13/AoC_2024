// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
var input = File.ReadAllLines("input.txt");

var result = 0;

foreach (var line in input)
{
	var numbers = line
		.Split(" ", StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse)
		.ToList();

	var boolBag = new ConcurrentBag<bool>();

	Parallel.For(
		0, numbers.Count, (i, state) =>
		{
			var numsCopy = new List<int>();

			foreach (var number in numbers)
				numsCopy.Add(number);
			
			numsCopy.RemoveAt(i);
			
			boolBag.Add(SafeNumbers(numsCopy));
		}
	);

	if (boolBag.Any(i => i))
		result++;
}

Console.WriteLine(result);

bool SafeNumbers(List<int> numbers)
{
	var direction = 0;
	
	for (int i = 0; i < numbers.Count; i++)
	{
		if (i == 0)
		{
			var diff = numbers[i] - numbers[i + 1];
			var diffAbs = Math.Abs(diff);
			var safeRange = diffAbs >= 1 && diffAbs <= 3;

			if (diff > 0 && safeRange)
				direction = -1;
			else if (diff < 0 && safeRange)
				direction = 1;
			else
				return false;
			
			i++;
		}
		else if (i == numbers.Count - 1)
		{
			break;
		}
		else
		{
			var last = numbers[i - 1];
			var current = numbers[i];
			var next = numbers[i + 1];

			var lastDiffAbs = Math.Abs(last - current);
			var nextDiffAbs = Math.Abs(current - next);

			if (lastDiffAbs < 1 || lastDiffAbs > 3 || nextDiffAbs < 1 || nextDiffAbs > 3)
				return false;

			if (direction > 0 && !(last < current && current < next))
				return false;
			
			if (direction < 0 && !(last > current && current > next))
				return false;
		}
	}

	return true;
}