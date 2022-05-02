using Photon.Pun;
using Photon.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Game.Character.Player
{
    public class ScoreView : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();
        }
        
        public void UpdateScore()
        {
            _view.RPC(nameof(RPCUpdateScore), RpcTarget.All);
        }
        
        [PunRPC]
        public void RPCUpdateScore()
        {
            scoreText.text = "";
            foreach (var kvp in ScoreManager.ME.ScoresByNick())
            {
                scoreText.text += $"{kvp.Key} = {kvp.Value}\n";
            }
        }
    }
}