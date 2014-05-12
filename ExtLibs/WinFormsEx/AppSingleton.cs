   // © 2004 IDesign Inc. All rights reserved 
   //Questions? Comments? go to 
   //http://www.idesign.net

using System;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormsEx
{
   public class SingletonApp 
   {
      static Mutex m_Mutex;

      public static void Run()
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run();
         }
      }
      public static void Run(ApplicationContext context)
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run(context);
         }
      }
      public static void Run(Form mainForm)
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run(mainForm);
         }
      }
      static bool IsFirstInstance()
      {
         Assembly assembly = Assembly.GetEntryAssembly();
         string name = assembly.FullName.ToString();

         m_Mutex = new Mutex(false,name);
         bool owned = false;
         owned = m_Mutex.WaitOne(TimeSpan.Zero,false);
         return owned ;
      }
      static void OnExit(object sender,EventArgs args)
      {
         m_Mutex.ReleaseMutex();
         m_Mutex.Close();
      }
   }
}
