namespace AtmApplication;

public class FileHelper
{
    private static StreamWriter StreamWriter()
    {
        string today = $"EOD_{DateTime.Now.ToString("dd-M-yyy")}.txt";
        string fraudLogPath = AppDomain.CurrentDomain.BaseDirectory + "\\EODReports\\" + today;

        FileStream fileStream = new FileStream(fraudLogPath, FileMode.Append, FileAccess.Write, FileShare.Write);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        return streamWriter;
    }
    public static void WriteFraudsToFile()
    {
        StreamWriter streamWriter = StreamWriter();

        streamWriter.WriteLine($"({DateTime.Now}) - Wrong login attempt.");
        streamWriter.Close();
    }    

    public static void WriteWithdrawToFile(User user, int amount)
    {
        StreamWriter streamWriter = StreamWriter();

        streamWriter.WriteLine($"({DateTime.Now}) - {user.FullName} withdrew ${amount}.");
        streamWriter.Close();
    }
    public static void WriteDepositToFile(User user, int amount)
    {
        StreamWriter streamWriter = StreamWriter();

        streamWriter.WriteLine($"({DateTime.Now}) - {user.FullName} deposited ${amount} in cash.");
        streamWriter.Close();
    }

    public static void WriteTransferToFile(User mainUser, int amount, User user2)
    {
        StreamWriter streamWriter = StreamWriter();

        streamWriter.WriteLine($"({DateTime.Now}) - {mainUser.FullName} transferred ${amount} to {user2.FullName}");
        streamWriter.Close();
    }
    public static void ReadFromFile()
    {
        string today = $"EOD_{DateTime.Now.ToString("dd-M-yyy")}.txt";
        string fraudLogPath = AppDomain.CurrentDomain.BaseDirectory + "\\EODReports\\" + today;
        
        StreamReader streamReader = new StreamReader(fraudLogPath);

        Console.WriteLine(streamReader.ReadToEnd());
        streamReader.Close();
    }
}