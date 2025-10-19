using UnityEngine;

public class LetterManager : MonoBehaviour
{
    
    public static LetterManager Instance;

    [System.Serializable]
    public class WordData
    {
        public string wordId;
        public string fullWord;
        public bool[] collectedLetters; 
        public bool isCompleted = false;
    }

    public WordData[] words;
    public UIManager uiManager;

    void Awake()
    {
        if (Instance != null)
            Instance = this;
    
    else
        Destroy(gameObject);
    foreach (var w in words)
        {
            w.collectedLetters = new bool[w.fullWord.Length];
            LoadWordCompletionState(w);
        }
    }
    private void LoadWordCompletionState(WordData word)
    {
        // PlayerPrefs.GetInt retorna 0 (false) si no troba la clau
        word.isCompleted = (PlayerPrefs.GetInt("Word_" + word.wordId + "_Complete", 0) == 1);
    }
    private void SaveWordCompletionState(WordData word)
    {
        // 1 per True, 0 per False
        PlayerPrefs.SetInt("Word_" + word.wordId + "_Complete", word.isCompleted ? 1 : 0);
        PlayerPrefs.Save(); // Guarda al disc
    }
    public bool IsWordCompleted(string wordId)
    {
        foreach (var w in words)
        {
            if (w.wordId == wordId)
            {
                return w.isCompleted;
            }
        }
        return false;
    }
    public void RegisterCollectedLetter(string wordId, int index, char letter)
    {
        foreach (var w in words)
        {
            if (w.wordId == wordId)
            {
                if (w.collectedLetters[index]) return; // Evitar registrar dues vegades

                w.collectedLetters[index] = true;
                
                // Actualitzar UI
                uiManager?.UpdateWordUI(wordId, w.collectedLetters, w.fullWord);

                // Comprovar si la paraula s'ha completat
                bool complete = true;
                foreach (bool b in w.collectedLetters) if (!b) complete = false;
                
                if (complete && !w.isCompleted) // Si s'ha completat PER PRIMERA VEGADA
                {
                    w.isCompleted = true; // MARCAR com a completada
                    SaveWordCompletionState(w); // GUARDAR a PlayerPrefs
                    
                    Debug.Log("¡Palabra completada! " + w.fullWord);
                    // Opcionalment, crida a RewardManager
                    // RewardManager.Instance?.GiveWordReward(wordId);
                }
                break;
            }
        }
    }
}
