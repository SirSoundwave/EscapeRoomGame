using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBook : MonoBehaviour
{
    public GameObject menu;
    public Animator animator;

    public void Menu() {
        menu.SetActive(true);
        animator.SetTrigger("open");
    }
}
