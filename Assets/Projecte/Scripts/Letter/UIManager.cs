// En UIManager.cs
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

    // --- NUEVO MÉTODO START ---
    void Start()
    {
        if (LetterManager.Instance != null)
        {
            // 1. Asignarse como el UIManager actual en el Singleton
            LetterManager.Instance.uiManager = this;

            // 2. Actualizar toda la UI con el estado actual del LetterManager
            InitializeAllWordDisplays();
        }
        else
        {
            Debug.LogError("UIManager: No se pudo encontrar LetterManager.Instance al iniciar.");
        }
    }

    // --- NUEVA FUNCIÓN ---
    // Esta función actualiza toda la UI basándose en los datos del LetterManager
    void InitializeAllWordDisplays()
    {
        if (LetterManager.Instance.words == null) return;

        // Recorre las palabras guardadas en el manager
        foreach (var wordData in LetterManager.Instance.words)
        {
            // Llama a tu función existente para actualizar la UI de CADA palabra
            UpdateWordUI(wordData.wordId, wordData.collectedLetters, wordData.fullWord);
        }
    }


    // Llamar desde LetterManager cuando se recoge una letra (o desde Start)
    public void UpdateWordUI(string wordId, bool[] collectedLetters, string fullWord)
    {
        foreach (var w in wordUIs)
        {
            if (w.wordId == wordId)
            {
                string display = "";
                for (int i = 0; i < fullWord.Length; i++)
                {
                    // Asegurarse de que el array de 'collectedLetters' es válido
                    if (collectedLetters != null && i < collectedLetters.Length && collectedLetters[i])
                        display += fullWord[i];   // letra recogida
                    else
                        display += "_";           // letra faltante

                    display += " ";                // espacio entre letras
                }
                w.uiText.text = display.TrimEnd(); // Usar TrimEnd() es un poco más limpio
                break;
            }
        }
    }
}