// En CollectionDisplayUI.cs

using UnityEngine;
using TMPro;

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
            Debug.LogWarning("CollectionDisplayUI: No se encuentra LetterManager.Instance.");
            return;
        }

        foreach (var display in displayWords)
        {
            string wordId = display.wordId;
            TMP_Text textComponent = display.uiText;

            string fullWord = "";
            bool[] collectedLetters = null;


            foreach (var w in LetterManager.Instance.words)
            {
                if (w.wordId == wordId)
                {
                    fullWord = w.fullWord.ToUpper();
                    collectedLetters = w.collectedLetters;
                    break;
                }
            }


            if (string.IsNullOrEmpty(fullWord) || collectedLetters == null)
            {
                textComponent.text = "_ _ _ _";
                continue;
            }


            string displayString = "";
            for (int i = 0; i < fullWord.Length; i++)
            {

                if (i < collectedLetters.Length && collectedLetters[i])
                    displayString += fullWord[i];
                else
                    displayString += hiddenChar;

                displayString += Spacing;
            }


            textComponent.text = displayString.TrimEnd();
        }
    }
}