using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public string name;
    public int level;
    public float exp;
    public float currentHP;

    public Stats stats;
    public Stats temporaryStatChanges;

    public List<StatsObject> moves = new List<StatsObject>();
    public List<StatsObject> items = new List<StatsObject>();
    public List<StatsObject> equipment = new List<StatsObject>();

    public Vector3 position;
    public Vector3 scale;
    public Sprite sprite;

    public static Unit Create(
        string name = "",
        float exp = 0f,
        int level = 0,
        float currentHP = 0f,
        Stats stats = null,
        Stats temporaryChanges = null,
        List<StatsObject> moves = null,
        List<StatsObject> items = null,
        List<StatsObject> equipment = null,
        Vector3 position = new Vector3(),
        Vector3 scale = new Vector3(),
        Sprite sprite = null)
    {
        if (stats == null)
        {
            stats = Stats.Create();
        }

        if (temporaryChanges == null)
        {
            temporaryChanges = Stats.Create();
        }

        return new Unit
        {
            name = name,
            exp = exp,
            level = level,
            currentHP = currentHP,
            stats = stats,
            temporaryStatChanges = temporaryChanges,
            moves = moves,
            items = items,
            equipment = equipment,
            position = position,
            scale = scale,
            sprite = sprite
        };
    }

    public bool TakeDamage(float damage)
    {
        currentHP -= damage * CalculateMitigationPercentage();

        if (currentHP > CalculateTotalStat(StatType.health))
        {
            currentHP = CalculateTotalStat(StatType.health);
        }

        return currentHP <= 0;
    }

    public void Heal(float amount)
    {
        currentHP += amount;

        if (currentHP > CalculateTotalStat(StatType.health))
        {
            currentHP = CalculateTotalStat(StatType.health);
        }
    }

    public float CalculateMitigationPercentage()
    {
        return 50f / (50f + CalculateTotalStat(StatType.defense));
    }

    public float CalculateLuckPercentage()
    {
        return (-50f / (50f + CalculateTotalStat(StatType.luck))) + 1;
    }

    public bool IsDodge()
    {
        return Random.value < CalculateLuckPercentage();
    }

    public bool IsCritical()
    {
        return Random.value < CalculateLuckPercentage();
    }

    public float CalculateTotalStat(StatType statType)
    {
        return CalculateStat(statType, StatTypeType.BASE) * CalculateStat(statType, StatTypeType.BASEMULTIPLIER);
    }

    public float CalculateStat(StatType statType, StatTypeType statTypeType)
    {
        float value;
        if (statTypeType != StatTypeType.BASEMULTIPLIER)
        {
            value = 0f;
            value += stats.stats[statType].stat[statTypeType];
            value += temporaryStatChanges.stats[statType].stat[statTypeType];

            foreach (StatsObject equipmentPiece in equipment)
            {
                value += equipmentPiece.stats.stats[statType].stat[statTypeType];
            }
        } 
        else
        {
            value = 1f;
            value *= stats.stats[statType].stat[statTypeType];
            value *= temporaryStatChanges.stats[statType].stat[statTypeType];

            foreach (StatsObject equipmentPiece in equipment)
            {
                value *= equipmentPiece.stats.stats[statType].stat[statTypeType];
            }
        }
        
        return value;
    }

    public float CalculateDamage(StatsObject action)
    {
        float damage = 0f;
        foreach (KeyValuePair<StatType, Stat> statPair in action.stats.stats)
        {
            if (statPair.Value.isUsed)
            {
                damage += 
                    (CalculateStat(statPair.Key, StatTypeType.BASE) + action.stats.stats[statPair.Key].stat[StatTypeType.BASE]) *
                    (CalculateStat(statPair.Key, StatTypeType.BASEMULTIPLIER) * action.stats.stats[statPair.Key].stat[StatTypeType.BASEMULTIPLIER]);
            }
        }
        return damage;  
    }

    public List<string> ApplyActionStatChanges(StatsObject action)
    {
        StatType[] allStatTypes = (StatType[])System.Enum.GetValues(typeof(StatType));
        StatTypeType[] allStatTypeTypes = (StatTypeType[])System.Enum.GetValues(typeof(StatTypeType));

        Stats oldStats = Stats.Create();

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                oldStats.stats[statType].stat[statTypeType] = stats.stats[statType].stat[statTypeType] + temporaryStatChanges.stats[statType].stat[statTypeType];
            }
        }

        int multiplier = 1;

        if (action.isTurnBasedPermanent)
        {
            multiplier = action.numberOfTurnsInInventoryPermanent;
        }

        foreach (StatType statType in allStatTypes)
        {
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASE] += action.stats.stats[statType].stat[StatTypeType.BASETEMPORARY] * multiplier;
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASETEMPORARY] += action.stats.stats[statType].stat[StatTypeType.BASETEMPORARYTEMPORARY] * multiplier;
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASEPERMANENT] += action.stats.stats[statType].stat[StatTypeType.BASEPERMANENTTEMPORARY] * multiplier;
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASEMULTIPLIER] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERTEMPORARY] * multiplier;
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASEMULTIPLIERTEMPORARY] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERTEMPORARYTEMPORARY] * multiplier;
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASEMULTIPLIERPERMANENT] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERPERMANENTTEMPORARY] * multiplier;

            stats.stats[statType].stat[StatTypeType.BASE] += action.stats.stats[statType].stat[StatTypeType.BASEPERMANENT] * multiplier;
            stats.stats[statType].stat[StatTypeType.BASETEMPORARY] += action.stats.stats[statType].stat[StatTypeType.BASETEMPORARYPERMANENT] * multiplier;
            stats.stats[statType].stat[StatTypeType.BASEPERMANENT] += action.stats.stats[statType].stat[StatTypeType.BASEPERMANENTPERMANENT] * multiplier;
            stats.stats[statType].stat[StatTypeType.BASEMULTIPLIER] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERPERMANENT] * multiplier;
            stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERTEMPORARY] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERTEMPORARYPERMANENT] * multiplier;
            stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERPERMANENT] += action.stats.stats[statType].stat[StatTypeType.BASEMULTIPLIERPERMANENTPERMANENT] * multiplier;
        }

        foreach (StatType statType in allStatTypes)
        {
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASE] += CalculateStat(statType, StatTypeType.BASETEMPORARY);
            temporaryStatChanges.stats[statType].stat[StatTypeType.BASEMULTIPLIER] += CalculateStat(statType, StatTypeType.BASEMULTIPLIERTEMPORARY);

            stats.stats[statType].stat[StatTypeType.BASE] += CalculateStat(statType, StatTypeType.BASEPERMANENT);
            stats.stats[statType].stat[StatTypeType.BASEMULTIPLIER] += CalculateStat(statType, StatTypeType.BASEMULTIPLIERPERMANENT);
        }

        Heal((CalculateStat(StatType.heal, StatTypeType.BASE) + action.stats.stats[StatType.heal].stat[StatTypeType.BASE]) *
            (CalculateStat(StatType.heal, StatTypeType.BASEMULTIPLIER) * action.stats.stats[StatType.heal].stat[StatTypeType.BASEMULTIPLIER]));

        Stats newStats = Stats.Create();

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                newStats.stats[statType].stat[statTypeType] = stats.stats[statType].stat[statTypeType] + temporaryStatChanges.stats[statType].stat[statTypeType];
            }
        }

        List<string> change = new List<string>();

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                newStats.stats[statType].stat[statTypeType] -= oldStats.stats[statType].stat[statTypeType];
            }
        }

        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                if (newStats.stats[statType].stat[statTypeType] != 0f)
                {
                    
                    string modifier = " decreased";
                    string statTypeString = statType.ToString();
                    float stat = newStats.stats[statType].stat[statTypeType];
                    if (newStats.stats[statType].stat[statTypeType] > 0)
                    {
                        modifier = " increased";
                    }
                    if (statTypeString == "health")
                    {
                        statTypeString = "max health";
                    }
                    if (statTypeString == "heal")
                    {
                        statTypeString = "regen";
                    }
                    if (statTypeType == StatTypeType.BASE)
                    {
                        change.Add("base " + statTypeString + modifier + " by " + GameData.NumberFormatter(Mathf.Abs(stat)) + "!");
                    } else if (statTypeType == StatTypeType.BASEMULTIPLIER)
                    {
                        change.Add("base " + statTypeString + " multiplier" + modifier + " by " + GameData.NumberFormatter(100 * stat) + "%!");
                    }
                        
                }
            }
        }
        return change;
    }

    public void ApplyOnTurnStatChanges()
    {   
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].isTurnBasedPermanent)
            {
                items[i].numberOfTurnsInInventoryPermanent++;
            }
        }
    }
}
