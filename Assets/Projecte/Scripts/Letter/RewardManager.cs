using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void GiveWordReward(string wordId)
    {
        // Ejemplo: dar monedas o desbloquear skin
        Debug.Log($"Dar recompensa por completar {wordId}");
        // GameEconomy.Instance.AddCoins(100);
    }
}
