using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;

//===================================================================
//              Not including Table of Contents
//NOTE*: Dark red based text: used for Title and admin menu.
//NOTE*: Cyan based text: used for item names and prices.
//NOTE*: Yellow based text: used for User input.
//NOTE*: Red based text: used for error messages.
//NOTE*: Green based text: used for success messages.
//NOTE*: White based text: used for normal text.
//===================================================================
namespace NRT_Vending_Machine
{
    internal class Program
    {
        // public struct is used to hold the details for each vending machine item.
        public struct VendingItem
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public int InitialStock { get; set; } // Used to restock the machine.
        }

       
        // A list to hold all vending machine items.
        private static readonly List<VendingItem> VendingMachineItems = new List<VendingItem>();

        //Admin password
        private const string AdminPassword = "NRT_Owner1738";
        private const decimal MAX_MONEY_INPUT = 250.00m; // Maximum ammount  of money that can be inserted

        // Track total revenue from purchases
        private static decimal totalRevenue = 0;

        static void Main(string[] args)
        {
            Console.Title = "NRT Vending Machine";
            PopulateVendingMachineData(); 
            ShowMainMenu();
        }

        static void PopulateVendingMachineData()
        {
            VendingMachineItems.Clear();
            VendingMachineItems.AddRange(new VendingItem[]
            {
                  new VendingItem { Category = "Snacks", Name = "Lays", Price = 15.00m, Stock = 8, InitialStock = 8 },
                new VendingItem { Category = "Snacks", Name = "Doritos", Price = 16.99m, Stock = 6, InitialStock = 6 },
                new VendingItem { Category = "Snacks", Name = "Chocolates", Price = 22.00m, Stock = 5, InitialStock = 5 },
                new VendingItem { Category = "Snacks", Name = "Nuts", Price = 13.50m, Stock = 3, InitialStock = 3 },
                new VendingItem { Category = "Drinks", Name = "Water", Price = 10.00m, Stock = 10, InitialStock = 10 },
                new VendingItem { Category = "Drinks", Name = "Energy Drink", Price = 23.00m, Stock = 1, InitialStock = 5 },
                new VendingItem { Category = "Drinks", Name = "Juice", Price = 15.00m, Stock = 4, InitialStock = 6 },
                new VendingItem { Category = "Drinks", Name = "Cold Drinks", Price = 12.50m, Stock = 11, InitialStock = 12 },
                new VendingItem { Category = "Drinks", Name = "Sports Drink", Price = 22.00m, Stock = 7, InitialStock = 7 },
                new VendingItem { Category = "Drinks", Name = "Tea", Price = 13.00m, Stock = 6, InitialStock = 8 }
            });
        }

        // ===================================================================
        // DISPLAY METHODS
        // ===================================================================

        static void DrawHeader()
        {
            AnsiConsole.Clear();
            var header = new FigletText("NRT Vending Machine").Centered().Color(Color.Red);
            AnsiConsole.Write(header);
            AnsiConsole.Write(new Rule().RuleStyle(new Style(Color.Grey19)).Centered());
            AnsiConsole.Write("\n");
            DisplayItemsTable();
            AnsiConsole.Write(new Rule().RuleStyle(new Style(Color.Grey19)).Centered());
        }

        static void DisplayItemsTable()
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Alignment(Justify.Center);

            table.AddColumn("[yellow]Category[/]").Centered();
            table.AddColumn("[yellow]Item No.[/]").Centered();
            table.AddColumn("[cyan]Item Name[/]").Centered();
            table.AddColumn("[cyan]Price[/]").Centered();
            table.AddColumn("[cyan]Stock[/]").Centered();

            string lastCategory = null;
            for (int i = 0; i < VendingMachineItems.Count; i++)
            {
                var item = VendingMachineItems[i];
                string categoryDisplay = (item.Category != lastCategory) ? item.Category : " ";

                if (lastCategory != null && item.Category != lastCategory)
                {
                    table.AddRow("[grey]-----------[/]", "[grey]-----------[/]", "[grey]-----------[/]", "[grey]-----------[/]", "[grey]-----------[/]");
                }

                table.AddRow(
                    categoryDisplay,
                    (i + 1).ToString(),
                    $"[i]{item.Name}[/]",
                    $"R{item.Price:F2}",
                    item.Stock > 0 ? item.Stock.ToString() : "[red]Out[/]"
                ).Centered();
                lastCategory = item.Category;
            }
            AnsiConsole.Write(table);
        }

        // ===================================================================
        // Main menu and Methods
        // ===================================================================

        static void ShowMainMenu()
        {
            while (true)
            {
                DrawHeader();
                PlayNRT();
                WriteLineInColor("\nPlease select an option:",ConsoleColor.Yellow);
                Console.WriteLine("  0. Admin Menu");
                Console.WriteLine("  1. Purchase Item");
                Console.WriteLine("  5. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    
                    case "0":
                        AdminMenu();
                        break;

                    case "1":
                        ItemSelectionAndPurchase();
                        break;

                    case "5":
                        WriteLineInColor("Thank you for using NRT Vending Machine!", ConsoleColor.Cyan);
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Black;
                        return;

                    default:
                        WriteLineInColor("Invalid choice. Please try again.", ConsoleColor.Red);
                        PauseAndReturn();
                        break;
                }
            }
        }
        // ===================================================================
        // For Item Select Menu
        // ===================================================================
        static void ItemSelectionAndPurchase()
        {
            DrawHeader();
            Console.WriteLine("Please enter the number of the item you wish to purchase (or 0 to cancel).");

            // 1. Get Item Selection
            int itemIndex = -1;
            while (true)
            {
                WriteLineInColor("Enter item number: ", ConsoleColor.Yellow);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int itemNumber))
                {
                    if (itemNumber == 0) return; // Cancel and go back to main menu
                    if (itemNumber > 0 && itemNumber <= VendingMachineItems.Count)
                    {
                        itemIndex = itemNumber - 1; 
                        break;
                    }
                    else
                    {
                        WriteLineInColor("Item number is not valid. Please try again.", ConsoleColor.Red);
                    }
                }
                else
                {
                    WriteLineInColor("Invalid input. Please enter a number.", ConsoleColor.Red);
                }
            }

            VendingItem selectedItem = VendingMachineItems[itemIndex];

            // 2. Check Stock
            if (selectedItem.Stock <= 0)
            {
                WriteLineInColor($"{selectedItem.Name} is out of stock.", ConsoleColor.Red);
                PauseAndReturn();
                return;
            }

            WriteLineInColor($"You selected: {selectedItem.Name} - Price: R{selectedItem.Price:F2}", ConsoleColor.Green);

            // 3. Get Money
            decimal moneyInserted = -1;
            while (true)
            {
                WriteLineInColor($"Please insert money (R{selectedItem.Price:F2}): ", ConsoleColor.Yellow);
                Console.Write("R");
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out moneyInserted))
                {
                    // Validate money input
                    if (moneyInserted < 0)
                    {
                        WriteLineInColor("Amount must be positive.", ConsoleColor.Red);
                        continue;
                    }
                    if (moneyInserted > MAX_MONEY_INPUT)
                    {
                        WriteLineInColor($"Maximum allowed is R{MAX_MONEY_INPUT:F2}.", ConsoleColor.Red);
                        continue;
                    }
                    if (moneyInserted < selectedItem.Price)
                    {
                        WriteLineInColor("Insufficient funds. Transaction cancelled.", ConsoleColor.Red);
                        PauseAndReturn();
                        return;
                    }
                    break; // Valid input
                }
                else
                {
                    WriteLineInColor("Invalid amount. Please enter a valid number.", ConsoleColor.Red);
                }
            }

            // 4. Process Purchase
            decimal change = moneyInserted - selectedItem.Price;
            Console.WriteLine($"Dispensing {selectedItem.Name}...");
            Thread.Sleep(1000);

            // Update stock
            selectedItem.Stock--;
            VendingMachineItems[itemIndex] = selectedItem;

            // Add the item price to total revenue
            totalRevenue += selectedItem.Price;

            WriteLineInColor($"\nEnjoy your {selectedItem.Name}!", ConsoleColor.Green);
            if (change > 0)
            {
                WriteLineInColor($"Your change: R{change:F2}", ConsoleColor.Green);
            }

            PauseAndReturn();
        }

        // ===================================================================
        // For Admin Menu
        // ===================================================================
        static void AdminMenu()
        {
            DrawHeader();
            WriteLineInColor("--- ADMIN LOGIN ---", ConsoleColor.DarkRed);

            while (true)
            {
                WriteLineInColor("\nPlease select an option:",ConsoleColor.Yellow);
                Console.WriteLine("  0. Enter Admin Password");
                Console.WriteLine("  1. Return to Main Menu");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // If the user selects 1, he will return to the main menu.
                    return;
                }
                else if (choice == "0")
                {
                    // If the user selects 0, he will enter the admin password 
                    Thread.Sleep(500);
                    Console.Clear();
                    PasswordMenu();
                    break;
                }
                else
                {
                    WriteLineInColor("Invalid option. Please choose 0 or 1.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    AdminMenu();
                    WriteLineInColor("--- ADMIN LOGIN ---", ConsoleColor.DarkRed);
                }

                static void PasswordMenu()
                {
                    DrawHeader();
                    WriteLineInColor("--- ADMIN LOGIN ---", ConsoleColor.DarkRed);

                    int attempts = 0;
                    const int maxAttempts = 3;

                    while (attempts < maxAttempts)
                    {
                        Console.Write("Enter admin password: ");
                        string password = ReadPassword(); // Reads password without showing it and uses "****"

                        if (password == AdminPassword)
                        {
                            WriteLineInColor("\nAccess granted.", ConsoleColor.Green);
                            Thread.Sleep(1000);
                            ShowAdminOptions();
                            return; // Return to main menu when done with admin options
                        }
                        else
                        {
                            attempts++;
                            WriteLineInColor($"\nIncorrect password. You have {maxAttempts - attempts} attempts left.", ConsoleColor.Red);
                        }
                    }

                    WriteLineInColor("\nMax login attempts exceeded. Returning to main menu.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    ShowMainMenu(); // Return to main menu after failed attempts
                }
            }
        }

        static void ShowAdminOptions()
        {
            while (true)
            {
                DrawHeader();
                WriteLineInColor("--- ADMIN OPTIONS ---", ConsoleColor.DarkRed);
                Console.WriteLine("1. Resupply Stock");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. Return to Main Menu");
                WriteLineInColor("Enter your choice: ", ConsoleColor.Yellow);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ResupplyAllStock();
                        WriteLineInColor("All items have been resupplied to their initial stock level.", ConsoleColor.Green);
                        PauseAndReturn();
                        break;
                    case "2":
                        WriteLineInColor("All money has been withdrawn from the machine.", ConsoleColor.Green);
                        WithdrawRevenue();
                        break;
                    case "3":
                        return; // Exit to admin menu
                    default:
                        WriteLineInColor("Invalid choice. Please try again.", ConsoleColor.Red);
                        PauseAndReturn();
                        break;
                }
            }
        }

        // ===================================================================
        // Admin methods: WithdrawRevenue
        // ===================================================================
        static void WithdrawRevenue()
        {
            DrawHeader();
            if (totalRevenue <= 0)
            {
                WriteLineInColor("No revenue to withdraw.", ConsoleColor.Yellow);
            }
            else
            {
                decimal withdrawnAmount = totalRevenue;
                totalRevenue = 0; // Reset revenue
                WriteLineInColor($"Successfully withdrawn R{withdrawnAmount:F2}!", ConsoleColor.Green);
            }
            PauseAndReturn();
        }

        // ===================================================================
        // Admin methods: ResupplyAllStock
        // ===================================================================

        static void ResupplyAllStock()
        {
            for (int i = 0; i < VendingMachineItems.Count; i++)
            {
                var item = VendingMachineItems[i];
                item.Stock = item.InitialStock;
                VendingMachineItems[i] = item;
            }
        }

        static void WriteLineInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void PauseAndReturn()
        {
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey(true);
        }

        
        // ===================================================================
        // It'sa me, Mario! Play the NRT sound (Nintendo don't sue me)
        // ===================================================================
        static void PlayNRT()
        {
            Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); 
            Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125);
            Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); 
            Thread.Sleep(375); Console.Beep(392, 150);
        }

        // ===================================================================
        // For entering admin password
        // ===================================================================
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b"); // allows backspace to work
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
    }
}