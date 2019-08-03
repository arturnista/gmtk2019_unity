using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
 
    [SerializeField]
    private float time;

    void Start()
    {
        Destroy(gameObject, time);
    }

}
