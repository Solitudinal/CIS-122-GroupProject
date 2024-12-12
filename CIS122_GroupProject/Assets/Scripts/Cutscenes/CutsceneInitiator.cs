using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Written by Niloy Sarker Bappy

public class CutsceneInitiator : MonoBehaviour
{
    // Start is called before the first frame update

    private CutsceneHandler cutsceneHandler;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cutsceneHandler.PlayNextElement();

        }
    }



}
