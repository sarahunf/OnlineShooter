using System;
using Photon.Realtime;
using UnityEngine;

namespace Game.Collectible
{
    public class Collectible : MonoBehaviour
    {
        public static event Action<Player, int> OnCollectedDestroyed;
        public Player player;
        [HideInInspector] public int score;

        private void OnDisable()
        {
            OnCollectedDestroyed?.Invoke(player, score);
        }
    }
}