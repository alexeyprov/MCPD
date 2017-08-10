using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PwdEncryption
{
	public class CryptoUtility
	{
		#region Data Members
		private static SymmetricAlgorithm _alg = new RijndaelManaged();
		#endregion

		#region Public API
		public static void Encrypt(string infile, string outfile, string pwd)
		{
			DoCryptoTransform(infile, outfile, pwd, true);
		}

		public static void Decrypt(string infile, string outfile, string pwd)
		{
			try
			{
				DoCryptoTransform(infile, outfile, pwd, false);
			}
			catch (CryptographicException ce)
			{
				throw new WrongPasswordException("Password is wrong", ce);
			}
		}
		#endregion

		#region Implementation
		private static void DoCryptoTransform(string infile, string outfile, string pwd, bool isEncryption)
		{
			InitAlgorithm(pwd);
			try
			{
				ICryptoTransform ct = (isEncryption) ?  _alg.CreateEncryptor() :
					_alg.CreateDecryptor();

				Stream ifs = new FileStream(infile, FileMode.Open, FileAccess.Read);
				// TODO: try/catch and dispose ifs if needed
				Stream ofs = new FileStream(outfile, FileMode.Create, FileAccess.Write);
				if (isEncryption)
				{
					ofs = new CryptoStream(ofs, ct, CryptoStreamMode.Write);
				}
				else
				{
					ifs = new CryptoStream(ifs, ct, CryptoStreamMode.Read);
				}

				using (ifs)
				{
					using (ofs)
					{
						// Wrap input or output stream depending on the crypto operation

						// Process data by blocks of 4096 bytes
						const int BLOCK_SIZE = 0x1000;
						byte[] buf = new byte[BLOCK_SIZE];

						int bytesRead = ifs.Read(buf, 0, BLOCK_SIZE);
						while (bytesRead != 0)
						{
							ofs.Write(buf, 0, bytesRead);
							bytesRead = ifs.Read(buf, 0, BLOCK_SIZE);
						}
					}
				}
			}
			finally
			{
				FinalizeAlgorithm();
			}
		}

		private static void InitAlgorithm(string password)
		{
			byte[] SALT = { 0x63, 0xF2, 0x7C, 0x21, 0x8A, 0xCD, 0x13, 0x42, 0x7E };
			const int ITERATIONS = 5;

			DeriveBytes keygen = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password),
				SALT, ITERATIONS);

			_alg.Key = keygen.GetBytes(_alg.KeySize / 8);
			_alg.IV = keygen.GetBytes(_alg.BlockSize / 8);

			keygen.Reset();
		}

		private static void FinalizeAlgorithm()
		{
			_alg.Clear();
		}
		#endregion
	}
}
