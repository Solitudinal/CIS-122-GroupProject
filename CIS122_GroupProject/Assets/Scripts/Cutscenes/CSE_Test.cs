using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Niloy Sarker Bappy

public class CSE_Test : CutsceneElementBase
{
    // Start is called before the first frame update
    

    public override void Execute()
    {
        StartCoroutine( WaitAndAdvance());
        Debug.Log("Executing" + name);
    }


}
