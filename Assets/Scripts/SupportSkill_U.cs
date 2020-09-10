using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class SupportSkill_U : Skill_U
{
    public enum ESuppType
    {
        AtkUp,
        DefUp,
        SpeedDown,
    }

    [SerializeField]
    private ESuppType suppType;
    
    public ESuppType SuppType { get => suppType; }
     
    public void SetType(int settedType)
    {
        switch (settedType)
        {
            case (1):
                suppType = ESuppType.AtkUp;
                break;
            case (2):
                suppType = ESuppType.DefUp;
                break;
            case (3):
                suppType = ESuppType.SpeedDown;
                break;
        }
    }
}
