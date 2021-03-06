using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace KellermanSoftware.CompareNetObjects.Reports
{
    /// <summary>
    /// Output files and launch WinMerge
    /// </summary>
    public class WinMergeReport : BaseDualFileReport
    {
        /// <summary>
        /// Launch the WinMerge
        /// </summary>
        /// <param name="expectedFilePath">The path to write the expected results</param>
        /// <param name="actualFilePath">The path to write the actual results</param>
        public override void LaunchApplication(string expectedFilePath, string actualFilePath)
        {
            if (String.IsNullOrEmpty(Path.GetDirectoryName(expectedFilePath)))
                expectedFilePath = Path.Combine(FileHelper.GetCurrentDirectory(), expectedFilePath);

            if (String.IsNullOrEmpty(Path.GetDirectoryName(actualFilePath)))
                actualFilePath = Path.Combine(FileHelper.GetCurrentDirectory(), actualFilePath);

            string args = string.Format("\"{0}\" \"{1}\"", expectedFilePath, actualFilePath);

            ProcessHelper.Shell(FindWinMerge(), args, ProcessWindowStyle.Normal, false);
        }

        private string FindWinMerge()
        {
            //It should be in the Program Files (x86) directory
            string programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            if (!String.IsNullOrEmpty(programFilesPath))
            {
                string[] directories = Directory.GetDirectories(programFilesPath, "WinMerge");

                foreach (var directory in directories.OrderByDescending(o => o))
                {
                    string[] files = Directory.GetFiles(directory, "WinMergeU.exe");

                    if (files.Any())
                        return files.First();
                }
            }

            programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles");

            if (!String.IsNullOrEmpty(programFilesPath))
            {
                string[] directories = Directory.GetDirectories(programFilesPath, "WinMerge");

                foreach (var directory in directories.OrderByDescending(o => o))
                {
                    string[] files = Directory.GetFiles(directory, "WinMergeU.exe");

                    if (files.Any())
                        return files.First();
                }
            }

            throw new FileNotFoundException("WinMergeU.exe");
        }
    }
}
