using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Permissions;
using System.Text;

using Microsoft.Win32.SafeHandles;

namespace IsoStorage
{
    [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class LoginPrefs
    {
        public LoginPrefs(string myUserName)
        {
            _userName = myUserName;
            _isNewPrefs = GetPrefsForUser();
        }
        string _userName;

        string _newsUrl;
        public string NewsUrl
        {
            get { return _newsUrl; }
            set { _newsUrl = value; }
        }

        string _sportsUrl;
        public string SportsUrl
        {
            get { return _sportsUrl; }
            set { _sportsUrl = value; }
        }
        bool _isNewPrefs;
        public bool NewPrefs
        {
            get { return _isNewPrefs; }
        }

        private bool GetPrefsForUser()
        {
            try
            {
                // Retrieve an IsolatedStorageFile for the current Domain and Assembly.
                IsolatedStorageFile isoFile =
                    IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Domain,
                    null,
                    null);

                IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream(_userName,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

                // The code executes to this point only if a file corresponding to the username exists.
                // Though you can perform operations on the stream, you cannot get a handle to the file.

                try
                {

                    SafeFileHandle aFileHandle = isoStream.SafeFileHandle;
                    Console.WriteLine("A pointer to a file handle has been obtained. "
                        + aFileHandle.ToString() + " "
                        + aFileHandle.GetHashCode());
                }
                catch (Exception e)
                {
                    // Handle the exception.
                    Console.WriteLine("Expected exception");
                    Console.WriteLine(e);
                }

                using (StreamReader reader = new StreamReader(isoStream))
                {
                    // Read the data.
                    this.NewsUrl = reader.ReadLine();
                    this.SportsUrl = reader.ReadLine();
                }
                return false;
            }
            catch (System.IO.FileNotFoundException)
            {
                // Expected exception if a file cannot be found. This indicates that we have a new user.
                return true;
            }
        }

        public bool GetIsoStoreInfo()
        {
            // Get a User store with type evidence for the current Domain and the Assembly.
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain,
                typeof(System.Security.Policy.Url),
                typeof(System.Security.Policy.Url));

            String[] dirNames = isoFile.GetDirectoryNames("*");
            String[] fileNames = isoFile.GetFileNames("*");

            // List directories currently in this Isolated Storage.
            if (dirNames.Length > 0)
            {
                for (int i = 0; i < dirNames.Length; ++i)
                {
                    Console.WriteLine("Directory Name: " + dirNames[i]);
                }
            }

            // List the files currently in this Isolated Storage.
            // The list represents all users who have personal preferences stored for this application.
            if (fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; ++i)
                {
                    Console.WriteLine("File Name: " + fileNames[i]);
                }
            }

            isoFile.Close();
            return true;
        }

        public double SetPrefsForUser()
        {
            try
            {
                IsolatedStorageFile isoFile;
                isoFile = IsolatedStorageFile.GetUserStoreForDomain();

                // Open or create a writable file.
                IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream(this._userName,
                    FileMode.OpenOrCreate,
                    FileAccess.Write,
                    isoFile);

                return SetPrefsHelper(isoFile, isoStream);
            }
            catch (IsolatedStorageException ex)
            {
                // Add code here to handle the exception.
                Console.WriteLine(ex);
            }
            return 0.0;
        }

        public void DeleteFiles()
        {
            try
            {
                IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Domain,
                    typeof(System.Security.Policy.Url),
                    typeof(System.Security.Policy.Url));

                String[] dirNames = isoFile.GetDirectoryNames("*");
                String[] fileNames = isoFile.GetFileNames("*");

                // List the files currently in this Isolated Storage.
                // The list represents all users who have personal
                // preferences stored for this application.
                if (fileNames.Length > 0)
                {
                    for (int i = 0; i < fileNames.Length; ++i)
                    {
                        // Delete the files.
                        isoFile.DeleteFile(fileNames[i]);
                    }
                    // Confirm that no files remain.
                    fileNames = isoFile.GetFileNames("*");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // This method deletes directories in the specified Isolated Storage, after first 
        // deleting the files they contain. In this example, the Archive directory is deleted. 
        // There should be no other directories in this Isolated Storage.
        public void DeleteDirectories()
        {
            try
            {
                IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Domain,
                    typeof(System.Security.Policy.Url),
                    typeof(System.Security.Policy.Url));
                String[] dirNames = isoFile.GetDirectoryNames("*");
                String[] fileNames = isoFile.GetFileNames("Archive\\*");

                // Delete all the files currently in the Archive directory.

                if (fileNames.Length > 0)
                {
                    for (int i = 0; i < fileNames.Length; ++i)
                    {
                        // Delete the files.
                        isoFile.DeleteFile("Archive\\" + fileNames[i]);
                    }
                    // Confirm that no files remain.
                    fileNames = isoFile.GetFileNames("Archive\\*");
                }


                if (dirNames.Length > 0)
                {
                    for (int i = 0; i < dirNames.Length; ++i)
                    {
                        // Delete the Archive directory.
                    }
                }
                dirNames = isoFile.GetDirectoryNames("*");
                isoFile.Remove();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public double SetNewPrefsForUser()
        {
            try
            {
                byte inputChar;
                IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Domain,
                    typeof(System.Security.Policy.Url),
                    typeof(System.Security.Policy.Url));

                // If this is not a new user, archive the old preferences and 
                // overwrite them using the new preferences.
                if (!this._isNewPrefs)
                {
                    if (isoFile.GetDirectoryNames("Archive").Length == 0)
                        isoFile.CreateDirectory("Archive");
                    else
                    {
                        IsolatedStorageFileStream source =
                            new IsolatedStorageFileStream(this._userName, FileMode.OpenOrCreate,
                            isoFile);
                        // This is the stream from which data will be read.
                        Console.WriteLine("Is the source file readable? " + (source.CanRead ? "true" : "false"));
                        Console.WriteLine("Creating new IsolatedStorageFileStream for Archive.");

                        // Open or create a writable file.
                        IsolatedStorageFileStream target =
                            new IsolatedStorageFileStream("Archive\\ " + this._userName,
                            FileMode.OpenOrCreate,
                            FileAccess.Write,
                            FileShare.Write,
                            isoFile);
                        Console.WriteLine("Is the target file writable? " + (target.CanWrite ? "true" : "false"));
                        // Stream the old file to a new file in the Archive directory.
                        if (source.IsAsync && target.IsAsync)
                        {
                            // IsolatedStorageFileStreams cannot be asynchronous.  However, you
                            // can use the asynchronous BeginRead and BeginWrite functions
                            // with some possible performance penalty.

                            Console.WriteLine("IsolatedStorageFileStreams cannot be asynchronous.");
                        }
                        else
                        {
                            Console.WriteLine("Writing data to the new file.");
                            while (source.Position < source.Length)
                            {
                                inputChar = (byte)source.ReadByte();
                                target.WriteByte(inputChar);
                            }

                            // Determine the size of the IsolatedStorageFileStream
                            // by checking its Length property.
                            Console.WriteLine("Total Bytes Read: " + source.Length);
                        }

                        // After you have read and written to the streams, close them.
                        target.Close();
                        source.Close();
                    }
                }

                // Open or create a writable file with a maximum size of 10K.
                IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream(this._userName,
                    FileMode.OpenOrCreate,
                    FileAccess.Write,
                    FileShare.Write,
                    10240,
                    isoFile);

                isoStream.Position = 0;  // Position to overwrite the old data.
                return SetPrefsHelper(isoFile, isoStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0.0;
        }

        private double SetPrefsHelper(IsolatedStorageFile isoFile, IsolatedStorageFileStream isoStream)
        {
            using (StreamWriter writer = new StreamWriter(isoStream))
            {
                // Update the data based on the new inputs.
                writer.WriteLine(this.NewsUrl);
                writer.WriteLine(this.SportsUrl);

                Console.WriteLine("CurrentSize = " + isoFile.CurrentSize.ToString());
                Console.WriteLine("MaximumSize = " + isoFile.MaximumSize.ToString());
                // Calculate the amount of space used to record this user's preferences.
                return isoFile.CurrentSize / isoFile.MaximumSize;
            }
        }
    }
}
