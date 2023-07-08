using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] Sumo parentSumo;
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Sumo>(out Sumo sumo))
        {
            sumo.OnPushedFrom(parentSumo);
            parentSumo.OnPushBack();
            Debug.Log("pushed");
        }    
    }
}
