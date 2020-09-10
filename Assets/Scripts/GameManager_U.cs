using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_U : MonoBehaviour
{
    [Header("Elementos de juego")]
    [SerializeField]
    private Judge_U judge;

    private void Awake()
    {
        judge = GameObject.FindGameObjectWithTag("Judge").GetComponent<Judge_U>();
    }

    private void Start()
    {
        judge.Initialize();
    }
}
