using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class CollectionDisplayUI : MonoBehaviour
{
    [System.Serializable]
    public class DisplayWord
    {
        public string wordId;
        public TMP_Text uiText;
    }

    public DisplayWord[] displayWords;
    public char hiddenChar = '_';
    private const string Spacing = " ";

    void OnEnable()
    {
        UpdateAllWordDisplays();
    }

    void UpdateAllWordDisplays()
    {
        if (LetterManager.Instance == null)
        {
            return;
        }
        foreach (var display in displayWords)
        {
            string wordId = display.wordId;
            TMP_Text textComponent = display.uiText;

            // 1. Comprova si la paraula està completa
            bool isComplete = LetterManager.Instance.IsWordCompleted(wordId);

            // 2. Troba la paraula completa (necessitem la longitud o la paraula)
            string fullWord = "";
            int wordLength = 0;
            
            // Cerca la paraula completa a LetterManager
            foreach (var w in LetterManager.Instance.words)
            {
                if (w.wordId == wordId)
                {
                    fullWord = w.fullWord.ToUpper();
                    wordLength = fullWord.Length;
                    break;
                }
            }

            if (wordLength == 0)
            {
                textComponent.text = "ERROR: ID no válido";
                continue;
            }

            string displayString = "";

            if (isComplete)
            {
                // Si està completa, es veu la paraula sencera
                for (int i = 0; i < wordLength; i++)
                {
                    displayString += fullWord[i];
                    displayString += Spacing;
                }
            }
            else
            {
                // Si no està completa, es veuen les siluetes (?????)
                for (int i = 0; i < wordLength; i++)
                {
                    displayString += hiddenChar;
                    displayString += Spacing;
                }
                // Opcional: mostrar les lletres recollides fins ara 
                // Aquesta versió mostra només la silueta si NO està completada
            }

            textComponent.text = displayString;
        }
    }
    
}
