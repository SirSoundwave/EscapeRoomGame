using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryPanel : MonoBehaviour
{
    public GameObject LevelUpPanel;
    public GameObject RewardPanel;
    public GameObject ReplacementPanel;
    public GameObject StatsPanel;

    public Animator VictoryPanelAnimator;

    public Text levelTitleText;
    public Text levelContinueText;
    public Slider levelExpSlider;
    public Text levelExpText;
    private float expGained;
    private float fillDuration = 1f;
    private int timesLeveledUp;
    public List<int> LevelUIStats = new List<int> { 0, 0, 0, 0, 0 };
    public Text[] LevelStatsTexts;

    public GameObject[] RewardsGO;
    public int currentRewardListIndex = 0;
    public List<StatsObject> rewards;
    
    public Text replacementTitleText;
    public GameObject[] Replacements;
    private bool isMove;

    public Slider statsExpSlider;
    public Text statsExpText;
    public Text statsLevelText;
    public Text statsContinueText;
    public Text[] statsTexts;
    public Text movesText;
    public Text itemsText;
    public GameObject[] Equipment;

    public IEnumerator ShowLevelUpPanel()
    {
        BattleSystem.playerUnit.temporaryStatChanges = Stats.Create();

        timesLeveledUp = 0;
        LevelUIStats = new List<int> { 0, 0, 0, 0, 0 };
        levelTitleText.text = "Leveled Up " + timesLeveledUp + " Times!";

        LevelStatsTexts[0].text = 
            "Max HP: " + BattleSystem.playerUnit.stats.stats[StatType.health].stat[StatTypeType.BASE] +
            "\nBase Attack: " + BattleSystem.playerUnit.stats.stats[StatType.attack].stat[StatTypeType.BASE] + 
            "\nBase Defense: " + BattleSystem.playerUnit.stats.stats[StatType.defense].stat[StatTypeType.BASE] +
            "\nBase Agility: " + BattleSystem.playerUnit.stats.stats[StatType.agility].stat[StatTypeType.BASE] +
            "\nBase Luck: " + BattleSystem.playerUnit.stats.stats[StatType.luck].stat[StatTypeType.BASE];
        LevelStatsTexts[1].text = "";
        LevelStatsTexts[2].text = "";
        expGained = BattleSystem.enemyUnit.exp;

        gameObject.SetActive(true);
        LevelUpPanel.SetActive(true);
        VictoryPanelAnimator.SetTrigger("VictoryFromTop");
        yield return new WaitForSeconds(1f);
        if (BattleSystem.playerUnit.level == 100)
        {
            levelExpText.text = "Max Level!";
            levelExpSlider.value = 1;
        } else
        {
            StartExpBarAnimation();
        }
    }

    public void StartExpBarAnimation()
    {
        float targetFill = expGained / GameData.expNeededForNextLevel[BattleSystem.playerUnit.level - 1];
        expGained -= GameData.expNeededForNextLevel[BattleSystem.playerUnit.level - 1];
        targetFill = Mathf.Clamp01(targetFill);
        StopAllCoroutines();

        StartCoroutine(FillExpBar(targetFill));
    }

    private IEnumerator FillExpBar(float targetFill)
    {
        float currentFill = levelExpSlider.value;
        float timer = 0f;

        while (timer < fillDuration)
        {
            timer += Time.deltaTime;

            float newFill = Mathf.Lerp(currentFill, targetFill, timer / fillDuration);

            levelExpSlider.value = newFill;
            levelExpText.text = Mathf.FloorToInt(newFill * GameData.expNeededForNextLevel[BattleSystem.playerUnit.level - 1]) + "/" + GameData.expNeededForNextLevel[BattleSystem.playerUnit.level - 1];

            yield return null;
        }

        if (expGained >= 0)
        {
            levelExpSlider.value = 0;
            timesLeveledUp++;
            BattleSystem.playerUnit.level++;

            LevelUIStats[0] += GameData.healthGainedPerLevel[BattleSystem.playerUnit.level - 2];
            LevelUIStats[1] += GameData.attackGainedPerLevel[BattleSystem.playerUnit.level - 2];
            LevelUIStats[2] += GameData.defenseGainedPerLevel[BattleSystem.playerUnit.level - 2];
            LevelUIStats[3] += GameData.agilityGainedPerLevel[BattleSystem.playerUnit.level - 2];
            LevelUIStats[4] += GameData.luckGainedPerLevel[BattleSystem.playerUnit.level - 2];

            levelTitleText.text = "Leveled Up " + timesLeveledUp + " Times!";
            LevelStatsTexts[0].text =
            "Max HP: " + BattleSystem.playerUnit.stats.stats[StatType.health].stat[StatTypeType.BASE] + " (+" + LevelUIStats[0] + ")" +
            "\nBase Attack: " + BattleSystem.playerUnit.stats.stats[StatType.attack].stat[StatTypeType.BASE] + " (+" + LevelUIStats[1] + ")" +
            "\nBase Defense: " + BattleSystem.playerUnit.stats.stats[StatType.defense].stat[StatTypeType.BASE] + " (+" + LevelUIStats[2] + ")" +
            "\nBase Agility: " + BattleSystem.playerUnit.stats.stats[StatType.agility].stat[StatTypeType.BASE] + " (+" + LevelUIStats[3] + ")" +
            "\nBase Luck: " + BattleSystem.playerUnit.stats.stats[StatType.luck].stat[StatTypeType.BASE] + " (+" + LevelUIStats[4] + ")";

            if(BattleSystem.playerUnit.level < 100)
            {
                StartExpBarAnimation();
            } else
            {
                levelExpText.text = "Max Level!";
                levelExpSlider.value = 1;
                BattleSystem.playerUnit.exp = 0;
                UpdateStats();
            }
        } else
        {
            levelExpSlider.value = targetFill;
            BattleSystem.playerUnit.exp = targetFill * GameData.expNeededForNextLevel[BattleSystem.playerUnit.level - 1];
            UpdateStats();
        }
    }

    public void UpdateStats()
    {

        BattleSystem.playerUnit.currentHP += LevelUIStats[0];
        BattleSystem.playerUnit.stats.stats[StatType.health].stat[StatTypeType.BASE] += LevelUIStats[0];
        BattleSystem.playerUnit.stats.stats[StatType.attack].stat[StatTypeType.BASE] += LevelUIStats[1];
        BattleSystem.playerUnit.stats.stats[StatType.defense].stat[StatTypeType.BASE] += LevelUIStats[2];
        BattleSystem.playerUnit.stats.stats[StatType.agility].stat[StatTypeType.BASE] += LevelUIStats[3];
        BattleSystem.playerUnit.stats.stats[StatType.luck].stat[StatTypeType.BASE] += LevelUIStats[4];
        levelContinueText.gameObject.SetActive(true);
    }

    public void CalculateReward()
    {
        if (GameData.rewardList[currentRewardListIndex] == "Move")
        {
            rewards = GetUniqueElements(GameData.moves.GetRange(BattleSystem.currentEnemyId * 4 + 2, 4));
        }
        else if (GameData.rewardList[currentRewardListIndex] == "Item")
        {
            rewards = GetUniqueElements(GameData.items.GetRange(BattleSystem.currentEnemyId * 3, 3));
        }
        else if (GameData.rewardList[currentRewardListIndex] == "Weapon")
        {
            rewards = GetUniqueElements(GameData.equipment.GetRange(BattleSystem.currentEnemyId * 4, 4));
        }
    }

    public void PrePreRewardPanel(bool isNew)
    {
        StartCoroutine(PreRewardPanel(isNew));
    }

    public IEnumerator PreRewardPanel(bool isNew)
    {
        VictoryPanelAnimator.SetTrigger("VictoryToBottom");
        yield return new WaitForSeconds(1f);
        ReplacementPanel.SetActive(false);
        ShowRewardPanel(isNew);
    }

    public void ShowRewardPanel(bool isNew)
    {
        if (isNew)
        {
            CalculateReward();
        }

        for (int i = 0; i < 3; i++)
        {
            if (GameData.rewardList[currentRewardListIndex] != "Move")
            {
                RewardsGO[i].GetComponent<Animator>().gameObject.SetActive(false);
            }
            RewardsGO[i].GetComponent<SpriteRenderer>().sprite = rewards[i].sprite;
            RewardsGO[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = rewards[i].name;
        }
        levelContinueText.gameObject.SetActive(false);
        LevelUpPanel.SetActive(false);
        RewardPanel.SetActive(true);
        VictoryPanelAnimator.SetTrigger("VictoryFromTop");

        //workaround for removing animation sprites
        for (int i = 0; i < 3; i++)
        {
            RewardsGO[i].SetActive(true);
        }
    }

    public void RewardSelection(int selected)
    {
        if (GameData.rewardList[currentRewardListIndex] == "Move")
        {
            BattleSystem.playerUnit.moves.Add(rewards[selected]);
        }
        else if (GameData.rewardList[currentRewardListIndex] == "Item")
        {
            BattleSystem.playerUnit.items.Add(rewards[selected]);
        }
        else if (GameData.rewardList[currentRewardListIndex] == "Weapon")
        {
            switch (rewards[selected].equipmentType)
            {
                case EquipmentType.WEAPON:
                    BattleSystem.playerUnit.equipment[0] = rewards[selected];
                    break;
                case EquipmentType.ARMOR:
                    BattleSystem.playerUnit.equipment[1] = rewards[selected];
                    break;
                case EquipmentType.MISC:
                    BattleSystem.playerUnit.equipment[2] = rewards[selected];
                    break;
            }
        }

        if (BattleSystem.playerUnit.moves.Count > 4)
        {
            isMove = true;
            StartCoroutine(PreReplacementPanel());
            replacementTitleText.text = "Choose a Move to Replace";
        } 
        else if (BattleSystem.playerUnit.items.Count > 4)
        {
            isMove = false;
            StartCoroutine(PreReplacementPanel());
            replacementTitleText.text = "Choose an Item to Replace";
        } 
        else
        {
            currentRewardListIndex++;
            if (GameData.rewardList[currentRewardListIndex] == "End")
            {
                currentRewardListIndex++;
                PrePreStatsPanel(BattleSystem.playerUnit, false);
            }
            else
            {
                StartCoroutine(PreRewardPanel(true));
            }
        }
    }

    public IEnumerator PreReplacementPanel()
    {
        VictoryPanelAnimator.SetTrigger("VictoryToBottom");
        yield return new WaitForSeconds(1f);
        ShowReplacementPanel();
    }

    public void ShowReplacementPanel()
    {
        RewardPanel.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            SpriteRenderer replacementSpriteRenderer = Replacements[i].GetComponent<SpriteRenderer>();
            TextMeshProUGUI replacementText = Replacements[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            if (isMove)
            {
                replacementSpriteRenderer.sprite = BattleSystem.playerUnit.moves[i].sprite;
                replacementText.text = BattleSystem.playerUnit.moves[i].name;
            }
            else
            {
                replacementSpriteRenderer.sprite = BattleSystem.playerUnit.items[i].sprite;
                replacementText.text = BattleSystem.playerUnit.items[i].name;
            }
        }

        ReplacementPanel.SetActive(true);
        VictoryPanelAnimator.SetTrigger("VictoryFromTop");
    }

    public void ReplacementSelection(int selected)
    {
        if (selected == 4)
        {
            if (isMove)
            {
                BattleSystem.playerUnit.moves.RemoveAt(4);
            }
            else
            {
                BattleSystem.playerUnit.items.RemoveAt(4);
            }
            StartCoroutine(PreRewardPanel(false));
        } 
        else
        {
            if (isMove)
            {
                BattleSystem.playerUnit.moves[selected] = BattleSystem.playerUnit.moves[4];
                BattleSystem.playerUnit.moves.RemoveAt(4);
            } 
            else
            {
                BattleSystem.playerUnit.items[selected] = BattleSystem.playerUnit.items[4];
                BattleSystem.playerUnit.items.RemoveAt(4);
            }
            
            currentRewardListIndex++;
            StartCoroutine(PreRewardPanel(true));
        }
    }

    public void PrePreStatsPanel(Unit unit, bool isInBattle)
    {
        StartCoroutine(PreStatsPanel(unit, isInBattle));
    }

    public IEnumerator PreStatsPanel(Unit unit, bool isInBattle)
    {
        VictoryPanelAnimator.SetTrigger("VictoryToBottom");
        yield return new WaitForSeconds(1f);
        ShowStatsPanel(unit);
    }

    public void HideStatsPanel()
    {
        VictoryPanelAnimator.SetTrigger("VictoryToBottom");
    }

    public void ShowStatsPanel(Unit unit)
    {
        statsLevelText.text = "Level: " + unit.level;
        if (unit.exp > GameData.expNeededForNextLevel[unit.level - 1])
        {
            statsExpSlider.value = 1f;
            statsExpText.text = "Reward: " + unit.exp;
        } else
        {
            statsExpSlider.value = unit.exp / GameData.expNeededForNextLevel[unit.level - 1];
            statsExpText.text = (int)unit.exp + "/" + GameData.expNeededForNextLevel[unit.level - 1];
        }

        for (int i = 0; i < unit.equipment.Count; i++)
        {
            Equipment[i].GetComponent<SpriteRenderer>().sprite = unit.equipment[i].sprite;
            Equipment[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = unit.equipment[i].name;
        }

        movesText.text = "";
        for (int i = 0; i < unit.moves.Count; i++)
        {
            movesText.text += unit.moves[i].name;
            if (i + 1 < unit.moves.Count)
            {
                movesText.text += "\n";
            }
        }

        itemsText.text = "";
        for (int i = 0; i < unit.items.Count; i++)
        {
            itemsText.text += unit.items[i].name;
            if (i + 1 < unit.items.Count)
            {
                itemsText.text += "\n";
            }
        }

        statsTexts[0].text =
            "\nMax Health: " + GameData.NumberFormatter(unit.CalculateStat(StatType.health, StatTypeType.BASE)) +
            "\nRegen: " + GameData.NumberFormatter(unit.CalculateStat(StatType.heal, StatTypeType.BASE)) +
            "\nAttack: " + GameData.NumberFormatter(unit.CalculateStat(StatType.attack, StatTypeType.BASE)) +
            "\nDefense: " + GameData.NumberFormatter(unit.CalculateStat(StatType.defense, StatTypeType.BASE)) +
            "\nAgility: " + GameData.NumberFormatter(unit.CalculateStat(StatType.agility, StatTypeType.BASE)) +
            "\nLuck: " + GameData.NumberFormatter(unit.CalculateStat(StatType.luck, StatTypeType.BASE));

        statsTexts[1].text =
            "\nMax Health: " + GameData.NumberFormatter(unit.CalculateStat(StatType.health, StatTypeType.BASEMULTIPLIER)) + "x" +
            "\nRegen: " + GameData.NumberFormatter(unit.CalculateStat(StatType.heal, StatTypeType.BASEMULTIPLIER)) + "x" +
            "\nAttack: " + GameData.NumberFormatter(unit.CalculateStat(StatType.attack, StatTypeType.BASEMULTIPLIER)) + "x" +
            "\nDefense: " + GameData.NumberFormatter(unit.CalculateStat(StatType.defense, StatTypeType.BASEMULTIPLIER)) + "x" +
            "\nAgility: " + GameData.NumberFormatter(unit.CalculateStat(StatType.agility, StatTypeType.BASEMULTIPLIER)) + "x" +
            "\nLuck: " + GameData.NumberFormatter(unit.CalculateStat(StatType.luck, StatTypeType.BASEMULTIPLIER)) + "x";

        statsTexts[2].text =
            "\nMax Health: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.health)) +
            "\nRegen: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.heal)) +
            "\nAttack: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.attack)) +
            "\nDefense: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.defense)) +
            "\nAgility: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.agility)) +
            "\nLuck: " + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.luck));
                
        RewardPanel.SetActive(false);
        StatsPanel.SetActive(true);
        VictoryPanelAnimator.SetTrigger("VictoryFromTop");
    }

    private List<StatsObject> GetUniqueElements(List<StatsObject> inputList)
    {
        List<StatsObject> tempList = new List<StatsObject>(inputList);

        for (int i = tempList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            StatsObject temp = tempList[i];
            tempList[i] = tempList[j];
            tempList[j] = temp;
        }

        List<StatsObject> result = tempList.GetRange(0, Mathf.Min(3, tempList.Count));

        return result;
    }

    public void OnHover(int index)
    {
        string firstHeader = "Damage Dealt";
        if (rewards[index].equipmentType != EquipmentType.NONE)
        {
            firstHeader = "Stats";
        }
        else if (rewards[index].isAttack)
        {
            firstHeader += ": " + GameData.NumberFormatter(BattleSystem.playerUnit.CalculateDamage(rewards[index]));
        }
        TooltipSystem.Show(firstHeader, GameData.DescriptionCreator(rewards[index]));

        if (GameData.rewardList[currentRewardListIndex] == "Move")
        {
            RewardsGO[index].GetComponent<Animator>().SetTrigger(rewards[index].iconAnimationName);
        }
    }

    public void OnHoverReplacement(int index)
    {
        string firstHeader = "Damage Dealt";
        StatsObject action;
        if (isMove)
        {
            action = BattleSystem.playerUnit.moves[index];
        } else
        {
            action = BattleSystem.playerUnit.items[index];
        }
        firstHeader += ": " + GameData.NumberFormatter(BattleSystem.playerUnit.CalculateDamage(action));
        
        TooltipSystem.Show(firstHeader, GameData.DescriptionCreator(action));

        if (GameData.rewardList[currentRewardListIndex] == "Move")
        {
            Replacements[index].GetComponent<Animator>().SetTrigger(action.iconAnimationName);
        }
    }

    public void OnExit(int index)
    {
        TooltipSystem.Hide();

        RewardsGO[index].GetComponent<Animator>().SetTrigger("None");
    }
    public void OnExitReplacement(int index)
    {
        TooltipSystem.Hide();

        Replacements[index].GetComponent<Animator>().SetTrigger("None");
    }
}
