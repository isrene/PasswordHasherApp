using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PasswordHasherApp;
using System.Runtime.CompilerServices;

class Program
{

    protected IOptions<HashingOptions> options;

    public Program(IOptions<HashingOptions> _options)
    {
        options = _options;
    }
    static void Main(string[] args)
    {


        RunHasher test = new RunHasher();

        Console.WriteLine("Welcome  to the HashTest, Enter a texto to Encrypt: \n\n ");

        string Password = Console.ReadLine();


        test.RunTest(Password);

    }


}

    