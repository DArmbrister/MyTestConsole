using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MyTestConsole.Utilities;
using System.IO;

namespace MyTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            if (args[0] != "*" + "\\")
            {
                args[0] = args[0] + "\\";
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("First argument was an invalid directory");
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(0);
            }

            try
            {
                int dummy = Convert.ToInt32(args[1]);
            }
            catch
            {
                Console.WriteLine("Second argument must be an integer.");
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(0);
            }

            int startCd = Convert.ToInt32(args[1]);
            StreamWriter logFile = new StreamWriter(args[0] + startCd.ToString() + "Log.txt");
            string Arguments = args[2] + " " + args[3];
            var RobotOperator = new RobotDriveController();
            int i = 0;

            if (RobotOperator.CheckDriveIsValid(args[3]) == false)
            {
                Console.WriteLine("4th argument must be a valid Cd drive.");
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(0);
            }

            if (RobotOperator.GetDriveStatus(args[3]) == false)
            {
                RobotOperator.GetInputCD(Arguments);
            }

            while (RobotOperator.GetDriveStatus(args[3]) == true)
            {
                int CdNumber = startCd + i;
                RobotOperator.CopyCdContent(args[0], CdNumber.ToString(), logFile, args[3]);
                RobotOperator.DipositOutputCD(Arguments);
                RobotOperator.GetInputCD(Arguments);
                i++;
            }
            logFile.Close();
        }
    }
}