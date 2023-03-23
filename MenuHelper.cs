namespace AtmApplication;

public class MenuHelper
{
    public static void ControlSelection(out int selection)
    {
        bool selectionController;
        do
        {
            selectionController = int.TryParse(Console.ReadLine(), out selection);
            if (selection > 0 && selectionController == true)
                selectionController = true;
            else
                selectionController = false;

            if (selectionController == false)
                Console.Write("Please make a valid selection : ");
        } while (selectionController == false);
    }
    public static void ControlSelection(out int selection, int maxValue)
    {
        bool selectionController;
        do
        {
            selectionController = int.TryParse(Console.ReadLine(), out selection);
            if (selection > 0 && selection <= maxValue && selectionController == true)
                selectionController = true;
            else
                selectionController = false;
            if (selectionController == false)
                Console.Write("Please make a valid selection : ");
        } while (selectionController == false);
    }
    public static void ControlSelection(out int selection, int maxValue, int minValue)
    {
        bool selectionController;
        do
        {
            selectionController = int.TryParse(Console.ReadLine(), out selection);
            if (selection >= minValue && selection <= maxValue && selectionController == true)
                selectionController = true;
            else
                selectionController = false;
            if (selectionController == false)
                Console.Write("Please make a valid selection : ");
        } while (selectionController == false);
    }

    public static User SignUpNewUser()
    {
        User newUser = new User();

        Console.Clear();

        Console.WriteLine("Sign-up");
        Console.WriteLine("-------");
        Console.WriteLine("");

        Console.Write("Full name : ");
        newUser.FullName = Console.ReadLine();

        Console.Write("Username : ");
        newUser.Username = Console.ReadLine();

        Console.Write("Password : ");
        newUser.Password = HidePassword();
        Console.WriteLine("");

        Console.Write("Account number : ");
        newUser.AccountNumber = Console.ReadLine();

        return newUser;
    }

    public static bool LoginScreen(out Guid id)
    {
        id = Guid.NewGuid();
        bool control = true;
        List<User> _users = JsonHelper.JsonDeserialize();
        Console.Clear();

        Console.WriteLine("Log-in");
        Console.WriteLine("------");
        Console.WriteLine("");

        Console.Write("Username : ");
        string username = Console.ReadLine();

        Console.Write("Password : ");
        string password = HidePassword();
        Console.WriteLine("");

        foreach (var user in _users)
        {
            if(username ==  user.Username && password == user.Password)
            {
                control = true;
                id = user.Id;
                break;
            }
            else
                control = false;
        }
        return control;
    }

    public static void GetMainMenu()
    {
        Console.WriteLine("*Welcome to Patika Bank*");
        Console.WriteLine("------------------------");
        Console.WriteLine("");
        Console.WriteLine("Please press (1) to Log-in");
        Console.WriteLine("Please press (2) to Sign-up");
    }
    public static void GetTransactionMenu()
    {
        Console.Clear();
        Console.WriteLine("Please select transaction");
        Console.WriteLine("-------------------------");
        Console.WriteLine();
        Console.WriteLine("1-) Withdrawal");
        Console.WriteLine("2-) Cash Deposit");
        Console.WriteLine("3-) Transfer Funds");
        Console.WriteLine("4-) End Of Day Report");
        Console.WriteLine("0-) Log-out");
    }

    public static string HidePassword()
        {
        string pass = "";

        do
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                pass += key.KeyChar;
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, (pass.Length - 1));
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        } while (true);

        return pass;
    }
    public static void TranscationHead(string header)
    {
        string line = "";
        
        for (int i = 0; i < header.Length; i++)
            line = line + "-";
            
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(line);
        Console.WriteLine("");

        
        
    }
}

