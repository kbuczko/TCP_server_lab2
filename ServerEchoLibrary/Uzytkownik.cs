using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerEchoLibrary
{
    class Uzytkownik
    {

        /// <summary>
        /// Funkcja do logowania uzytkownika
        /// </summary>
        /// <param name="login"></param>
        /// <param name="haslo"></param>
        /// <returns></returns>
        public string Login(string login, string haslo)
        {
            using (var streamReader = new StreamReader("C:/Users/mrace/OneDrive/Pulpit/test.txt"))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] user = line.Split();
                    if (user[0] == login && user[1] == haslo)
                    {
                        return "Zalogowano pomyslnie.\r\n";

                    }
                    if (user[0] == login && user[1] != haslo)
                    {
                        return "Bledne haslo.\r\n";
                    }
                }
            }
            return "Brak takiego uzytkownika.\r\n";
        }
        /// <summary>
        /// Funkcja do rejestrowania uzytkownika
        /// </summary>
        /// <param name="login"></param>
        /// <param name="haslo"></param>
        /// <returns></returns>
        public string Register(string login, string haslo)
        {

            using (var streamReader = new StreamReader("C:/Users/mrace/OneDrive/Pulpit/test.txt"))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] user_data = line.Split();
                    if (user_data[0] == login)
                    {
                        return "Istnieje uzytkownik o takim loginie\r\n";
                    }
                }
            }

            using (StreamWriter sw = File.AppendText("C:/Users/mrace/OneDrive/Pulpit/test.txt"))
            {
                sw.WriteLine(login + " " + haslo);
            }
            return "Uzytkownik zostal dodany\r\n";
        }
        /// <summary>
        /// Funkcja do usuwania uzytkownikow
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string Remove(string login)
        {
            List<string> arrLine = new List<string>();


            using (var streamReader = new StreamReader("C:/Users/mrace/OneDrive/Pulpit/test.txt"))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] user_data = line.Split();
                    if (user_data[0] != login)
                    {
                        arrLine.Add(line);
                    }
                }
            }

            File.WriteAllLines("C:/Users/mrace/OneDrive/Pulpit/test.txt", arrLine);
            return "Uzytkownik zostal usuniety.\r\n";

        }
        /// <summary>
        /// Funkcja do drukowania uzytkownikow
        /// </summary>
        /// <returns></returns>
        public string Show()
        {
            string[] lines = File.ReadAllLines("C:/Users/mrace/OneDrive/Pulpit/test.txt");
            return string.Join("\r\n", lines);
        }
    }
}