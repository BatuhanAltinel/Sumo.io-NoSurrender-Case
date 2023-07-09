using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offSet;
    [SerializeField] float _followSpeed = 2f;
    
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
        // transform.position = Vector3.MoveTowards(transform.position , playerTransform.position + offSet,_followSpeed * Time.deltaTime);
        transform.position = playerTransform.position + offSet;
    }


}
