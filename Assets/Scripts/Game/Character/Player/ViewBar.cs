using UnityEngine;
using UnityEngine.UI;

namespace Game.Character.Player
{
    public class ViewBar : MonoBehaviour
    {
        [SerializeField] private Image fillBar;
        internal float startSize;

        public void UpdateView(byte amount)
        {
            fillBar.fillAmount = amount/startSize;
            if (fillBar.fillAmount < 0.05f)
            {
                fillBar.fillAmount = 0;
            }
        }
    }
}