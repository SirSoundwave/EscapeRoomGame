using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    // public Sprite sprite;

    public GameEvent potionChannel;
    public Button button;

    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         UsePotion();
    //     }
    // }

    // public void UsePotion() {
    //     name = gameObject.name;
    //     gameObject.GetComponent<Image>().sprite = sprite;

    //     potionChannel.Raise(this, name);
    // }

    public GameObject potion;
    public Animator animator;

    public void UsePotion() {
        // potion.SetActive(true);
        // animator.SetTrigger("use");

        potionChannel.Raise(this, name);
        // button.interactable = false;
    }

    // public void RefillPotion() {
    //     button.interactable = true;
    // }
}
