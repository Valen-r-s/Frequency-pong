using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float samplingRate;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false; 
        samplingRate = AudioSettings.outputSampleRate; // Frecuencia de muestreo
    }

    public void PlayFrequency(float frequency, float duration = 0.2f, float amplitude = 0.1f)
    {
        // Generar una onda sinusoidal
        int sampleCount = Mathf.CeilToInt(samplingRate * duration);
        float[] audioData = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            audioData[i] = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * i / samplingRate);
        }

        // Asignar datos al AudioSource y reproducir
        AudioClip clip = AudioClip.Create("FrequencyTone", sampleCount, 1, (int)samplingRate, false);
        clip.SetData(audioData, 0);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
