using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private SpriteRenderer sprite;
    public int image_nbr;
    public bool inRightPlace;
    public Vector3 correctPosition;
    public static float imageSpeed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, imageSpeed * Time.fixedDeltaTime);

        if(targetPosition == correctPosition && GameObject.FindObjectOfType<SlidePuzzleScript>().GetShufflingComplete())
        {
            inRightPlace = true;
        }

        else
        {
            inRightPlace = false;
        }
    }
}
