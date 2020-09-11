using CRITTERS_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter_U : MonoBehaviour
{
    [Header("Critter Information")]
    [SerializeField]
    private string name;
    [SerializeField]
    private float hp;

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
    private List<Skill_U> moveset = new List<Skill_U>();

    public string Name { get => name; }
    public float HP { get => hp; }
    public int BaseAttack { get => baseAttack; }
    public int BaseDefense { get => baseDefense; }
    public int BaseSpeed { get => baseSpeed; }
    public int AttackStat { get => attackStat; set => attackStat = value; }
    public int DefenseStat { get => defenseStat; set => defenseStat = value; }
    public int SpeedStat { get => speedStat; set => speedStat = value; }
    public Eaffinity Affinity { get => affinity; }
    public List<Skill_U> Moveset { get => moveset; set => moveset = value; }

    public enum Eaffinity
    {
        Fire,
        Wind,
        Water,
        Earth,
        Light,
        Dark
    }

    public void SetStats()
    {
        AttackStat = BaseAttack;
        DefenseStat = BaseDefense;
        SpeedStat = BaseSpeed;
    }
    public string TakeDamage(AttackSkill_U receivedSkill, Critter_U attakingCritter)
    {
        float damageValue = (attakingCritter.AttackStat + receivedSkill.Power) * AffinityMultiplier(receivedSkill);
        hp -= damageValue;

        return (Name + " ha recibido " + damageValue + " de daño, por " + receivedSkill.Name );
    }

    public string ReceiveBuff(SupportSkill_U receivedSkill, Critter_U targetCritter)
    {
        string message = "";

        if (receivedSkill.SuppType == SupportSkill_U.ESuppType.AtkUp && AtkUpCounter < 3)
        {
            int atkValue = (AttackStat + (BaseAttack * 20) / 100);
            AttackStat += atkValue;
            AtkUpCounter++;

            message = (Name + " ha aumentado su ataque ");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.AtkUp && AtkUpCounter >= 3)
        {
            message = ("El ataque de " + Name + " No puede aumentar más");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.DefUp && DefUpCounter < 3)
        {
            int defenseValue = (DefenseStat + (BaseDefense * 20) / 100);
            DefenseStat += defenseValue;
            DefUpCounter++;

            message = (Name + " ha aumentado su defensa ");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.DefUp && DefUpCounter >= 3)
        {
            message = ("La Defensa de " + Name + " No puede aumentar más");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.SpeedDown && targetCritter.SpeedDownCounter < 3)
        {
            int speedValue = (targetCritter.SpeedStat - (targetCritter.BaseSpeed * 20) / 100);
            targetCritter.SpeedStat -= speedValue;
            targetCritter.SpeedDownCounter++;

            message = ("La velocidad de " + targetCritter.Name + " se ha reducido ");
        }
        else if (receivedSkill.SuppType == SupportSkill_U.ESuppType.SpeedDown && targetCritter.SpeedDownCounter >= 3)
        {
            message = ("La Velocidad de " + targetCritter.Name + " No puede disminur más");
        }

        return message;
    }

    public float AffinityMultiplier(AttackSkill_U recieveSkill)
    {
        float affinityMultiplier = 0;

        if (Affinity == Eaffinity.Earth && recieveSkill.Affinity.ToString() == Eaffinity.Fire.ToString())
        {
            affinityMultiplier = 0;
        }
        else 
        if (   (Affinity.ToString() == recieveSkill.Affinity.ToString()) || 
               (Affinity == Eaffinity.Fire && recieveSkill.Affinity.ToString() == Eaffinity.Water.ToString()) || 
               (Affinity == Eaffinity.Water && recieveSkill.Affinity.ToString() == Eaffinity.Wind.ToString()) ||
               (Affinity == Eaffinity.Earth && recieveSkill.Affinity.ToString() == Eaffinity.Wind.ToString())   )
        {
            affinityMultiplier = 0.5f;
        }
        else 
        if (   (Affinity == Eaffinity.Light && recieveSkill.Affinity.ToString() == Eaffinity.Dark.ToString()) ||
               (Affinity == Eaffinity.Dark && recieveSkill.Affinity.ToString() == Eaffinity.Light.ToString()) ||
               (Affinity == Eaffinity.Water && recieveSkill.Affinity.ToString() == Eaffinity.Fire.ToString()) ||
               (Affinity == Eaffinity.Wind && recieveSkill.Affinity.ToString() == Eaffinity.Water.ToString()) ||
               (Affinity == Eaffinity.Wind && recieveSkill.Affinity.ToString() == Eaffinity.Earth.ToString())   )
        {
            affinityMultiplier = 2f;
        }
        else
        {
            affinityMultiplier = 1f;
        }

        return affinityMultiplier;
    }
}
