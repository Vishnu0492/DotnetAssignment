
using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace InvokePythonScriptAssignment4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Path to the Python script
            string scriptPath = @"C:\Users\Administrator\source\repos\day21\InvokePythonScriptAssignment4";


            // Define the numbers to sum
            int num1 = 5;
            int num2 = 10;
            // Creating a new process to invoke the Python script

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "C:\\Users\\Administrator\\AppData\\Local\\Microsoft\\WindowsApps\\python.exe";
                process.StartInfo.Arguments = $"\"{scriptPath}\" {num1} {num2}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                try
                {

                    process.Start();



                    // Capture the output
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                    else
                    {
                        Console.WriteLine($"The sum is: {output.Trim()}"); //Displaying the output
                    }


                }


                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            
            }
        }
    }
}
