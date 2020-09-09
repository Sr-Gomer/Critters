using System;
using System.Collections.Generic;
using System.Text;

namespace CRITTERS_
{
    class GameManager
    {
        public Player darkar = new Player(3);
        public Player mecoboy = new Player(3);
        public Critter fightingCritter1 { get; private set; }
        public Critter fightingCritter2 { get; private set; }



        public string ThrowCritter()
        {
            string resultado = "";
            fightingCritter1 = darkar.critters.Dequeue();
            fightingCritter2 = mecoboy.critters.Dequeue();

            resultado = "Darkar ha lanzado a " + fightingCritter1.Name + " y Mecoboy ha lanzado a " + fightingCritter2.Name;

            return resultado;
         }

        public string ReplaceCritter(Critter critterReplaced)
        {
            string resultado = "";
            bool darkarCanFight = false;
            bool mecoboyCanFight = false;
            if(critterReplaced == fightingCritter1)
            {
                mecoboy.critters.Enqueue(fightingCritter1);
                fightingCritter1 = darkar.critters.Dequeue();
                if(fightingCritter1.HP <= 0)
                {
                    foreach( Critter critter in darkar.critters)
                    {
                        if(critter.HP > 0)
                        {
                            fightingCritter1 = critter;
                            darkarCanFight = true;

                            goto DarkarThrow;
                        }
                    }
                }
                else
                {
                 darkarCanFight = true;
                    goto DarkarThrow;
                }
                if (darkarCanFight == false)
                {
                    EndGame(1);
                }
            DarkarThrow:
                resultado = ("Darkar Lanza a " + fightingCritter1.Name);
            }
        
            

            if (critterReplaced == fightingCritter2)
            {
                darkar.critters.Enqueue(fightingCritter2);
                fightingCritter2 = mecoboy.critters.Dequeue();
                if (fightingCritter2.HP <= 0)
                {
                    foreach (Critter critter in mecoboy.critters)
                    {
                        if (critter.HP > 0)
                        {
                            fightingCritter2 = critter;
                            mecoboyCanFight = true;
                            goto MecoboyThrow;
                            
                        }
                    }
                }
                else
                {
                    mecoboyCanFight = true;
                    goto MecoboyThrow;
                }
                if (mecoboyCanFight == false)
                {
                    EndGame(2);
                }
            MecoboyThrow:
                resultado = "Mecoboy Lanza a " + fightingCritter2.Name;
            }
        

 
            
            return resultado;
        }
        
        public void EndGame(int playerLost)
        {
            if(playerLost == 1)
            {
                Console.WriteLine("Darkar Ya no puede Pelear, La victoría es para Mecoboy!! \n Presione enter para terminar");
                Console.ReadLine();
                System.Environment.Exit(0);
            }
            if (playerLost == 2)
            {
                Console.WriteLine("Mecoboy Ya no puede Pelear, La victoría es para Darkar!!  \n Presione enter para terminar");
                System.Environment.Exit(0);
            }
        }
        
        public void Combat(Critter fightingCritter1, Critter fightingCritter2)
        {
            if(fightingCritter1.SpeedStat > fightingCritter2.SpeedStat)
            {
                Skill usedSkill1 = fightingCritter1.UseSkill();
                if(usedSkill1 is AttackSkill atkUsed1)
                {
                    usedSkill1 = atkUsed1;
                    fightingCritter2.TakeDamage(atkUsed1, fightingCritter1);
                }
                else if(usedSkill1 is SupportSkill suppUsed1)
                {
                    usedSkill1 = suppUsed1;
                    if(suppUsed1.SuppType == SupportSkill.ESuppType.AtkUp || suppUsed1.SuppType == SupportSkill.ESuppType.DefUp)
                    {
                        fightingCritter1.ReceiveBuff(suppUsed1);
                    }
                    else
                    {
                        fightingCritter1.ThrowDebuff(fightingCritter2);
                    }
                }

                CheckHP(fightingCritter2);
                
                    Skill usedSkill2 = fightingCritter2.UseSkill();
                    if (usedSkill1 is AttackSkill atkUsed2)
                    {
                        usedSkill2 = atkUsed2;
                        fightingCritter1.TakeDamage(atkUsed2, fightingCritter2);
                    }
                    else if (usedSkill2 is SupportSkill suppUsed2)
                    {
                        usedSkill2 = suppUsed2;
                        if (suppUsed2.SuppType == SupportSkill.ESuppType.AtkUp || suppUsed2.SuppType == SupportSkill.ESuppType.DefUp)
                        {
                            fightingCritter2.ReceiveBuff(suppUsed2);
                        }
                        else
                        {
                            fightingCritter2.ThrowDebuff(fightingCritter1);
                        }
                    }
                CheckHP(fightingCritter1);
            }
            else
            {
                Skill usedSkill2 = fightingCritter2.UseSkill();
                if (usedSkill2 is AttackSkill atkUsed2)
                {
                    usedSkill2 = atkUsed2;
                    fightingCritter1.TakeDamage(atkUsed2, fightingCritter2);
                }
                else if (usedSkill2 is SupportSkill suppUsed2)
                {
                    usedSkill2 = suppUsed2;
                    if (suppUsed2.SuppType == SupportSkill.ESuppType.AtkUp || suppUsed2.SuppType == SupportSkill.ESuppType.DefUp)
                    {
                        fightingCritter2.ReceiveBuff(suppUsed2);
                    }
                    else
                    {
                        fightingCritter2.ThrowDebuff(fightingCritter1);
                    }
                }

                CheckHP(fightingCritter1);
                    Skill usedSkill1 = fightingCritter1.UseSkill();
                    if (usedSkill1 is AttackSkill atkUsed1)
                    {
                        usedSkill1 = atkUsed1;
                        fightingCritter2.TakeDamage(atkUsed1, fightingCritter1);
                    }
                    else if (usedSkill1 is SupportSkill suppUsed1)
                    {
                        usedSkill1 = suppUsed1;
                        if (suppUsed1.SuppType == SupportSkill.ESuppType.AtkUp || suppUsed1.SuppType == SupportSkill.ESuppType.DefUp)
                        {
                            fightingCritter1.ReceiveBuff(suppUsed1);
                        }
                        else
                        {
                            fightingCritter1.ThrowDebuff(fightingCritter2);
                        }
                    }

                CheckHP(fightingCritter2);
            }
            
        }

        public void CheckHP(Critter targetCritter)
        {
            if (targetCritter.HP < 0)
            {
                Console.WriteLine(targetCritter.Name + " Ha sido Derrotado");
                Console.WriteLine(ReplaceCritter(targetCritter));
            }
        }
        

        
    }
}
