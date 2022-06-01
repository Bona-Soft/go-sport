using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MYB.FaltaUno.Model.Entities
{
	public struct Mail
	{
		private string _EmailAddress;

		public string Type;

		public string EmailAddress
		{
			get
			{
				return _EmailAddress;
			}
			set
			{
				if (IsValidEmail(value))
					_EmailAddress = value;
				else
					throw new Exception("Invalid email format");
			}
		}

		public string Domain
		{
			get
			{
				if (EmailAddress != null)
					return EmailAddress.Split('@')[1];
				return null;
			}
		}

		public static bool IsValidEmail(string strIn)
		{
			if (String.IsNullOrEmpty(strIn))
				return false;

			// Use IdnMapping class to convert Unicode domain names.
			try
			{
				strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
											 RegexOptions.None, TimeSpan.FromMilliseconds(200));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}

			if (strIn == "")
				return false;

			// Return true if strIn is in valid e-mail format.
			try
			{
				return Regex.IsMatch(strIn,
						@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
						@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
						RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		private static string DomainMapper(System.Text.RegularExpressions.Match match)
		{
			// IdnMapping class with default property values.
			IdnMapping idn = new IdnMapping();

			string domainName = match.Groups[2].Value;
			try
			{
				domainName = idn.GetAscii(domainName);
			}
			catch (ArgumentException)
			{
				return "";
			}
			return match.Groups[1].Value + domainName;
		}
	}
}