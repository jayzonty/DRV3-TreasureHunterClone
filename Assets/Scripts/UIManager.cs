using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMineClone
{
    public class UIManager : MonoBehaviour
    {
        public Canvas uiCanvas;

        public GameObject gameOverPanelPrefab;

        public GameObject tutorialPanelPrefab;

        private GameObject m_gameOverPanel;
        private GameObject m_tutorialPanel;

        private void Start()
        {
            if ( gameOverPanelPrefab != null )
            {
                m_gameOverPanel = Instantiate( gameOverPanelPrefab, uiCanvas.transform );
                HideGameOverPanel();
            }

            if ( tutorialPanelPrefab != null )
            {
                m_tutorialPanel = Instantiate( tutorialPanelPrefab, uiCanvas.transform );
                HideTutorialPanel();
            }
        }

        public void ShowGameOverPanel()
        {
            if ( m_gameOverPanel != null )
            {
                m_gameOverPanel.SetActive( true );
            }
        }

        public void HideGameOverPanel()
        {
            if ( m_gameOverPanel != null )
            {
                m_gameOverPanel.SetActive( false );
            }
        }

        public void ShowTutorialPanel()
        {
            if ( m_tutorialPanel != null )
            {
                m_tutorialPanel.SetActive( true );
            }
        }

        public void HideTutorialPanel()
        {
            if ( m_tutorialPanel != null )
            {
                m_tutorialPanel.SetActive( false );
            }
        }
    }
}
