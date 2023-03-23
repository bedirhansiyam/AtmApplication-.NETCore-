using System.Text.Json;

namespace AtmApplication;

static class JsonHelper
{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + "users.json";
    private static JsonSerializerOptions Options()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNameCaseInsensitive = true;
        options.WriteIndented = true;
        options.IncludeFields = true;
        return options;
    }

    public static List<User> JsonDeserialize()
    {
        string deserialize = File.ReadAllText(filePath);
        JsonSerializerOptions options = Options();
        List<User> _user = JsonSerializer.Deserialize<List<User>>(deserialize,options);
        return _user;
    }

    public static void JsonSerialize(List<User> userList)
    {
        JsonSerializerOptions options = Options();
        string json = JsonSerializer.Serialize(userList,options);
        File.WriteAllText(filePath,json);
    }
}