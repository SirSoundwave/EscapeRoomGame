using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    public Sprite sprite;

    public GameEvent potionChannel;

    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         UsePotion();
    //     }
    // }

    public void UsePotion() {
        name = gameObject.name;
        gameObject.GetComponent<Image>().sprite = sprite;

        potionChannel.Raise(this, name);
    }
}
