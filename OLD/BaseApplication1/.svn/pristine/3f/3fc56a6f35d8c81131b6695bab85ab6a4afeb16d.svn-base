#region References

using MYB.BaseApplication.Security.Configuration;
using System;
using System.Security.Cryptography;

#endregion References

namespace MYB.BaseApplication.Infrastructure.Security
{
	public class KeyManagement
	{
		//private static readonly string testKey  = "zdQDmXN55cNNX8yTdHCPg4xGcfw/IVuJvl6GSV81ctc=";
		//private static readonly string salt = "dd2fXjowjdG5Ba8SD26HSw==";
		public static byte[] GetSignKey()
		{
			//replace this code later to get the kew from the correct place
			//Have in mind that it should be a shared cluster key
			return Convert.FromBase64String(SecuritySettings.GetSecuritySection().SignKey);
		}

		public static byte[] GetEncryptionKey()
		{
			//replace this code later to get the kew from the correct place
			//Have in mind that it should be a shared cluster key
			return Convert.FromBase64String(SecuritySettings.GetSecuritySection().EncKey);
		}

		public static byte[] GetIV()
		{
			byte[] IV = new Byte[16];
			// The array is now filled with cryptographically strong random bytes.
			RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();
			_rngProvider.GetBytes(IV);

			return IV;
		}
	}
}