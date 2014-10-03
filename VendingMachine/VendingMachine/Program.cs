using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
	class InventoryItem
	{
		public string Id {get;set;}
		public string Name {get;set;}
		public float Cost {get; set;}
		public int Quantity {get; set;}
	}

	class ChangeLot
	{
		public string ChangeType { get; set; }
		public float Value { get; set; }
		public int Quantity { get; set; }
	}

	class Program
	{
		static string[] input = null;
		static int inputIndex = 0;

		static Dictionary<String, InventoryItem> s_inventory = new Dictionary<string,InventoryItem>();
		static List<ChangeLot> s_change = new List<ChangeLot>();
		static List<String> s_actions = new List<string>();

		static string GetInputline()
		{
			var res = input[inputIndex];
			++inputIndex;
			return res;
		}

		static void BackUpOneInputLine()
		{
			--inputIndex;
		}

		static bool hasMoreInput()
		{
			return inputIndex <= input.Length;
		}

		static void AddInventoryItem(string itemStr)
		{
			var props = itemStr.Split(',');
			var item = new InventoryItem();
			item.Id = props[0];
			item.Name = props[0];
			item.Cost = float.Parse(props[0]);
			item.Quantity = int.Parse(props[0]);
		}

		static void AddChangeLot(string item)
		{
			var props = item.Split(',');
			var lot = new ChangeLot();
			lot.ChangeType = props[0];
			lot.Value = float.Parse(props[1]);
			lot.Quantity = int.Parse(props[2]);

			s_change.Add(lot);
		}

		static void AddAction(String input)
		{
			s_actions.AddRange(input.Split(','));
		}

		static void ReadInput()
		{
			bool foundChangeLot = false;
			while(hasMoreInput())
			{
				var input = GetInputline();
				if(!foundChangeLot)
				{
					// either change lot or inventory item
					if(3 == 
						(from c in input
						where c == ','
						select c).Count())
					{
						// its an inventory item
						AddInventoryItem(input);
					}
					else
					{
						// its a change lot item
						foundChangeLot = true;
						AddChangeLot(input);
					}
				}
				else
				{
					// it can be either change lot or action
					if(input[0] == '$')
					{
						// its a change lot
						AddChangeLot(input);
					}
					else
					{
						// its an action
						AddAction(input);
					}
				}
			}
		}

		static void DoActions()
		{
			foreach(var action in s_actions)
			{
				// TODO::jT do action
			}
		}

		static void OutputResults()
		{
			// TODO::JT
		}

		static void Main(string[] args)
		{
			// bleh, bad style...
			input = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
			ReadInput();
			DoActions();

		}
	}
}
