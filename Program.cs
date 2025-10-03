using NRT;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading;

namespace NRTVending
{
    class Program
    {
        static void Main(string[] args)
        {
            Music.PlayLoop("1.wav"); // all music was produced by - nazim :)

            while (true)
            {
                AnsiConsole.Clear();
                ShowBanner();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[orange1]Choose an option:[/]")
                        .AddChoices(new[] { "Admin Menu", "User Menu", "Music Options", "Exit" }));

                switch (choice)
                {
                    case "Admin Menu":
                        var adminchoice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[orange1]Choose an option:[/]")
                                .AddChoices(new[] { "Admin Login", "Back" }));
                        switch (adminchoice)
                        {
                            case "Admin Login":
                                AdminLogin();
                                break;

                            case "Back":
                                break;
                        }
                        break;

                    case "User Menu":

                        var userchoice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[orange1]Choose an option:[/]")
                                .AddChoices(new[] { "User Login", "Back" }));
                        switch (userchoice)
                        {
                            case "User Login":
                                UserLogin();
                                break;

                            case "Back":
                                break;
                        }
                        break;

                    case "Music Options":
                        MusicMenu();
                        break;

                    case "Exit":
                        Music.Stop();
                        ShowByeBanner();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        CenterText("╔══════════════════════════════╗");
                        CenterText("║         Developers:          ║");
                        CenterText("║ ---------------------------- ║");
                        CenterText("║    Nazim, MRidhaa, Toufeeq   ║");
                        CenterText("║ ---------------------------- ║");
                        CenterText("╚══════════════════════════════╝");

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        CenterText("╔══════════════════════════════╗");
                        CenterText("║       Quality Engineers:     ║");
                        CenterText("║ ---------------------------- ║");
                        CenterText("║    Matthew, Jawaad, Siphe    ║");
                        CenterText("║ ---------------------------- ║");
                        CenterText("╚══════════════════════════════╝");

                        Console.ResetColor();
                        Console.ReadKey();
                        Console.WriteLine("\n");
                        Console.ForegroundColor = ConsoleColor.Red;
                        CenterText("------------------ WARNING! ------------------");
                        CenterText("If you have epilepsy do NOT enter party mode!");
                        CenterText("---------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine("\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        CenterText("--------------------- CHOOSE -------------------");
                        CenterText("Press [enter] to Exit or [P] to enter Party Mode");
                        CenterText("------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.Black;
                        string endchoice = Console.ReadLine().ToUpper();
                        if (endchoice == "P")
                        {
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            CenterText("╔══════════════════════════════╗");
                            CenterText("║      PARTY MODE ACTIVATED    ║");
                            CenterText("╚══════════════════════════════╝");

                            for (int i = 0; i < 30; i++)
                            {
                                Console.BackgroundColor = (ConsoleColor)(i % 16);
                                Console.Clear();
                                Thread.Sleep(100);
                            }

                            Console.ResetColor();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            CenterText("---------------------------------------------------");
                            CenterText("Hope you enjoyed the party, Press any key to exit");
                            CenterText("---------------------------------------------------");
                            Music.Stop();
                            Console.ResetColor();

                        }
                        else
                        {
                            return;
                        }
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.Black;
                        return;
                }
            }
        }

        // ----------------- BANNERS -------------------
        static void ShowBanner()
        {
            var figlet = new FigletText("NRT VENDING").Centered().Color(Color.Red1);
            AnsiConsole.Write(figlet);
            Console.WriteLine();
        }

        static void ShowByeBanner()
        {
            var figlet = new FigletText("Thank You :)").Centered().Color(Color.Green1);
            AnsiConsole.Write(figlet);
            Console.WriteLine();
        }

        // ----------------- NAZIM'S MUSIC MENU -----------------
        static void MusicMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                ShowBanner();

                var musicchoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[orange1]Music Options (Artist: Nazim):[/]")
                        .AddChoices(new[] { "Afternoon", "Windows XP", "8-BIT Console Music", "Stop Music", "Back" }));

                switch (musicchoice)
                {
                    case "Afternoon":
                        Music.PlayLoop("1.wav");
                        AnsiConsole.MarkupLine("[green1]Playing 'Afternoon'...[/]");
                        Thread.Sleep(700);
                        break;
                    case "Windows XP":
                        AnsiConsole.MarkupLine("[green1]Playing 'Windows XP'...[/]");
                        Music.PlayLoop("2.wav");
                        Thread.Sleep(700);
                        break;
                    case "8-BIT Console Music":
                        AnsiConsole.MarkupLine("[green1]Playing '8-BIT Console Music'...[/]");
                        Music.PlayLoop("3.wav");
                        Thread.Sleep(700);
                        break;
                    case "Stop Music":
                        Music.Stop();
                        AnsiConsole.MarkupLine("[yellow]Music stopped.[/]");
                        Thread.Sleep(700);
                        break;
                    case "Back":
                        return;
                }
            }
        }

        // ----------------- ADMIN LOGIN -----------------
        static void AdminLogin()
        {
            int adminattempts = 0;
            const int maxAttempts = 3;
            while (adminattempts < maxAttempts)
            {
                string username = AnsiConsole.Ask<string>("Enter [orange1]Admin Username[/]:");
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter [orange1]Password[/]:").Secret());

                var admin = Database.GetAdmin(username, password);
                if (admin != null)
                {
                    while (true)
                    {
                        AnsiConsole.Clear();
                        ShowBanner();
                        AnsiConsole.MarkupLine($"Welcome: [orange1]{admin.Username}[/]");
                        decimal vmBalance = Database.GetVendingBalance();
                        AnsiConsole.MarkupLine($"[bold green1]Welcome {admin.Username}! \nVending Machine Cashout: R{vmBalance} [/]\n"); //MACHINE BALANCE
                        AnsiConsole.MarkupLine($"[bold green1]Admin Balance: R{admin.Balance:0.00} [/]\n"); //ADMIN BALANCE


                        ShowUsersTable();
                        ShowItemsTable();

                        var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[orange1]Select an option:[/]")
                                .AddChoices(new[] { "User Management", "Item Management", "Retrieve Vending Machine Balance", "Logout" }));

                        switch (choice)
                        {
                            case "User Management":

                                var userManagement = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("[orange1]Select an option:[/]")
                                .AddChoices(new[] { "Add User", "Delete User", "Back" }));
                                switch (userManagement)
                                {
                                    case "Add User":
                                        AddUser();
                                        break;
                                    case "Delete User":
                                        DeleteUser();
                                        break;
                                    case "Back":
                                        break;
                                }
                                break;
                            case "Item Management":

                                var itemManagement = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("[orange1]Select an option:[/]")
                                .AddChoices(new[] { "Add Item", "Update Item Stock", "Delete Item", "Back" }));
                                switch (itemManagement)
                                {
                                    case "Add Item":
                                        AddItem();
                                        break;
                                    case "Update Item Stock":
                                        UpdateItemStock();
                                        break;
                                    case "Delete Item":
                                        DeleteItem();
                                        break;
                                    case "Back":
                                        break;
                                }
                                break;
                            case "Retrieve Vending Machine Balance":

                                RetrieveVendingBalance(admin, m: null);
                                break;
                            case "Logout":
                                return;
                        }
                    }
                }
                else
                {
                    adminattempts++;
                    AnsiConsole.MarkupLine($"[red1]Invalid credentials! Attempts remaining: {maxAttempts - adminattempts}[/]");
                    if (adminattempts < maxAttempts)
                    {
                        var retrychoice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Would you like to [orange1]try again[/]?")
                            .AddChoices(new[] { "YES", "NO", }));
                        switch (retrychoice)
                        {
                            case "YES":

                                continue;


                            case "NO":
                                AnsiConsole.MarkupLine("[yellow]Returning to Main Menu...[/]");
                                Thread.Sleep(1200);
                                return;

                        }
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[red1]Max attempts reached![/]");
                        AnsiConsole.MarkupLine("[yellow]Returning to Main Menu...[/]");
                        Thread.Sleep(2000);
                        return;
                    }
                }
            }
        }

        // ----------------- USER LOGIN -----------------
        static void UserLogin()
        {
            AnsiConsole.Clear();
            ShowBanner();

            ShowRegisteredUsers();

            string studentId = "";
            while (true)
            {
                studentId = AnsiConsole.Ask<string>("Enter [orange1]Student ID[/] (8 chars):");
                if (string.IsNullOrEmpty(studentId) || studentId.Length != 8)
                {
                    AnsiConsole.MarkupLine("[red1]Student ID must be exactly 8 characters![/]");
                    continue;
                }
                break;
            }

            string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter [orange1]Password[/]:").Secret());
            var user = Database.GetUser(studentId, password);
            if (user == null)
            {
                AnsiConsole.MarkupLine("[red1]Invalid credentials! Press any key to return...[/]");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                AnsiConsole.Clear();
                ShowBanner();
                AnsiConsole.MarkupLine($"[bold green1]Welcome {user.Name}!\nBalance: R{user.Balance:0.00}[/]\n");

                ShowItemsTable();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[orange1]Select an option: [/]")
                        .AddChoices(new[] { "Deposit", "Purchase", "Withdraw", "Logout" }));

                switch (choice)
                {
                    case "Deposit":
                        var depositchoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[orange1]Choose an option:[/]")
                        .AddChoices(new[] { "Card", "Cash", "Back" }));

                        switch (depositchoice)
                        {
                            case "Card":
                                AnsiConsole.MarkupLine("[yellow]Processing card...[/]");
                                Thread.Sleep(1500);
                                cardDeposit(user);
                                break;
                            case "Cash":
                                AnsiConsole.MarkupLine("[white]Cash allowed (notes & coins): R1,R2,R5,R10,R20,R50,R100,R200 [/]");
                                cashDeposit(user);
                                Thread.Sleep(1000);
                                break;
                            case "Back":
                                break;
                        }

                        break;
                    case "Purchase":
                        Purchase(user);
                        break;
                    case "Withdraw":
                        Withdraw(user);
                        break;
                    case "Logout":
                        return;
                }
            }
        }

        // ----------------- TABLES -----------------
        static void ShowItemsTable()
        {
            var items = Database.GetAllItems();
            var table = new Table().Centered();
            table.AddColumn(new TableColumn("[red1]ID[/]").Centered());
            table.AddColumn(new TableColumn("[cyan1]Name[/]").Centered());
            table.AddColumn(new TableColumn("[green1]Type[/]").Centered());
            table.AddColumn(new TableColumn("[orange1]Price[/]").Centered());
            table.AddColumn(new TableColumn("[orange1]Stock[/]").Centered());

            foreach (var i in items)
                table.AddRow(i.Id.ToString(), i.Name, i.Type, string.Format("R{0:0.00}", i.Price), i.Stock.ToString());

            AnsiConsole.Write(table);
            Console.WriteLine();
        }

        static void ShowUsersTable()
        {
            var users = Database.GetAllUsers();
            var table = new Table().Centered();
            table.AddColumn(new TableColumn("[red1]ID[/]").Centered());
            table.AddColumn(new TableColumn("[orange1]StudentID[/]").Centered());
            table.AddColumn(new TableColumn("[cyan1]Name[/]").Centered());
            table.AddColumn(new TableColumn("[green1]Balance[/]").Centered());

            foreach (var u in users)
                table.AddRow(u.Id.ToString(), u.StudentId, u.Name, string.Format("R{0:0.00}", u.Balance));

            AnsiConsole.Write(table);
            Console.WriteLine();
        }

        static void ShowRegisteredUsers()
        {
            var RegisteredUsers = Database.GetAllUsers();
            var Registeredtable = new Table().Centered();
            Registeredtable.AddColumn(new TableColumn("[orange1] StudentID[/]").Centered());
            Registeredtable.AddColumn(new TableColumn("[cyan1] Name[/]").Centered());

            foreach (var ru in RegisteredUsers)
                Registeredtable.AddRow(ru.StudentId, ru.Name);
            AnsiConsole.Write(Registeredtable);
            Console.WriteLine();
        }

        // ----------------- USER ACTIONS -----------------
        static void cardDeposit(User user)
        {
            bool validDeposit = false;
            while (!validDeposit)
            {
                var prompt = new TextPrompt<decimal>("[orange1]Enter deposit amount: R[/]")
                    .Validate(val =>
                    {
                        if (val <= 0)
                            return ValidationResult.Error("[red]Amount must be positive[/]");
                        if (user.Balance + val >= 1000.01m)
                            return ValidationResult.Error($"[red]Deposit cannot exceed maximum balance of R1000.00[/]\nCurrent balance: R{user.Balance:0.00}\nMaximum deposit allowed: R{(1000.00m - user.Balance):0.00}");
                        return ValidationResult.Success();
                    });

                decimal amount = AnsiConsole.Prompt(prompt);
                user.Balance += amount;
                Database.UpdateUserBalance(user);
                AnsiConsole.MarkupLine("[yellow]Processing Payment...[/]");
                Thread.Sleep(1000);
                AnsiConsole.MarkupLine($"[green1]Successfully deposited R{amount:0.00}. New balance: R{user.Balance:0.00}[/]");
                validDeposit = true;
            }
            Console.ReadKey();
        }
        static void cashDeposit(User user)
        {
            bool validCashDeposit = false;
            decimal[] validAmounts = { 1, 2, 5, 10, 20, 50, 100, 200 };
            while (!validCashDeposit)
            {
                var prompt = new TextPrompt<decimal>("[orange1]Enter cash amount: R[/]")
                    .Validate(val =>
                    {
                        if (Array.IndexOf(validAmounts, val) == -1)
                            return ValidationResult.Error("[red]Amount must be of the options above[/]");
                        if (user.Balance + val >= 1000.01m)
                            return ValidationResult.Error($"[red]Deposit cannot exceed maximum balance of R1000.00[/]\nCurrent balance: R{user.Balance:0.00}\nMaximum deposit allowed: R{(1000.00m - user.Balance):0.00}");
                        return ValidationResult.Success();
                    });

                decimal amount = AnsiConsole.Prompt(prompt);
                user.Balance += amount;
                Database.UpdateUserBalance(user);
                AnsiConsole.MarkupLine("[yellow]Processing Payment...[/]");
                Thread.Sleep(2000);
                AnsiConsole.MarkupLine($"[green1]Successfully deposited R{amount:0.00}. New balance: R{user.Balance:0.00}[/]");
                validCashDeposit = true;
            }
            Console.ReadKey();
        }

        static void Withdraw(User user)
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[orange1]Choose a withdrawal option:[/]")
                .AddChoices(new[] { "Withdraw All", "Withdraw Specific Amount", "Back" }));

            switch (choice)
            {
                case "Withdraw All":
                    if (user.Balance <= 0)
                    {
                        AnsiConsole.MarkupLine("[red1]No funds available to withdraw![/]");
                        Console.ReadKey();
                        break;
                    }
                    decimal allAmount = user.Balance;
                    user.Balance = 0;
                    Database.UpdateUserBalance(user);
                    AnsiConsole.MarkupLine($"[green1]You withdrew R{allAmount:0.00}![/]");
                    Console.ReadKey();
                    break;

                case "Withdraw Specific Amount":
                    decimal amount = AnsiConsole.Ask<decimal>("[orange1]Enter withdraw amount:[/]");
                    if (amount > user.Balance)
                    {
                        AnsiConsole.MarkupLine("[red1]Insufficient balance![/]");
                        Console.ReadKey();
                        break;
                    }
                    user.Balance -= amount;
                    Database.UpdateUserBalance(user);
                    AnsiConsole.MarkupLine($"[green1]You withdrew R{amount:0.00}![/]");
                    Console.ReadKey();
                    break;

                case "Back":
                    return;
            }
        }

        static void Purchase(User user)
        {
            var idPrompt = new TextPrompt<int>("Enter [yellow]Item ID[/] to purchase:")
                .Validate(i => i >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid ID[/]"));
            int id = AnsiConsole.Prompt(idPrompt);

            var item = Database.GetItem(id);
            if (item == null)
            {
                AnsiConsole.MarkupLine("[red1]Item not found![/]");
                Console.ReadKey();
                return;
            }

            if (item.Stock <= 0)
            {
                AnsiConsole.MarkupLine("[red1]Item out of stock![/]");
                Console.ReadKey();
                return;
            }
            if (user.Balance < item.Price)
            {
                AnsiConsole.MarkupLine("[red1]Insufficient balance![/]");
                Console.ReadKey();
                return;
            }

            Database.PurchaseItem(user, item);

            // refresh local user from DB
            var refreshed = Database.GetUserByStudentId(user.StudentId);
            if (refreshed != null)
                user.Balance = refreshed.Balance;

            AnsiConsole.MarkupLine(string.Format("[green1]Successfully purchased {0}. New balance: R{1:0.00}[/]", item.Name, user.Balance));
            Console.ReadKey();
        }

        // ----------------- ADMIN CRUD -----------------
        static void AddUser()
        {
            // role selection
            var role = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orange1]Select User Role:[/]")
                    .AddChoices(new[] { "Software Developer", "Quality Engineer", "Data Analyst" }));

            string prefix;
            if (role == "Software Developer") prefix = "SD";
            else if (role == "Quality Engineer") prefix = "QE";
            else prefix = "DA";

            string numericPart = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter numeric part for [orange1]Student ID (6 digits):[/]")
                    .Validate(input =>
                    {
                        for (int i = 0; i < input.Length; i++)
                            if (!char.IsDigit(input[i]))
                                return ValidationResult.Error("[red]Only numbers allowed[/]");
                        if (string.IsNullOrEmpty(input) || input.Length != 6)
                            return ValidationResult.Error("[red]Must be exactly 6 digits[/]");
                        string candidate = prefix + input;
                        if (Database.GetUserByStudentId(candidate) != null)
                            return ValidationResult.Error("[red]Student ID already exists[/]");

                        return ValidationResult.Success();
                    }));
            string studentId = prefix + numericPart;

            string name = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [orange1]Name:[/]")
                    .Validate(n =>
                    {
                        if (string.IsNullOrEmpty(n))
                            return ValidationResult.Error("[red]Name is required[/]");
                        if (n.Length > 20)
                            return ValidationResult.Error("[red]Name cannot exceed 20 Letters[/]");
                        if (!n.All(char.IsLetter))
                            return ValidationResult.Error("[red]Name must contain only letters[/]");

                        return ValidationResult.Success();
                    }));

            string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter [orange1]Password[/] (max 8 chars, 1 uppercase, 1 number, 1 special):").Secret());
            while (!IsValidPassword(password))
            {
                AnsiConsole.MarkupLine("[red1]Password invalid! Must be max 8 chars, include 1 uppercase letter, 1 number, 1 special character.[/]");
                password = AnsiConsole.Prompt(new TextPrompt<string>("Enter [orange1]Password[/] (max 8 chars, 1 uppercase, 1 number, 1 special):").Secret());
            }
            AnsiConsole.MarkupLine("[yellow]Adding user...[/]");
            Thread.Sleep(1000);
            Database.AddUser(new User { StudentId = studentId, Name = name, Password = password });
            AnsiConsole.MarkupLine(string.Format("[green1]User added: Student ID: {0} successfully[/]", studentId));
            Thread.Sleep(2000);
        }

        static bool IsValidPassword(string pwd)
        {
            if (string.IsNullOrEmpty(pwd) || pwd.Length > 8) return false;
            bool hasUpper = false, hasDigit = false, hasSpecial = false;
            for (int i = 0; i < pwd.Length; i++)
            {
                char c = pwd[i];
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsLetterOrDigit(c)) hasSpecial = true;
            }
            return hasUpper && hasDigit && hasSpecial;
        }

        static void DeleteUser()
        {

            while (true)
            {
                var idPrompt = new TextPrompt<int>("Enter [red1]User ID[/] to delete:")
                .Validate(i => i > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid ID[/]"));
                int id = AnsiConsole.Prompt(idPrompt);

                var existing = Database.GetUserById(id);
                if (existing != null)
                {

                    Database.DeleteUser(id);
                    AnsiConsole.MarkupLine("[yellow]User being deleted...[/]");
                    Thread.Sleep(1000);
                    AnsiConsole.MarkupLine("[green1]User deleted successfully![/]");
                    Thread.Sleep(1500);
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]User ID not found![/]");
                    var retrychoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Would you like to [orange1]try again[/]?")
                        .AddChoices(new[] { "YES", "NO", }));
                    switch (retrychoice)
                    {
                        case "YES":
                            continue;

                        case "NO":
                            AnsiConsole.MarkupLine("[yellow]Returning to Main Menu...[/]");
                            Thread.Sleep(1200);
                            return;
                    }
                }
            }
        }

        static void AddItem()
        {
            string name = AnsiConsole.Prompt(

                new TextPrompt<string>("Enter [orange1]Item Name[/] (max 20 chars): ")
                .Validate(n =>
                {
                    if (string.IsNullOrWhiteSpace(n))
                        return ValidationResult.Error("[red]Name is required[/]");

                    if (n.Length > 20)
                        return ValidationResult.Error("[red]Max 20 characters allowed[/]");

                    return ValidationResult.Success();
                })
            );

            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select Category:")
                    .AddChoices(new[] { "Snack", "Drink" }));

            decimal price = AnsiConsole.Prompt(
                new TextPrompt<decimal>("Enter [orange1]Price[/] (max R500):")
                    .Validate(p => (p >= 0 && p <= 500) ? ValidationResult.Success() : ValidationResult.Error("[red]Price must be between 0 and R500[/]")));

            int stock = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter [orange1]Stock[/] (0-20):")
                    .Validate(s => (s >= 0 && s <= 20) ? ValidationResult.Success() : ValidationResult.Error("[red]Stock must be between 0 and 20[/]")));

            Database.AddItem(new Item { Name = name, Type = category, Price = price, Stock = stock });
            AnsiConsole.MarkupLine("[yellow]Adding item...[/]");
            Thread.Sleep(1000);
            AnsiConsole.MarkupLine("[green1]Item added successfully![/]");
            Thread.Sleep(1500);
        }

        static void UpdateItemStock()
        {
            while (true)
            {
                int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter [orange1]Item ID[/] to update stock:")
                    .Validate(i => i >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid ID[/]")));

                var item = Database.GetItem(id);
                if (item != null)
                {
                    int newStock = AnsiConsole.Prompt(new TextPrompt<int>("Enter new [orange1]Stock[/] (0-20):")
                    .Validate(s => (s >= 0 && s <= 20) ? ValidationResult.Success() : ValidationResult.Error("[red]Stock must be between 0 and 20[/]")));

                    item.Stock = newStock;
                    Database.UpdateItemStock(item);
                    AnsiConsole.MarkupLine("[green1]Stock updated successfully![/]");
                    Thread.Sleep(1500);
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red1]Item not found![/]");
                    var retrychoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Would you like to [orange1]try again[/]?")
                        .AddChoices(new[] { "YES", "NO", }));
                    switch (retrychoice)
                    {
                        case "YES":
                            continue;

                        case "NO":
                            AnsiConsole.MarkupLine("[yellow]Returning to Main Menu...[/]");
                            Thread.Sleep(1200);
                            return;
                    }
                }
            }
        }

        static void DeleteItem()
        {
            int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter [red1]Item ID[/] to delete:")
                .Validate(i => i >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid ID[/]")));

            var existing = Database.GetItem(id);
            if (existing == null)
            {
                AnsiConsole.MarkupLine("[red1]Item ID not found![/]");
                Console.ReadKey();
                return;
            }

            Database.DeleteItem(id);
            AnsiConsole.MarkupLine("[green1]Item deleted successfully![/]");
            Console.ReadKey();
        }

        static void RetrieveVendingBalance(Admin admin, Machine m)
        {

            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[orange1]Choose a withdrawal option:[/]")
                .AddChoices(new[] { "Withdraw All", "Withdraw Specific Amount", "Back" }));

            switch (choice)
            {
                case "Withdraw All":
                    decimal vmBalance = Database.GetVendingBalance(); // get total vending machine balance
                    if (vmBalance <= 0)
                    {
                        AnsiConsole.MarkupLine("[yellow]No funds in vending machine to retrieve![/]");
                        Console.ReadKey();
                        return;
                    }
                    Database.UpdateAdminBalance(admin); //vending machine balance goes to admin balance
                    Database.ResetVendingBalance(); // reset vending machine balance to 0

                    var refreshedAdmin = Database.GetAdmin(admin.Username, admin.Password);
                    if (refreshedAdmin != null)
                        admin.Balance = refreshedAdmin.Balance;
                    AnsiConsole.MarkupLine(string.Format($"[green1]R{vmBalance} retrieved from vending machine![/]", $"Balance:{vmBalance}"));
                    Console.ReadKey();
                    break;

                case "Withdraw Specific Amount":
                    decimal vmBalance2 = Database.GetVendingBalance(); //Machine balance
                    decimal val = AnsiConsole.Ask<decimal>("[orange1]Enter withdraw amount:[/]");
                    if (vmBalance2 < val)
                    {
                        AnsiConsole.MarkupLine("[red1]Insufficient balance![/]");
                        Console.ReadKey();
                        break;
                    }
                    else if (vmBalance2 >= val)
                    {
                        decimal add2Admin = admin.Balance + val;
                        vmBalance2 = vmBalance2 - val;
                        admin.Balance = add2Admin;
                        Database.Add2VendingBalance(decimal.Negate(val)); //deduct from vending machine balance

                    }
                    Database.UpdateAdminBalance(admin);
                    AnsiConsole.MarkupLine($"[green1]You withdrew R{val}![/]");
                    Console.ReadKey();
                    break;


                case "Back":
                    return;
            }
        }

        // ----------------- STYLE -----------------
        static void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = Math.Max(0, (windowWidth - text.Length) / 2);
            Console.WriteLine(new string(' ', padding) + text);
        }
    }
}
