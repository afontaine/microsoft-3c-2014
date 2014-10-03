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
		static string[] input_arr = null;
		static int inputIndex = 0;

		static Dictionary<String, InventoryItem> s_inventory = new Dictionary<string,InventoryItem>();
		static List<ChangeLot> s_change = new List<ChangeLot>();
		static List<String> s_actions = new List<string>();

		static string GetInputline()
		{
			var res = input_arr[inputIndex];
			++inputIndex;
			return res;
		}

		static void BackUpOneInputLine()
		{
			--inputIndex;
		}

		static bool hasMoreInput()
		{
			return inputIndex < input_arr.Length;
		}

		static void AddInventoryItem(string itemStr)
		{
			var props = itemStr.Split(',');
			var item = new InventoryItem();
			item.Id = props[0];
			item.Name = props[1];
			item.Cost = float.Parse(props[2]);
			item.Quantity = int.Parse(props[3]);

			s_inventory[item.Id] = item;
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

		static float s_changeBox = 0;
		static void DoActions()
		{
			bool hasNumber = false;
			bool hasLetter = false;

			int curNumber = -1;
			char curLetter = 'x';

			float curMoney = 0.0F;

			foreach(var action in s_actions)
			{
				if(action[0] == '#')
				{
					// refund
					curMoney = 0.0F;
					hasNumber = false;
					hasLetter = false;
				}
				else if(action.Length == 1)
				{
					// its either a number or a letter
					int inputNum = 0;
					if(int.TryParse(action, out inputNum))
					{
						// it was a number
						curNumber = inputNum;
						hasNumber = true;
					}
					else
					{
						// it was a letter
						curLetter = action[0];
						hasLetter = true;
						hasNumber = false;
					}
				}
				else if(action[0] == '$')
				{
					// they put in money
					float moneyInput = float.Parse(action.Substring(1));
					curMoney += moneyInput;
				}
				// ignoring the else case for now

				if(hasNumber && hasLetter)
				{
					// button was pushed
					string id = curLetter.ToString() + curNumber.ToString();
					var item = s_inventory[id];
					if(item != null)
					{
						if(item.Quantity > 0 && item.Cost <= curMoney)
						{
							// buy the item
							item.Quantity--;


							hasNumber = false;
							hasLetter = false;

						}
					}
					
				}
			}
		}

		static void OutputResults()
		{
			// TODO::JT
		}

		static void Main(string[] args)
		{
			// bleh, bad style...
			input_arr = System.IO.File.ReadAllLines(@"C:\input.txt");
			ReadInput();
			DoActions();
			OutputResults();
		}
	}
}
