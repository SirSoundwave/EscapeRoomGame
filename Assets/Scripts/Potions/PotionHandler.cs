using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public GameEvent potionChannel;
    public GameEvent completionChannel;
    public GameEvent uiChannel;
    
    private List<int> numbers;
    private List<int> potions;

    // Start is called before the first frame update
    void Start()
    {
        numbers = new List<int>();
        potions = new List<int>();

        for (int i = 0; i < 3; i++) {
            System.Random rand = new System.Random();
            while (true)
            {
                int num = rand.Next(1, 6);
                if (!numbers.Contains(num))
                {
                    numbers.Add(num);
                    break;
                }
            }
            
            
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
                potions.Add(1);
            } else if (pot == "Red Potion") {
                potions.Add(2);
            } else if (pot == "Orange Potion") {
                potions.Add(3);
            } else if (pot == "Blue Potion") {
                potions.Add(4);
            } else if (pot == "Yellow Potion") {
                potions.Add(5);
            }

            Debug.Log((string) data);
        }

        if (potions.Count == 3 && potions[0] == numbers[0] && potions[1] == numbers[1] && potions[2] == numbers[2]) {
            completionChannel.Raise(this, 0);
            Debug.Log("Hooray!");
        } else if (potions.Count == 3) {
            potions = new List<int>();
            potionChannel.Raise(this, 0);
            Debug.Log("boo hoo");
        }
    }
}
