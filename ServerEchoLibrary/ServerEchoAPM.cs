using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServerEchoLibrary
{
    public class ServerEchoAPM : ServerEcho

    {
        Uzytkownik nowy = new Uzytkownik();

        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public ServerEchoAPM(IPAddress IP, int port) : base(IP, port)

        {

        }

        protected override void AcceptClient()

        {

            while (true)

            {



                TcpClient tcpClient = TcpListener.AcceptTcpClient();

                Stream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);

                //callback style

                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);


                // async result style

                //IAsyncResult result = transmissionDelegate.BeginInvoke(Stream, null, null);

                ////operacje......

                //while (!result.IsCompleted) ;

                ////sprzątanie

            }

        }

        /// <summary>
        /// Funkcja wysylajaca wiadomosc do klienta
        /// </summary>
        /// <param name="s"></param>
        /// <param name="m">wiadomosc do wyslania</param>
        public static void SendMessage(NetworkStream s, string m)
        {
            byte[] buffer = new byte[1024];
            buffer = Encoding.ASCII.GetBytes(m);
            s.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Funkcja odbierajaca wiadomosc od klienta
        /// </summary>
        /// <param name="s">strumień </param>
        /// <returns></returns>
        public static string GetMessage(NetworkStream s)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                s.Read(buffer, 0, buffer.Length);
                string m = Encoding.ASCII.GetString(buffer).Replace("\0", "");
                if (m != "\r\n") return m;
            }
        }



        private void TransmissionCallback(IAsyncResult ar)

        {

            // sprzątanie

        }
        /// <summary>
        /// Funkcja rozpoczynajaca przesylanie danych z klientem
        /// </summary>
        /// <param name="stream"></param>
        protected override void BeginDataTransmission(NetworkStream stream)

        {



            bool sprawdz = false;

            while (true)
            {
                try
                {

                    SendMessage(stream, "Wybierz operacje do wykonania: [1]- loguje uzytkownika, [2]-usuwa uzytkownika, [3]-rejestruje uzytkownika " +
                        "[4]-wyswietla wszystkich uzytkownikow" + "\r\n");
                    string wybor = GetMessage(stream);

                    switch (wybor)
                    {
                        case "1":
                            {
                                SendMessage(stream, "Wpisz login: ");
                                string login = GetMessage(Stream);
                                SendMessage(stream, "Wpisz haslo: ");
                                string haslo = GetMessage(Stream);
                                SendMessage(stream, nowy.Login(login, haslo));
                                break;
                            }
                        case "2":
                            {
                                SendMessage(stream, "Wpisz login uzytkownika: ");
                                string login = GetMessage(Stream);
                                SendMessage(stream, nowy.Remove(login));
                                break;
                            }
                        case "3":
                            {
                                SendMessage(stream, "Wpisz login: ");
                                string login = GetMessage(Stream);
                                SendMessage(stream, "Wpisz haslo: ");
                                string haslo = GetMessage(Stream);
                                SendMessage(stream, nowy.Register(login, haslo));
                                break;
                            }
                        case "4":
                            {
                                SendMessage(stream, nowy.Show() + "\r\n");
                                break;
                            }


                    }


                }
                catch (IOException e)

                {



                }
            }

        }
        /// <summary>
        /// Funkcja uruchamiajaca serwer
        /// </summary>
        public override void Start()

        {

            StartListening();

            //transmission starts within the accept function

            AcceptClient();


        }
        static void Main(string[] args)
        {
        }

    }
}
