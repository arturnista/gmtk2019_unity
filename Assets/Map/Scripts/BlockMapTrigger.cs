﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMapTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameController.main.OnStartGame();
    }
}
