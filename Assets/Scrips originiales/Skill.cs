using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace CRITTERS_
{
    public abstract class Skill
    {
        public string Name { get; protected set; }
        public int Power { get; protected set; }

        string[] names = { "Anillazo", "Boosted", "Telaraña... espera que?", "Aliento mañanero", "Mordelon" };
        


        public enum Eaffinity
        {
            Fire,
            Wind,
            Water,
            Earth,
            Light,
            Dark
        }

        public Eaffinity Affinity { get; protected set; }
        public Skill(int name,int power, int affinity)
        {
            Name = names[name];
            Power = power;
            SetAffinity(affinity);
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
        
    }
}
