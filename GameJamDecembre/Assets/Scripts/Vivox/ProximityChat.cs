using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ProximityChat : MonoBehaviour
{
    [SerializeField]
    private AudioClip _microphoneClip;

    // the audio source of this player
    [SerializeField]
    private AudioSource _thisSource;

    // the list that contains every players' audio sources
    [SerializeField]
    private List<AudioSource> _playersAudioSource = new();

    // Singleton
    #region Singleton
    private static ProximityChat _instance;

    public static ProximityChat Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Proximity Chat Manager");
                _instance = go.AddComponent<ProximityChat>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log($"<b><color=#{Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion

    private void Start()
    {
        _thisSource = this.GetComponent<AudioSource>();

        _playersAudioSource.Add(_thisSource);
        print(Microphone.devices[0]);
        if (Microphone.devices.Length > 0)
        {
            print("Start record");
            _microphoneClip = Microphone.Start(Microphone.devices[0], true, 10, 48000);
        }
    }

    public AudioClip GetMicrophoneClip()
    {
        return _microphoneClip;
    }

    public void PlayMicrophoneClip()
    {
        //await Task.Delay(3000);
        _thisSource.PlayOneShot(GetMicrophoneClip());
    }

    public void Update()
    {
        if (_playersAudioSource.Count <= 0) return;
        foreach (AudioSource audioSource in _playersAudioSource)
        {
            // clamp the distance (divided by two for balance) to transform it to volume (the closer the loudest IN THEORY, HAS TO INVERT THIS CLAMP)
            //float newVolume = Mathf.Clamp(Vector3.Distance(this.transform.position, audioSource.transform.position) / 2, 0, 5);
            //audioSource.volume = newVolume;
        }
        PlayMicrophoneClip();
        print("played micro clip");
    }
}

