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
            sumo.OnScoreUpOnFeed();
            sumo.IncreasePushPower();
            sumo.ScaleUp();
            gameObject.SetActive(false);
        }
    }
}
