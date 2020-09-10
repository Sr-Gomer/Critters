﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge_U : MonoBehaviour
{
    private Player_U player;
    private Player_U enemy;

    private Critter_U inGamePlayerCritter;
    private Critter_U inGameEnemyCritter;

    private GameObject gameMessagesPanel;
    private Text gameMessages;

    private GameObject[] panels;
    private SkillUI_U[] playerSkills = new SkillUI_U[3];

    private int playerDeathCount = 0, enemyDeathCount = 0;

    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_U>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Player_U>();

        inGamePlayerCritter = player.critters[0];
        inGameEnemyCritter = enemy.critters[0];

        inGamePlayerCritter.gameObject.SetActive(true);
        inGameEnemyCritter.gameObject.SetActive(true);

        gameMessagesPanel = GameObject.FindGameObjectWithTag("Messages");
        gameMessages = gameMessagesPanel.GetComponentInChildren<Text>();

        gameMessagesPanel.SetActive(false);

        panels = GameObject.FindGameObjectsWithTag("SkillInfo");
        for (int i = 0; i < panels.Length; i++)
        {
            playerSkills[i] = panels[i].GetComponent<SkillUI_U>();

            playerSkills[i].fillText(   inGamePlayerCritter.Moveset[i],
                                        inGamePlayerCritter.Moveset[i].Name,
                                        inGamePlayerCritter.Moveset[i].Power.ToString(),
                                        inGamePlayerCritter.Moveset[i].Affinity.ToString());
        }
    }

    public Critter_U InGamePlayerCritter { get => inGamePlayerCritter;}
    public Critter_U InGameEnemyCritter { get => inGameEnemyCritter;}

    public void Action(int skillNumber)
    {
        gameMessagesPanel.SetActive(true);

        if (InGamePlayerCritter.SpeedStat >= InGameEnemyCritter.SpeedStat)
        {
            StartCoroutine(PlayerAction(skillNumber, 0f));
            StartCoroutine(EnemyAction(3f));
        }
        else
        {
            StartCoroutine(EnemyAction(0f));
            StartCoroutine(PlayerAction(skillNumber, 3f));
        }

        gameMessagesPanel.SetActive(false);
    }

    IEnumerator PlayerAction(int skillNumber,float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        if (InGamePlayerCritter.Moveset[skillNumber] is AttackSkill_U)
        {
            gameMessages.text = InGameEnemyCritter.TakeDamage(InGamePlayerCritter.Moveset[skillNumber] as AttackSkill_U, InGamePlayerCritter);

            if(InGameEnemyCritter.HP <= 0)
            {
                ChangeCritter(InGameEnemyCritter, player, enemy, false);
            }
        }
        else
        {
            gameMessages.text = InGamePlayerCritter.ReceiveBuff(InGamePlayerCritter.Moveset[skillNumber] as SupportSkill_U, inGameEnemyCritter);
        }
    }

    IEnumerator EnemyAction(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        int enemySkillNumber;

        enemySkillNumber = Random.Range(0, 3);

        if (InGameEnemyCritter.Moveset[enemySkillNumber] is AttackSkill_U)
        {
            gameMessages.text = InGamePlayerCritter.TakeDamage(InGameEnemyCritter.Moveset[enemySkillNumber] as AttackSkill_U, InGameEnemyCritter);

            if (InGameEnemyCritter.HP <= 0)
            {
                ChangeCritter(InGamePlayerCritter, enemy, player, true);
            }
        }
        else
        {
            gameMessages.text = InGameEnemyCritter.ReceiveBuff(InGameEnemyCritter.Moveset[enemySkillNumber] as SupportSkill_U, InGamePlayerCritter);
        }
    }

    private void ChangeCritter(Critter_U deathCitter, Player_U newOwner, Player_U oldOwner, bool isPlayer)
    {
        newOwner.AddCritter(deathCitter);
        deathCitter.gameObject.SetActive(false);

        if (isPlayer && enemyDeathCount < 3)
        {
            inGameEnemyCritter = oldOwner.critters[oldOwner.critters.IndexOf(deathCitter) + 1];
            inGameEnemyCritter.gameObject.SetActive(true);
            enemyDeathCount++;
        }
        else if (enemyDeathCount >= 3)
        {
            gameMessagesPanel.SetActive(true);
            gameMessages.text = "Victory";
        }
        else if (!isPlayer && playerDeathCount < 3)
        {
            inGamePlayerCritter = oldOwner.critters[oldOwner.critters.IndexOf(deathCitter) + 1];
            inGamePlayerCritter.gameObject.SetActive(true);
            playerDeathCount++;

            for (int i = 0; i < panels.Length; i++)
            {
                playerSkills[i].fillText(inGamePlayerCritter.Moveset[i],
                                            inGamePlayerCritter.Moveset[i].name,
                                            inGamePlayerCritter.Moveset[i].Power.ToString(),
                                            inGamePlayerCritter.Moveset[i].Affinity.ToString());
            }
        }
        else if (playerDeathCount >= 3)
        {
            gameMessagesPanel.SetActive(true);
            gameMessages.text = "Defeat";
        }
    }

    private void ActivatedPanel()
    {
        Game
    }

}
