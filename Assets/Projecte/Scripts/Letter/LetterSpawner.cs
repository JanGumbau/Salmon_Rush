using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject letterPrefab;
    public string wordId = "EVENT1";
    public int letterIndex = 0; 
    public char letterChar = 'A';

    
    public void SpawnAt(Vector3 pos)
    {
        var go = Instantiate(letterPrefab, pos, Quaternion.identity);
        var letterComp = go.GetComponent<Letter>();
        if (letterComp != null)
        {
            letterComp.wordId = wordId;
            letterComp.letterIndex = letterIndex;
            letterComp.letterChar = letterChar;
            
        }
    }
}

