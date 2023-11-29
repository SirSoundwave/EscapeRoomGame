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

    // Start is called before the first frame update
    void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.020f);

        if(targetPosition == correctPosition)
        {
            inRightPlace = true;
        }

        else
        {
            inRightPlace = false;
        }
    }
}
