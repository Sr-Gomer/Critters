using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_U : MonoBehaviour
{
    public Queue<Critter_U> critters = new Queue<Critter_U>();

    public void AddCritter(Critter_U critter)
    {
        critters.Enqueue(critter);
    }
}
