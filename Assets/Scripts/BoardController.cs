using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMineClone
{
    public class BoardController : MonoBehaviour
    {
        public int width = 50;
        public int height = 20;

        public GameObject wallPiecePrefab;
        public GameObject boardPiecePrefab;

        public Color wallColor = Color.black;

        public List<Color> colorLevels;

        private Board m_boardData;

        private void Awake()
        {
            
        }

        private void Start()
        {
            m_boardData = new Board( width, height );
            m_boardData.InitializeBoard();

            for( int x = 0; x <= width + 1; x++ )
            {
                for( int y = 0; y <= height + 1; y++ )
                {
                    if ( ( x < 1 ) || ( x > width ) || ( y < 1 ) || ( y > height ) )
                    {
                        GameObject wall = Instantiate( wallPiecePrefab );
                        wall.transform.position = new Vector3( x, y, 0.0f );

                        SpriteRenderer spriteRenderer = wall.GetComponent<SpriteRenderer>();
                        if ( spriteRenderer != null )
                        {
                            spriteRenderer.color = wallColor;
                        }
                    }
                    else
                    {
                        GameObject piece = Instantiate( boardPiecePrefab );
                        piece.transform.position = new Vector3( x, y, 0.0f );

                        BoardPieceController boardPieceController = piece.GetComponent<BoardPieceController>();
                        boardPieceController.pieceData = m_boardData.GetPiece( x, y );
                        boardPieceController.parentBoard = this;

                        SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
                        if ( spriteRenderer != null )
                        {
                            int level = m_boardData.GetPiece( x, y ).Level;

                            spriteRenderer.color = colorLevels[level];
                        }
                    }
                }
            }
        }
    }
}
