using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    ObjectPool _objPool;

    [SerializeField] private GameObject _ground;
    private float _areaRadius;
    [SerializeField] private float _spawnRepeatTime = 5f;
    private Vector3 _feedRotation = new Vector3(0,0,90);


    void Awake()
    {
        _objPool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        _areaRadius = _ground.transform.localScale.x / 2;
        InvokeRepeating("SpawnFeedObject",1f,_spawnRepeatTime);
    }

    void SpawnFeedObject()
    {
        if(GameManager.Instance.IsState(GameState.InGame))
        {
            GameObject go = _objPool.GetObjectFromPool();

            if(go != null)
            {
                go.transform.position = new Vector3(GetRandomPositionPoint(),1.2f,GetRandomPositionPoint());
                go.transform.rotation = Quaternion.Euler(_feedRotation);
            }
            
        }
        
    }

    float GetRandomPositionPoint()
    {
        float radius = 0;
        radius = Random.Range(-_areaRadius/2,_areaRadius/2);

        return radius;
    }
}
