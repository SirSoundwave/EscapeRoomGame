using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
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
        
    }
}
