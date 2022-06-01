#region References

using System;
using System.Security.Cryptography;
using System.Text;

#endregion References

namespace MYB.BaseApplication.Framework.Cryptography
{
	public class RijndaelCipher
	{
		private const string DefaultPasskey = "ekwCCY/Tqns39KNTwC5kPefN8SwfU1+cylFvU9Rcy+XEI7q5v/NcoGwpsIZddd3h4lfpljSpHAzj7q6kHzvwFg==";

		private readonly RijndaelManaged _cipher = new RijndaelManaged();

		/// <summary>
		///   Creates an instance using the default pass key.
		///   This should only be used by testing purposes.
		///   An application specific passKey must be used on production enviroments.
		/// </summary>
		public RijndaelCipher() : this(DefaultPasskey)
		{
		}

		/// <summary>
		///   Creates an instance using the specified passKey.
		/// </summary>
		/// <param name="passKey"> Free text to use as base to create the criptographic key </param>
		public RijndaelCipher(string passKey)
		{
			this._cipher.GenerateKey();
			this._cipher.GenerateIV();
			var passKeyBytes = Encoding.Default.GetBytes(passKey);
			var criptoKey = new MD5CryptoServiceProvider().ComputeHash(passKeyBytes);

			_cipher.BlockSize = 128;
			_cipher.Mode = CipherMode.ECB;
			_cipher.Padding = PaddingMode.Zeros;
			_cipher.Key = criptoKey;
		}

		/// <summary>
		///   Encrypts the specified text
		/// </summary>
		/// <param name="textToEncrypt"> Candidate text to encrypt </param>
		/// <returns> Encrypted text </returns>
		public string Encrypt(string textToEncrypt)
		{
			var textToEncryptBytes = Encoding.Default.GetBytes(textToEncrypt);
			var encryptedBytes = CryptoTransformer.GetTransformedBytes(textToEncryptBytes, _cipher.CreateEncryptor());
			var encryptedText = Convert.ToBase64String(encryptedBytes);

			return encryptedText;
		}

		/// <summary>
		///   Encrypts the specified text
		/// </summary>
		/// <param name="textToEncrypt"> Candidate text to encrypt </param>
		/// <param name="key"> secret key 256 bits</param>
		/// <param name="IV"> SALT 128 bits </param>
		/// <returns> Encrypted text </returns>
		public byte[] Encrypt(string textToEncrypt, byte[] key, byte[] IV)
		{
			var keys = new Rfc2898DeriveBytes(key, IV, 1000);
			_cipher.Key = keys.GetBytes(_cipher.KeySize / 8);
			_cipher.IV = keys.GetBytes(_cipher.BlockSize / 8); ;
			return CryptoTransformer.GetTransformedBytes(Encoding.UTF8.GetBytes(textToEncrypt), _cipher.CreateEncryptor());
		}

		public byte[] Decrypt(byte[] textToDecrypt, byte[] key, byte[] IV)
		{
			var keys = new Rfc2898DeriveBytes(key, IV, 1000);
			_cipher.Key = keys.GetBytes(_cipher.KeySize / 8);
			_cipher.IV = keys.GetBytes(_cipher.BlockSize / 8); ;
			return CryptoTransformer.GetTransformedBytes(textToDecrypt, _cipher.CreateDecryptor());
		}

		/// <summary>
		///   Encrypts the specified text, using the specified passKey
		/// </summary>
		/// <param name="textToEncrypt"> Candidate text to encrypt </param>
		/// <param name="passKey"> Specific passKey to use only on this call </param>
		/// <returns> Encrypted text </returns>
		public static string Encrypt(string textToEncrypt, string passKey)
		{
			var cipher = new RijndaelCipher(passKey);
			return cipher.Encrypt(textToEncrypt);
		}

		/// <summary>
		///   Decrypts the specified encrypted text
		/// </summary>
		/// <param name="textToDecrypt"> Encrypted text to decrypt </param>
		/// <returns> Original text </returns>
		public string Decrypt(string textToDecrypt)
		{
			var encryptedBytes = Convert.FromBase64String(textToDecrypt);
			var decryptedBytes = CryptoTransformer.GetTransformedBytes(encryptedBytes, _cipher.CreateDecryptor());
			var decryptedText = Encoding.Default.GetString(decryptedBytes).TrimEnd(new[] { '\0' });

			return decryptedText;
		}

		/// <summary>
		///   Decrypts the specified ecrypted text, using the specified passKey
		/// </summary>
		/// <param name="textToDecrypt"> Encrypted text to decrypt </param>
		/// <param name="passKey"> passKey used to encrypt the original text </param>
		/// <returns> Original text </returns>
		public static string Decrypt(string textToDecrypt, string passKey)
		{
			var cipher = new RijndaelCipher(passKey);
			return cipher.Decrypt(textToDecrypt);
		}
	}
}