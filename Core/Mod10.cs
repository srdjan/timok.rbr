using System;

namespace Timok.Core
{
	public class Mod10 {
		// return a 0 as a valid check digit
		static public bool IsValidCheckDigit(string pInputNumber) {
			int _cd = 0, _b, _t, _numLength = pInputNumber.Length - 1;
			for (int _i = _numLength; _i >= 0; _i--) {	
				_b = Int32.Parse(pInputNumber[_i].ToString());
				_t = ((_numLength - _i) % 2 == 0) ? _b : _b * 2;
				_cd += (_t > 9) ? _t - 9 : _t;
			}
			return (_cd % 10 == 0)? true : false;
		}

		//
		//Reverse the number 
		//Multiply all the digits in odd positions (The first digit, the third digit, etc) by 2. 
		//If any one is greater than 9 subtract 9 from it. 
		//Sum those numbers up 
		//Add the even numbered digits (the second, fourth, etc) to the number you got in the previous step 
		//The check digit is the amount you need to add to that number to make a multiple of 10. 
		//
		//So if you got 68 in the previous step the check digit would be 2. 
		//You can calculate the digit in code using checkdigit = ((sum / 10 + 1) * 10 - sum) % 10 
		static public void AddCheckDigit(ref string pInputNumber) {
			int _sum = 0, _current, _temp, _numLength = pInputNumber.Length;
			for (int _i = _numLength-1; _i >= 0; _i--) {
				_current = Int32.Parse(pInputNumber[_i].ToString());
				
				_temp = ((_numLength - _i) % 2 == 0) ? _current : _current * 2;

				_sum += (_temp > 9) ? _temp - 9 : _temp;
			}
			int _checkdigit = ((_sum / 10 + 1) * 10 - _sum) % 10;
			pInputNumber += _checkdigit.ToString();
		}
	}
}
