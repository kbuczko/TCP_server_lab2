using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServerEchoLibrary;

namespace Serwer
{
    class Serwer
    {
        static void Main(string[] args)
        {
            ServerEchoAPM apm = new ServerEchoAPM(IPAddress.Any, 8000);
            apm.Start();
        }

    }
}
