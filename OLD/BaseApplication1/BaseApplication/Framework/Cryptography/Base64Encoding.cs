#region References

using System;
using System.Text;

#endregion References

namespace MYB.BaseApplication.Framework.Cryptography
{
	public static class Base64Encoder
	{
		public static string Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		public static string Encode(byte[] UTF8PlainText)
		{
			return Convert.ToBase64String(UTF8PlainText);
		}

		public static string Decode(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
}