using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeadCode
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] lines = System.IO.File.ReadAllLines(@"C:\input.txt");
			bool[] linesTouched = new bool[lines.Length];

			var index = 0;

			while(index < lines.Length)
			{
				if(linesTouched[index])
				{
					// we've been here before, in a loop, terminate
					break;
				}
				else
				{
					linesTouched[index] = true;

					if(lines[index].Equals("NEXT"))
					{
						++index;
					}
					else
					{
						// I would prefer a Regex here... no time...
						var res = lines[index].Split(' ');
						index = int.Parse(res[1].Trim()) - 1;
					}
				}
			}

			for(int i = 0 ; i < linesTouched.Length; ++i)
			{
				if (!linesTouched[i])
				{
					Console.WriteLine(i + 1);
				}
			}
		}
	}
}
