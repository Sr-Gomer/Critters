using System.Collections;
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

    private CritterInfo_U playerCritterInfo;
    private CritterInfo_U enemyCritterInfo;

    private int playerDeathCount = 0, enemyDeathCount = 0;

    private bool isDeadPlayer;
    private bool isDeadEnemy;

    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_U>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Player_U>();

        inGamePlayerCritter = player.critters[0];
        inGamePlayerCritter.SetStats();
        inGameEnemyCritter = enemy.critters[0];
        inGameEnemyCritter.SetStats();

        inGamePlayerCritter.gameObject.SetActive(true);
        inGameEnemyCritter.gameObject.SetActive(true);

        playerCritterInfo = GameObject.Find("UI_P").GetComponent<CritterInfo_U>();
        enemyCritterInfo = GameObject.Find("UI_E").GetComponent<CritterInfo_U>();

        playerCritterInfo.SetBaseValues(InGamePlayerCritter.HP,InGamePlayerCritter.Name,InGamePlayerCritter.Affinity.ToString());
        enemyCritterInfo.SetBaseValues(InGameEnemyCritter.HP,InGameEnemyCritter.Name,InGameEnemyCritter.Affinity.ToString());

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

        isDeadPlayer = false;
        isDeadEnemy = false;
    }

    public Critter_U InGamePlayerCritter { get => inGamePlayerCritter; private set { inGamePlayerCritter = value; } }
    public Critter_U InGameEnemyCritter { get => inGameEnemyCritter; private set { inGameEnemyCritter = value; } }

    public void Action(int skillNumber)
    {
        
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
    }

    IEnumerator PlayerAction(int skillNumber,float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        gameMessagesPanel.SetActive(true);
        if (InGamePlayerCritter.Moveset[skillNumber] is AttackSkill_U)
        {
            gameMessages.text = InGameEnemyCritter.TakeDamage(InGamePlayerCritter.Moveset[skillNumber] as AttackSkill_U, InGamePlayerCritter);
            enemyCritterInfo.UpdateHP(InGameEnemyCritter.HP);

            if(InGameEnemyCritter.HP <= 0)
            {
                ChangeCritter(InGameEnemyCritter, player, enemy, false);
            }
        }
        else
        {
            gameMessages.text = InGamePlayerCritter.ReceiveBuff(InGamePlayerCritter.Moveset[skillNumber] as SupportSkill_U, inGameEnemyCritter);
        }


        if (enemyDeathCount < 3)
        {
            yield return new WaitForSeconds(timeDelay + 2.98f);
            gameMessagesPanel.SetActive(false); 
        }
    }

    IEnumerator EnemyAction(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        gameMessagesPanel.SetActive(true);

        int enemySkillNumber;

        enemySkillNumber = Random.Range(0, 3);

        if (InGameEnemyCritter.Moveset[enemySkillNumber] is AttackSkill_U)
        {
            gameMessages.text = InGamePlayerCritter.TakeDamage(InGameEnemyCritter.Moveset[enemySkillNumber] as AttackSkill_U, InGameEnemyCritter);
            playerCritterInfo.UpdateHP(InGamePlayerCritter.HP);
            if (InGamePlayerCritter.HP <= 0)
            {
                ChangeCritter(InGamePlayerCritter, enemy, player, true);
            }
        }
        else
        {
            gameMessages.text = InGameEnemyCritter.ReceiveBuff(InGameEnemyCritter.Moveset[enemySkillNumber] as SupportSkill_U, InGamePlayerCritter);
        }

        if (playerDeathCount < 3)
        {
            yield return new WaitForSeconds(timeDelay + 2.98f);
            gameMessagesPanel.SetActive(false);
        }
        
    }

    private void ChangeCritter(Critter_U deathCitter, Player_U newOwner, Player_U oldOwner, bool isPlayer) 
    {
        newOwner.AddCritter(deathCitter);
        deathCitter.gameObject.SetActive(false);

        if (isPlayer && playerDeathCount < 3)
        {
            playerDeathCount++;
            if (playerDeathCount < 3)
            {
                InGamePlayerCritter = oldOwner.critters[oldOwner.critters.IndexOf(deathCitter) + 1];
                InGamePlayerCritter.gameObject.SetActive(true);
                InGamePlayerCritter.SetStats();
                playerCritterInfo.SetBaseValues(InGamePlayerCritter.HP, InGamePlayerCritter.Name, InGamePlayerCritter.Affinity.ToString());
                playerCritterInfo.HideCrittersIndicator(oldOwner.critters.IndexOf(deathCitter));
            }

            for (int i = 0; i < panels.Length; i++)
            {
                playerSkills[i].fillText(InGamePlayerCritter.Moveset[i],
                                            InGamePlayerCritter.Moveset[i].name,
                                            InGamePlayerCritter.Moveset[i].Power.ToString(),
                                            InGamePlayerCritter.Moveset[i].Affinity.ToString());
            }


            if(playerDeathCount >= 3)
            {
                gameMessagesPanel.SetActive(true);
                gameMessages.text = "Defeat";
                playerCritterInfo.HideLastCritter();
            }

        }
        else if (!isPlayer && enemyDeathCount < 3)
        {
            enemyDeathCount++;
            if(enemyDeathCount < 3)
            {
                InGameEnemyCritter = oldOwner.critters[oldOwner.critters.IndexOf(deathCitter) + 1];
                InGameEnemyCritter.gameObject.SetActive(true);
                InGameEnemyCritter.SetStats();
                enemyCritterInfo.SetBaseValues(InGameEnemyCritter.HP, InGameEnemyCritter.Name, InGameEnemyCritter.Affinity.ToString());
                enemyCritterInfo.HideCrittersIndicator(oldOwner.critters.IndexOf(deathCitter));
            }

            if (enemyDeathCount >= 3)
            {
                enemyCritterInfo.HideLastCritter();
                gameMessagesPanel.SetActive(true);
                gameMessages.text = "Victory";
            }
        }
    }



}
