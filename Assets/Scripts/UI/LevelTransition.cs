using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public CustomGameEvent exitActions;

    public Animator transition;

    public float transitionTime = 1f;
    public float saveTime = 1f;

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
            OnExit();
            StartCoroutine(LoadLevelTransition((int)data));
        }
    }

    public void LoadLevel(Component sender, object data)
    {
        if (data is int)
        {
            OnExit();
            StartCoroutine(LoadSceneDelayed((int)data));
        }
    }

    IEnumerator LoadLevelTransition(int index)
    {
        transition.SetTrigger("EndLevel");
        yield return new WaitForSeconds(Mathf.Max(transitionTime, saveTime));
        Debug.Log("Loading scene index " + index);
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        OnExit();
        Debug.Log("Loading scene index " + (SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadSceneDelayed(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadSceneDelayed(int index)
    {
        yield return new WaitForSeconds(saveTime);
        SceneManager.LoadScene(index);
    }

    public void OnExit()
    {
        exitActions.Invoke(this, null);
    }

}
