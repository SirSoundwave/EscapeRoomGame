using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public static Unit playerUnit;
    public static Unit enemyUnit;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI finalText;
    public Text tutorialTitleText;
    public Text finalTitleText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject CombatButtons;
    public GameObject[] ActionButtons;

    public VictoryPanel BattleVictoryPanel;
    public GameObject TutorialPanel;
    public GameObject FinalPanel;

    public Animator ActionAnimator;
    public Animator PlayerAnimator;
    public Animator AnimatorOnPlayer;
    public Animator EnemyAnimator;
    public Animator AnimatorOnEnemy;

    public Animator DialougePanelAnimator;
    public Animator TutorialPanelAnimator;
    public Animator FinalPanelAnimator;

    public static float difficultyMultiplier = 1f;
    private int currentBattleTurns = 1;

    private const int BUTTONS_COUNT = 4;
    public static int currentEnemyId = 0;
    private bool actionTypeIsMove;

    public AudioSource audioSource;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameData.Initialize();
        playerUnit = GameData.player;
        enemyUnit = GameData.enemies[0];

        dialogueText.text = "A wild " +	enemyUnit.name + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        OnShowTutorialPanel();
        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {
        PlayerAnimator.SetTrigger("Idle");
        EnemyAnimator.SetTrigger("Idle");
        if (currentBattleTurns > 5)
        {
            difficultyMultiplier *= 1.1f;
        }
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerAction(int index)
    {
        
        StatsObject action = actionTypeIsMove ? playerUnit.moves[index] : playerUnit.items[index];
        dialogueText.text = "Used " + action.name + "!";

        PlayerAnimator.SetTrigger("None");
        EnemyAnimator.SetTrigger("None");
        DialougePanelAnimator.SetTrigger("Right");

        yield return new WaitForSeconds(1f);

        PlayerAnimator.SetTrigger(action.userAnimationName);
        yield return new WaitForSeconds(action.userAnimationDuration);

        AnimatorOnPlayer.SetTrigger(action.onUserAnimationName);
        yield return new WaitForSeconds(action.onUserAnimationDuration);

        ActionAnimator.SetTrigger("Player" + action.actionAnimationName);
        yield return new WaitForSeconds(action.actionAnimationDuration);

        EnemyAnimator.SetTrigger(action.recieverAnimationName);
        yield return new WaitForSeconds(action.recieverAnimationDuration);

        AnimatorOnEnemy.SetTrigger(action.onReceiverAnimationName);
        yield return new WaitForSeconds(action.onReceiverAnimationDuration);

        dialogueText.text = "";
        bool isDead = false;
        int multiplier = 1;
        if (action.isAttack)
        {
            if (enemyUnit.IsDodge())
            {
                dialogueText.text = "The attack was dodged!";
            } else
            {
                if (playerUnit.IsCritical())
                {
                    multiplier = 2;
                    dialogueText.text = "Critical hit! ";
                }
                float damage = playerUnit.CalculateDamage(action) * multiplier;
                isDead = enemyUnit.TakeDamage(damage);
                dialogueText.text += "Dealt " + GameData.NumberFormatter(damage * enemyUnit.CalculateMitigationPercentage()) + " damage!";
                if (enemyUnit.CalculateMitigationPercentage() != 1f)
                {
                    dialogueText.text += "\n(" + GameData.NumberFormatter( damage - damage * enemyUnit.CalculateMitigationPercentage()) + " damage was mitigated by defense!)";
                }
                playerHUD.SetHP(playerUnit.currentHP, playerUnit.CalculateTotalStat(StatType.health));
                enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.CalculateTotalStat(StatType.health));
                yield return new WaitForSeconds(1f);
            }
            
            progressText.gameObject.SetActive(true);
            yield return StartWaitingForClick();
            progressText.gameObject.SetActive(false);
        }

        float oldPlayerHp = playerUnit.currentHP;
        float oldEnemyHp = enemyUnit.currentHP;
        List<string> changes = playerUnit.ApplyActionStatChanges(action);
        playerUnit.ApplyOnTurnStatChanges();

        playerHUD.SetHP(playerUnit.currentHP, playerUnit.CalculateTotalStat(StatType.health));
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.CalculateTotalStat(StatType.health));

        if (oldPlayerHp != playerUnit.currentHP || oldEnemyHp != enemyUnit.currentHP)
        {
            yield return new WaitForSeconds(1f);
        }

        foreach (string change in changes)
        {
            dialogueText.text = "Gunk Thunder's " + change;
            progressText.gameObject.SetActive(true);
            yield return StartWaitingForClick();
            progressText.gameObject.SetActive(false);
        }

        if (!actionTypeIsMove) playerUnit.items.RemoveAt(index);

        if (isDead)
        {
            state = BattleState.WON;
            EnemyAnimator.SetTrigger("Death");
            yield return new WaitForSeconds(1f);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAction());
        }

    }

    IEnumerator EnemyAction()
    {
        StatsObject action = enemyUnit.moves[UnityEngine.Random.Range(0, 4)];
        dialogueText.text = enemyUnit.name + " uses\n" + action.name + "!";

        yield return new WaitForSeconds(1f);

        EnemyAnimator.SetTrigger(action.userAnimationName);
        yield return new WaitForSeconds(action.userAnimationDuration);

        AnimatorOnEnemy.SetTrigger(action.onUserAnimationName);
        yield return new WaitForSeconds(action.onUserAnimationDuration);

        ActionAnimator.SetTrigger("Enemy" + action.actionAnimationName);
        yield return new WaitForSeconds(action.actionAnimationDuration);

        PlayerAnimator.SetTrigger(action.recieverAnimationName);
        yield return new WaitForSeconds(action.recieverAnimationDuration);

        AnimatorOnPlayer.SetTrigger(action.onReceiverAnimationName);
        yield return new WaitForSeconds(action.onReceiverAnimationDuration);

        dialogueText.text = "";
        bool isDead = false;
        int multiplier = 1;

        if (action.isAttack)
        {
            if (playerUnit.IsDodge())
            {
                dialogueText.text = "The attack was dodged!";
            }
            else
            {
                if (enemyUnit.IsCritical())
                {
                    multiplier = 2;
                    dialogueText.text = "Critical hit! ";
                }
                float damage = enemyUnit.CalculateDamage(action) * multiplier * difficultyMultiplier;
                isDead = playerUnit.TakeDamage(damage);
                dialogueText.text += "Dealt " + GameData.NumberFormatter(damage * playerUnit.CalculateMitigationPercentage()) + " damage!";
                if (playerUnit.CalculateMitigationPercentage() != 1f)
                {
                    dialogueText.text += "\n(" + GameData.NumberFormatter(damage - damage * playerUnit.CalculateMitigationPercentage()) + " damage was mitigated by defense!)";
                }
                playerHUD.SetHP(playerUnit.currentHP, playerUnit.CalculateTotalStat(StatType.health));
                enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.CalculateTotalStat(StatType.health));
                yield return new WaitForSeconds(1f);
            }
            
            progressText.gameObject.SetActive(true);
            yield return StartWaitingForClick();
            progressText.gameObject.SetActive(false);
        }

        float oldPlayerHp = playerUnit.currentHP;
        float oldEnemyHp = enemyUnit.currentHP;
        List<string> changes = enemyUnit.ApplyActionStatChanges(action);
        enemyUnit.ApplyOnTurnStatChanges();

        playerHUD.SetHP(playerUnit.currentHP, playerUnit.CalculateTotalStat(StatType.health));
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.CalculateTotalStat(StatType.health));

        if (oldPlayerHp != playerUnit.currentHP || oldEnemyHp != enemyUnit.currentHP)
        {
            yield return new WaitForSeconds(1f);
        }

        foreach (string change in changes)
        {
            dialogueText.text = enemyUnit.name + "'s " + change;
            progressText.gameObject.SetActive(true);
            yield return StartWaitingForClick();
            progressText.gameObject.SetActive(false);
        }

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            currentBattleTurns++;
            CombatButtons.SetActive(true);
            DialougePanelAnimator.SetTrigger("Left");
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            StartCoroutine(BattleWon());
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    IEnumerator BattleWon()
    {
        BattleVictoryPanel.StatsPanel.SetActive(false);
        BattleVictoryPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        progressText.gameObject.SetActive(true);
        yield return StartWaitingForClick();
        progressText.gameObject.SetActive(false);
        if (currentEnemyId == 4)
        {
            OnShowFinalPanel();
        }
        else 
        {
            StartCoroutine(BattleVictoryPanel.ShowLevelUpPanel());
        }
    }

    public void StartNextBattle()
    {
        if (state != BattleState.WON)
        {
            BattleVictoryPanel.HideStatsPanel();
            return;
        }
        DialougePanelAnimator.SetTrigger("Left");
        StatType[] allStatTypes = (StatType[])System.Enum.GetValues(typeof(StatType));
        StatTypeType[] allStatTypeTypes = (StatTypeType[])System.Enum.GetValues(typeof(StatTypeType));

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                enemyUnit.stats.stats[statType].stat[statTypeType] -= GameData.enemiesUnedited[currentEnemyId].stats.stats[statType].stat[statTypeType];
            }
        }

        Stats gainedStats = enemyUnit.stats;

        currentEnemyId++;
        enemyUnit = GameData.enemies[currentEnemyId];

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                enemyUnit.stats.stats[statType].stat[statTypeType] += gainedStats.stats[statType].stat[statTypeType];
            }
        }

        enemyUnit.currentHP = enemyUnit.CalculateTotalStat(StatType.health);

        enemy.GetComponent<Transform>().position = enemyUnit.position;
        enemy.GetComponent<Transform>().localScale = enemyUnit.scale;
        enemy.GetComponent<SpriteRenderer>().sprite = enemyUnit.sprite;
        enemyHUD.SetHUD(enemyUnit);
        playerHUD.SetHUD(playerUnit);
        state = BattleState.PLAYERTURN;
        currentBattleTurns = 1;
        OnShowTutorialPanel();
        audioSource.clip = Resources.Load<AudioClip>("Audio/" + currentEnemyId);
        audioSource.Play();
        PlayerTurn();
        CombatButtons.SetActive(true);
    }

    public void OnShowActionsButton(bool actionTypeIsMove)
    {
        if (state != BattleState.PLAYERTURN)
            return;
        CombatButtons.SetActive(false);
        this.actionTypeIsMove = actionTypeIsMove;

        List<StatsObject> playerActions = actionTypeIsMove ? playerUnit.moves : playerUnit.items;

        for (int i = 0; i < playerActions.Count; i++)
        {
            ActionButtons[i].transform.GetChild(0).GetComponent<Text>().text = playerActions[i].name;
            ActionButtons[i].SetActive(true);
        }

        ActionButtons[BUTTONS_COUNT].SetActive(true);
    }

    public void OnActionButton(int index)
    {
        for (int i = 0; i < BUTTONS_COUNT + 1; i++)
        {
            ActionButtons[i].SetActive(false);
        }
        StartCoroutine(PlayerAction(index));
    }

    public void OnActionButtonHover(int index)
    {
        StatsObject action;
        if (actionTypeIsMove)
        {
            action = playerUnit.moves[index];
        } else
        {
            action = playerUnit.items[index];
        }

        string firstHeader = "Damage Dealt";
        if (action.isAttack)
        {
            firstHeader += ": " + GameData.NumberFormatter(playerUnit.CalculateDamage(action));
        }
        TooltipSystem.Show(firstHeader, GameData.DescriptionCreator(action));
    }

    public void OnActionButtonExit()
    {
        TooltipSystem.Hide();
    }

    public void OnBackButton()
    {
        for (int i = 0; i < BUTTONS_COUNT + 1; i++)
        {
            ActionButtons[i].SetActive(false);
        }
        CombatButtons.SetActive(true);
        dialogueText.text = "Choose an action:";
    }

    private IEnumerator StartWaitingForClick()
    {
        yield return new WaitForSeconds(0.1f);
        Input.ResetInputAxes();
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    public void OnShowStatsPanel(bool isPlayer)
    {
        BattleVictoryPanel.gameObject.SetActive(true);
        if (isPlayer)
        {
            BattleVictoryPanel.ShowStatsPanel(playerUnit);
        } else
        {
            BattleVictoryPanel.ShowStatsPanel(enemyUnit);
        }
    }

    string[] tutorialStrings = {
        "This is the final battle! Use your moves to defeat the slimes!",
        "Some moves and items have permanent effects which permanently alter your stats. Other moves and items have temporary effects, which only last for the current battle.\n\nUtilize these permanent and temporary effects wisely!",
        "Don't take too long on a fight! After 10 turns, all enemies get a permanent multiplicative damage buff!",
        "Some effects occur every turn! Use this to become unstoppable!",
        "Give up! You can't win!\n- Big Egg"
        };

    string[] finalStrings = {
        "The tutorial...\n\nif you want to unlock the REAL game with all 17 enemies, click here to pay $70!",
        "You suck! Click to try again and maybe not suck this time!"
        };

    public void OnShowTutorialPanel()
    {
        if (currentEnemyId != 0)
        {
            tutorialTitleText.text = "Tip";
        }
        tutorialText.text = tutorialStrings[currentEnemyId];
        TutorialPanel.SetActive(true);
        TutorialPanelAnimator.SetTrigger("VictoryFromLeft");
    }

    public void OnHideTutorialPanel()
    {
        TutorialPanelAnimator.SetTrigger("VictoryToRight");
    }

    public void OnShowFinalPanel()
    {
        if (state == BattleState.WON)
        {
            finalTitleText.text = "You Won!";
            finalText.text = finalStrings[0];
        } else
        {
            finalTitleText.text = "You Lost!";
            finalText.text = finalStrings[1];
        }

        FinalPanel.SetActive(true);
        FinalPanelAnimator.SetTrigger("VictoryFromLeft");
    }

    public void OnHideFinalPanel()
    {
        FinalPanelAnimator.SetTrigger("VictoryToRight");
        Application.Quit();
    }
}
