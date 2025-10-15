using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterPopupUI : MonoBehaviour
{
    public static LetterPopupUI Instance;

    [Header("UI")]
    public GameObject popupObject;       // El objeto (text) que aparece arriba
    public TMP_Text popupText;           // Texto que mostrará la letra
    public float showTime = 1f;          // Tiempo visible
    public Vector3 startPos = new Vector3(0, -80, 0);
    public Vector3 endPos = new Vector3(0, -30, 0);
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float timer = 0f;
    private bool showing = false;

    void Awake()
    {
        Instance = this;
        popupObject.SetActive(false);
    }

    void Update()
    {
        if (!showing) return;

        timer += Time.deltaTime;
        float t = timer / showTime;

        // movimiento vertical suave
        popupObject.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

        // fade-out
        float alpha = fadeCurve.Evaluate(t);
        popupText.alpha = alpha;

        if (t >= 1f)
        {
            popupObject.SetActive(false);
            showing = false;
        }
    }

    public void ShowLetter(char letter)
    {
        popupText.text = letter.ToString().ToUpper();
        popupText.alpha = 1f;
        popupObject.transform.localPosition = startPos;
        popupObject.SetActive(true);

        timer = 0f;
        showing = true;
    }
}

