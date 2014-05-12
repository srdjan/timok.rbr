using System;
using System.Collections;
using System.Text;

namespace Timok.Rbr.BLL.ImportExport {
  public class DialCodeUtil {

    private DialCodeUtil() { }

    public static void DoCompaction(ref long[] pDialCodeNumbers) {
      while (compact(ref pDialCodeNumbers)) { /* DO NOTHING */		}
    }


    //---------------- Private --------------------------------------------
    private static bool compact(ref long[] pDialCodeNumbers) {
      ArrayList _compactedDialCodeNumbers = new ArrayList();

      bool _success = false;
      int _i = 0;
      while (_i < pDialCodeNumbers.Length) {
        if (pDialCodeNumbers[_i].ToString().EndsWith("0") && (pDialCodeNumbers.Length - _i) >= 10) {
          if (compactRange(pDialCodeNumbers, _i)) {
            long _dialCodeNumber = long.Parse(pDialCodeNumbers[_i].ToString().Substring(0, pDialCodeNumbers[_i].ToString().Length - 1));
            _compactedDialCodeNumbers.Add(_dialCodeNumber);
            _i += 10;
            _success = true;
          }
          else {
            _compactedDialCodeNumbers.Add(pDialCodeNumbers[_i]);
            _i++;
          }
        }
        else {
          _compactedDialCodeNumbers.Add(pDialCodeNumbers[_i]);
          _i++;
        }
      }
      pDialCodeNumbers = (long[]) _compactedDialCodeNumbers.ToArray(typeof(long));
      return _success;
    }

    private static bool compactRange(long[] pDialCodeNumbers, int pStartIndex) {
      long _saved = pDialCodeNumbers[pStartIndex];
      for (int _i = pStartIndex + 1; _i < pStartIndex + 10; _i++) {
        if ((_saved + 1) != pDialCodeNumbers[_i]) {
          return false;
        }
        _saved = pDialCodeNumbers[_i];
      }
      return true;
    }
  }
}