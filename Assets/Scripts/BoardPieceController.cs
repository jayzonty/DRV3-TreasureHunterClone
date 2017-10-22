using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace MindMineClone
{
    public class BoardPieceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public BoardController parentBoard;

        public BoardPiece pieceData;

        private SpriteRenderer m_spriteRenderer;

        private void Awake()
        {
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            pieceData.OnPieceDestroyed += HandlePieceDestroyed;

            pieceData.OnPieceLevelChanged += HandlePieceLevelChanged;

            pieceData.OnPieceHighlighted += HandlePieceHighlighted;
            pieceData.OnPieceUnhighlighted += HandlePieceUnhighlighted;
        }

        private void HandlePieceDestroyed( BoardPiece boardPiece )
        {
            Destroy( gameObject );
        }

        private void HandlePieceLevelChanged( BoardPiece boardPiece, int oldLevel, int newLevel )
        {
            m_spriteRenderer.color = parentBoard.colorLevels[newLevel];
        }

        private void HandlePieceHighlighted( BoardPiece boardPiece )
        {
            Color color = m_spriteRenderer.color;
            color.a = 0.5f;

            m_spriteRenderer.color = color;
        }

        private void HandlePieceUnhighlighted( BoardPiece boardPiece )
        {
            m_spriteRenderer.color = parentBoard.colorLevels[boardPiece.Level];
        }

        public void OnPointerEnter( PointerEventData eventData )
        {
            if( !pieceData.Board.HasNeighbors( pieceData.X, pieceData.Y ) )
            {
                return;
            }

            List<BoardPiece> outline;
            List<BoardPiece> pieces = pieceData.Board.GetConnectedPieces( pieceData.X, pieceData.Y, out outline );
            for( int i = 0; i < pieces.Count; i++ )
            {
                pieces[i].Highlight();
            }
        }

        public void OnPointerExit( PointerEventData eventData )
        {
            if ( !pieceData.Board.HasNeighbors( pieceData.X, pieceData.Y ) )
            {
                return;
            }

            List<BoardPiece> outline;
            List<BoardPiece> pieces = pieceData.Board.GetConnectedPieces( pieceData.X, pieceData.Y, out outline );
            for ( int i = 0; i < pieces.Count; i++ )
            {
                pieces[i].Unhighlight();
            }
        }

        public void OnPointerClick( PointerEventData eventData )
        {
            int numPiecesCleared = pieceData.Board.ClearPieces( pieceData.X, pieceData.Y );

            GameObject.FindObjectOfType<ScoreManager>().Score += numPiecesCleared;

            if( !pieceData.Board.HasValidMoves() )
            {
                GameObject.FindObjectOfType<GameState>().IsGameOver = true;
                //GameObject.FindObjectOfType<UIManager>().ShowGameOverPanel();
            }
        }
    }
}
