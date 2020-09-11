using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterInfo_U : MonoBehaviour
{
    
    private Slider CritterHealth;
    private Text CritterName;
    private Text CritterAffinity;

    [SerializeField]
    private string NameOBJ;
    [SerializeField]
    private string AffinityOBJ;



    public void Awake()
    {
        CritterHealth = GetComponentInChildren<Slider>();
        CritterName = GameObject.Find(NameOBJ).GetComponent<Text>();
        CritterAffinity = GameObject.Find(AffinityOBJ).GetComponent<Text>();
    }

    public void SetBaseValues(float hpValue,string name, string affinity)
    {
        CritterHealth.maxValue = hpValue;
        CritterHealth.value = hpValue;
        CritterName.text = name;
        CritterAffinity.text = affinity;
    }

    public void UpdateHP(float hpValue)
    {
      
    }

    

}
