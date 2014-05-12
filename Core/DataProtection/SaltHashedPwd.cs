using System;
using System.Security.Cryptography;
using System.Text;

namespace Timok.Core.DataProtection {
  public sealed class SaltHashedPwd {
    readonly string salt;
    readonly string hashedPwd;
    //private const int saltLength = 6;

    //public string Salt { get { return _salt; } }
    public string Value {
      get { return hashedPwd; }
    }

    public static SaltHashedPwd FromClearPwd(string pClearPassword, string pSalt) {
      var _salt = pSalt;
      var _hashedPwd = _calculateHash(pClearPassword, _salt);
      return new SaltHashedPwd(_hashedPwd, _salt);
    }

    public static SaltHashedPwd FromSaltHashedPwd(string pSaltHashedPassword, string pSalt) {
      return new SaltHashedPwd(pSaltHashedPassword, pSalt);
    }

    public static string CreateRandomSalt() {
      // Generate a cryptographic random number using the cryptographic
      // service provider
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
      return false;
    }

    //--------------------------- privates
    SaltHashedPwd(string pSaltHashedPassword, string pSalt) {
      salt = pSalt;
      hashedPwd = pSaltHashedPassword;
    }

    static string _calculateHash(string pClearPassword, string pSalt) {
      var _data = _toByteArray(pSalt + pClearPassword);
      var _bhash = _calculateHash(_data);
      return Convert.ToBase64String(_bhash);
    }

    static byte[] _calculateHash(byte[] pData) {
      return new SHA1CryptoServiceProvider().ComputeHash(pData);
    }

    static byte[] _toByteArray(string pString) {
      return Encoding.UTF8.GetBytes(pString);
    }
  }
}