using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Localization.Settings;

public class ControllerLanguage : MonoBehaviour
{
    private bool _active = false;

    void Start()
    {
      
        int ID = PlayerPrefs.GetInt("LocalKey", 0);
        changeLocale(ID);
    }

    public void changeLocale(int localeID)
    {
        if (_active)
        {
            return;
        }

        if (!gameObject.activeInHierarchy)
        {
            
            gameObject.SetActive(true);
          
        }

        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("Localkey", localeID);

        _active = false;

  
    }
}