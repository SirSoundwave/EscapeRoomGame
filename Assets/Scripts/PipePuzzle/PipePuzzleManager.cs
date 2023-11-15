using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzleManager : MonoBehaviour
{
    public GameEvent WinChannel;

    public GameObject pipesHolder;
    private GameObject[] pipes;
    private List<PipeScript> origins = new List<PipeScript>();
    private List<PipeScript> ends = new List<PipeScript>();

    int totalPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = pipesHolder.transform.childCount;
        pipes = new GameObject[totalPipes];
        for(int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
            PipeScript pipe = pipes[i].GetComponent<PipeScript>();
            if (pipe.GetPipeType().Equals(PipeType.ORIGIN))
            {
                origins.Add(pipe);
            } else if (pipe.GetPipeType().Equals(PipeType.END))
            {
                ends.Add(pipe);
            }
        }
        //Debug.Log("Total pipes: " + totalPipes);
        //Debug.Log("Total origins: " + origins.Count);
    }

    // Update is called once per frame
    void Update()
    {
        CheckFill();
        CheckWin();
    }

    private void CheckFill()
    {
        //Debug.Log("Pipes to check: " + pipes.Length);
        foreach(var pipeObject in pipes)
        {
            pipeObject.GetComponent<PipeScript>().setFilled(false);
        }

        Queue<PipeScript> check = new Queue<PipeScript>();
        HashSet<PipeScript> finished = new HashSet<PipeScript>();

        foreach(PipeScript pipe in origins)
        {
            check.Enqueue(pipe);
        }

        while(check.Count > 0)
        {
            PipeScript pipe = check.Dequeue();
            //Debug.Log("Pipe type: " + pipe.GetPipeType());
            finished.Add(pipe);
            List<PipeScript> connected = pipe.ConnectedPipes();
            //Debug.Log("Connected pipes found: " + connected.Count);
            foreach(var connectedPipe in connected)
            { 
                if (!finished.Contains(connectedPipe))
                {
                    //Debug.Log("Enqueueing New Pipe " + connectedPipe.pipeType);
                    check.Enqueue(connectedPipe);
                }
            }
            //Debug.Log("Finished pipes: " + finished.Count);
            //Debug.Log("Pipes to check: " + check.Count);
        }

        foreach(PipeScript filled in finished)
        {
            filled.setFilled(true);
        }

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<PipeScript>().UpdateFilled();
        }

    }

    private void CheckWin()
    {
        bool winCondMet = true;
        foreach(PipeScript end in ends)
        {
            if (!end.getFilled())
            {
                winCondMet = false;
                break;
            }
        }
        if (winCondMet)
        {
            WinChannel.Raise(this, null);
        }
    }
}
