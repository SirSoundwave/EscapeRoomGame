using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    public GameEvent potionChannel;

    public void UsePotion() {
        potionChannel.Raise(this, name);
    }
}
