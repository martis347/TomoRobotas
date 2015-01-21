using System;
using System.IO;
using log4net.Config;
using Topshelf;

namespace Infrastructure
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));

            HostFactory.Run(x =>
            {
                x.Service<HttpApiService>(s =>
                {
                    //158.129.18.175
                    //192.168.43.16
                    s.ConstructUsing(name => new HttpApiService(new Uri("http://localhost:1234/")));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartManually();
                x.SetDescription("RobotsAtWar");
                x.SetDisplayName("RobotsAtWar");
                x.SetServiceName("RobotsAtWar");
            });
        }
    }
}
