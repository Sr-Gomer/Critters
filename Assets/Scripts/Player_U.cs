using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_U : MonoBehaviour
{
    [SerializeField]
    public List<Critter_U> critters = new List<Critter_U>();

    public void AddCritter(Critter_U critter)
    {
        critters.Add(critter);
    }
}
