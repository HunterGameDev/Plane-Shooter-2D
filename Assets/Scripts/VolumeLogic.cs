using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeLogic : MonoBehaviour
{
    public static VolumeLogic Instance;

    public AudioMixer masterMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetVolume(-10);
    }

    public void SetVolume(float sliderVolume)
    {
        masterMixer.SetFloat("MasterVolumeParam", sliderVolume);
    }
}
