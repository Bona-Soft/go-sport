using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.Helpers
{
   public static class HashHelper
   {
      private const int SaltByteLength = 24;
      private const int DerivedKeyLength = 24;

      #region " PRIVATE METHODS "
      private static byte[] GenerateRandomSalt()
      {
         var csprng = new RNGCryptoServiceProvider();
         var salt = new byte[SaltByteLength];
         csprng.GetBytes(salt);
         return salt;
      }

      private static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount)
      {
         byte[] hashValue;
         var valueToHash = string.IsNullOrEmpty(password) ? string.Empty : password;
         using (var pbkdf2 = new Rfc2898DeriveBytes(valueToHash, salt, iterationCount))
         {
            hashValue = pbkdf2.GetBytes(DerivedKeyLength);
         }
         return hashValue;
      }

      private static bool ConstantTimeComparison(byte[] passwordGuess, byte[] actualPassword)
      {
         uint difference = (uint)passwordGuess.Length ^ (uint)actualPassword.Length;
         for (var i = 0; i < passwordGuess.Length && i < actualPassword.Length; i++)
         {
            difference |= (uint)(passwordGuess[i] ^ actualPassword[i]);
         }

         return difference == 0;
      }
      #endregion

      #region " PUBLIC METHODS "
      public static string Create(string stringToHash)
      {
         var salt = GenerateRandomSalt();
         var iterationCount = 7;//GetIterationCount();
         var hashValue = GenerateHashValue(stringToHash, salt, iterationCount);
         var iterationCountBtyeArr = BitConverter.GetBytes(iterationCount);
         var valueToSave = new byte[SaltByteLength + DerivedKeyLength + iterationCountBtyeArr.Length];
         Buffer.BlockCopy(salt, 0, valueToSave, 0, SaltByteLength);
         Buffer.BlockCopy(hashValue, 0, valueToSave, SaltByteLength, DerivedKeyLength);
         Buffer.BlockCopy(iterationCountBtyeArr, 0, valueToSave, salt.Length + hashValue.Length, iterationCountBtyeArr.Length);
         return Convert.ToBase64String(valueToSave);
      }

      public static bool Verify(string stringGuess, string hashToCompare)
      {
         //ingredient #1: password salt byte array
         var salt = new byte[SaltByteLength];

         //ingredient #2: byte array of password
         var actualPasswordByteArr = new byte[DerivedKeyLength];

         //convert actualSavedHashResults to byte array
         var actualSavedHashResultsBtyeArr = Convert.FromBase64String(hashToCompare);

         //ingredient #3: iteration count
         var iterationCountLength = actualSavedHashResultsBtyeArr.Length - (salt.Length + actualPasswordByteArr.Length);
         var iterationCountByteArr = new byte[iterationCountLength];
         Buffer.BlockCopy(actualSavedHashResultsBtyeArr, 0, salt, 0, SaltByteLength);
         Buffer.BlockCopy(actualSavedHashResultsBtyeArr, SaltByteLength, actualPasswordByteArr, 0, actualPasswordByteArr.Length);
         Buffer.BlockCopy(actualSavedHashResultsBtyeArr, (salt.Length + actualPasswordByteArr.Length), iterationCountByteArr, 0, iterationCountLength);
         var passwordGuessByteArr = GenerateHashValue(stringGuess, salt, BitConverter.ToInt32(iterationCountByteArr, 0));
         return ConstantTimeComparison(passwordGuessByteArr, actualPasswordByteArr);
      }

      public static string ToHash(this string strToHash)
      {
         return Create(strToHash);
      }
      #endregion
   }
}
