using UnityEngine;
using UnityEngine.SceneManagement;

namespace MindMineClone
{
    public class PlayButtonBehavior : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene( "Game" );
        }
    }
}
