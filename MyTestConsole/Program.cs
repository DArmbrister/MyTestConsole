using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MyTestConsole.Utilities;

namespace MyTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           string Arguments = "--ComPort=COM3 --drive=I:";
           var RobotOperator = new RobotDriveController();
           RobotOperator.GetInputCD(Arguments);
           RobotOperator.CopyCdContent(args[0],args[1]);
           RobotOperator.DipositOutputCD(Arguments);
        }
    }
}
