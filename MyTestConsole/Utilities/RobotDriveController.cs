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

        public void CopyCdContent(string baseFilePath, string CdNumber)
        {
            var filePath = Path.Combine(baseFilePath, CdNumber+"\\");
            var yay = System.IO.DriveInfo.GetDrives();
            var driveDirectory = GetDriveStatus();
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (string dirPath in System.IO.Directory.EnumerateDirectories(
                driveDirectory, "*", System.IO.SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(driveDirectory, filePath));
            }
            foreach (string newPath in Directory.GetFiles(
                driveDirectory, "*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(driveDirectory, filePath));
            }


            Console.WriteLine("Finished copying CD " + CdNumber);
        }
    }
}
