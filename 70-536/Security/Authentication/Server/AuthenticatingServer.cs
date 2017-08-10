using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Principal;

public class AuthenticatingServer 
{
    public static void Main() 
    {   
        // Create an IPv4 TCP/IP socket. 
        TcpListener listener = new TcpListener(IPAddress.Any, 11000);

        // Listen for incoming connections.
        listener.Start();
        while (true) 
        {
            // Application blocks while waiting for an incoming connection.
            // Type CNTL-C to terminate the server.
            using (TcpClient clientRequest = listener.AcceptTcpClient())
			{
	            Console.WriteLine("Client connected.");

    	        // A client has connected. 
        	    try
            	{
	                AuthenticateClient(clientRequest);
    	        }
        	    catch (Exception e)
            	{
                	Console.WriteLine(e);
	            }
			}
        }
	}

    public static void AuthenticateClient(TcpClient clientRequest)
    {
        NetworkStream stm = clientRequest.GetStream();

        // Create the NegotiateStream.
        using (NegotiateStream authStream = new NegotiateStream(stm, false))
		{
            // Save the current client and NegotiateStream instance 
            // in a ClientState object.
            ClientState cState = new ClientState(authStream);

            // Listen for the client authentication request.
            authStream.BeginAuthenticateAsServer(
                new AsyncCallback(EndAuthenticateCallback),
                cState);

            // Wait until the authentication completes.
            cState.Wait();

            authStream.BeginRead(cState.Buffer, 0, cState.Buffer.Length, 
                   new AsyncCallback(EndReadCallback), 
                   cState);
            cState.Wait();

            // Finished with the current client.
        }
	}

    // The following method is invoked by the
    // BeginAuthenticateAsServer callback delegate.
    private static void EndAuthenticateCallback(IAsyncResult ar)
    {
        // Get the saved data.
        ClientState cState = (ClientState) ar.AsyncState;
        NegotiateStream authStream = (NegotiateStream) cState.AuthenticatedStream;

        Console.WriteLine("Ending authentication.");

        // Any exceptions that occurred during authentication are
        // thrown by the EndAuthenticateAsServer method.
        try 
        {
            // This call blocks until the authentication is complete.
            authStream.EndAuthenticateAsServer(ar);

			// Display properties of the authenticated client.
            IIdentity id = authStream.RemoteIdentity;
            Console.WriteLine("{0} was authenticated using {1}.", 
                id.Name, 
                id.AuthenticationType);
        }
        catch (AuthenticationException e)
        {
             Console.WriteLine(e);
             Console.WriteLine("Authentication failed - closing connection.");
        }
        catch (Exception e)
        {
             Console.WriteLine(e);
             Console.WriteLine("Closing connection.");
        }
		finally
		{
            cState.Signal();
		}

    }
        
    private static void EndReadCallback(IAsyncResult ar)
    {
        // Get the saved data.
        ClientState cState = (ClientState) ar.AsyncState;
        AuthenticatedStream authStream = cState.AuthenticatedStream;

        // Read the client message.
        try
        {
            int bytes = authStream.EndRead(ar);
			if (bytes != 0)
            {
				cState.FlushBuffer(bytes);
                //authStream.BeginRead(cState.Buffer, 0, cState.Buffer.Length, 
                //    new AsyncCallback(EndReadCallback), 
                //    cState);
                //return;

                IIdentity id = ((NegotiateStream) authStream).RemoteIdentity;
            	Console.WriteLine("{0} says {1}", id.Name, cState.Message);
			}

        }
        catch (Exception e)
        {
            // A real application should do something
            // useful here, such as logging the failure.
            Console.WriteLine("Client message exception:");
            Console.WriteLine(e);
        }
		finally
		{
            cState.Signal();
        }
    }
}
