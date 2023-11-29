using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private ImagesScript[] images;
    [SerializeField] private GameObject winPanel;
    private bool shufflingComplete = false;
    private int emptySpaceIndex = 15;
    private Camera cam;
    private bool wonGame;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // wait half a second before shuffling
        StartCoroutine(WaitToShuffle(3.0f));
    }

    IEnumerator WaitToShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                clickSound.Play();

                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2.1f)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    ImagesScript imageScript = hit.transform.GetComponent<ImagesScript>();
                    emptySpace.position = imageScript.targetPosition;
                    imageScript.targetPosition = lastEmptySpacePosition;
                    int imageIdx = findIndex(imageScript);
                    images[emptySpaceIndex] = images[imageIdx];
                    images[imageIdx] = null;
                    emptySpaceIndex = imageIdx;
                }
            }
        }

        if(!wonGame && shufflingComplete)
        {
            Debug.Log("WE shouldn't be here!");
            int correctImages = 0;

            foreach (var a in images)
            {
                if (a != null)
                {
                    if (a.inRightPlace)
                    {
                        correctImages++;
                    }
                }
            }

            if (correctImages == images.Length - 1)
            {
                wonGame = true;
                winPanel.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Shuffle()
    {
        Debug.Log("Shuffling!");
        if (emptySpaceIndex != 15) {

            Vector3 imageAtIdx = images[15].targetPosition;
            images[15].targetPosition = emptySpace.position;
            images[emptySpaceIndex] = images[15];
            images[15] = null;
            emptySpaceIndex = 15;
        }

        int inversion;

        do
        {

            for (int i = 0; i < 15; i++)
            {
                if (images[i] != null)
                {
                    var lastPosition = images[i].targetPosition;
                    int rnd = Random.Range(0, 14);
                    images[i].targetPosition = images[rnd].targetPosition;
                    images[rnd].targetPosition = lastPosition;
                    var img = images[i];
                    images[i] = images[rnd];
                    images[rnd] = img;
                }
            }

            inversion = getInversions();
            // can print a statement to see how many times we shuffle!
        } while (inversion % 2 == 1);

        shufflingComplete = true;
    }

    public int findIndex(ImagesScript iscrypt)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
            {
                if (images[i] == iscrypt)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    // When inversion is an even number our puzzle is solveable
    int getInversions()
    {
        int inversionsSum = 0;
        for(int i = 0; i < images.Length; i++)
        {
            int thisImageInversion = 0;
            for(int j = i; j < images.Length; j++)
            {
                if (images[j] != null)
                {
                    if (images[i].image_nbr > images[j].image_nbr)
                    {
                        thisImageInversion++;
                    }
                }
            }

            inversionsSum += thisImageInversion;
        }

        return inversionsSum;
    }
}
