using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] Sumo parentSumo;
    float pushForce;

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Sumo>(out Sumo sumo))
        {
            sumo.ScaleShiftOnCrash();
            
            parentSumo.ScaleShiftOnCrash();
            parentSumo.OnPushBack();

            if(other.GetComponent<Pusher>())
            {
                CalculateThePushForce(sumo);

                sumo.OnPushedFrom(parentSumo,pushForce);
                Debug.Log("calculated push");
                return;
            }
            sumo.OnPushedFrom(parentSumo);
            Debug.Log("pushed");
        }
    }

    void CalculateThePushForce(Sumo sumo)
    {
        float pushForceDifference = Mathf.Abs(sumo.GetPushForce() - parentSumo.GetPushForce());
        pushForce = pushForceDifference;
    }
}
