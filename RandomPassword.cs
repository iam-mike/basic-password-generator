using System;

namespace pwdGen
{
    using System.Text.RegularExpressions;

    public static class PasswordGenerator
    {
        /// <summary>
        /// Generates a random password based on the rules passed in the parameters
        /// </summary>
        /// <param name="lowerRequired">Bool to say if lowercase are required</param>
        /// <param name="upperRequired">Bool to say if uppercase are required</param>
        /// <param name="numRequired">Bool to say if numerics are required</param>
        /// <param name="othersRequired">Bool to say if special characters are required</param>
        /// <param name="spacesRequired">Bool to say if spaces are required</param>
        /// <param name="passLength">Length of password required. Should be between 8 and 128</param>
        /// <returns></returns>
        public static string
        GeneratePassword(
            bool lowerRequired,
            bool upperRequired,
            bool numRequired,
            bool othersRequired,
            bool spacesRequired,
            int passLength
        )
        {
            const int MAX_CONSECUTIVE = 2;
            const string LOWERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMS = "0123456789";
            const string OTHERS = @"!#$%&*@\";
            const string SPACE = " ";
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 128;

            if (passLength >= MIN_LENGTH && passLength <= MAX_LENGTH)
            {
                var characterSet = "";

                if (lowerRequired)
                {
                    characterSet += LOWERS;
                }

                if (upperRequired)
                {
                    characterSet += UPPERS;
                }

                if (numRequired)
                {
                    characterSet += NUMS;
                }

                if (othersRequired)
                {
                    characterSet += OTHERS;
                }

                if (spacesRequired)
                {
                    characterSet += SPACE;
                }

                char[] password = new char[passLength];
                int characterSetLength = characterSet.Length;

                Random random = new Random();
                for (
                    var characterPosition = 0;
                    characterPosition < passLength;
                    characterPosition++
                )
                {
                    password[characterPosition] =
                        characterSet[random.Next(characterSetLength - 1)];

                    bool v =
                        !(
                        characterPosition <= MAX_CONSECUTIVE ||
                        password[characterPosition] !=
                        password[characterPosition - 1] ||
                        password[characterPosition - 1] !=
                        password[characterPosition - 2]
                        );
                    bool moreThanTwoIdenticalInARow = v;

                    if (moreThanTwoIdenticalInARow)
                    {
                        characterPosition--;
                    }
                }

                return string.Join(null, password);
            }
            return "Password length must be between 8 and 128.";
        }

        /// <summary>
        /// Checks if the password created is valid
        /// </summary>
        /// <param name="includeLowercase">Bool to say if lowercase are required</param>
        /// <param name="includeUppercase">Bool to say if uppercase are required</param>
        /// <param name="includeNumeric">Bool to say if numerics are required</param>
        /// <param name="includeSpecial">Bool to say if special characters are required</param>
        /// <param name="includeSpaces">Bool to say if spaces are required</param>
        /// <param name="password">Generated password</param>
        /// <returns>True or False to say if the password is valid or not</returns>
        public static bool
        PasswordIsValid(
            bool includeLowercase,
            bool includeUppercase,
            bool includeNumeric,
            bool includeSpecial,
            bool includeSpaces,
            string password
        )
        {
            const string REGEX_LOWERCASE = @"[a-z]";
            const string REGEX_UPPERCASE = @"[A-Z]";
            const string REGEX_NUMERIC = @"[\d]";
            const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
            const string REGEX_SPACE = @"([ ])+";

            bool lowerCaseIsValid =
                !(
                includeLowercase &&
                (!includeLowercase || !Regex.IsMatch(password, REGEX_LOWERCASE))
                );
            bool upperCaseIsValid =
                !(
                includeUppercase &&
                (!includeUppercase || !Regex.IsMatch(password, REGEX_UPPERCASE))
                );
            bool numericIsValid =
                !(
                includeNumeric &&
                (!includeNumeric || !Regex.IsMatch(password, REGEX_NUMERIC))
                );
            bool symbolsAreValid =
                !(
                includeSpecial &&
                (!includeSpecial || !Regex.IsMatch(password, REGEX_SPECIAL))
                );
            bool spacesAreValid =
                !(
                includeSpaces &&
                (!includeSpaces || !Regex.IsMatch(password, REGEX_SPACE))
                );

            return lowerCaseIsValid &&
            upperCaseIsValid &&
            numericIsValid &&
            symbolsAreValid &&
            spacesAreValid;
        }
    }
}
