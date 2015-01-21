using log4net;

namespace Business
{
    public class BattleField
    {
        private static ILog _logger;
        public static Warrior Warrior1;
        public static Opponent Opponent;

        public void StartBattle()
        {
            _logger = LogManager.GetLogger(typeof(BattleField));

            StartBattleCore();

            _logger.Info("warior1 life = " + Warrior1.GetLife() + " " + Warrior1.GetName());
            _logger.Info("opponent life = " + Opponent.GetLife() + " " + Opponent.GetName());
        }

        private void StartBattleCore()
        {
            Opponent = new Opponent();

            Warrior1 = new Warrior("warrior1", Opponent, Strategy.YourStrategy());

            FightLoop();


//            Thread.Sleep(1000);
//            Console.WriteLine("Battle starts in 3");
//            Thread.Sleep(1000);
//            Console.WriteLine("Battle starts in 2");
//            Thread.Sleep(1000);
//            Console.WriteLine("Battle starts in 1");
//            Thread.Sleep(1000);

        }

        private void FightLoop()
        {
            while (true)
            {
                if (!Warrior1.IsAlive() || !Opponent.IsAlive())
                {
                    break;
                }

                Warrior1.ExecuteNextCommand();

            }
        }

    }
}