using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public GameEvent potionChannel;
    public GameEvent uiChannel;
    
    private List<string> potions;

    // Start is called before the first frame update
    void Start()
    {
        potions = new List<string>();
    }

    public void UpdatePotion(Component sender, object data)
    {
        if (!this.Equals(sender))
        {
            potions.Add((string) data);
            Debug.Log((string) data);
        }

        //Debug.Log(potions.Count);

        if (potions.Count == 3 && potions[0] == "Pink Potion" && potions[1] == "Blue Potion" && potions[2] == "Yellow Potion") {
            uiChannel.Raise(this, 0);
            Debug.Log("Hooray!");
        } else if (potions.Count == 3) {
            
            potions = new List<string>();
            potionChannel.Raise(this, 0);
            Debug.Log("boo hoo");
        }
    }
}
