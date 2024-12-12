using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//written by Niloy Sarker Bappy

public class CutsceneHandler : MonoBehaviour
{
    public Camera cam;
    public CinemachineVirtualCamera vCam;

    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;


    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
    }

    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Execute();
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }
}
