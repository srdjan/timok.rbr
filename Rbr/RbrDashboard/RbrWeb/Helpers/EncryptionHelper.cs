using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace RbrWeb.Helpers {
	internal static class EncryptionHelper {
		public static Object EncryptObject(object pObject, string pPassword, string pSalt) {
			var _key = getHashKey(pPassword, pSalt);
			var _type = pObject.GetType();
			var _properties = _type.GetProperties();

			object _object = null;
			foreach (var _property in _properties) {
				if (_property.PropertyType == typeof (string)) {
					var _pi = _type.GetProperty(_property.Name, BindingFlags.Public | BindingFlags.Instance);

					try {
						_object = _pi.GetValue(pObject, null);
					}
					catch (Exception _ex) {
						Debug.WriteLine(_ex.ToString());
					}

					var _string = (string) _object;
					_property.SetValue(pObject, encrypt(_key, _string), null);
				}
			}
			return pObject;
		}

		public static Object DecryptObject(object pObject, string pPassword, string pSalt) {
			var _key = getHashKey(pPassword, pSalt);
			var _type = pObject.GetType();
			var _properties = _type.GetProperties();

			object _object = null;
			foreach (var _property in _properties) {
				if (_property.PropertyType == typeof (string)) {
					var _pi = _type.GetProperty(_property.Name, BindingFlags.Public | BindingFlags.Instance);

					try {
						_object = _pi.GetValue(pObject, null);
					}
					catch (Exception _ex) {
						Debug.WriteLine(_ex.ToString());
					}

					var _string = (string) _object;
					_property.SetValue(pObject, decrypt(_key, _string), null);
				}
			}
			return pObject;
		}

		public static string Encrypt(string pStringToEncrypt, string pPassword, string pSalt) {
			var _key = getHashKey(pPassword, pSalt);
			return encrypt(_key, pStringToEncrypt);
		}

		public static string Decrypt(string pStringToDencrypt, string pPassword, string pSalt) {
			var _key = getHashKey(pPassword, pSalt);
			return decrypt(_key, pStringToDencrypt);
		}

		//---------------------------------------------- private ------------------------------------------
		static string encrypt(byte[] pKey, string pDataToEncrypt) {
			// Set key and IV
			var _encryptor = new AesManaged { Key = pKey, IV = pKey };

			// create memory stream
			using (var _encryptionStream = new MemoryStream()) {
				// Create crypto stream
				using (var _encrypt = new CryptoStream(_encryptionStream, _encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
					var data = Encoding.UTF8.GetBytes(pDataToEncrypt);
					_encrypt.Write(data, 0, data.Length);
					_encrypt.FlushFinalBlock();
					_encrypt.Close();

					// Return encrypted data as base64 string
					return Convert.ToBase64String(_encryptionStream.ToArray());
				}
			}
		}

		static string decrypt(byte[] pKey, string pEncryptedString) {
			// Set key and IV
			var _decryptor = new AesManaged {Key = pKey, IV = pKey};

			// convert base64 string to byte array
			var _encryptedData = Convert.FromBase64String(pEncryptedString);

			// create  memory stream
			using (var _decryptionStream = new MemoryStream()) {
				// Create crypto stream
				using (var _decrypt = new CryptoStream(_decryptionStream, _decryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
					_decrypt.Write(_encryptedData, 0, _encryptedData.Length);
					_decrypt.Flush();
					_decrypt.Close();

					// Return unencrypted data
					var _decryptedData = _decryptionStream.ToArray();
					return Encoding.UTF8.GetString(_decryptedData, 0, _decryptedData.Length);
				}
			}
		}

		static byte[] getHashKey(string pHashKey, string pSalt) {
			var _encoder = new UTF8Encoding();
			var _saltBytes = _encoder.GetBytes(pSalt);

			var _rfc = new Rfc2898DeriveBytes(pHashKey, _saltBytes);
			return _rfc.GetBytes(16);
		}
	}
}