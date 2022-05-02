using System;
using Photon.Pun;
using Photon.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Photon.Scripts.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField createInput;
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private Button createButton;
        [SerializeField] private TMP_InputField joinInput;
        [SerializeField] private Button joinButton;

        public void Awake()
        {
            createButton.onClick.AddListener(HandleRoomCreation);
            joinButton.onClick.AddListener(JoinRoom);
            nameInput.onValueChanged.AddListener(ChangeName);
        }

        private void HandleRoomCreation()
        {
            if (createInput.text == "") return;
            ConnectPhotonManager.ME.CreateRoom(createInput.text);
        }

        private void JoinRoom()
        {
            if (joinInput.text == "") return;
            ConnectPhotonManager.ME.JoinRoom(joinInput.text);
        }

        private void ChangeName(string input)
        {
            ConnectPhotonManager.ME.SaveNickName(input);
        }
    }
}