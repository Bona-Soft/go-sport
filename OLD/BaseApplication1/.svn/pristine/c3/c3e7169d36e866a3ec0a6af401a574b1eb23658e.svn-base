#region References

using System.IO;
using System.Security.Cryptography;

#endregion References

namespace MYB.BaseApplication.Framework.Cryptography
{
	/// <summary>
	///   Holder class for ICryptoTransform calling methods
	/// </summary>
	internal class CryptoTransformer
	{
		/// <summary>
		///   Calls ICryptoTransform transformation
		/// </summary>
		/// <param name="bytesToTransform"> Candidate byte array to transform </param>
		/// <param name="transform"> ICryptoTransform implementation to execute </param>
		/// <returns> Transformed byte array </returns>
		public static byte[] GetTransformedBytes(byte[] bytesToTransform, ICryptoTransform transform)
		{
			byte[] transformedBytes;

			using (var memStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
					cryptoStream.FlushFinalBlock();
					cryptoStream.Close();
				}
				transformedBytes = memStream.ToArray();
				memStream.Close();
			}

			return transformedBytes;
		}
	}
}