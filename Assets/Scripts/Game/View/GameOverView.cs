using System;
using Game.Character.Player;
using Photon.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Player = Photon.Realtime.Player;

namespace Game.View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button returnToMainMenu;

        private void Start()
        {
            returnToMainMenu.onClick.AddListener(ReturnToMainMenu);
        }

        public void Activate(Player player)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            scoreText.text = "";
            scoreText.text = scoreText.text += $"SCORE \n {ScoreManager.ME.GetLocalPlayersScore(player).ToString()}";
        }
        
        private void ReturnToMainMenu()
        {
            ScenesManager.ME.LoadScene(ScenesManager.MainMenu);
            ConnectPhotonManager.ME.LeaveRoom();
        }
    }
}