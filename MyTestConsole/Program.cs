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
            int startCd = Convert.ToInt32(args[1]);
            int numberOfCDs = Convert.ToInt32(args[2]);
            StreamWriter logFile = new StreamWriter(args[0] + startCd.ToString() + "Log.txt");
     
            for (int CdNumber = startCd; CdNumber < startCd + numberOfCDs ; CdNumber++)
            {
                string Arguments = args[3] + " " + args[4];
                var RobotOperator = new RobotDriveController();
                RobotOperator.GetInputCD(Arguments);
                RobotOperator.CopyCdContent(args[0], CdNumber.ToString(), logFile);
                RobotOperator.DipositOutputCD(Arguments);  
            }
            logFile.Close();
        }
    }
}