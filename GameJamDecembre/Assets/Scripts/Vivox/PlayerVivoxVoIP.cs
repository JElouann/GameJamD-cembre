using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VivoxVo : MonoBehaviour
{
    [SerializeField]
    private Toggle _uiVoiceToggle;
    [SerializeField]
    private GameObject _thisPlayerHead;

    private void Start()
    {
        _uiVoiceToggle = GameObject.Find("Vivox Toggle Voice Activation").GetComponent<Toggle>();
        if(_uiVoiceToggle.isOn )
        {
            this.GetComponent<VivoxPlayer>().LoginToVivoxAsync();
        }

        this.GetComponent<VivoxPlayer>().setPlayerHeadPos(_thisPlayerHead);
        GameObject.Find("Vivox Toggle VoiceActivation").SetActive(false);
    }
}
