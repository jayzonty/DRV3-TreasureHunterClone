using UnityEngine;
using UnityEngine.UI;

namespace MindMineClone
{
    public class ScoreManager : MonoBehaviour
    {
        public Text scoreText;

        private int m_score = 0;
        public int Score
        {
            get
            {
                return m_score;
            }

            set
            {
                m_score = value;

                if( scoreText != null )
                {
                    scoreText.text = string.Format( scoreTextFormat, m_score );
                }
            }
        }

        private string scoreTextFormat = "Score: {0}";

        private void Start()
        {
            if ( scoreText != null )
            {
                scoreText.text = string.Format( scoreTextFormat, m_score );
            }
        }
    }
}
