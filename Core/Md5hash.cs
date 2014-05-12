using System;
using System.Security.Cryptography;
using System.Text;

namespace Timok.Core 
{
	public class Md5hash {
		public static string Get(string pInputStr) {
			byte[] _input = Encoding.UTF8.GetBytes(pInputStr);
			byte[] _output = MD5.Create().ComputeHash(_input);
			return Convert.ToBase64String(_output);
		}
	}
}