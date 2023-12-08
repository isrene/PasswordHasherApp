using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PasswordHasherApp
{

    public class RunHasher : IRunHasher
    {
        protected HashingOptions options;
        //protected IOptions<HashingOptions> options;


        protected string? Hashed;

        public RunHasher()
        {
                
        }

        public RunHasher(IOptions<HashingOptions> _options)
        {
            options = new HashingOptions();
           // options = _options;


        }

        public void RunTest(string password)

        {

            Console.Clear(); 

            PasswordHasher pwH = new PasswordHasher();

            Console.WriteLine("Before Hashing, text is: " + password);
            Console.WriteLine("\n\n");
            Console.WriteLine("after hashing: \n\n");

            Hashed = pwH.Hash(password);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(Hashed);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Passwored again: \n");

            var pass= Console.ReadLine();


            var checkPass = pwH.Check(Hashed, pass);

            Console.WriteLine(" \n\n");

            if (checkPass.Verified == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Wrong Password!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Password Matched! ");
                Console.ForegroundColor = ConsoleColor.White;
            }




        }
    }

    
}
