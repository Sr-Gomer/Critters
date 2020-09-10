using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI_U : MonoBehaviour
{
    [Header("Skill Information")]
    [SerializeField]
    public Text name;
    public Text power;
    public Text type;
    public Text affinity;

    [SerializeField]
    public int skillIndex;

    public void fillText(Critter_U inGameCritter)
    {
        if(inGameCritter.Moveset[skillIndex] is AttackSkill_U)
        {
            name.text = inGameCritter.Moveset[skillIndex].name;
            power.text = inGameCritter.Moveset[skillIndex].Power.ToString();
            type.text = "Attack Skill";
            affinity.text = inGameCritter.Moveset[skillIndex].Affinity.ToString();
        }
        else
        {
            name.text = inGameCritter.Moveset[skillIndex].name;
            power.text = "0";
            type.text = (inGameCritter.Moveset[skillIndex] as SupportSkill_U).SuppType.ToString();
            affinity.text = inGameCritter.Moveset[skillIndex].Affinity.ToString();
        }
    }
}
