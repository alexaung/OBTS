using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Extensions
{
    public static class OBTSString
    {
        public static string GetRandomString(this string value, int seed)
        {
            //use the following string to control your set of alphabetic characters to choose from
            //for example, you could include uppercase too
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Random is not truly random,
            // so we try to encourage better randomness by always changing the seed value
            Random rnd = new Random((seed + DateTime.Now.Millisecond));

            // basic 5 digit random number
            string result = rnd.Next(10000, 99999).ToString();

            // single random character in ascii range a-z
            string alphaChar = alphabet.Substring(rnd.Next(0, alphabet.Length - 1), 1);

            // random position to put the alpha character
            int replacementIndex = rnd.Next(0, (result.Length - 1));
            result = result.Remove(replacementIndex, 1).Insert(replacementIndex, alphaChar);

            return result;
        }

    }
}