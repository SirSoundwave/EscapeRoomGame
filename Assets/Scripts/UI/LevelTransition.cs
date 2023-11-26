using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    public void EndScreen()
    {
        StartCoroutine(EndScreenEnumer());
    }

    IEnumerator EndScreenEnumer()
    {
        yield return new WaitForSeconds(1);
        transition.SetTrigger("EndLevel");
    }

    public void TransitionIntoLevel(Component sender, object data)
    {
        if (data is int)
        {
            StartCoroutine(LoadLevelTransition((int)data));
        }
    }

    public void LoadLevel(Component sender, object data)
    {
        if (data is int)
        {
            SceneManager.LoadScene((int)data);
        }
    }

    IEnumerator LoadLevelTransition(int index)
    {
        transition.SetTrigger("EndLevel");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Loading scene index " + index);
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading scene index " + (SceneManager.GetActiveScene().buildIndex + 1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
