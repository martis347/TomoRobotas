using System;
using System.Threading;
using log4net;

namespace Business
{
    public class Battle
    {
        private static ILog _logger;
        public static Warrior Warrior1;
        public static Warrior Opponent;

        public void Start()
        {
            _logger = LogManager.GetLogger(typeof(Battle));

            Warrior1 = new Warrior("warrior1");
            Opponent = new Warrior("opponent");

            Warrior1.Enemy = Opponent;
            Opponent.Enemy = Warrior1;

            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 3");
            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 2");
            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 1");
            Thread.Sleep(1000);

            Warrior1.FightLoop(Strategy.YourStrategy());

            _logger.Info("warior1 life = " + Warrior1.GetLife() + " " + Warrior1.GetName());
            _logger.Info("opponent life = " + Opponent.GetLife() + " " + Opponent.GetName());
        }

        public void Stop()
        {
            _logger.Info("Service stopped");
        }
    }
}