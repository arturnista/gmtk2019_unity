using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    
    private PlayerMovement playerMovement;

    private Vector3 center = new Vector3(0f, 0f, -10f);

    private bool isFollowPlayer;

    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        isFollowPlayer = true;
    }

    void OnDisable()
    {
        transform.position = new Vector3(0f, 0f, -10f);
    }

    void Update()
    {
        if(isFollowPlayer)
        {
            Vector3 pos = playerMovement.transform.position;    
            pos.z = -10f;
            transform.position = pos;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, center, 8f * Time.deltaTime);
        }
    }

    public void StartGame()
    {
        isFollowPlayer = false;
    }

    public void FinishGame()
    {
        isFollowPlayer = true;        
    }

}
