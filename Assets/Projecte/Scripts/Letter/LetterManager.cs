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
        Instance = this;

        foreach (var w in words)
        {
            w.collectedLetters = new bool[w.fullWord.Length];
        }
    }

    public void RegisterCollectedLetter(string wordId, int index, char letter)
    {
        foreach (var w in words)
        {
            if (w.wordId == wordId)
            {
                w.collectedLetters[index] = true;
                // actualizar UI
                uiManager.UpdateWordUI(wordId, w.collectedLetters, w.fullWord);

                // aquí puedes añadir lógica de recompensa si palabra completa
                bool complete = true;
                foreach (bool b in w.collectedLetters) if (!b) complete = false;
                if (complete)
                {
                    Debug.Log("¡Palabra completada! " + w.fullWord);
                }
                break;
            }
        }
    }
}
