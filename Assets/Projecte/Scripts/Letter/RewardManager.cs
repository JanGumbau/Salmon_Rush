using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GiveWordReward(string wordId)
    {
        
        Debug.Log($"Dar recompensa por completar {wordId}");
        
    }
}
