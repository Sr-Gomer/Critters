using CRITTERS_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter_U : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int hp;

    [SerializeField]
    private int baseAttack;
    private int attackStat;
    [SerializeField]
    private int baseDefense;
    private int defenseStat;
    [SerializeField]
    private int baseSpeed;
    private int speedStat;

    private int AtkUpCounter;
    private int DefUpCounter;
    private int SpeedDownCounter;


    [SerializeField]
    private Eaffinity affinity;

    [SerializeField]
    public List<Skill_U> moveset = new List<Skill_U>();

    public string Name { get => name; }
    public int HP { get => hp; }
    public int BaseAttack { get => baseAttack; }
    public int BaseDefense { get => baseDefense; }
    public int BaseSpeed { get => baseSpeed; }
    public int AttackStat { get => attackStat; set => attackStat = value; }
    public int DefenseStat { get => defenseStat; set => defenseStat = value; }
    public int SpeedStat { get => speedStat; set => speedStat = value; }
    public Eaffinity Affinity { get => affinity; }

    public enum Eaffinity
    {
        Fire,
        Wind,
        Water,
        Earth,
        Light,
        Dark
    }

    public Skill_U UseSkill(int numeroElegido)
    {
        return moveset[numeroElegido];
    }

    public void TakeDamage(AttackSkill_U receivedSkill, Critter_U attakingCritter)
    {
        int damageValue = (attakingCritter.BaseAttack + receivedSkill.Power);
        hp -= damageValue;
        //Console.WriteLine(attakingCritter.Name + " Le ha hecho " + damageValue + " Con la Skill " + receivedSkill.Name + " a " + Name);
    }

    public void ReceiveBuff(SupportSkill_U receivedSkill)
    {
        if (receivedSkill.SuppType == SupportSkill_U.ESuppType.AtkUp && AtkUpCounter < 3)
        {
            int atkValue = (AttackStat + (BaseAttack * 20) / 100);
            AttackStat += atkValue;
            //Console.WriteLine(Name + " Ha aumentado su Atk hasta " + AttackStat);
            AtkUpCounter++;
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.AtkUp && AtkUpCounter >= 3)
        {
            //Console.WriteLine("El ataque de " + Name + " No puede aumentar más");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.DefUp && DefUpCounter < 3)
        {
            int defenseValue = (DefenseStat + (BaseDefense * 20) / 100);
            DefenseStat += defenseValue;
            //Console.WriteLine(Name + " Ha aumentado su Def hasta " + DefenseStat);
            DefUpCounter++;
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.DefUp && DefUpCounter >= 3)
        {
            //Console.WriteLine("La Defensa de " + Name + " No puede aumentar más");
        }
    }

    public void ThrowDebuff(Critter_U targetCritter)
    {
        if (targetCritter.SpeedDownCounter < 3)
        {
            int speedValue = (targetCritter.SpeedStat - (targetCritter.BaseSpeed * 20) / 100);
            targetCritter.SpeedStat -= speedValue;
            //Console.WriteLine("La velocidad de " + targetCritter.Name + " ha bajado hasta " + speedValue);
            targetCritter.SpeedDownCounter++;
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Critter_U u &&
               base.Equals(obj) &&
               AttackStat == u.AttackStat;
    }
}
