using System;
using System.Collections.Generic;
using System.Linq;
using Game.Character.Player;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Player = Photon.Realtime.Player;

namespace Photon.Scripts.Managers
{
    public class ScoreManager : MonoBehaviourPunCallbacks
    {
        public static ScoreManager ME;
        private ScoreView _scoreView;
        
        private void Awake()
        {
            if (ME != null && ME != this)
            {
                Destroy(gameObject);
            }
            else
            {
                ME = this;
                DontDestroyOnLoad(ME);
            }
        }
        
        public void AddScoreToPlayer(Player player, int amount)
        {
            Player[] currentPlayers = ConnectPhotonManager.ME.PlayersInRoom();
            
            foreach (var p in currentPlayers)
            {
                if (p.Equals(player))
                {
                    var currentScore = p.GetScore();
                    currentScore += amount;
                    p.SetScore(currentScore);
                }
            }
            
            UpdateScoreView();
        }

        private void UpdateScoreView()
        {
            
            if (!_scoreView)
            {
                _scoreView = FindObjectOfType<ScoreView>();
            }
            else
            {
                _scoreView.UpdateScore();
            }
        }

        public Dictionary<String, int> ScoresByNick()
        {
            Player[] currentPlayers = ConnectPhotonManager.ME.PlayersInRoom();
            return currentPlayers.ToDictionary(player => player.NickName, GetLocalPlayersScore);
        }
        
        public int GetLocalPlayersScore(Player player)
        {
            return player.GetScore();
        }

    }
}