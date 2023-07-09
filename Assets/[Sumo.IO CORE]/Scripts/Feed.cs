using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
    [SerializeField] float _turnSpeed;

    void Update()
    {
        RotateTheFeed();
    }

    void RotateTheFeed()
    {
        transform.Rotate(Vector3.right * _turnSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Sumo>(out Sumo sumo))
        {
            EventManager.OnFeeding.Invoke(sumo);
            sumo.OnScoreUpOnFeed();
            sumo.IncreasePushPower();
            gameObject.SetActive(false);
        }
    }
}
