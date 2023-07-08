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
            // sumo.OnScaleUp();
            // sumo.OnScoreUp();
            EventManager.OnFeed.Invoke(sumo);
            gameObject.SetActive(false);
        }
    }
}
