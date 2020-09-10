using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_U : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int power;

    [SerializeField]
    protected Eaffinity affinity;

    public enum Eaffinity
    {
        Fire,
        Wind,
        Water,
        Earth,
        Light,
        Dark
    }

    public string Name { get => name; }
    public int Power { get => power; }
    public Eaffinity Affinity { get => affinity; }

}
