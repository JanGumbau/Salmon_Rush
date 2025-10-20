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
    }

    public WordData[] words;
    public UIManager uiManager;

  

    void Awake()
    {
       
        if (Instance == null)
        {
            
            Instance = this;
           
            DontDestroyOnLoad(gameObject);

           
            foreach (var w in words)
            {
              
                if (w.collectedLetters == null || w.collectedLetters.Length != w.fullWord.Length)
                {
                    w.collectedLetters = new bool[w.fullWord.Length];
                }
            }
        }
        else
        {
           
            Destroy(gameObject);
        }
        
    }

    public void RegisterCollectedLetter(string wordId, int index, char letter)
    {
        foreach (var w in words)
        {
            if (w.wordId == wordId)
            {
                w.collectedLetters[index] = true;
               
                uiManager.UpdateWordUI(wordId, w.collectedLetters, w.fullWord);

               
                bool complete = true;
                foreach (bool b in w.collectedLetters) if (!b) complete = false;
                // En LetterManager.cs, dentro de RegisterCollectedLetter()

                if (complete)
                {
                    Debug.Log("¡Palabra completada! " + w.fullWord);
                   
                    RewardManager.Instance?.GiveWordReward(w.wordId);
                }
                break;
            }
        }
    }
}
