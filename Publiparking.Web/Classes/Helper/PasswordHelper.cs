using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes.Helper
{
    public static class PasswordHelper
    {
        public static bool CheckSicurezzaPassword(string p_password, string p_SicurezzaPassword)
        {
            if (!string.IsNullOrEmpty(p_SicurezzaPassword))
            {
                Regex regex = new Regex(p_SicurezzaPassword);
                Match match = regex.Match(p_password);
                return match.Success;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Genera la password di sistema randomicamente
        /// </summary>
        /// <param name="Lenght"></param>
        /// <param name="NonAlphaNumericChars"></param>
        /// <returns></returns>
        public static string GeneratePassword(int Lenght, int NonAlphaNumericChars)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
            Random rd = new Random();

            if (NonAlphaNumericChars > Lenght || Lenght <= 0 || NonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Lenght];
            int[] pos = new int[Lenght];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Lenght - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Lenght);
                for (j = 0; j < Lenght; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Lenght;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Lenght - NonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Lenght - NonAlphaNumericChars; i < Lenght; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Lenght];
            for (i = 0; i < Lenght; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
        }
    }

}
