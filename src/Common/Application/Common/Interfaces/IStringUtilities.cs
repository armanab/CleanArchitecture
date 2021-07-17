namespace CleanApplication.Application.Common.Interfaces
{
    public interface IStringUtilities
    {
        string MD5Hash(string input);

        string Hash(string input);

        string Sha512Hash(string input);

        string GenerateRandom(int length, string chars);

        string GenerateText(int length);

        string RandomNumber(int length = 1);

        string RandomAlphabet(int length = 1);

    }
}
