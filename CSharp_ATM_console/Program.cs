using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ATM_console
{
    class Program
    {
        /*
         * ATM Cash Machine Console Application
         * C# - Andrew Harkins
         */
        static void Main(string[] args)
        {
            // variables for running program, cashBills, cashBillsAvailable.
            bool valid = true;
            int[] cashBills = { 100, 50, 20, 10, 5, 1 };
            int[] billCounts = { 10, 10, 10, 10, 10, 10 };

            while (valid)
            {
                // get user input(0,1), delimiter of $, letter choices
                string choice = Console.ReadLine();
                string letterPick = choice.Substring(0, 1).ToUpper();
                string wAmount = "";
                if(choice.Length > 2)
                {
                    wAmount = choice.Substring(choice.IndexOf("$") + 1);
                }
                if(GetUserQuits(letterPick))
                {
                    if(letterPick.Equals("W"))
                    {
                        // make copy of billCounts so validDispense() doesnt mess up values.
                        int amt = Convert.ToInt32(wAmount);
                        int amtCopy = Convert.ToInt32(wAmount);
                        int[] billsNeeded = BillsNeeded(amt);
                        int[] billsCopy = new int[6];
                        Array.Copy(billCounts, billsCopy, 6);                        

                        if (ValidDispense(amtCopy, billsCopy, billsNeeded))
                        {
                            Console.WriteLine("\nSuccess: Dispensed $" + amt + "\n");
                            Dispense(amt, billCounts);
                            DisplayATMContents(cashBills, billCounts);
                        }
                        else
                        {
                            Console.WriteLine("\nFailure: insufficient funds\n");
                        }
                    }
                    else if (letterPick.Equals("R"))
                    {
                        // Restock the billCounts values to 10 default 
                        RestockATM(billCounts);
                        DisplayATMContents(cashBills, billCounts);
                    }
                    else if (letterPick.Equals("I"))
                    {
                        // display each bill that is picked and how many
                        Inquiry(billCounts, choice);
                    }
                    else
                        Console.WriteLine("\nFailure: Invalid Command");
                } 
                else
                {
                    valid = false;
                }
                //Console.ReadLine();
            }
        }
        // Display method
        public static void DisplayATMContents(int[] bills, int[] billsCount)
        {
            Console.WriteLine("Machine balance:\n");
            for(int i = 0; i < 6; i++)
            {
                if(billsCount[i] != 0)
                {
                    Console.WriteLine("$" + bills[i] + "-" + billsCount[i] + "\n");
                }
            }
        }
        // Restock method
        public static void RestockATM( int[] cashBillsRefill)
        {
            for(int i = 0; i < cashBillsRefill.Length; i++)
            {
                cashBillsRefill[i] = 10;
            }
        }
        // Validate dispense if 'valid' can call dispense
        public static bool ValidDispense(int amount, int[] billsATMtotal, int[] billsNeeded)
        {
            int[] billsCounter = billsATMtotal;
            int[] cashBills = { 100, 50, 20, 10, 5, 1 };
            int total = amount;
            bool isValid = true;
            for (int i = 0; i < billsATMtotal.Length; i++)
            {
                if(total >= cashBills[i])
                {
                    billsCounter[i] = billsCounter[i] - billsNeeded[i];
                }
                if(billsCounter[i] < 0)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
        // Dispense method does calculations for bill
        public static void Dispense(int amount, int[] totalCash)
        {
            int[] cashBills = { 100, 50, 20, 10, 5, 1 };
            int[] billCounter = { 0, 0, 0, 0, 0, 0 };
            int[] bCounter = { 0, 0, 0, 0, 0, 0 };
            int withdraw = amount;
            if(billCounter[0] <= totalCash[0])
            {
                while(withdraw >= 100)
                {
                    int hundredsAdded = totalCash[0] * cashBills[0];
                    if(withdraw <= hundredsAdded)
                    {
                        billCounter[0] = withdraw / cashBills[0];
                        withdraw = withdraw - billCounter[0] * cashBills[0];
                        break;
                    }
                    else
                    {
                        billCounter[0] = hundredsAdded / 100;
                        withdraw = withdraw - billCounter[0] * cashBills[0];
                        break;
                    }
                }
                totalCash[0] -= billCounter[0];
            } 
            if(billCounter[1] <= totalCash[1])
            {
                while(withdraw >= 50)
                {
                    int fiftiesAdded = totalCash[1] * cashBills[1];
                    if(withdraw <= fiftiesAdded)
                    {
                        billCounter[1] = withdraw / cashBills[1];
                        withdraw = withdraw - billCounter[1] * cashBills[1];
                        break;
                    }
                    else
                    {
                        billCounter[1] = fiftiesAdded / 50;
                        withdraw = withdraw - billCounter[1] * cashBills[1];
                        break;
                    }
                }
                totalCash[1] = totalCash[1] - billCounter[1];
            }
            if(billCounter[2] <= totalCash[2])
            {
                while(withdraw >= 20)
                {
                    int twentiesAdded = totalCash[2] * cashBills[2];
                    if(withdraw <= twentiesAdded)
                    {
                        billCounter[2] = withdraw / cashBills[2];
                        withdraw = withdraw - billCounter[2] * cashBills[2];
                        break;
                    }
                    else
                    {
                        billCounter[2] = twentiesAdded / 20;
                        withdraw = withdraw - billCounter[2] * cashBills[2];
                        break;
                    }                    
                }
                totalCash[2] = totalCash[2] - billCounter[2];
            }
            if(billCounter[3] <= totalCash[3])
            {
                while(withdraw >= 10)
                {
                    int tensAdded = totalCash[3] * cashBills[3];
                    if(withdraw <= tensAdded)
                    {
                        billCounter[3] = withdraw / cashBills[3];
                        withdraw = withdraw - billCounter[3] * cashBills[3];
                        break;
                    }
                    else
                    {
                        billCounter[3] = tensAdded / 10;
                        withdraw = withdraw - billCounter[3] * cashBills[3];
                        break;
                    }
                }
                totalCash[3] = totalCash[3] - billCounter[3];
            }
            if (billCounter[4] <= totalCash[4])
            {
                while (withdraw >= 5)
                {
                    int fivesAdded = totalCash[4] * cashBills[4];
                    if (withdraw <= fivesAdded)
                    {
                        billCounter[4] = withdraw / cashBills[4];
                        withdraw = withdraw - billCounter[4] * cashBills[4];
                        break;
                    }
                    else
                    {
                        billCounter[4] = fivesAdded / 5;
                        withdraw = withdraw - billCounter[4] * cashBills[4];
                        break;
                    }
                }
                totalCash[4] = totalCash[4] - billCounter[4];
            }
            if (billCounter[5] <= totalCash[5])
            {
                while (withdraw >= 1)
                {
                    int onesAdded = totalCash[5] * cashBills[5];
                    if (withdraw <= onesAdded)
                    {
                        billCounter[5] = withdraw / cashBills[5];
                        withdraw = withdraw - billCounter[5] * cashBills[5];
                        break;
                    }
                    else
                    {
                        billCounter[5] = onesAdded / 1;
                        withdraw = withdraw - billCounter[5] * cashBills[5];
                        break;
                    }
                }
                totalCash[5] = totalCash[5] - billCounter[5];
            }
            else
            {
               // Console.WriteLine("no funds");
            }
        }
        // BillsNeeded method used to validate dispense
        public static int[] BillsNeeded(int amount)
        {
            int[] cashBills = { 100, 50, 20, 10, 5, 1 };
            int[] billCounter = { 0, 0, 0, 0, 0, 0 };
            for(int i = 0; i < cashBills.Length; i++)
            {
                billCounter[i] = amount / cashBills[i];
                amount = amount - (billCounter[i] * cashBills[i]);
            }
            return billCounter;
        }
        // inquery method determines what bill choices and displays them
        public static void Inquiry(int[] billsInATM, string query)
        {
            int[] cashBills = { 100, 50, 20, 10, 5, 1 };
            int[] billCounter = billsInATM;
            string phrase = query;
            char delimiter = '$';
            string[] str_nums = phrase.Split(delimiter);
            foreach(var str in str_nums)
            {
                if (str.Equals("1"))
                {
                    Console.WriteLine("$1 - " + billCounter[5] + "\n");
                }
                else if (str.Equals("5"))
                {
                    Console.WriteLine("$5 - " + billCounter[4] + "\n");
                }
                else if (str.Equals("10"))
                {
                    Console.WriteLine("$10 - " + billCounter[3] + "\n");
                }
                else if (str.Equals("20"))
                {
                    Console.WriteLine("$20 - " + billCounter[2] + "\n");
                }
                else if (str.Equals("50"))
                {
                    Console.WriteLine("$50 - " + billCounter[1] + "\n");
                }
                else if (str.Equals("100"))
                {
                    Console.WriteLine("$100 - " + billCounter[0] + "\n");
                }
                else
                {

                }
            }
        }
        // Quit console application method.
        public static bool GetUserQuits(string input)
        {
            bool isValid = true;
            if (input.Equals("Q"))
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
