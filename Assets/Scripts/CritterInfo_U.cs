using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterInfo_U : MonoBehaviour
{
    
    private Slider CritterHealth;
    private Text CritterName;
    private Text CritterAffinity;
    private Image[] crittersIndicators = new Image[3];
    
    [SerializeField]
    private string NameOBJ;
    [SerializeField]
    private string AffinityOBJ;
    [SerializeField]
    private string CrittersIndicatorName;

    public void Awake()
    {
        CritterHealth = GetComponentInChildren<Slider>();
        CritterName = GameObject.Find(NameOBJ).GetComponent<Text>();
        CritterAffinity = GameObject.Find(AffinityOBJ).GetComponent<Text>();
        crittersIndicators[0] = GameObject.Find(CrittersIndicatorName + 1).GetComponent<Image>(); //PlayerCrittersIndicator EnemyCrittersIndicator
        crittersIndicators[0].color = new Color32(0, 255, 0, 100);
        crittersIndicators[1] = GameObject.Find(CrittersIndicatorName + 2).GetComponent<Image>();
        crittersIndicators[2] = GameObject.Find(CrittersIndicatorName + 3).GetComponent<Image>();
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
        CritterHealth.value = hpValue;
    }

    public void HideCrittersIndicator(int hideNumb)
    {
        crittersIndicators[hideNumb].gameObject.SetActive(false);
        crittersIndicators[hideNumb + 1].color = new Color32(0,255,0,100);
    }

    public void HideLastCritter()
    {
        crittersIndicators[2].gameObject.SetActive(false);
    }

}
