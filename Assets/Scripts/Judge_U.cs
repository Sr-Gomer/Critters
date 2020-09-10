using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge_U : MonoBehaviour
{
    private Player_U player;
    private Player_U enemy;

    private Critter_U inGamePlayerCritter;
    private Critter_U inGameEnemyCritter;

    public void Action(int skillNumber)
    {
        if (inGamePlayerCritter.moveset[skillNumber] is AttackSkill_U)
        {
            inGameEnemyCritter.TakeDamage(inGamePlayerCritter.moveset[skillNumber] as AttackSkill_U, inGamePlayerCritter);
        }
        else
        {
            inGameEnemyCritter.ReceiveBuff(inGamePlayerCritter.moveset[skillNumber] as SupportSkill_U, inGamePlayerCritter);
        }
    }
}
