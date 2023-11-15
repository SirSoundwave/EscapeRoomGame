using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public GameEvent potionChannel;
    public GameEvent completionChannel;
    public GameEvent uiChannel;
    
    //private List<string> potions;
    private List<int> numbers;
    private List<int> potionList;

    // Start is called before the first frame update
    void Start()
    {
        //potions = new List<string>();
        numbers = new List<int>();
        potionList = new List<int>();

        for (int i = 0; i < 3; i++) {
            System.Random rand = new System.Random();
            int num = rand.Next(1, 6);
            if (i > 0) {
                while (num == numbers[i - 1]) {
                    num = rand.Next(1, 6);
                }
            }
            if (i > 1) {
                while (num == numbers[i - 2]) {
                    num = rand.Next(1, 6);
                }
            }
            numbers.Add(num);
            Debug.Log(numbers[i]);
        }
        
        uiChannel.Raise(this, numbers);
    }

    public void UpdatePotion(Component sender, object data)
    {
        if (!this.Equals(sender))
        {
            string pot = (string) data;
            if (pot == "Pink Potion") {
                potionList.Add(1);
            } else if (pot == "Red Potion") {
                potionList.Add(2);
            } else if (pot == "Orange Potion") {
                potionList.Add(3);
            } else if (pot == "Blue Potion") {
                potionList.Add(4);
            } else if (pot == "Yellow Potion") {
                potionList.Add(5);
            }

            // potions.Add((string) data);
            Debug.Log((string) data);
        }

        //Debug.Log(potions.Count);

        if (potionList.Count == 3 && potionList[0] == numbers[0] && potionList[1] == numbers[1] && potionList[2] == numbers[2]) {
            completionChannel.Raise(this, 0);
            Debug.Log("Hooray!");
        } else if (potionList.Count == 3) {
            potionList = new List<int>();
            potionChannel.Raise(this, 0);
            Debug.Log("boo hoo");
        }

        // if (potions.Count == 3 && potions[0] == "Pink Potion" && potions[1] == "Blue Potion" && potions[2] == "Yellow Potion") {
        //     uiChannel.Raise(this, 0);
        //     Debug.Log("Hooray!");
        // } else if (potions.Count == 3) {
            
        //     potions = new List<string>();
        //     potionChannel.Raise(this, 0);
        //     Debug.Log("boo hoo");
        // }
    }
}
