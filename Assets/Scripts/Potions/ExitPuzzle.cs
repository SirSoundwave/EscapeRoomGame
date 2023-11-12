using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPuzzle : MonoBehaviour
{
    public GameObject panel;
    public GameObject backButton;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Display() {
        panel.SetActive(true);
        Destroy(backButton);
    }

    public void Exit() {
        SceneManager.LoadScene("SampleScene");
    }
}
