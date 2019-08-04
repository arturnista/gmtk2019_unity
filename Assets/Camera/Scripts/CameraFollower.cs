using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void OnDisable()
    {
        transform.position = new Vector3(0f, 0f, -10f);
    }

    void Update()
    {
        Vector3 pos = playerMovement.transform.position;    
        pos.z = -10f;
        transform.position = pos;
    }

}
