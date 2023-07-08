using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
    [SerializeField] float _turnSpeed;
    bool _isSumoTouch;

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
        if(other.TryGetComponent<Sumo>(out Sumo sumo) && !_isSumoTouch)
        {
            _isSumoTouch = true;
            EventManager.OnFeeding.Invoke(sumo);
            sumo.OnScoreUpOnFeed();
            sumo.IncreasePushPower();
            gameObject.SetActive(false);
        }
    }
}
