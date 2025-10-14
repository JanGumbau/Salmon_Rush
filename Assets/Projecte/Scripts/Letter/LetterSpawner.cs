using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject letterPrefab;
    public string wordId = "EVENT1";
    public int letterIndex = 0; // el índice para este spawn
    public char letterChar = 'A';

    // Llama a este método cuando generes la sección del mapa
    public void SpawnAt(Vector3 pos)
    {
        var go = Instantiate(letterPrefab, pos, Quaternion.identity);
        var letterComp = go.GetComponent<Letter>();
        if (letterComp != null)
        {
            letterComp.wordId = wordId;
            letterComp.letterIndex = letterIndex;
            letterComp.letterChar = letterChar;
            // Ajusta sprite si usas atlas según letterChar
        }
    }
}

