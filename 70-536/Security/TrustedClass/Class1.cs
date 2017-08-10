using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

[assembly:AllowPartiallyTrustedCallers]
namespace TrustedClass
{
	/// <summary>
	/// Provides static methods for allowing trusted and partially trusted code to write files.
	/// </summary>
	public class Distrust
	{
        /// <summary>
        /// Writes contents to a file... if you have FileIOPermission
        /// </summary>
	    public static void WriteToFile(string fileName, string contents)
        {
            // No need to demand FileIOPermission here, because the .NET Framework
            // takes care of that when I use the StreamWriter
            // The explicit demand would look like this, however.
            // FileIOPermission newFilePermission = new FileIOPermission(FileIOPermissionAccess.Write, fileName);
            // newFilePermission.Demand();

            // Caller has the necessary permission, so write to the file
            StreamWriter newFile = new StreamWriter(fileName);
            newFile.WriteLine(contents);
            newFile.Close();
        }

        /// <summary>
        /// Writes contents to a file
        /// </summary>
        public static void WriteToFileWrapper(string fileName, string contents)
        {
            try
            {
                FileIOPermission perm = new FileIOPermission(FileIOPermissionAccess.Write, fileName);
                perm.Assert();
                WriteToFile(fileName, contents);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }
    }
}
