using System.Text.Json;

public class CustomPropertyNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // If the name starts with a dollar sign, remove it.
        if (name.StartsWith("$"))
        {
            return name.Substring(1);
        }
        return name;
    }
}
