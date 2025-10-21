using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfigurarSonido : MonoBehaviour
{
    [Header("Componentes")]
    public AudioMixer audioMixer; // Arrastra tu AudioMixer aquí
    public Slider musicSlider;    // Arrastra tu Slider de música aquí

    [Header("Clave del Mixer")]
    public string mixerParameter = "VolumenMusica"; // El nombre exacto del parámetro expuesto

    [Header("Clave de Guardado")]
    public string playerPrefsKey = "VolumenMusicaGuardado"; // Dónde se guarda el valor

    void Start()
    {
        // Carga el valor guardado (o usa 1.0f si no hay nada)
        float savedVolume = PlayerPrefs.GetFloat(playerPrefsKey, 1.0f);

        // Asigna este valor al slider al iniciar
        musicSlider.value = savedVolume;

        // Llama a la función para que aplique el volumen al mixer
        // Lo hacemos aquí para asegurar que el volumen se carga al empezar la escena
        SetMusicVolume(savedVolume);
    }

    /// <summary>
    /// Esta función debe ser llamada por el evento "OnValueChanged" del Slider.
    /// </summary>
    /// <param name="sliderValue">El valor del slider (entre 0.0001 y 1).</param>
    public void SetMusicVolume(float sliderValue)
    {
        // El AudioMixer usa decibelios (dB), una escala logarítmica.
        // Un slider nos da un valor lineal (0 a 1).
        // Usamos esta fórmula para convertirlo correctamente.
        // Mathf.Log10(sliderValue) * 20
        // Si el valor es 0, Log10 da infinito negativo, por eso el mínimo del slider será 0.0001

        audioMixer.SetFloat(mixerParameter, Mathf.Log10(sliderValue) * 20);

        // Guarda el valor del slider (no el valor en dB) para la próxima vez
        PlayerPrefs.SetFloat(playerPrefsKey, sliderValue);
    }
}