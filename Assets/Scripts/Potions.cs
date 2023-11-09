using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite sprite;

    public void UsePotion() {
        renderer.sprite = sprite;
    }
}
