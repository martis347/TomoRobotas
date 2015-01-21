using System;
using System.Threading;
using Business;
using log4net.Config;
using NUnit.Framework;

namespace RobotsAtWar.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _warrior1;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {
            _warrior1 = new Warrior("You");
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(3, 5)]
        [TestCase(4, 0)]
        [TestCase(Int32.MaxValue, 0)]
        [TestCase(Int32.MinValue, 0)]
        public void CheckAttackAction(int time, int damage)
        {
            _warrior1.Check();
            Assert.AreEqual(_warrior1.Attack(time), damage);
        }

        [Test]
        [TestCase(-9, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 0)]
        public void CheckGetAttackedAction(int damage, int receivedDamage)
        {
            _warrior1.Check();
            Assert.AreEqual(_warrior1.GetAttacked(damage), receivedDamage);
        }

        [Test]
        public void CheckGetAttackedActionWhileDefending()
        {
            Thread thread = new Thread(() => _warrior1.Defend(3));
            thread.Start();
            Thread.Sleep(500);
            Assert.AreEqual(_warrior1.GetAttacked(2), 0);
        }

        [Test]
        [TestCase(-9, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(3, 8)]
        [TestCase(Int32.MaxValue, 0)]
        public void CheckRestAction(int health, int expected)
        {
            Assert.AreEqual(_warrior1.Rest(health), expected);
        }

        //TODO: test a propper object.
//        [Test]
//        [TestCase()]
//        public void CheckCheckAction()
//        {
//            Thread thread = new Thread(() => _warrior1.Defend(3));
//            thread.Start();
//            Thread.Sleep(500);
//            Assert.AreEqual(_opponent.Check(), States.Defending.ToString());
//
//            thread = new Thread(() => _warrior1.Attack(3));
//            thread.Start();
//            Thread.Sleep(500);
//            Assert.AreEqual(_opponent.Check(), States.Attacking.ToString());
//
//            thread = new Thread(() => _warrior1.Rest(3));
//            thread.Start();
//            Thread.Sleep(500);
//            Assert.AreEqual(_opponent.Check(), States.Resting.ToString());
//        }

//        [Test]
//        public void CheckInterupting()
//        {
//            Thread warriorThread = new Thread(() => _warrior1.Attack(3));
//            Thread opponentThread = new Thread(() => _opponent.Attack(1));
//            warriorThread.Start();
//            Thread.Sleep(500);
//            Assert.AreEqual(_opponent.Check(), States.Attacking.ToString());
//            opponentThread.Start();
//            Thread.Sleep(1500);
//            Assert.AreEqual(_opponent.Check(), States.Interrupted.ToString());
//        }
    }
}
