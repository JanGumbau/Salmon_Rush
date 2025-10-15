using UnityEngine;
using TMPro;  // si usas TextMeshPro

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class WordUI
    {
        public string wordId;
        public TMP_Text uiText;
    }

    public WordUI[] wordUIs;

    // Llamar desde LetterManager cuando se recoge una letra
    public void UpdateWordUI(string wordId, bool[] collectedLetters, string fullWord)
    {
        foreach (var w in wordUIs)
        {
            if (w.wordId == wordId)
            {
                string display = "";
                for (int i = 0; i < fullWord.Length; i++)
                {
                    if (collectedLetters[i])
                        display += fullWord[i];   // letra recogida
                    else
                        display += "_";           // letra faltante
                    display += " ";                // espacio entre letras
                }
                w.uiText.text = display;
                break;
            }
        }
    }
}
