/*
 * Copyright © 2005, Patrik Bohman
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */

using System;
using System.Runtime.InteropServices;  
using System.Drawing;


namespace Pabo.Calendar
{
	
	/// <summary>
	/// Summary description for ThemeManager.
	/// </summary>
	public class ThemeManager
	{
		
		
		public ThemeManager()
		{
			  
		}

		internal bool _IsAppThemed()
		{
			try
			{
				// Check which version of ComCtl32 thats in use..
				NativeMethods.DLLVERSIONINFO version = new NativeMethods.DLLVERSIONINFO();
				version.cbSize = Marshal.SizeOf(typeof(NativeMethods.DLLVERSIONINFO));
								
				int ret = NativeMethods.DllGetVersion(ref version);
				// If MajorVersion > 5 themes are allowed.
				if (version.dwMajorVersion >= 6) 			
					return true;
				else
					return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal void _CloseThemeData(IntPtr hwnd)
		{
			try
			{
				NativeMethods.CloseThemeData(hwnd);
			}
			catch (Exception)
			{
				
			}
		}
		
		internal IntPtr _OpenThemeData(IntPtr hwnd, string classes)
		{
			try
			{
				return NativeMethods.OpenThemeData(hwnd, classes );
			}
			catch (Exception)
			{
				return System.IntPtr.Zero;
			}
		}

		internal int _GetWindowTheme(IntPtr hwnd)
		{
			try
			{
				return NativeMethods.GetWindowTheme(hwnd);
			}
			catch (Exception)
			{
				return -1;
			}

		}

		internal bool _IsThemeActive()
		{
			try
			{
				return NativeMethods.IsThemeActive();
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal Color _GetThemeColor ( IntPtr hTheme, int partID, int stateID,int propID )
		{
			int color;

			try 
			{
				NativeMethods.GetThemeColor ( hTheme, partID, stateID, propID, out color );
				return ColorTranslator.FromWin32 ( color );
			}
			catch (Exception) 
			{
				return Color.Empty;
			}
		}
	}
}
