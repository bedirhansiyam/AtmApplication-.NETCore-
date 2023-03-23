using System.Text.Json;

namespace AtmApplication;
class Program
{
    static void Main(string[] args)
    {
        List<User> _users = JsonHelper.JsonDeserialize();
        bool control = true;
        int selected = 0;
        Guid id = Guid.Empty;
        
        do
        {           
            if(selected != 1)
            {
                MenuHelper.GetMainMenu();
                MenuHelper.ControlSelection(out selected,2);
            }             

            switch (selected)
            {
                case 1:
                    control = MenuHelper.LoginScreen(out id);

                    if(control == false)
                    {
                        FileHelper.WriteFraudsToFile();

                        Console.WriteLine("");
                        Console.WriteLine("Username or password is incorrect.");
                        Console.WriteLine("");
                        Console.WriteLine("Please press any key to Log-in again or press (1) to return to the previous menu");
                        string chosen =  Console.ReadLine();

                        if(chosen == "1")
                        {
                            selected = 0;
                            Console.Clear();
                        }                            
                    }
                break;
                case 2:                    
                    User newUser = MenuHelper.SignUpNewUser();
                    _users.Add(newUser);
                    JsonHelper.JsonSerialize(_users);

                    Console.WriteLine("");
                    Console.WriteLine("Sign-up successful. Please press any key to Log-in.");
                    Console.ReadKey();
                    control = false;
                    selected = 1;
                break;
                default:
                break;
            }
        } while (control == false);
        
        User mainUser = _users.FirstOrDefault(x => x.Id == id);

        selected = -1;
        int amount;
        string accountNumber;
        do
        {
            MenuHelper.GetTransactionMenu();
            MenuHelper.ControlSelection(out selected,4,0);
            switch (selected)
            {
                case 1: 
                    do
                    {
                        MenuHelper.TranscationHead("Withdrawal");

                        Console.Write("Please enter the amount you want to withdraw : ");
                        MenuHelper.ControlSelection(out amount);
    
                        if(amount < mainUser.Balance)
                        {
                            mainUser.Balance = mainUser.Balance - amount;
                            FileHelper.WriteWithdrawToFile(mainUser,amount);
                            JsonHelper.JsonSerialize(_users);
    
                            Console.WriteLine("");
                            Console.WriteLine($"Withdrawal successful. Current balance : ${mainUser.Balance}");
                            Console.WriteLine("Please press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Insufficient balance. Please press any key to re-enter the amount.");
                            Console.ReadKey();
                        }
                    } while (amount > mainUser.Balance);
                    break;

                case 2:
                    MenuHelper.TranscationHead("Cash Deposit");

                    Console.Write("Please enter the amount you want to deposit : ");
                    MenuHelper.ControlSelection(out amount);

                    mainUser.Balance = mainUser.Balance + amount;
                    FileHelper.WriteDepositToFile(mainUser,amount);
                    JsonHelper.JsonSerialize(_users);

                    Console.WriteLine("");
                    Console.WriteLine($"Cash deposit successful. Current balance : ${mainUser.Balance}");
                    Console.WriteLine("Please press any key to continue.");
                    Console.ReadKey();

                    break;
                    
                case 3:
                    bool isAccountNumberCorrect = true;
                    User toTheUser = new User();
                    do
                    {
                        MenuHelper.TranscationHead("Transfer Funds");

                        Console.Write("Please enter the account number you want to transfer : ");
                        accountNumber = Console.ReadLine();

                        Console.Write("Please enter the amount you want to withdraw : ");
                        MenuHelper.ControlSelection(out amount);

                        foreach (var user in _users)
                        {
                            if(user.AccountNumber == accountNumber)
                            {
                                toTheUser = user;
                                isAccountNumberCorrect = true;
                                break;
                            }
                            else
                                isAccountNumberCorrect = false;
                        }
    
                        if(amount < mainUser.Balance && isAccountNumberCorrect == true)
                        {
                            mainUser.Balance = mainUser.Balance - amount;
                            toTheUser.Balance = toTheUser.Balance + amount;
                            FileHelper.WriteTransferToFile(mainUser,amount,toTheUser);
                            JsonHelper.JsonSerialize(_users);
    
                            Console.WriteLine("");
                            Console.WriteLine($"Transfer successful. Current balance : ${mainUser.Balance}");
                            Console.WriteLine("Please press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Insufficient balance or wrong account number. Please press any key to re-enter the information.");
                            Console.ReadKey();
                        }                     
                    } while (amount > mainUser.Balance || isAccountNumberCorrect == false);
                    break;

                case 4:
                    MenuHelper.TranscationHead("End Of Day Report");

                    FileHelper.ReadFromFile();  

                    Console.WriteLine("");
                    Console.WriteLine("Please press any key to continue.");
                    Console.ReadKey();                  
                    
                    break;

                default:
                    break;
            }
            
        } while (selected != 0);
    }
}
