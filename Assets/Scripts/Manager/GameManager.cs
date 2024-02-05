using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;
    [HideInInspector]
    public InputHandler InputHandler;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);         
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        InitManager();
    }

    private void InitManager()
    {
        InputHandler = new InputHandler();
    }

    private void Update()
    {
        InputHandler.InputUpdate();
    }
}
