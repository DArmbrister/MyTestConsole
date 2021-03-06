﻿using System;
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
            String[] commandArray = new String[] {" --open"," --command=I"," --close"};
            foreach (string command in commandArray)
            {
                var proc = new Process();
                proc.StartInfo.FileName = "ULCLI.exe";
                proc.StartInfo.Arguments = Arguments + command;
                proc.Start();
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
                proc.Close();
            }
            System.Threading.Thread.Sleep(15000);
        }

        public void DipositOutputCD(string Arguments)
        {
            String[] commandArray = new String[] { 
                " --open", " --command=G", " --close", " --command=A" };
            foreach (string command in commandArray)
            {
                var proc = new Process();
                proc.StartInfo.FileName = "ULCLI.exe";
                proc.StartInfo.Arguments = Arguments + command;
                proc.Start();
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
                proc.Close();
            }
        }
        public bool CheckDriveIsValid(string driveName)
        {
            string drvName = driveName.Substring(Math.Max(0, driveName.Length - 2));
            DriveInfo[] collectionOfDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in collectionOfDrives)
            {
                if (drive.Name == drvName)
                {
                    if (drive.DriveType == DriveType.CDRom)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GetDriveStatus(string driveName)
        {
            string drvName = driveName.Substring(Math.Max(0, driveName.Length - 2));
            DriveInfo[] collectionOfDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in collectionOfDrives)
            {
                if (drive.Name == drvName)
                {
                    if (drive.DriveType == DriveType.CDRom)
                    {
                        if (drive.IsReady == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void CopyCdContent(string baseFilePath, string CdNumber,StreamWriter logFile,string driveName)
        {
            string originalFilePath = null;
            var filePath = Path.Combine(baseFilePath, CdNumber + "\\");
            var yay = System.IO.DriveInfo.GetDrives();
            string driveDirectory = null;
            if (GetDriveStatus(driveName) == true)
            {
                driveDirectory = driveName;
            }
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
