namespace AtmApplication;

public class User
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; } = 0;
}