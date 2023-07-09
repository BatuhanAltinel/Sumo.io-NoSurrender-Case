using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offSet;
    
    void Awake()
    {
        offSet = transform.position - playerTransform.position;
    }
    private void LateUpdate() 
    {
        FollowPlayer();      
    }

    void FollowPlayer()
    {
        transform.position = playerTransform.position + offSet;
    }


}
