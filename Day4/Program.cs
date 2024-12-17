const short GRID_SIZE = 140;

var d4Data = File.ReadAllLines("d4_data.txt");
var dataGrid = new char[GRID_SIZE,GRID_SIZE];

for (int i = 0; i < GRID_SIZE; i++)
for (int j = 0; j < GRID_SIZE; j++)
	dataGrid[i, j] = d4Data[i][j];

var p1Result = FindCrossWord("XMAS", dataGrid);

Console.WriteLine(p1Result);

int FindCrossWord(string word, char[,] puzzle)
{
	var found = 0;
	var wordLength = word.Length;
	
	for (int i = 0; i < GRID_SIZE; i++)
	for (int j = 0; j < GRID_SIZE; j++)
	{
		var searchNorth = i >= wordLength - 1;
		var searchSouth = i <= GRID_SIZE - wordLength;
		var searchWest = j >= wordLength - 1;
		var searchEast = j <= GRID_SIZE - wordLength;
		var searchNorthWest = searchNorth && searchWest;
		var searchNorthEast = searchNorth && searchEast;
		var searchSouthWest = searchSouth && searchWest;
		var searchSouthEast = searchSouth && searchEast;

		if (searchNorth)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i - k, j];
			}

			found += word == text ? 1 : 0;
		}

		if (searchSouth)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i + k, j];
			}

			found += word == text ? 1 : 0;
		}

		if (searchEast)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i, j + k];
			}

			found += word == text ? 1 : 0;
		}

		if (searchWest)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i, j - k];
			}

			found += word == text ? 1 : 0;
		}

		if (searchNorthWest)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i - k, j - k];
			}

			found += word == text ? 1 : 0;
		}

		if (searchNorthEast)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i - k, j + k];
			}

			found += word == text ? 1 : 0;
		}
		
		if (searchSouthWest)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i + k, j - k];
			}

			found += word == text ? 1 : 0;
		}

		if (searchSouthEast)
		{
			var text = "";

			for (int k = 0; k < wordLength; k++)
			{
				text += puzzle[i + k, j + k];
			}

			found += word == text ? 1 : 0;
		}
	}
	
	return found;
}
