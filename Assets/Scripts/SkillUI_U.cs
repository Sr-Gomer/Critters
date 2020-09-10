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

    public void fillText(Skill_U castedSkill, string name, string power, string affinity)
    {
        if(castedSkill is AttackSkill_U)
        {
            this.name.text = name;
            this.power.text = power;
            this.type.text = "Attack Skill";
            this.affinity.text = affinity;
        }
        else
        {
            this.name.text = name;
            this.power.text = "0";
            this.type.text = (castedSkill as SupportSkill_U).SuppType.ToString();
            this.affinity.text = affinity;
        }
    }
}
