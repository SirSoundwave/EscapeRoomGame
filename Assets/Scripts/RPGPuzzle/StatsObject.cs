using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsObject
{
    public string name;
    public string description;
    public string damageDescription;
    public string temporaryDescription;
    public string permanentDescription;
    public Stats stats;
    public bool isAttack;
    public bool isStatChange;
    public bool isTurnBasedPermanent;
    public int numberOfTurnsInInventoryPermanent;
    public string userAnimationName;
    public float userAnimationDuration;
    public string onUserAnimationName;
    public float onUserAnimationDuration;
    public string recieverAnimationName;
    public float recieverAnimationDuration;
    public string onReceiverAnimationName;
    public float onReceiverAnimationDuration;
    public string actionAnimationName;
    public float actionAnimationDuration;
    public string iconAnimationName;
    public EquipmentType equipmentType;
    public Sprite sprite;

    public static StatsObject Create(
        string name = "",
        string description = "",
        string damageDescription = "",
        string temporaryDescription = "",
        string permanentDescription = "",
        Stats stats = null,
        bool isAttack = false,
        bool isStatChange = false,
        bool isTurnBasedPermanent = false,
        int numberOfTurnsInInventoryPermanent = 0,
        string userAnimationName = "None",
        float userAnimationDuration = 0f,
        string onUserAnimationName = "None",
        float onUserAnimationDuration = 0f,
        string recieverAnimationName = "None",
        float recieverAnimationDuration = 0f,
        string onReceiverAnimationName = "None",
        float onReceiverAnimationDuration = 0f,
        string actionAnimationName = "None",
        float actionAnimationDuration = 0f,
        string iconAnimationName = "None",
        EquipmentType equipmentType = EquipmentType.NONE,
        Sprite sprite = null)
    {
        if (stats == null)
        {
            stats = Stats.Create();
        }

        return new StatsObject
        {
            name = name,
            description = description,
            damageDescription = damageDescription,
            temporaryDescription = temporaryDescription,
            permanentDescription = permanentDescription,
            stats = stats,
            isAttack = isAttack,
            isStatChange = isStatChange,
            isTurnBasedPermanent = isTurnBasedPermanent,
            numberOfTurnsInInventoryPermanent = numberOfTurnsInInventoryPermanent,
            userAnimationName = userAnimationName,
            userAnimationDuration = userAnimationDuration,
            onUserAnimationName = onUserAnimationName,
            onUserAnimationDuration = onUserAnimationDuration,
            recieverAnimationName = recieverAnimationName,
            recieverAnimationDuration = recieverAnimationDuration,
            onReceiverAnimationName = onReceiverAnimationName,
            onReceiverAnimationDuration = onReceiverAnimationDuration,
            actionAnimationName = actionAnimationName,
            actionAnimationDuration = actionAnimationDuration,
            iconAnimationName = iconAnimationName,
            equipmentType = equipmentType,
            sprite = sprite
        };
    }
}

public enum StatType { health, heal, attack, defense, agility, luck, experience }

public enum StatTypeType { 
    BASE, 
    BASETEMPORARY, BASETEMPORARYTEMPORARY, BASETEMPORARYPERMANENT, 
    BASEPERMANENT, BASEPERMANENTTEMPORARY, BASEPERMANENTPERMANENT,
    BASEMULTIPLIER, 
    BASEMULTIPLIERTEMPORARY, BASEMULTIPLIERTEMPORARYTEMPORARY, BASEMULTIPLIERTEMPORARYPERMANENT, 
    BASEMULTIPLIERPERMANENT, BASEMULTIPLIERPERMANENTTEMPORARY, BASEMULTIPLIERPERMANENTPERMANENT
}
public class Stat {
    public Dictionary<StatTypeType, float> stat;
    public bool isUsed;

    public static Stat Create(
        Dictionary<StatTypeType, float> stat = null,
        bool isUsed = false
        )
    {
        StatTypeType[] allStatTypeTypes = (StatTypeType[])System.Enum.GetValues(typeof(StatTypeType));

        if (stat == null)
        {
            stat = new Dictionary<StatTypeType, float>();
        }

        foreach (StatTypeType statTypeType in allStatTypeTypes)
        {
            if (!stat.ContainsKey(statTypeType))
            {
                if (statTypeType == StatTypeType.BASEMULTIPLIER)
                {
                    stat.Add(statTypeType, 1f);
                }
                else
                {
                    stat.Add(statTypeType, 0f);
                }
            }
        }

        return new Stat
        {
            stat = stat,
            isUsed = isUsed
        };
    }
}

public class Stats
{
    public Dictionary<StatType, Stat> stats;

    public static Stats Create(
        Dictionary<StatType, Stat> stats = null
        )
    {
        StatType[] allStatTypes = (StatType[])System.Enum.GetValues(typeof(StatType));

        if (stats == null)
        {
            stats = new Dictionary<StatType, Stat>();
        }

        foreach (StatType statType in allStatTypes)
        {
            if (!stats.ContainsKey(statType))
            {
                stats.Add(statType, Stat.Create());
            }

        }

        return new Stats
        {
            stats = stats,
        };
    }
}