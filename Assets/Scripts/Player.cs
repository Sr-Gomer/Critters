using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CRITTERS_
{
    class Player
    {
        public Queue<Critter> critters = new Queue<Critter>(); //Critter collection for the player
        
        public Player(int numberCritters)
        {
            FillCritters(critters,numberCritters);
        }
        public void FillCritters(Queue<Critter> fillingCritters, int numberCritters)
        {
            Random random = new Random();
            if (fillingCritters == critters)
            {
                
                for (int i = 0; i < numberCritters; i++)
                {
                    critters.Enqueue(new Critter(random.Next(0,6), random.Next(1, 101), random.Next(1, 101), random.Next(1, 51), random.Next(1, 101), random.Next(1, 7)));
                }
            }
        }
    }
}
