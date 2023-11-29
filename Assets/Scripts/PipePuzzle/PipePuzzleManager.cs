using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzleManager : MonoBehaviour
{
    public GameEvent WinChannel;

    public GameObject pipesHolder;
    private GameObject[] pipes;
    private List<Pipe> origins = new List<Pipe>();
    private List<Pipe> ends = new List<Pipe>();

    public GameEvent pipeWipe;

    int totalPipes = 0;

    int winTicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = pipesHolder.transform.childCount;
        pipes = new GameObject[totalPipes];
        for(int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
            Pipe pipe = pipes[i].GetComponent<Pipe>();
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

    private void Update()
    {
        RotatePipes();
        WipePipes();
    }

    private void LateUpdate()
    {
        
        CheckFill();
        CheckWin();

    }

    private void WipePipes()
    {
        //Debug.Log("Wiping all");
        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<Pipe>().Wipe();
        }
    }

    private void RotatePipes()
    {
        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<Pipe>().setFilled(false);
            pipe.GetComponent<Pipe>().UpdateFilled();
            pipe.GetComponent<Pipe>().RotatePipe();
        }
    }

    private void CheckFill()
    {
        

        //Debug.Log("Checking fill");
        Queue<Pipe> check = new Queue<Pipe>();
        HashSet<Pipe> finished = new HashSet<Pipe>();

        foreach(Pipe pipe in origins)
        {
            check.Enqueue(pipe);
        }

        while(check.Count > 0)
        {
            Pipe pipe = check.Dequeue();
            //Debug.Log("Pipe type: " + pipe.GetPipeType());
            finished.Add(pipe);
            List<Pipe> connected = pipe.ConnectedPipes();
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

        foreach(Pipe filled in finished)
        {
            filled.setFilled(true);
            filled.UpdateFilled();
        }
        //Debug.Log("Updated fill");
    }

    private void CheckWin()
    {
        //Debug.Log("Checking win");
        bool winCondMet = true;
        foreach(Pipe end in ends)
        {
            if (!end.getFilled())
            {
                winCondMet = false;
                break;
            }
        }
        if (winCondMet)
        {
            if(winTicks > 10)
            {
                WinChannel.Raise(this, null);
            } else
            {
                winTicks++;
            }
            
        } else
        {
            winTicks = 0;
        }
    }
}
