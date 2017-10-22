using UnityEngine;
using UnityEngine.SceneManagement;

namespace MindMineClone
{
    public class MainMenuButtonBehavior : MonoBehaviour
    {
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene( "MainMenu" );
        }
    }
}
