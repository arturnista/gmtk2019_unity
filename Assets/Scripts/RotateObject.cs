﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField]
    private float speed;

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }

}
