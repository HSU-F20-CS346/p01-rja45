using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PortScanner
{
    class Program
    {
        static void Main()

        {
            for (; ; )
            {
              
                Console.WriteLine("==========================================================");

                Console.WriteLine("<Server/Host>:     provide a domain name or IP address");
                Console.WriteLine("");
                Console.WriteLine("<Port(s)>:         provide a single port number, ");
                Console.WriteLine("                   a port range (i.e.- 1:22), or ");
                Console.WriteLine(@"                   keyword ""COMMON"" to scan all ");
                Console.WriteLine("                   well-known ports");
                Console.WriteLine("");
                Console.WriteLine(@"[<Protocol>]:      either ""TCP"" or ""UDP""");
                Console.WriteLine(" *Optional*        (UDP currently not supported)");
                Console.WriteLine("");
                Console.WriteLine("Enter PortScanner Parameters and press Enter: ");
                Console.WriteLine("<Server/Host> <Port(s)> [<Protocol>]");
                Console.WriteLine("i.e.- 216.58.194.74 80");
                Console.WriteLine("i.e.- amazon.com 70:80 TCP");
                Console.WriteLine("i.e.- facebook.com COMMON TCP");

                Console.WriteLine("==========================================================");



                string input = Console.ReadLine();

                string[] args = input.Split(' ');

                //if ((args.Length < 2) || (args.Length > 3))
                //{ // Test for correct # of args
                //    throw new ArgumentException("Parameters: <Server> <Port(s) [for a range of ports, specify <lowest port>:<highest port> or ]> [Protocol (default TCP if empty)]");
                //}

                String server = args[0];  // Server name or IP address

                //Check whether user supplied one port or a range of ports (lowPort:highPort)
                string[] ports = args[1].Split(':');





                if (ports.Length == 1)
                {

                    if ((args.Length == 3 && args[2] == "TCP") || args.Length == 2)
                    {

                        //Checks for COMMON keyword
                        if (ports[0] == "COMMON")
                        {
                            for (int i = 0; i < 1024; i++)
                            {
                                var client = new TcpClient();
                                var result = client.BeginConnect(server, i, null, null);

                                //result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                                result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(1000));

                                if (!client.Connected)
                                {
                                    // we have not connected
                                    Console.WriteLine("Port " + i + ":" + " CLOSED");
                                }
                                else
                                {
                                    // we have connected
                                    Console.WriteLine("Port " + i + ":" + " OPEN");
                                    client.EndConnect(result);

                                }

                            }
                        }
                        else
                        {
                            int lowPort = Int32.Parse(ports[0]);
                            //This loop checks port status with TCP


                            // Create socket that is connected to server on specified port



                            var client = new TcpClient();
                            var result = client.BeginConnect(server, lowPort, null, null);

                            //result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                            result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(1000));

                            if (!client.Connected)
                            {
                                // we have not connected
                                Console.WriteLine("Port " + lowPort + ":" + " CLOSED");
                            }
                            else
                            {
                                // we have connected
                                Console.WriteLine("Port " + lowPort + ":" + " OPEN");
                                client.EndConnect(result);

                            }
                            // client.EndConnect(result);

                            //  client.Close();
                        }






                    }
                    //else if (args.Length == 3 && args[2] == "UDP")
                    //{

                    //    UdpClient client = null;

                    //    try
                    //    {
                    //        // Create socket that is connected to server on specified port
                    //        client = new UdpClient(server, lowPort);
                    //        Console.WriteLine("Port " + lowPort + ":" + " OPEN");
                    //        client.Close();
                    //    }
                    //    catch
                    //    {
                    //        Console.WriteLine("Port " + lowPort + ":" + " CLOSED");
                    //    }
                    //}

                    //else
                    //{
                    //    throw new ArgumentException(args[2] + " is a protocol not supported by PortScanner");
                    //}
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                //If a port range is specified, the port ranges are parsed and split then checked for ascending order
                if (ports.Length == 2)
                {
                    int lowPort = Int32.Parse(ports[0]);
                    int highPort = Int32.Parse(ports[1]);


                    if (lowPort > highPort)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("**************************************************************************");
                        Console.WriteLine("Port Ranges must be in order of <lowest port>:<highest port> .  Try again.");
                        Console.WriteLine("**************************************************************************");
                        Console.WriteLine("");
                        Console.WriteLine("");

                    }


                    //This runs if the protocol is either not specified or is specified as "TCP" 
                    if ((args.Length == 3 && args[2] == "TCP") || args.Length == 2)
                    {

                        //This loop checks port status from the lowPort to the highPort with TCP
                        for (int i = lowPort; i <= highPort; i++)
                        {
                            var client = new TcpClient();
                            var result = client.BeginConnect(server, i, null, null);

                            //result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                            result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(1000));

                            if (!client.Connected)
                            {
                                // we have not connected
                                Console.WriteLine("Port " + i + ":" + " CLOSED");
                            }
                            else
                            {
                                // we have connected
                                Console.WriteLine("Port " + i + ":" + " OPEN");
                                client.EndConnect(result);
                            }





                            // client.Close();
                        }
                    }
                    //else if (args.Length == 3 && args[2] == "UDP")
                    //{
                    //    for (int i = lowPort; i <= highPort; i++)
                    //    {
                    //        UdpClient client = null;

                    //        try
                    //        {
                    //            // Create socket that is connected to server on specified port
                    //            client = new UdpClient(server, i);
                    //            Console.WriteLine("Port " + i + ":" + " OPEN");
                    //            client.Close();
                    //        }
                    //        catch
                    //        {
                    //            Console.WriteLine("Port " + i + ":" + " CLOSED");
                    //            client.Close();
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    throw new ArgumentException(args[2] + " is a protocol not supported by PortScanner");
                }



                if (ports.Length > 2 || args.Length > 3)
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("******************************************");
                    Console.WriteLine("Incorrect Parameters specified. Try again.");
                    Console.WriteLine("******************************************");
                    Console.WriteLine("");
                    Console.WriteLine("");

                }
            }
        }

    }
}
    










