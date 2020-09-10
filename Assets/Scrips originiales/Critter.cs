using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRITTERS_
{
    class Critter
    {
        public string Name { get; private set; }                    //Attributes needed

        string[] names = { "Chorizard", "Wartortuga","HojiRana","Erizao","Perita","Retrasodrilo" }; //No me juzgue profe 
        public int BaseAttack { get; private set; }                //Each stat will have a base value, which won't change, and an actual stat which is gonna be used in combat just in case there's some modifications 
        public int AttackStat { get; private set; }

        public int BaseDefense { get; private set; }
        public int DefenseStat { get; private set; }

        public int BaseSpeed { get; private set; }
        public int SpeedStat { get; private set; }

        private int AtkUpCounter;
        private int DefUpCounter;
        private int SpeedDownCounter;
        public Eaffinity Affinity { get; private set; }

        public List<Skill> moveset = new List<Skill>();

        public int HP { get; private set; }

        public enum Eaffinity
        {
            Fire,
            Wind,
            Water,
            Earth,
            Light,
            Dark
        }

        public Critter(int name, int baseAttack, int baseDefense, int baseSpeed, int HP, int affinityNumber)
        {
            Name = names[name];
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            BaseSpeed = baseSpeed;
            this.HP = HP;

            AttackStat = baseAttack;
            DefenseStat = baseDefense;
            SpeedStat = baseSpeed;

            SetAffinity(affinityNumber);
            FillMoveset(moveset);



        }

        public void SetAffinity(int affinityNumber)
        {

            switch (affinityNumber)
            {
                case 1:
                    Affinity = Eaffinity.Fire;
                    break;
                case 2:
                    Affinity = Eaffinity.Wind;
                    break;
                case 3:
                    Affinity = Eaffinity.Water;
                    break;
                case 4:
                    Affinity = Eaffinity.Earth;
                    break;
                case 5:
                    Affinity = Eaffinity.Light;
                    break;
                case 6:
                    Affinity = Eaffinity.Dark;
                    break;

            }
        }
        public void FillMoveset(List<Skill> Crittermoveset)
        {
            Random random = new Random();
            if (Crittermoveset == moveset)
            {
                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        moveset.Add(new AttackSkill(random.Next(0,4), random.Next(1, 10), random.Next(1, 7)));
                    }
                    else
                    {
                        moveset.Add(new SupportSkill(random.Next(0,4), random.Next(1, 7),0,random.Next(1,4)));
                    }
                }

            }
        }

        public Skill UseSkill()
        {
            Random random = new Random();
            int numeroElegido = random.Next(0, (moveset.Count-1));

            return moveset[numeroElegido];
        }

        public void TakeDamage(AttackSkill receivedSkill, Critter attakingCritter)
        {
           int damageValue = (attakingCritter.BaseAttack + receivedSkill.Power);
            HP -= damageValue;
            Console.WriteLine(attakingCritter.Name + " Le ha hecho " + damageValue + " Con la Skill " + receivedSkill.Name + " a " + Name);
        }

        public void ReceiveBuff(SupportSkill receivedSkill)
        {
            if(receivedSkill.SuppType == SupportSkill.ESuppType.AtkUp && AtkUpCounter<3)
            {
                int atkValue = (AttackStat + (BaseAttack * 20) / 100);
                AttackStat += atkValue;
                Console.WriteLine(Name + " Ha aumentado su Atk hasta " + AttackStat);
                AtkUpCounter++;
            }
            else if (receivedSkill.SuppType == SupportSkill.ESuppType.AtkUp && AtkUpCounter >= 3)
            {
                Console.WriteLine("El ataque de " + Name + " No puede aumentar más");
            }
            else if (receivedSkill.SuppType == SupportSkill.ESuppType.DefUp && DefUpCounter < 3)
            {
                int defenseValue = (DefenseStat + (BaseDefense * 20) / 100);
                DefenseStat += defenseValue;
                Console.WriteLine(Name + " Ha aumentado su Def hasta " + DefenseStat);
                DefUpCounter++;
            }
            else if (receivedSkill.SuppType == SupportSkill.ESuppType.DefUp && DefUpCounter >= 3)
            {
                Console.WriteLine("La Defensa de " + Name + " No puede aumentar más");
            }
        }

        public void ThrowDebuff(Critter targetCritter)
        {
            if (targetCritter.SpeedDownCounter < 3)
            {
                int speedValue = (targetCritter.SpeedStat - (targetCritter.BaseSpeed * 20) / 100);
                targetCritter.SpeedStat -= speedValue;
                Console.WriteLine("La velocidad de " + targetCritter.Name + " ha bajado hasta " + speedValue);
                targetCritter.SpeedDownCounter++;
            }
        }
    }
}
