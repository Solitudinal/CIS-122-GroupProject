using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Written by Niloy Sarker Bappy

public class CutsceneElementBase : MonoBehaviour
{
    // Start is called before the first frame update

    public float duration;

    public CutsceneHandler cutsceneHandler { get; private set; }

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute()
    {

    }
    protected IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }


}
