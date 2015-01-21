using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Communication
{
    public class Opponent
    {
        public static void WaitForOpponent()
        {
            while (true)
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(ReadSetting("Url") + "attack");
                    request.GetResponse();

                    Console.WriteLine("Found!!!");

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Waiting...");
                }
            }
        }

        public static int Post(string action, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(ReadSetting("Url") + action);
            request.Timeout = 100000;

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return Int32.Parse(responseString);
        }

        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return null;
        }
    }
}
