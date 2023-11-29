using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EquipmentType { NONE, WEAPON, ARMOR, MISC }

public class GameData : MonoBehaviour
{
    public static List<StatsObject> moves = new List<StatsObject>();
    public static List<StatsObject> items = new List<StatsObject>();
    public static List<StatsObject> equipment = new List<StatsObject>();
    public static List<string> rewardList = new List<string>();
    public static List<Unit> enemies = new List<Unit>();
    public static List<Unit> enemiesUnedited = new List<Unit>();
    public static Unit player = new Unit();

    public static List<int> healthGainedPerLevel = new List<int>();
    public static List<int> attackGainedPerLevel = new List<int>();
    public static List<int> defenseGainedPerLevel = new List<int>();
    public static List<int> agilityGainedPerLevel = new List<int>();
    public static List<int> luckGainedPerLevel = new List<int>();
    public static List<int> expNeededForNextLevel = new List<int>();

    public static void Initialize()
    {
        rewardList.Add("Move");
        rewardList.Add("Item");
        rewardList.Add("Weapon");
        rewardList.Add("End");

        rewardList.Add("Move");
        rewardList.Add("Item");
        rewardList.Add("Weapon");
        rewardList.Add("End");

        rewardList.Add("Move");
        rewardList.Add("Item");
        rewardList.Add("Weapon");
        rewardList.Add("End");

        rewardList.Add("Move");
        rewardList.Add("Item");
        rewardList.Add("Weapon");
        rewardList.Add("End");

        //remove

        InitializePlayerLevelUpStats();
        InitializeMoves();
        InitializeItems();
        InitializeEquipment();
        InitializePlayer();
        InitializeEnemy();
    }

    public static void InitializePlayer()
    {
        player = Unit.Create(
            name: "Gunk Thunder",
            level: 5,
            currentHP: 10,
            stats: Stats.Create(
                    stats: new Dictionary<StatType, Stat>
                    {
                        {
                            StatType.health, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, 10f },
                                })
                        },
                        {
                            StatType.attack, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, 1f },
                                })
                        }
                    }
                ),
            temporaryChanges: Stats.Create(),
            moves: new List<StatsObject>
            {
                moves[0],
                moves[1]
            },
            items: new List<StatsObject>
            {
            },
            equipment: new List<StatsObject>
            {
                StatsObject.Create(),
                StatsObject.Create(),
                StatsObject.Create()
            });
    }

    public static void InitializeEnemy()
    {
        List<int> enemyLevels = new List<int>();
        List<int> enemyLevelsDisplay = new List<int>();

        int baseLevel = 1;

        for (int i = 0; i < 18; i++)
        {
            int levelRange = (i == 4 || i == 10 || i == 17) ? 10 : 5;
            enemyLevels.Add(Random.Range(0, levelRange));

            enemyLevelsDisplay.Add(enemyLevels[i] + baseLevel);

            baseLevel += levelRange;
        }

        List<string> enemyNameList = new List<string> {
        "Blue Slime",
        "Plant Slime",
        "Cube Slime",
        "Jelly Slime",
        "Blue King Slime"
        };

        List<List<int>> enemyMaxHPList = new List<List<int>> {
        new List<int> { 3, 4, 5, 6, 7 },
        new List<int> { 8, 9, 10, 11, 12 },
        new List<int> { 30, 32, 34, 36, 38 },
        new List<int> { 51, 54, 57, 60, 63 },
        new List<int> { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190 }
        };

        List<List<int>> enemyBaseAttackList = new List<List<int>> {
        new List<int> { 1, 1, 1, 1, 1 },
        new List<int> { 2, 2, 3, 3, 3 },
        new List<int> { 7, 7, 8, 9, 9 },
        new List<int> { 17, 18, 19, 20, 21 },
        new List<int> { 20, 22, 24, 26, 28, 30, 32, 34, 36, 38 }
        };

        List<List<int>> enemyBaseDefenseList = new List<List<int>> {
        new List<int> { 0, 0, 0, 0, 0 },
        new List<int> { 5, 5, 5, 5, 5 },
        new List<int> { 17, 19, 21, 23, 25 },
        new List<int> { 15, 18, 21, 23, 24 },
        new List<int> { 30, 33, 36, 39, 42, 45, 48, 51, 54, 57 }
        };

        List<List<int>> enemyExpList = new List<List<int>> {
        new List<int> { 50, 60, 70, 80, 90 },
        new List<int> { 200, 220, 240, 260, 280 },
        new List<int> { 520, 570, 620, 670, 720 },
        new List<int> { 810, 890, 980, 1070, 1150 },
        new List<int> { 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300, 2400 }
        };

        List<Vector3> enemyPositionList = new List<Vector3> {
        new Vector3(0f, 0f, 0),
        new Vector3(4.5f, 2.6f, 0),
        new Vector3(4.5f, 2.7f, 0),
        new Vector3(4.5f, 2.7f, 0),
        new Vector3(4.5f, 3f, 0)
        };

        List<Vector3> enemyScaleList = new List<Vector3> {
        new Vector3(1f, 1f, 0),
        new Vector3(0.7f, 0.7f, 0),
        new Vector3(1f, 1f, 0),
        new Vector3(0.9f, 0.9f, 0),
        new Vector3(0.7f, 0.7f, 0)
        };

        for (int i = 0; i < 5; i++)
        {
            Unit enemy = Unit.Create(
                name: enemyNameList[i],
                level: enemyLevelsDisplay[i],
                currentHP: enemyMaxHPList[i][enemyLevels[i]],
                stats: Stats.Create(
                    stats: new Dictionary<StatType, Stat>
                    {
                        {
                            StatType.health, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, enemyMaxHPList[i][enemyLevels[i]] },

                                })
                        },
                        {
                            StatType.attack, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, enemyBaseAttackList[i][enemyLevels[i]] },

                                })
                        },
                        {
                            StatType.defense, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, enemyBaseDefenseList[i][enemyLevels[i]] },

                                })
                        }
                    }
                ),
                exp: enemyExpList[i][enemyLevels[i]],
                moves: new List<StatsObject>
                {
                moves[i * 4 + 2],
                moves[i * 4 + 3],
                moves[i * 4 + 4],
                moves[i * 4 + 5]
                },
                items: new List<StatsObject>(),
                equipment: new List<StatsObject>(),
                position: enemyPositionList[i],
                scale: enemyScaleList[i],
                sprite: Resources.Load<Sprite>("Enemies/" + (i + 1))
            );

            for (int j = 0; j < 3; j++)
            {
                enemy.equipment.Add(StatsObject.Create());
            }

            Unit enemyUnedited = Unit.Create(
                name: enemyNameList[i],
                level: enemyLevelsDisplay[i],
                currentHP: enemyMaxHPList[i][enemyLevels[i]],
                stats: Stats.Create(
                    stats: new Dictionary<StatType, Stat>
                    {
                        {
                            StatType.health, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, enemyMaxHPList[i][enemyLevels[i]] },

                                })
                        },
                        {
                            StatType.attack, Stat.Create(
                                stat: new Dictionary<StatTypeType, float>
                                {
                                    { StatTypeType.BASE, enemyBaseAttackList[i][enemyLevels[i]] },

                                })
                        }
                    }
                ),
                exp: enemyExpList[i][enemyLevels[i]],
                moves: new List<StatsObject>
                {
                moves[i * 4 + 2],
                moves[i * 4 + 3],
                moves[i * 4 + 4],
                moves[i * 4 + 5]
                },
                items: new List<StatsObject>(),
                equipment: new List<StatsObject>(),
                position: enemyPositionList[i],
                scale: enemyScaleList[i],
                sprite: Resources.Load<Sprite>("Enemies/" + (i + 1))
            );

            for (int j = 0; j < 3; j++)
            {
                enemyUnedited.equipment.Add(StatsObject.Create());
            }

            enemies.Add(enemy);
            enemiesUnedited.Add(enemyUnedited);
        }
    }

    public static void InitializePlayerLevelUpStats()
    {
        for (int i = 1; i < 99; i++)
        {
            healthGainedPerLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 100f) + Random.value + 0.4f, 1.1f));
            attackGainedPerLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 500f) + Random.value + 0.3f, 1.1f));
            defenseGainedPerLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 500f) + Random.value + 0.3f, 1.1f));
            agilityGainedPerLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 1000f) + Random.value + 0.2f, 1.1f));
            luckGainedPerLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 1000f) + Random.value + 0.2f, 1.1f));
            expNeededForNextLevel.Add((int)Mathf.Pow((Random.value + i - 0.5f) * (i / 10f) + 3, 1.333f));

            if (i == 98) healthGainedPerLevel.Add(200);
            if (i == 98) attackGainedPerLevel.Add(100);
            if (i == 98) defenseGainedPerLevel.Add(100);
            if (i == 98) agilityGainedPerLevel.Add(50);
            if (i == 98) luckGainedPerLevel.Add(50);
            if (i == 98) expNeededForNextLevel.Add(10000);
        }
    }

    public static void InitializeMoves()
    {
        Sprite[] moveIconSprites = Resources.LoadAll<Sprite>("Icons/Icons");

        moves.Add(StatsObject.Create(
            name: "Punch I",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    { 
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 1f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 1500",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Explosion 1",
            onReceiverAnimationDuration: 0.75f,
            iconAnimationName: "Explosion 1",
            sprite: moveIconSprites[197]));

        moves.Add(StatsObject.Create(
            name: "Attack Up I",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 1",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 1",
            sprite: moveIconSprites[142]));

        moves.Add(StatsObject.Create(
            name: "Flame I",
            temporaryDescription: "+1 attack",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.5f },
                                { StatTypeType.BASETEMPORARY, 1f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 3000",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Flame 1",
            onReceiverAnimationDuration: 2.25f,
            iconAnimationName: "Flame 1",
            sprite: moveIconSprites[169]));

        moves.Add(StatsObject.Create(
            name: "Wind I",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.4f }
                            },
                            isUsed: true)
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f },
                                { StatTypeType.BASEMULTIPLIER, 0.4f }
                            },
                            isUsed: true)
                    }
                }
            ),
            actionAnimationName: "Wind 1",
            actionAnimationDuration: 1f,
            onReceiverAnimationName: "Explosion 1",
            onReceiverAnimationDuration: 0.5f,
            iconAnimationName: "Wind 1",
            sprite: moveIconSprites[39]));

        moves.Add(StatsObject.Create(
            name: "Defense Up I",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 3f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 2",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 2",
            sprite: moveIconSprites[204]));

        moves.Add(StatsObject.Create(
            name: "Punch II",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 1.2f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 1500",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Explosion 2",
            onReceiverAnimationDuration: 0.75f,
            iconAnimationName: "Explosion 2",
            sprite: moveIconSprites[81]));

        moves.Add(StatsObject.Create(
            name: "Dash I",
            description: "deals 75% attack + 75% agility damage",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.75f }
                            },
                            isUsed: true)
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.75f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Disappear 1000",
            userAnimationDuration: 0f,
            actionAnimationName: "Dash 1",
            actionAnimationDuration: 0.5f,
            onReceiverAnimationName: "Explosion 1",
            onReceiverAnimationDuration: 1f,
            iconAnimationName: "Dash",
            sprite: moveIconSprites[217]));

        moves.Add(StatsObject.Create(
            name: "Fireball I",
            description: "deals 80% attack damage + \npermanent+5% multipier to attack (additive)",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.8f },
                                { StatTypeType.BASEMULTIPLIERPERMANENT, 0.05f }
                            },
                            isUsed: true)
                    }
                }
            ),
            actionAnimationName: "Fireball 1",
            actionAnimationDuration: 1f,
            onReceiverAnimationName: "Explosion 3",
            onReceiverAnimationDuration: 0.5f,
            iconAnimationName: "Fireball 1",
            sprite: moveIconSprites[187]));

        moves.Add(StatsObject.Create(
            name: "Agility Up I",
            description: "temporary +2 agility gained per turn for this fight",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARYTEMPORARY, 2f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 4",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 4",
            sprite: moveIconSprites[34]));

        moves.Add(StatsObject.Create(
            name: "Recover I",
            description: "permanent +3 max health \n+5 current health",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 3f }
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 3",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 3",
            sprite: moveIconSprites[154]));

        moves.Add(StatsObject.Create(
            name: "Flame II",
            description: "deals 60% attack damage" + "\n+20% attack (additive) for the remainder of this fight",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.6f },
                                { StatTypeType.BASEMULTIPLIERTEMPORARY, 0.2f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 2500",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Flame 2",
            onReceiverAnimationDuration: 1.75f,
            iconAnimationName: "Flame 2",
            sprite: moveIconSprites[85]));

        moves.Add(StatsObject.Create(
            name: "Greed I",
            description: "permanent +5 luck -1 attack",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, -1f }
                            })
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 5f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 4",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 4",
            sprite: moveIconSprites[154]));

        moves.Add(StatsObject.Create(
            name: "Fireball II",
            description: "deals 80% attack damage + \npermanent +10% multipier to attack (additive)",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.9f },
                                { StatTypeType.BASEMULTIPLIERPERMANENT, 0.10f }
                            },
                            isUsed: true)
                    }
                }
            ),
            actionAnimationName: "Fireball 2",
            actionAnimationDuration: 1f,
            onReceiverAnimationName: "Explosion 5",
            onReceiverAnimationDuration: 0.5f,
            iconAnimationName: "Fireball 2",
            sprite: moveIconSprites[7]));

        moves.Add(StatsObject.Create(
            name: "Dash II",
            description: "deals 90% attack + 90% agility damage and a permanent +1 agility",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.9f }

                            },
                            isUsed: true)
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f },
                                { StatTypeType.BASEMULTIPLIER, 0.9f },
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Disappear 1000",
            userAnimationDuration: 0f,
            actionAnimationName: "Dash 1",
            actionAnimationDuration: 0.5f,
            onReceiverAnimationName: "Explosion 1",
            onReceiverAnimationDuration: 1f,
            iconAnimationName: "Dash",
            sprite: moveIconSprites[218]));

        moves.Add(StatsObject.Create(
            name: "Electric Ball I",
            description: "deals -10 + 200% attack damage",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, -10f },
                                { StatTypeType.BASEMULTIPLIER, 2f }
                            },
                            isUsed: true)
                    }
                }
            ),
            actionAnimationName: "Electric Ball 1",
            actionAnimationDuration: 1f,
            onReceiverAnimationName: "Explosion 5",
            onReceiverAnimationDuration: 0.5f,
            iconAnimationName: "Electric Ball 1",
            sprite: moveIconSprites[116]));

        moves.Add(StatsObject.Create(
            name: "Wind II",
            description: "deals 50% attack" + "\n150% luck damage" + "\n+3 luck for the remainder of this fight",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.5f }
                            },
                            isUsed: true)
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARY, 3f },
                                { StatTypeType.BASEMULTIPLIER, 1.5f }
                            },
                            isUsed: true)
                    }
                }
            ),
            actionAnimationName: "Wind 2",
            actionAnimationDuration: 1f,
            onReceiverAnimationName: "Explosion 2",
            onReceiverAnimationDuration: 0.5f,
            iconAnimationName: "Wind 2",
            sprite: moveIconSprites[26]));

        moves.Add(StatsObject.Create(
            name: "Attack Up II",
            description: "permanent +1 attack per turn",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENTPERMANENT, 1f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 1",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 1",
            sprite: moveIconSprites[142]));

        moves.Add(StatsObject.Create(
            name: "Regen I",
            description: "+10 health regened per turn for this fight",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARY, 10f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 3",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 3",
            sprite: moveIconSprites[154]));

        moves.Add(StatsObject.Create(
            name: "Punch III",
            description: "deals 60% of 25 + total attack damage",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 25f },
                                { StatTypeType.BASEMULTIPLIER, 0.6f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 1500",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Explosion 5",
            onReceiverAnimationDuration: 0.75f,
            iconAnimationName: "Explosion 5",
            sprite: moveIconSprites[177]));

        moves.Add(StatsObject.Create(
            name: "Defense Up II",
            description: "permanent +5% (additive) defense per turn ",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIERPERMANENTPERMANENT, 0.05f }
                            })
                    }
                }
            ),
            onUserAnimationName: "Status 2",
            onUserAnimationDuration: 1f,
            iconAnimationName: "Status 2",
            sprite: moveIconSprites[204]));

        moves.Add(StatsObject.Create(
            name: "Rend I",
            description: "deals 30% of your base health + 70% attack as damage",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.3f }
                            },
                            isUsed: true)
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.7f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 3000",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Rend 1",
            onReceiverAnimationDuration: 0.75f,
            iconAnimationName: "Rend 1",
            sprite: moveIconSprites[54]));

        moves.Add(StatsObject.Create(
            name: "Flame III",
            description: "deals 70% attack damage" + "\n+7 attack for the remainder of this fight",
            isAttack: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARY, 7f },
                                { StatTypeType.BASEMULTIPLIER, 0.7f }
                            },
                            isUsed: true)
                    }
                }
            ),
            userAnimationName: "Right 2500",
            userAnimationDuration: 0.75f,
            onReceiverAnimationName: "Flame 3",
            onReceiverAnimationDuration: 1.75f,
            iconAnimationName: "Flame 3",
            sprite: moveIconSprites[132]));
    }

    public static void InitializeItems()
    {
        items.Add(StatsObject.Create(
            name: "Tiny Attack Potion",
            description: "+10 current health\n +3 attack for the remainder of this fight",
            damageDescription: "+10 current health",
            temporaryDescription: "+3 attack",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 10f }
                            })
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARY, 3f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/1")));

        items.Add(StatsObject.Create(
            name: "Carrot",
            description: "permanent +10 max health" + "\n+20 current health",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 10f }
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 20f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/2")));

        items.Add(StatsObject.Create(
            name: "Tiny Defense Potion",
            description: "+15 current health\n+4 defense for the remainder of this fight",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 15f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARY, 4f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/3")));

        items.Add(StatsObject.Create(
            name: "Grade B Small Egg",
            description: "+3 max health per turn in inventory" + "\n+3 current health per turn in inventory",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 3f }
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 3f }
                            })
                    }
                }
            ),
            isTurnBasedPermanent: true,
            sprite: Resources.Load<Sprite>("Items/4")));

        items.Add(StatsObject.Create(
            name: "Tiny Agility\nPotion",
            description: "+20 current health\n temporary +4 agility per turn for the remainder of this fight",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 20f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARYTEMPORARY, 4f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/5")));

        items.Add(StatsObject.Create(
            name: "Old Bone",
            description: "-25 current health\npermanent +2 max health and +2 defense per turn",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENTPERMANENT, 2f }
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, -25f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENTPERMANENT, 2f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/6")));

        items.Add(StatsObject.Create(
            name: "Tiny Luck Potion",
            description: "+25 current health\ntemporary +10% luck per turn for the remainder of this fight",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 25f }
                            })
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIERTEMPORARYTEMPORARY, 0.10f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/7")));

        items.Add(StatsObject.Create(
            name: "Bronze Ore",
            description: "permanent +4 defense" + "\n-1 agility" + " per turn in inventory",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 4f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, -1f }
                            })
                    }
                }
            ),
            isTurnBasedPermanent: true,
            sprite: Resources.Load<Sprite>("Items/8")));

        items.Add(StatsObject.Create(
            name: "Small Attack Potion",
            description: "+30 current health\ntemporary +1 attack per turn, resets every battle",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 30f }
                            })
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASETEMPORARYPERMANENT, 3f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/9")));

        items.Add(StatsObject.Create(
            name: "Quick Fang",
            description: "-50 current health\npermanent +5% attack" + "\n+5% agility (additive)" + "\nper turn",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, -50f }
                            })
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIERPERMANENTPERMANENT, 0.05f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIERPERMANENTPERMANENT, 0.05f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/10")));

        items.Add(StatsObject.Create(
            name: "Small Defense Potion",
            description: "+60 current health\ntemporary +10% defense per turn, resets every battle",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 60f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIERTEMPORARYPERMANENT, 0.1f }
                            })
                    }
                }
            ),
            sprite: Resources.Load<Sprite>("Items/11")));

        items.Add(StatsObject.Create(
            name: "Grade A Large Egg",
            description: "+10 max health per turn in inventory" + "\n+15 current health per turn in inventory",
            isStatChange: true,
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 10f }
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 15f }
                            })
                    }
                }
            ),
            isTurnBasedPermanent: true,
            sprite: Resources.Load<Sprite>("Items/12")));
    }

    public static void InitializeEquipment()
    {
        equipment.Add(StatsObject.Create(
            name: "Rusty Sword",
            description: "Type: Weapon\n+2 attack",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 2f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/1")));

        equipment.Add(StatsObject.Create(
            name: "Flimsy Sandals",
            description: "Type: Armor\n+10 max health\n+2 defense" + "\n+2 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 2f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 2f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.ARMOR,
            sprite: Resources.Load<Sprite>("Equipment/2")));

        equipment.Add(StatsObject.Create(
            name: "Blunt Dagger",
            description: "Type: Weapon\n+1 attack" + "\n+1 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 1f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 1f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/3")));

        equipment.Add(StatsObject.Create(
            name: "Mochi Donut",
            description: "Type: Misc\n+2 current health per turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 2f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.MISC,
            sprite: Resources.Load<Sprite>("Equipment/4")));

        equipment.Add(StatsObject.Create(
            name: "Simple Lance",
            description: "+3 attack" + "\n1.1x attack multiplier",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 3f },
                                { StatTypeType.BASEMULTIPLIER, 1.1f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/5")));

        equipment.Add(StatsObject.Create(
            name: "Cloth Helm",
            description: "+20% max health\n+5 defense",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 1.2f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.ARMOR,
            sprite: Resources.Load<Sprite>("Equipment/6")));

        equipment.Add(StatsObject.Create(
            name: "Club",
            description: "+5 attack -5 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f },
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, -5f },
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/7")));

        equipment.Add(StatsObject.Create(
            name: "Bronze Band",
            description: "+1 max health\n+3 current health" + "\n+1 luck per turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f },
                            })
                    },
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 3f },
                            })
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f },
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.MISC,
            sprite: Resources.Load<Sprite>("Equipment/8")));

        equipment.Add(StatsObject.Create(
            name: "Simple Dagger",
            description: "+5 attack" + "\n+5 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 5f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/9")));

        equipment.Add(StatsObject.Create(
            name: "Uggs",
            description: "+25 max health\n +4 defense" + "\n+4 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 25f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 4f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 4f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.ARMOR,
            sprite: Resources.Load<Sprite>("Equipment/10")));

        equipment.Add(StatsObject.Create(
            name: "Longsword",
            description: "+6 attack\n" + "permanent +1 attack per turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 6f },
                                { StatTypeType.BASEPERMANENT, 1f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/11")));

        equipment.Add(StatsObject.Create(
            name: "Silver Necklace",
            description: "permanent +1 health regen per turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 1f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.MISC,
            sprite: Resources.Load<Sprite>("Equipment/12")));

        equipment.Add(StatsObject.Create(
            name: "Small Axe",
            description: "+12 attack -30% agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 12f },
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 0.7f },
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/13")));

        equipment.Add(StatsObject.Create(
            name: "2 Tan Glovers",
            description: "+35 max health\n+4 defense" + "\n1.2x attack multiplier",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 35f }
                            })
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEMULTIPLIER, 1.2f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 4f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.ARMOR,
            sprite: Resources.Load<Sprite>("Equipment/14")));

        equipment.Add(StatsObject.Create(
            name: "The Lifebringer",
            description: "+8 attack" + "\n+8 current health every turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 8f },
                            })
                    },
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 8f },
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/15")));

        equipment.Add(StatsObject.Create(
            name: "Jeweled Silver Necklace",
            description: "+4 permanent luck per turn" + "\n+7 current health per turn",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.heal, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 7f }
                            })
                    },
                    {
                        StatType.luck, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASEPERMANENT, 4f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.MISC,
            sprite: Resources.Load<Sprite>("Equipment/16")));

        equipment.Add(StatsObject.Create(
            name: "Curved Dagger",
            description: "+9 attack" + "\n+9 agility",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.attack, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 9f }
                            })
                    },
                    {
                        StatType.agility, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 9f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.WEAPON,
            sprite: Resources.Load<Sprite>("Equipment/17")));

        equipment.Add(StatsObject.Create(
            name: "Leather Belt",
            description: "+50 max health\n+50% max health\n+10 defense",
            stats: Stats.Create(
                stats: new Dictionary<StatType, Stat>
                {
                    {
                        StatType.health, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 50f },
                                { StatTypeType.BASEMULTIPLIER, 1.5f }
                            })
                    },
                    {
                        StatType.defense, Stat.Create(
                            stat: new Dictionary<StatTypeType, float>
                            {
                                { StatTypeType.BASE, 10f }
                            })
                    }
                }
            ),
            equipmentType: EquipmentType.ARMOR,
            sprite: Resources.Load<Sprite>("Equipment/18")));
    }


    public static string NumberFormatter(float number)
    {
        return System.Math.Round(number, 1, System.MidpointRounding.AwayFromZero).ToString();
    }

    public static string[] DescriptionCreator(StatsObject statsObject)
    {
        StatType[] allStatTypes = (StatType[])System.Enum.GetValues(typeof(StatType));
        StatTypeType[] allStatTypeTypes = (StatTypeType[])System.Enum.GetValues(typeof(StatTypeType));

        string[] desc = { "", "", "", "" };
        Stats empty = Stats.Create();
        foreach (StatType statType in allStatTypes)
        {
            foreach (StatTypeType statTypeType in allStatTypeTypes)
            {
                string start = "";
                string statTypeString = statType.ToString();
                string mid = " ";
                float stat = statsObject.stats.stats[statType].stat[statTypeType];
                string end = "";

                if (statsObject.stats.stats[statType].stat[statTypeType] != empty.stats[statType].stat[statTypeType])
                {
                    if (statTypeString == "health")
                    {
                        statTypeString = "max health";
                    }
                    else if (statTypeString == "heal")
                    {
                        statTypeString = "health regen";
                    }

                    if (stat > 0)
                    {
                        start += "+";
                    }
                    int index = 0;
                    if (statTypeType == StatTypeType.BASE)
                    {
                        index = 0;
                        if (statType == StatType.heal && statsObject.equipmentType == EquipmentType.NONE)
                        {
                            index = 1;
                            statTypeString = "current health";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASETEMPORARY)
                    {
                        index = 2;
                        if (statsObject.equipmentType != EquipmentType.NONE)
                        {
                            end = " per turn";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASETEMPORARYTEMPORARY)
                    {
                        index = 2;
                        end = " per turn of current battle";
                    }
                    else if (statTypeType == StatTypeType.BASETEMPORARYPERMANENT)
                    {
                        index = 2;
                        end = " per turn";
                    }
                    else if (statTypeType == StatTypeType.BASEPERMANENT)
                    {
                        index = 3;
                        if (statsObject.equipmentType != EquipmentType.NONE)
                        {
                            end = " per turn";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASEPERMANENTTEMPORARY)
                    {
                        index = 3;
                        end = " per turn of current battle";
                    }
                    else if (statTypeType == StatTypeType.BASEPERMANENTPERMANENT)
                    {
                        index = 3;
                        end = " per turn";
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIER)
                    {
                        index = 0;
                        if (statsObject.equipmentType == EquipmentType.NONE)
                        {
                            start = "";
                            stat *= 100;
                        }
                        else
                        {
                            stat *= 100;
                            stat -= 100;
                            if (stat < 0)
                            {
                                start = "";
                            }
                        }
                        mid = "% ";
                        if (statType == StatType.heal && statsObject.equipmentType == EquipmentType.NONE)
                        {
                            index = 1;
                            statTypeString = "current health";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERTEMPORARY)
                    {
                        index = 2;
                        stat *= 100;
                        mid = "% ";
                        if (statsObject.equipmentType != EquipmentType.NONE)
                        {
                            end = " per turn";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERTEMPORARYTEMPORARY)
                    {
                        index = 2;
                        stat *= 100;
                        mid = "% ";
                        end = " per turn of current battle";
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERTEMPORARYPERMANENT)
                    {
                        index = 2;
                        stat *= 100;
                        mid = "% ";
                        end = " per turn";
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERPERMANENT)
                    {
                        index = 3;
                        stat *= 100;
                        mid = "% ";
                        if (statsObject.equipmentType != EquipmentType.NONE)
                        {
                            end = " per turn";
                        }
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERPERMANENTTEMPORARY)
                    {
                        index = 3;
                        stat *= 100;
                        mid = "% ";
                        end = " per turn of current battle";
                    }
                    else if (statTypeType == StatTypeType.BASEMULTIPLIERPERMANENTPERMANENT)
                    {
                        index = 3;
                        stat *= 100;
                        mid = "% ";
                        end = " per turn";
                    }
                    if (statsObject.isTurnBasedPermanent)
                    {
                        end += " per turn in inventory";
                    }
                    desc[index] += start + NumberFormatter(stat) + mid + statTypeString + end + "\n";
                }
                else if (statsObject.stats.stats[statType].isUsed && statTypeType == StatTypeType.BASEMULTIPLIER)
                {
                    start = "";
                    stat *= 100;
                    mid = "% ";
                    desc[0] += start + NumberFormatter(stat) + mid + statTypeString + end + "\n";
                }
            }
        }
        desc[0].TrimEnd();
        desc[1].TrimEnd();
        desc[2].TrimEnd();
        return desc;
    }
}
