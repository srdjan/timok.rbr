// © 2004 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;

   
namespace WinFormsEx
{
	/// <summary>
	/// Provides thread-safe access to some methods and properties
	/// </summary>
   public class SafeLabel : Label
   {
      delegate void SetString(string text);
      delegate string GetString();

      override public string Text
      {
         set
         {
            if(InvokeRequired)
            {
               SetString setTextDel = new SetString(SetText); 
               try
               {
                  Invoke(setTextDel,new object[]{value});
               }
               catch{}
            }
            else
            {
               base.Text = value;
            }
         }
         get
         {
            if(InvokeRequired)
            {
               GetString getTextDel = new GetString(GetText); 
               string text = String.Empty;
               try
               {
                  text = (string)Invoke(getTextDel,null);
               }
               catch{}
               return text;
            }
            else
            {
               return base.Text;
            }
         }
      }
      void SetText(string text)
      {
         base.Text = text;
      }
      string GetText()
      {
         return base.Text;
      }
   }
}
