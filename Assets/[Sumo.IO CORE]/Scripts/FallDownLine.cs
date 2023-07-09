using UnityEngine;

public class FallDownLine : MonoBehaviour
{
    [SerializeField] PlayerSumo playerSumo;
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Sumo>(out Sumo sumo))
        {
            if(!sumo._fallingDown)
            {
                if(sumo == playerSumo)
                {
                    EventManager.OnGameStateChanged(GameState.Lose);
                }
                SumoManager.Instance.MinusSumoCount();
                SumoManager.Instance.CheckSumoCountForWin();

                EventManager.OnSumoFallDown?.Invoke();
                sumo.SetSumoFallen();

                SumoManager.Instance.SetAllAISumosTarget();
            }
        }
    }
}
