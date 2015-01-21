using System;
using System.IO;
using System.Net;
using System.Text;
using Business.Tools;

namespace Business
{
    public class Opponent
    {
        private const string OPPONENT_URL = "OpponentUrl";

        public static void WaitForOpponent()
        {
            while (true)
            {
                try
                {
                    // TODO: move htto logilc to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + "attack");
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
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + action);
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

        public int GetLife()
        {
            return 999;
        }

        public string GetName()
        {
            return "set my name!";
        }

        public bool IsAlive()
        {
            return true;
        }

        public string GetState()
        {
            return "OMFG";
        }

        public void FeelMyWrath(int damage)
        {
            Post("attack", "=" + damage);
        }
    }
}
