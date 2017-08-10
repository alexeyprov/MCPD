using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using ResourceCombiner.Properties;
using System.Diagnostics;
using System.ComponentModel;
using Westwind.Utilities;

namespace ResourceCombiner
{
    public class ResourceCombiner
    {
        
        /// <summary>
        /// The name of the output assembly. Include the .dll or .exe extension
        /// </summary>
        [Description("The name of the output assembly. Include the .dll or .exe extension"),DefaultValue(@"")]
        public string TargetFileName { get; set; }

        /// <summary>
        /// The path to the LocBaml application
        /// </summary>
        [Description("The path to the LocBaml application"),DefaultValue("LocBaml.exe")]
        public string LocBamlPath { get; set; }

        /// <summary>
        /// The neutral locale - must exist
        /// </summary>
        [Description("The neutral locale - must exist"),DefaultValue("en-US")]
        public string NeutralLocale { get; set; }

        /// <summary>
        /// The locale to translate to
        /// </summary>
        [Description("The locale to translate to"),DefaultValue("de-DE")]
        public string TranslateLocale { get; set; }

        /// <summary>
        /// The path and filename of the .NET Assembly Linker
        /// used to combine resources into a satellite assembly
        /// </summary>
        [Description("Executable path to the assembly linker. Must be either in path or otherwise be full path"),DefaultValue("al")]
        public string AssemblyLinkerFileName {get;set;}

        
        /// <summary>
        /// Determines whether we're writing to debug or release folders
        /// </summary>
        [Description("Determines whether we're writing to debug or release folders"),DefaultValue(false)]
        public bool ReleaseMode {get; set;}


        [Description("Folder relative to the target where locale.csv files are stored (ie. de-DE.csv). Use relative path without trailing backslash")]
        public string CsvFolder { get; set; }

        /// <summary>
        /// An error message when a method fails
        /// </summary>        
        public string ErrorMessage { get; set; }

        public ResourceCombiner()
        {
            // assume default path
            this.LocBamlPath = "LocBaml.exe";
            this.AssemblyLinkerFileName = @"C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\al.exe";
            this.TranslateLocale = "de-DE";
            this.NeutralLocale = "en-US";
            this.TargetFileName = @"C:\projects2008\Articles\WpfLocalization\WpfLocalization\bin\Debug\WpfLocalization.exe";
            this.LocBamlPath = @"C:\projects2008\Articles\WpfLocalization\WpfLocalization\bin\Debug\LocBaml.exe";
            this.CsvFolder = @"Res";
        }


        /// <summary>
        /// The location of the .NET SDK for the currently executing version of the 
        /// framework.
        /// </summary>
        private static string sdkLocation;


        /// <summary>
        /// Runs LocBaml and Al.exe to combine Baml and other project resources into a single resource assembly
        /// </summary>
        /// <returns></returns>
        public bool CombineResources()
        {
            string curPath = Directory.GetCurrentDirectory();
            bool res = this.CombineResourcesInternal();
            Directory.SetCurrentDirectory(curPath);
            return res;
        }
        
        private bool CombineResourcesInternal() 
        {
            if (!File.Exists(this.TargetFileName))
            {
                this.ErrorMessage = Resources.TargetFileDoesntExist;
                return false;
            }
            if (!File.Exists(this.LocBamlPath))
            {
                this.ErrorMessage = Properties.Resources.LocBamlDoesntExist;
                return false;
            }

            // path where we need to run apps out of - everything else will be relative
            string targetPath = Path.GetDirectoryName(this.TargetFileName);
            string targetFile = Path.GetFileNameWithoutExtension(this.TargetFileName);

            Directory.SetCurrentDirectory(targetPath);

            string objPath = @"..\..\obj\";
            if (this.ReleaseMode)
                objPath+= @"Release";
            else
                objPath += @"Debug";

            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = this.LocBamlPath;
            psi.WorkingDirectory = targetPath;
            psi.Arguments = "/generate " + objPath + @"\" + targetFile + ".g." + this.NeutralLocale + @".resources /trans:" + this.CsvFolder + @"/" + this.TranslateLocale + @".csv /out:" + this.TranslateLocale + " /culture:" + this.TranslateLocale;
            
            process.StartInfo = psi;
            process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            
            bool started = process.Start();

            if (!started)
            {
                this.ErrorMessage = Resources.CouldntStartLocBaml;
                return false;
            }


            process.WaitForExit();

            if (process.ExitCode != 0)
                return false;


            process = new Process();
            
            psi = new ProcessStartInfo();
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.FileName = this.AssemblyLinkerFileName;
            psi.WorkingDirectory = targetPath;
            psi.Arguments = "/template:\"" + Path.GetFileName(this.TargetFileName) + "\"" +                            
                            " /embed:" + this.TranslateLocale + "\\" + targetFile + ".g." + TranslateLocale + ".resources ";


            
            // Pick up any other resource DLLs in the obj folder
            StringBuilder sb = new StringBuilder();
            string[] resourceFiles = Directory.GetFiles(objPath, "*." + TranslateLocale + ".resources");
            foreach (string resourceFile in resourceFiles)
            {
                sb.Append(" /embed:" + resourceFile);
            }
            psi.Arguments += sb.ToString() +
                " /culture:" + this.TranslateLocale + " /out:" + this.TranslateLocale + "\\" + targetFile + ".resources.dll";

            process.StartInfo = psi;            
            started = process.Start();
            
            if (!started)
            {
                return false;
            }
                    
            process.WaitForExit();

            if (process.ExitCode > 0)
            {
                if (process.StandardOutput != null)
                {
                    StreamReader sreader = process.StandardOutput;
                    this.ErrorMessage = sreader.ReadToEnd();
                    sreader.Close();
                    if (string.IsNullOrEmpty(this.ErrorMessage))
                       this.ErrorMessage = Properties.Resources.AssemblyLinkerFailed;
                    this.ErrorMessage += "\r\n\r\n" + process.StartInfo.Arguments;
                }
                return false;
            }

            return true;
        }

        public bool SaveToFile(string filename)
        {
            ResourceCombiner combiner = this;
            return SerializationUtils.SerializeObject(combiner, filename, false);
        }

        /// <summary>
        /// Loads an instance of the combiner with settings from a file.
        /// 
        /// If the file doesn't exist a default instance is loaded.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ResourceCombiner LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
                return new ResourceCombiner();

            return SerializationUtils.DeSerializeObject(filename, typeof(ResourceCombiner), false) as ResourceCombiner;
        }


        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.ErrorMessage = e.Data.ToString();
        }


        /// <summary>
        /// Gets the location of the SDK folder on the computer
        /// </summary>
        /// <value>The SDK location.</value>
        private static string SdkLocation
        {
            get
            {
                if (sdkLocation == null)
                {
                    sdkLocation = String.Empty;

                    RegistryKey key = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\.NETFramework");
                    if (key != null)
                    {
                        object location = key.GetValue("sdkInstallRoot" +
                            ExecutingFrameworkVersion);
                        if (location != null)
                        {
                            sdkLocation = Path.Combine(location.ToString(), "Bin");
                        }
                    }
                }

                return sdkLocation;
            }
        }

        /// <summary>
        /// Gets the short version number of the executing framework, e.g. v1.1 or v2.0
        /// </summary>
        /// <value>The executing framework version.</value>
        private static string ExecutingFrameworkVersion
        {
            get
            {
                return RuntimeEnvironment.GetSystemVersion().Substring(0, 4);
            }
        }
    }
}
