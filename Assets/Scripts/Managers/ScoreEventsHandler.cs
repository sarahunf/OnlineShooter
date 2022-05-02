using Game.Collectible;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Scripts.Managers
{
    public class ScoreEventsHandler : MonoBehaviour
    {
        private void Start()
        {
            Collectible.OnCollectedDestroyed += CollectibleDestroyed;
        }

        private void OnDestroy()
        {
            Collectible.OnCollectedDestroyed -= CollectibleDestroyed;
        }

        private void CollectibleDestroyed(Player player, int score)
        {
            ScoreManager.ME.AddScoreToPlayer(player, score);
        }
    }
}