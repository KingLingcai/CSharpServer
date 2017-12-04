using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace HexiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string hostName = Dns.GetHostName();
            //IPAddress[] iPAddresses = Dns.GetHostAddresses(hostName);
            //foreach(IPAddress address in iPAddresses)
            //{
            //    Console.WriteLine(address.ToString());
            //    Console.ReadLine();
            //}
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            Console.WriteLine(randomNumber.ToString());
            Console.ReadLine();
        }
    }
}
