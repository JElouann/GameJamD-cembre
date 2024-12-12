using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Vivox;
using System;
using System.Security.Authentication;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Netcode;

public class VivoxPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject _localPlayerHead;
    private Vector3 _lastPlayerHeadPos;

    private string _gameChannelName = "Test3DChannel";
    private bool _isIn3DChannel = false;
    private Channel3DProperties _player3DProperties;

    private int _clientID;
    [SerializeField] private int _newVolumeMinusPlus50 = 0;

    private float _nextPosUpdate;

    private void Start()
    {
        if (IsLocalPlayer)
        {
            InitializeAsync();

            VivoxService.Instance.LoggedIn += onLoggedIn;
            VivoxService.Instance.LoggedOut += onLoggedOut;
        }
    }

    private void Update()
    {
        if (_isIn3DChannel && IsLocalPlayer)
        {
            if (Time.time > _nextPosUpdate)
            {
                updatePlayer3DPos();
                _nextPosUpdate += 0.3f;
            }
        }
    }

    async void InitializeAsync()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
        Debug.Log("Vivox: Initialize successfull");
    }

    public async void LoginToVivoxAsync()
    {
        if (IsLocalPlayer)
        {
            _clientID = (int)GameObject.Find("NetworkManager").GetComponent<NetworkManager>().LocalClientId;

            LoginOptions options = new LoginOptions();
            options.DisplayName = "Client" + _clientID;
            options.EnableTTS = true;
            await VivoxService.Instance.LoginAsync(options);

            join3DChannelAsync();
        }
    }

    public async void join3DChannelAsync()
    {
        await VivoxService.Instance.JoinPositionalChannelAsync(_gameChannelName, ChatCapability.AudioOnly, _player3DProperties);
        _isIn3DChannel = true;
        Debug.Log("Vivox: Sucessfully joined 3D channel");
    }

    public void updatePlayer3DPos()
    {
        VivoxService.Instance.Set3DPosition(_localPlayerHead, _gameChannelName);
        if (_localPlayerHead.transform.position != _lastPlayerHeadPos)
        {
            _lastPlayerHeadPos = _localPlayerHead.transform.position;
            Debug.Log("Vivox: 3D position SET:" + _localPlayerHead.transform.position);
        }
    }

    private void onLoggedIn()
    {
        if (VivoxService.Instance.IsLoggedIn)
        {
            Debug.Log("Vivox: Client" + _clientID + "Login sucessfull");
        }
        else
        {
            Debug.Log("Bébou: Cannot sign into Vivox, check your credentials and token settings");
        }
    }

    private void onLoggedOut()
    {
        _isIn3DChannel = false;

        VivoxService.Instance.LeaveAllChannelsAsync();
        Debug.Log("Vivox: Left from all channels");
        VivoxService.Instance.LogoutAsync();
        Debug.Log("Vivox: Logged out from Vivox");
    }

    public void setPlayerHeadPos(GameObject playerHead)
    {
        if (_localPlayerHead == null)
        {
            _localPlayerHead = playerHead;
            Debug.Log("Vivox: Successfully attached to PlayerHead.");
        }
    }

    public void updateVolume()
    {
        VivoxService.Instance.SetChannelVolumeAsync(_gameChannelName, _newVolumeMinusPlus50);
    }
}