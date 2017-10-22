using UnityEngine;
using UnityEngine.Events;

namespace MindMineClone
{
    public class GameState : MonoBehaviour
    {
        public UnityEvent OnGameOver;

        private bool m_isGameOver = false;
        public bool IsGameOver
        {
            get
            {
                return m_isGameOver;
            }

            set
            {
                m_isGameOver = value;
                if( m_isGameOver )
                {
                    if( OnGameOver != null )
                    {
                        OnGameOver.Invoke();
                    }
                }
            }
        }
    }
}
