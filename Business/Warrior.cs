using System;
using System.Collections.Generic;
using System.Threading;
using log4net;

namespace Business
{
    public enum States { Attacking, Defending, Resting, Checking, Interrupted, DoingNothing }

    public enum Actions {Attack, Defend, Rest, Check, DoNothing}

    public class Warrior
    {
        private readonly Opponent _opponent;
        private readonly List<Commands> _strategy;
        private int _currentActionNumber = 0;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Warrior));

        private States State { get; set; }
        private int Life { get; set; }
        private string Name { get; set; }

        public Warrior Enemy { get; set; }
        public int GetLife() { return Life; }
        public string GetName() { return Name; }

        public Warrior(string name)
        {
            Life = 10;
            Name = name;
        }

        public Warrior(string name, Opponent opponent, List<Commands> strategy)
        {
            _opponent = opponent;
            _strategy = strategy;
            Name = name;
        }

        public int Attack(int time)
        {
            if (time < 1 || time > 3)
            {
                Logger.Info("Invalid time!");
                State = States.DoingNothing;
                return 0;
            }

            State = States.Attacking;
            var damage = (time * 2) - 1;
            Logger.Info("You are trying to deal " + damage + " damage!");
            
            Thread.Sleep(time*1000);

            if (State == States.Interrupted)
            {
                Logger.Info("You were trying to attack, but you were interrupted");
                return 0;
            }

            Enemy.GetAttacked(Opponent.Post("attack", "=" + time));

            State = States.DoingNothing;

            return damage;
        }

        public int GetAttacked(int damage)
        {
            if (damage < 0 || damage > 5)
            {
                Logger.Info("Invalid damage!");
                return 0;
            }

            if (State == States.Defending || damage == 0)
            {
                Logger.Info(
                        (Name.Equals("warrior1")? "You" : "Opponent") 
                        + " didn`t lose any health because " +
                              (Name.Equals("warrior1") ? "you were" : "he was") + " defending!");
                return 0;                
            }
            else
            {
                if (State == States.Attacking || State == States.Resting)
                    State = States.Interrupted;

                Logger.Info(
                        (Name.Equals("warrior1") ? "You" : "Opponent")
                        + " had lost " + damage + " health points.");
                Life -= damage;

                return damage;
            }
        }

        public void Defend(int time)
        {
            if (time < 1)
            {
                Logger.Info("Invalid time!");
                return;
            }
            State = States.Defending;

            Logger.Info("You are defending for " + time + "s.");

            Thread.Sleep(time*1000);

            State = States.DoingNothing;
        }

        public int Rest(int time)
        {
            if (time < 1 || time > 10)
            {
                Logger.Info("Invalid time!");
                return 0;
            }
            State = States.Resting;

            Logger.Info("You are resting for " + time + "s");

            Thread.Sleep(time*1000);

            if (State == States.Interrupted)
            {
                Logger.Info("You were trying to rest, but you were interrupted");
                return 0;
            }

            State = States.DoingNothing;

            var healPoints = (int) Math.Pow(2.0, time);

            //Life += healPoints;

            //Opponent.Post("rest", "="+healPoints);

            return healPoints;
        }

        public int EnemyGetRested(int healPoints)
        {
            if (healPoints < 1)
            {
                Logger.Info("Invalid number!");
                return 0;
            }

            Logger.Info("Opponent has been healed by " + healPoints + " points!");
            Enemy.Life += healPoints;

            return healPoints;
        }

        public String Check()
        {
            Logger.Info("You are checking the State of your enemy");

            return Enemy.State.ToString();
        }

        public void DoNothing()
        {
            State = States.DoingNothing;
            Logger.Info("Idling");
        }

        private void ExecuteCommand(Commands command)
        {
            switch (command.Action)
            {
                case Actions.Attack :
                    Attack(command.Time);
                    break;
                case Actions.Defend :
                    Defend(command.Time);
                    break;
                case Actions.Rest :
                    Rest(command.Time);
                    break;
                case Actions.Check:
                    Check();
                    break;
                default :
                    DoNothing();
                    break;
            }
        }

        public bool IsAlive()
        {
            return Life > 0;
        }

        public void ExecuteNextCommand()
        {
            ExecuteCommand(_strategy[_currentActionNumber % _strategy.Count]);
        }
    }

    public class Commands
    {
        public Actions Action;
        public int Time;

        public Commands(Actions action, int time)
        {
            Action = action;
            Time = time;
        }
    }
}