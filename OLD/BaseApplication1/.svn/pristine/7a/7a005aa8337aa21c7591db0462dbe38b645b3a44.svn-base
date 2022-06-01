#region References

using System;
using System.IO;
using System.Security.Cryptography;

#endregion References

namespace MYB.BaseApplication.Framework.Cryptography
{
	/// <summary>
	/// Manages a Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function.
	/// The HMAC process mixes a secret key with the message data, hashes the result with the hash function,
	/// mixes that hash value with the secret key again, and then applies the hash function a second time.
	/// The output hash is 256 bits in length.
	/// HMACSHA256 accepts keys of any size, and produces a hash sequence 256 bits in length.
	/// </summary>
	/// <remarks>allows sign and verify both files and data</remarks>
	/// <author>rd 2014.11.03</author>
	public class HMACManagement
	{
		#region constants

		private const int K_BUFFERSIZE = 4096;

		#endregion constants

		#region variables

		private static RNGCryptoServiceProvider _rngProvider;

		#endregion variables

		#region ctor

		static HMACManagement()
		{
			_rngProvider = new RNGCryptoServiceProvider();
		}

		#endregion ctor

		#region hashing methods

		/// <summary>
		/// Create a random key using a random number generator.
		/// This would be the secret key shared by sender and receiver
		/// </summary>
		/// <returns>the secret key</returns>
		public static byte[] CreateSecretKey()
		{
			// Secret key shared by sender and receiver
			byte[] secretkey = new Byte[8];
			// The array is now filled with cryptographically strong random bytes.
			_rngProvider.GetBytes(secretkey);

			return secretkey;
		}

		//64 bits random nonce
		public static byte[] Nonce()
		{
			return CreateSecretKey();
		}

		////HMAC Shared Key
		//public static byte[] GetSignKey()
		//{
		//    // Secret key shared by sender and receiver
		//    byte[] secretkey = new Byte[32];
		//    _rngProvider.GetBytes(secretkey);
		//    // The array is now filled with cryptographically strong random bytes.
		//    return Encoding.UTF8.GetBytes("���İrh?�'S��b���(6�X�Mc�j��");
		//}

		#region array's hashing methods

		/// <summary>
		/// Compute a keyed hash for a plain data
		/// and creates a keyed hash prepended to the contents of the plain data
		/// </summary>
		/// <param name="key">secret key</param>
		/// <param name="plainData">data to sign</param>
		/// <returns>signed plain data</returns>
		public static byte[] Sign(byte[] key, byte[] plainData)
		{
			// Initialize the keyed hash object.
			using (HMACSHA256 hmac = new HMACSHA256(key))
			{
				// Compute the hash of the input plain data
				return hmac.ComputeHash(plainData);
			}
		}

		#endregion array's hashing methods

		#region file's hashing methods

		/// <summary>
		/// Compute a keyed hash for a input file and creates a target file with the keyed hash
		/// prepended to the contents of the output file
		/// </summary>
		/// <param name="key">secret key</param>
		/// <param name="inputFile">input filename</param>
		/// <param name="outputFile">output filename</param>
		public static void SignFile(byte[] key, string inputFile, string outputFile)
		{
			// Initialize the keyed hash object.
			using (HMACSHA256 hmac = new HMACSHA256(key))
			{
				using (FileStream fi = new FileStream(inputFile, FileMode.Open))
				{
					using (FileStream fo = new FileStream(outputFile, FileMode.Create))
					{
						// Compute the hash of the input file.
						byte[] hashValue = hmac.ComputeHash(fi);
						// Reset fi to the beginning of the file.
						fi.Position = 0;
						// Write the computed hash value to the output file.
						fo.Write(hashValue, 0, hashValue.Length);
						// Copy the contents of the inputFile to the outputFile.
						int bytesRead;
						// read 1K at a time
						byte[] buffer = new byte[K_BUFFERSIZE];
						do
						{
							// Read from the wrapping CryptoStream.
							bytesRead = fi.Read(buffer, 0, K_BUFFERSIZE);
							fo.Write(buffer, 0, bytesRead);
						} while (bytesRead > 0);
					}
				}
			}
		}

		/// <summary>
		/// Compares the key in the signed file with a new key created for the data portion of the file.
		/// If the keys compare the data has not been tampered with
		/// </summary>
		/// <param name="key">secret key</param>
		/// <param name="signedFile">signed filename</param>
		/// <returns>true if no tampering occurred; otherwise false</returns>
		public static bool VerifyFile(byte[] key, string signedFile)
		{
			bool result = true;
			// Initialize the keyed hash object.
			using (HMACSHA256 hmac = new HMACSHA256(key))
			{
				// Create an array to hold the keyed hash value read from the file.
				byte[] storedHash = new byte[hmac.HashSize / 8];
				// Create a FileStream for the source file.
				using (FileStream inStream = new FileStream(signedFile, FileMode.Open))
				{
					// Read in the storedHash.
					inStream.Read(storedHash, 0, storedHash.Length);
					// Compute the hash of the remaining contents of the file.
					// The stream is properly positioned at the beginning of the content,
					// immediately after the stored hash value.
					byte[] computedHash = hmac.ComputeHash(inStream);
					// compare the computed hash with the stored value
					int i = 0;
					while (i < storedHash.Length && result)
					{
						if (computedHash[i] != storedHash[i])
							result = false;
						i++;
					}
				}
			}
			return result;
		}

		#endregion file's hashing methods

		#endregion hashing methods
	}
}