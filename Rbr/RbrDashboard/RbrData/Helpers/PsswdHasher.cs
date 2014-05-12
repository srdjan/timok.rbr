using System;
using System.Security.Cryptography;
using System.Text;
using RbrCommon;

namespace RbrData.Helpers {
	public class PsswdHasher {
		readonly string salt;
		readonly string hashedPwd;
		public string Value { get { return hashedPwd; } }

		public static PsswdHasher FromClearPwd(string pClearPassword, string pSalt) {
			var _salt = pSalt;
			var _hashedPwd = _calculateHash(pClearPassword, _salt);
			return new PsswdHasher(_hashedPwd, _salt);
		}

		public static PsswdHasher FromSaltHashedPwd(string pSaltHashedPassword, string pSalt) {
			return new PsswdHasher(pSaltHashedPassword, pSalt);
		}

		public static string CreateRandomSalt() {
			// Generate a cryptographic random number using the cryptographic service provider
			var _rng = new RNGCryptoServiceProvider();
			var _buff = new byte[21];
			_rng.GetBytes(_buff);
      
			// Return a Base64 string representation of the random number
			return Convert.ToBase64String(_buff);
		}

		public bool Verify(string pClearPassword) {
			var _saltHashedPwd = _calculateHash(pClearPassword, salt);
			var _result = hashedPwd.Equals(_saltHashedPwd);
			if (_result) {
				return true;
			}
			Logger.Log(string.Format("SaltHashedpasswd.Verify: Password in: {0}, {1}", _saltHashedPwd, hashedPwd));
			return false;
		}

		//----------------------------------  privates -----------------------------------------
		PsswdHasher(string pSaltHashedPassword, string pSalt) {
			salt = pSalt;
			hashedPwd = pSaltHashedPassword;
		}

		static string _calculateHash(string pClearPassword, string pSalt) {
			var _data = _toByteArray(pSalt + pClearPassword);
			var _bhash = calculateHash(_data);
			return Convert.ToBase64String(_bhash);
		}

		static byte[] calculateHash(byte[] data) {
			return new SHA1CryptoServiceProvider().ComputeHash(data);
		}

		static byte[] _toByteArray(string s) {
			return Encoding.UTF8.GetBytes(s);
		}
	}
}