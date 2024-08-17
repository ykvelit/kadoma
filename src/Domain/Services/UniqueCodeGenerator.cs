namespace Domain.Services;
public class UniqueCodeGenerator : IUniqueCodeGenerator
{
    private readonly Random _random = new();
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string Generate(int length)
    {
        var codeChars = new char[length];

        for (var i = 0; i < length; i++)
        {
            var randomIndex = _random.Next(Characters.Length);

            codeChars[i] = Characters[randomIndex];
        }

        var code = new string(codeChars);

        return code;
    }
}
