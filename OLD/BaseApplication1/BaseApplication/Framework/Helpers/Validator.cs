using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace MYB.BaseApplication.Framework.Helpers
{
	public static class Validator
	{

        public static class Options
        {
            public static class ValidationRegex
            {
                public static Regex HasNumber = new Regex(@"[0-9]+");
                public static Regex HasUpperChar = new Regex(@"[A-Z]+");
                public static Regex HasMinMaxChars = new Regex(@"^.{"+ValidationOptions.MinValue+","+ValidationOptions.MaxValue+"}$");
                public static Regex HasLowerChar = new Regex(@"[a-z]+");
                public static Regex HasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            }

            public static class ValidationFlags
            {
                public static bool ValidateNumber = true;
                public static bool ValidateUpperChar = true;
                public static bool ValidateMinMaxChars = true;
                public static bool ValidateLowerChar = true;
                public static bool ValidateSymbols = false;
            }

            public static class ValidationOptions
            {
                public static int MinValue = 8;
                public static int MaxValue = 32;
            }

        }
		public static bool IsMail(string emailAddress)
		{
			try
			{
				return new EmailAddressAttribute().IsValid(emailAddress);
			}
			catch
			{
				return false;
			}
		}

		public static bool ValidatePassword(string password, out Tuple<int, string> ErrorMessage)
		{
			var input = password;
			ErrorMessage = new Tuple<int, string>(default(int), String.Empty);

			if (string.IsNullOrWhiteSpace(input))
			{
                ErrorMessage = new Tuple<int, string>(6, "Password can not be empty");
                return false;
            }

            var hasNumber = Options.ValidationRegex.HasNumber;
            var hasUpperChar = Options.ValidationRegex.HasUpperChar;
            var hasMinMaxChars = Options.ValidationRegex.HasMinMaxChars;
            var hasLowerChar = Options.ValidationRegex.HasLowerChar;
            var hasSymbols = Options.ValidationRegex.HasSymbols;

            if (!hasLowerChar.IsMatch(input) && Options.ValidationFlags.ValidateLowerChar)
            {
                ErrorMessage = new Tuple<int, string>(1, "Password must contain at least one lower case letter");
                return false;
            }
            else if (!hasUpperChar.IsMatch(input) && Options.ValidationFlags.ValidateUpperChar)
            {
                ErrorMessage = new Tuple<int, string>(2, "Password must contain at least one upper case letter");
                return false;
            }
            else if (!hasMinMaxChars.IsMatch(input) && Options.ValidationFlags.ValidateMinMaxChars)
            {
                ErrorMessage = new Tuple<int, string>(3, "Password must not be less than 8 characters or greater than 32 characters");
                return false;
            }
            else if (!hasNumber.IsMatch(input) && Options.ValidationFlags.ValidateNumber)
            {
                ErrorMessage = new Tuple<int, string>(4, "Password must contain at least one numeric value");
                return false;
            }
            else if (!hasSymbols.IsMatch(input) && Options.ValidationFlags.ValidateSymbols)
            {
                ErrorMessage = new Tuple<int, string>(5, "Password must contain At least one special case characters");
                return false;
            }
            else
            {
                return true;
            }
		}

		public static bool isCreditCard(string cardNumber)
		{
			int iAux;
			bool bAux;
			int checkSum;
			bool result = false;

			result = (cardNumber.Length >= 15 && cardNumber.Length <= 16) ? true : false;
			if (result)
			{
				bAux = false;
				checkSum = 0;
				for (int i = cardNumber.Length - 1; i >= 0; i--)
				{
					if (!Int32.TryParse(cardNumber[i].ToString(), out iAux))
					{
						return false;
					}

					if (iAux < 0)
						return false;
					if (bAux)
					{
						iAux = iAux * 2;
						if (10 <= iAux)
							iAux = iAux - 9;
					}
					checkSum = checkSum + iAux;
					bAux = !bAux;
				}
				result = ((checkSum % 10) == 0);
			}

			return result;
		}

		public static bool isInteger(string intString)
		{
			int iAux;
			return Int32.TryParse(intString.Trim(), out iAux);
		}

		public static bool isInteger64(string intString)
		{
			long iAux;
			return Int64.TryParse(intString.Trim(), out iAux);
		}

		public static bool isDecimal(string decimalString)
		{
			decimal dAux;
			return Decimal.TryParse(decimalString.Trim(), out dAux);
		}

		public static bool isValidCuit(string cuit, bool permiteVacio)
		{
			bool result = false;
			int iAux;
			decimal floatAux;
			int[] arrValues = { 20, 23, 24, 27, 30, 33, 34, 55 };

			cuit = cuit.Trim().Replace("-", "").Replace(".", "");
			result = (cuit.Length == 0) && permiteVacio;

			if (!result)
			{
				result = (cuit.Length == 11) && isInteger64(cuit);
				if (result)
				{
					iAux = Convert.ToInt32(cuit[0].ToString() + cuit[1].ToString());
					result = (arrValues.Contains(iAux));
					if (result)
					{
						iAux = Convert.ToInt32(cuit[0].ToString()) * 5;
						iAux = iAux + Convert.ToInt32(cuit[1].ToString()) * 4;
						iAux = iAux + Convert.ToInt32(cuit[2].ToString()) * 3;
						iAux = iAux + Convert.ToInt32(cuit[3].ToString()) * 2;
						iAux = iAux + Convert.ToInt32(cuit[4].ToString()) * 7;
						iAux = iAux + Convert.ToInt32(cuit[5].ToString()) * 6;
						iAux = iAux + Convert.ToInt32(cuit[6].ToString()) * 5;
						iAux = iAux + Convert.ToInt32(cuit[7].ToString()) * 4;
						iAux = iAux + Convert.ToInt32(cuit[8].ToString()) * 3;
						iAux = iAux + Convert.ToInt32(cuit[9].ToString()) * 2;
						iAux = iAux + Convert.ToInt32(cuit[10].ToString()) * 1;
						floatAux = iAux / 11;
						result = (floatAux == Math.Floor(floatAux));
					}
				}
			}
			return result;
		}
	}
}