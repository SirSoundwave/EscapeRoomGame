using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public GameEvent potionChannel;
    
    private List<string> potions;

    // Start is called before the first frame update
    void Start()
    {
        potions = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePotion(Component sender, object data)
    {
        if (!this.Equals(sender))
        {
            potions.Add((string) data);
            Debug.Log((string) data);
        }

        Debug.Log(potions.Count);

        if (potions.Count == 3 && potions[0] == "Pink Potion" && potions[1] == "Blue Potion" && potions[2] == "Yellow Potion") {
            Debug.Log("Hooray!");
        } else if (potions.Count == 3) {
            potionChannel.Raise(this, 0);
            potions = new List<string>();
            Debug.Log("boo hoo");
        }
    }
}
