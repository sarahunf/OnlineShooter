using UnityEngine;
using UnityEngine.SceneManagement;
namespace Photon.Scripts.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager ME;
        
        public const string MainMenu = "Main Menu";
        public const string Game = "Game";
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

        public void LoadScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}