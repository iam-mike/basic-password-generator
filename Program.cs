using System;

namespace pwdGen
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = GeneratePassword(15);
            Console.WriteLine ("Your password is: "
                               + password);
        }
        private static string GeneratePassword(int lengthOfPassword)
        {
            bool includeLowercase = true;
            bool includeUppercase = true;
            bool includeNumeric = true;
            bool includeSpecial = true;
            bool includeSpaces = false;
            string generated =
                PasswordGenerator
                    .GeneratePassword(includeLowercase,
                    includeUppercase,
                    includeNumeric,
                    includeSpecial,
                    includeSpaces,
                    lengthOfPassword);
            string password = generated;

            while (!PasswordGenerator
                    .PasswordIsValid(includeLowercase,
                    includeUppercase,
                    includeNumeric,
                    includeSpecial,
                    includeSpaces,
                    password)
            )
            {
                password = generated;
            }

            return password;
        }
    }
}
