using System.Text.RegularExpressions;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Timok.Core {

	public class EmailValidator {
    //Refs:
    //http://regexlib.com/Search.aspx , search for "" in Category: "Email", MinRating: "The Best"
    //http://www.cambiaresearch.com/cambia3/snippets/csharp/regex/email_regex.aspx

    //Lenient: private const	string eMailRegExprPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
    //Strict #1: private const string eMailRegExprPattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

    //Strict #2: for a single email address
    private const string eMailRegExprPattern = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

		private static Regex regEx = new Regex(eMailRegExprPattern, RegexOptions.Compiled);

		private EmailValidator() {}

    //NOTE: will validate only one(1) email address at the time
    public static bool Validate(string pEmailAddress, bool pAllowEmpty) {
      if (pEmailAddress == null || pEmailAddress.Length == 0) {
        return pAllowEmpty;
      }

      if (regEx.IsMatch(pEmailAddress)) {
        return true;
      }
      else {
        return false;
      }
    }

    //NOTE: will validate only one(1) email address at the time
		public static bool ValidateAndFormat(string pEmailAddresses, bool pAllowEmpty, out string pFormatedEmailAddresses) {
      pFormatedEmailAddresses = string.Empty;
      if (pEmailAddresses == null || pEmailAddresses.Length == 0) {
        return pAllowEmpty;
      }

      string _emailString = pEmailAddresses.Replace(';', ',').Trim().Trim(',');
      string[] _emails = _emailString.Split(',');
      if (_emails.Length == 0 && !pAllowEmpty) {
        return false;
      }

      List<string> _validEmails = new List<string>();
      foreach (string _email in _emails) {
				if (EmailValidator.Validate(_email.Trim(), pAllowEmpty)) {
					_validEmails.Add(_email.Trim());
				}
				else {
					return false;
				}        
      }

      //NOTE: join in this format "r1@ddd.com,r2@ddd.com" that's with comma [NO space]
      pFormatedEmailAddresses = string.Join(",", _validEmails.ToArray());
      return true;
		}
	}
}
