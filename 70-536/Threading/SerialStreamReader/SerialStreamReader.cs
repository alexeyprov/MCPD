using System;
using System.IO;
using System.Threading;

using LoMaN.IO;

namespace SerialStreamReader {

	class App {

		// The main serial stream
		static SerialStream ss;

		[STAThread]
		static void Main(string[] args) {

			// Create a serial port
			ss = new SerialStream();
			try {
				ss.Open("COM2");
			}
			catch (Exception e) {
				Console.WriteLine("Error: " + e.Message);
				return;
			}

			// Set port settings
			ss.SetPortSettings(9600);

			// Set timeout so read ends after 20ms of silence after a response
			ss.SetTimeouts(20, 0, 0, 0, 0);

			// Create the StreamWriter used to send commands
			StreamWriter sw = new StreamWriter(ss, System.Text.Encoding.ASCII);

			// Create the Thread used to read responses
			Thread responseReaderThread = new Thread(new ThreadStart(ReadResponseThread));
			responseReaderThread.Start();

			// Read all returned lines
			for (;;) {
				// Read command from console
				string command = Console.ReadLine();

				// Check for exit command
				if (command.Trim().ToLower() == "exit") {
					responseReaderThread.Abort();
					break;
				}

				// Write command to modem
				sw.WriteLine(command);
				sw.Flush();
			}
		}

		// Main loop for reading responses
		static void ReadResponseThread() {
			StreamReader sr = new StreamReader(ss, System.Text.Encoding.ASCII);
			try {
				for (;;) {
					// Read response from modem
					string response = sr.ReadLine();
					Console.WriteLine("Response: " + response);
				}
			}
			catch (ThreadAbortException) {
			}
		}
	}
}
