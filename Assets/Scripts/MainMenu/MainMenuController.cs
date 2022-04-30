using System;
using Photon.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Photon.Scripts.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField createInput;
        [SerializeField] private TMP_InputField maxPlayersInput;
        [SerializeField] private Button createButton;
        [SerializeField] private TMP_InputField joinInput;
        [SerializeField] private Button joinButton;

        public void Awake()
        {
            createButton.onClick.AddListener(HandleRoomCreation);
            joinButton.onClick.AddListener(JoinRoom);
        }

        private void HandleRoomCreation()
        {
            if (createInput.text == "") return;
            
            byte.TryParse(maxPlayersInput.text, out var numValue);
            if (numValue <= 0)
                ConnectPhotonManager.ME.CreateRoom(createInput.text);
            else
                ConnectPhotonManager.ME.CreateRoom(createInput.text, numValue);
        }

        private void JoinRoom()
        {
            if (joinInput.text == "") return;
            ConnectPhotonManager.ME.JoinRoom(joinInput.text);
        }
    }
}