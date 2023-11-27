using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public Animator transition;

    public void EndScreen()
    {
        StartCoroutine(EndScreenEnumer());
    }

    IEnumerator EndScreenEnumer()
    {
        yield return new WaitForSeconds(1);
        transition.SetTrigger("EndLevel");
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading scene index " + (SceneManager.GetActiveScene().buildIndex + 1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
