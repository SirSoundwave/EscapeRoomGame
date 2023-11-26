using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PipeType
{
    ORIGIN,
    MIDDLE,
    END
}

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    Dictionary<float, float> oppositeRotations = new Dictionary<float, float>();

    public PipeType pipeType = PipeType.MIDDLE;
    public bool straightPipe;
    public bool moveable = true;
    float[] correctRotations;

    public Sprite filledSprite;
    public Sprite emptySprite;

    private List<Transform> connectionBoxes = new List<Transform>();
    private bool filled;

    public LayerMask EndLayer;

    public GameEvent audioTrigger;

    private void Start()
    {
        if (moveable)
        {
            
            oppositeRotations.Add(0, 180);
            oppositeRotations.Add(90, 270);
            oppositeRotations.Add(180, 0);
            oppositeRotations.Add(270, 90);

            if (straightPipe)
            {
                correctRotations = new float[] { Mathf.Round(transform.eulerAngles.z), oppositeRotations[Mathf.Round(transform.eulerAngles.z)] };
            }
            else
            {
                correctRotations = new float[] { transform.eulerAngles.z };
            }

            int rand = Random.Range(0, rotations.Length);
            transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
            



        }
        for (int i = 0; i < transform.childCount; i++)
        {
            connectionBoxes.Add(transform.GetChild(i));
        }




    }

    public void UpdateFilled()
    {
        if (filled)
        {
            this.GetComponent<SpriteRenderer>().sprite = filledSprite;
        } else
        {
            this.GetComponent<SpriteRenderer>().sprite = emptySprite;
        }
    }

    public void setFilled(bool filled)
    {
        this.filled = filled;
    }

    public bool getFilled()
    {
        return filled;
    }

    public List<PipeScript> ConnectedPipes()
    {
        List<PipeScript> result = new List<PipeScript>();
        //Debug.Log("Ends to check: " + connectionBoxes.Count);
        foreach(var box in connectionBoxes)
        {
            //Debug.Log("Position: " + box.transform.position);
            //Debug.DrawRay(box.transform.position, (box.transform.position - box.parent.position).normalized * 0.2f, Color.green);
            RaycastHit2D[] hit = Physics2D.RaycastAll(box.transform.position, (box.transform.position - box.parent.position).normalized, 0.2f, EndLayer);
            
            for (int i = 0; i < hit.Length; i++)
            {
                //Debug.Log("hit");
                result.Add(hit[i].collider.transform.parent.GetComponent<PipeScript>());
            }
        }

        return result;
    }

    private void OnMouseDown()
    {
        if (moveable)
        {
            audioTrigger.Raise(this, "PipeMove");
            transform.Rotate(0, 0, 90);
            transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z));
        }

    }

    public bool isPlaced()
    {
        if (!moveable)
        {
            return true;
        }

        for(int i = 0; i < correctRotations.Length; i++)
        {
            if (transform.eulerAngles.z.Equals(correctRotations[i]))
            {
                return true;
            }
        }

        return false;
    }

    public PipeType GetPipeType()
    {
        return this.pipeType;
    }

    public void setMoveable(bool moveable)
    {
        this.moveable = moveable;
    }

}
