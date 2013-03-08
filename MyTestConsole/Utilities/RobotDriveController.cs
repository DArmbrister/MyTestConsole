using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace MyTestConsole.Utilities
{
    class RobotDriveController
    {

        public void GetInputCD(string Arguments)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "ULCLI.exe";
            proc.StartInfo.Arguments = Arguments + " --open --command=I --close";
            proc.Start();
            proc.WaitForExit();
            var exitCode = proc.ExitCode;
            proc.Close();
            System.Threading.Thread.Sleep(15000);
        }

        public void DipositOutputCD(string Arguments)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "ULCLI.exe";
            proc.StartInfo.Arguments = Arguments + " --open --command=G --close --command=A";
            proc.Start();
            proc.WaitForExit();
            var exitCode = proc.ExitCode;
            proc.Close();
        }

        public string GetDriveStatus()
        {
            DriveInfo[] collectionOfDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in collectionOfDrives)
            {
                if (drive.DriveType == DriveType.CDRom)
                {
                    if (drive.IsReady == true)
                    {
                        return drive.RootDirectory.ToString();
                    }
                }
            }
            return null;
        }

        public void CopyCdContent(string baseFilePath, string CdNumber,StreamWriter logFile)
        {
            string originalFilePath = null;
            var filePath = Path.Combine(baseFilePath, CdNumber+"\\");
            var yay = System.IO.DriveInfo.GetDrives();
            var driveDirectory = GetDriveStatus();
            if (driveDirectory != null)
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                try
                {
                    foreach (string dirPath in System.IO.Directory.EnumerateDirectories(
                        driveDirectory, "*", System.IO.SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(driveDirectory, filePath));
                    }
                    foreach (string newPath in Directory.GetFiles(
                        driveDirectory, "*", SearchOption.AllDirectories))
                    {
                        originalFilePath = newPath;
                        File.Copy(newPath, newPath.Replace(driveDirectory, filePath), true);
                    }
                    Console.WriteLine("CD Number " + CdNumber.ToString() + " is done copying");
                    logFile.WriteLine(DateTime.Now);
                    logFile.WriteLine("Succesful copy of CD " + CdNumber.ToString());
                    logFile.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Error during copy of " + CdNumber + " " + originalFilePath);
                    logFile.WriteLine(DateTime.Now);
                    logFile.WriteLine("Error during copy of " + CdNumber + " " + originalFilePath);
                    logFile.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Error: Could not find Cd " + CdNumber);
                logFile.WriteLine(DateTime.Now);
                logFile.WriteLine("Error: Could not find Cd " + CdNumber);
                logFile.WriteLine();
            }

            
        }
    }
}
