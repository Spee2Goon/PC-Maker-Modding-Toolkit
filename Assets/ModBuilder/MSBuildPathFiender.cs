using System;
using System.Diagnostics;

public class MSBuildPathFinder
{
    public static string GetMsBuildPath()
    {
        string vsWhere = Environment.ExpandEnvironmentVariables(@"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe");

        ProcessStartInfo startInfo = new ProcessStartInfo(vsWhere)
        {
            Arguments = "-latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\\**\\Bin\\MSBuild.exe",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = Process.Start(startInfo);

        process.WaitForExit();

        string finalPath = process.StandardOutput.ReadToEnd().Trim();

        return finalPath;
    }
}
