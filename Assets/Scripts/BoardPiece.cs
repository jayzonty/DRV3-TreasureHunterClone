using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMineClone
{
    public class BoardPiece
    {
        public delegate void PieceDestroyedDelegate( BoardPiece piece );
        public event PieceDestroyedDelegate OnPieceDestroyed;

        public delegate void PieceLevelChangedDelegate( BoardPiece piece, int oldLevel, int newLevel );
        public event PieceLevelChangedDelegate OnPieceLevelChanged;

        public delegate void PieceHighlightedDelegate( BoardPiece piece );
        public event PieceHighlightedDelegate OnPieceHighlighted;

        public delegate void PieceUnhighlightedDelegate( BoardPiece piece );
        public event PieceUnhighlightedDelegate OnPieceUnhighlighted;

        private Board m_board;
        public Board Board
        {
            get
            {
                return m_board;
            }
        }

        private int m_x;
        public int X
        {
            get
            {
                return m_x;
            }
        }

        private int m_y;
        public int Y
        {
            get
            {
                return m_y;
            }
        }

        /*
         * -1 = wall
         * 0 and above = actual level of piece
         */
        private int m_level;
        public int Level
        {
            get
            {
                return m_level;
            }

            set
            {
                int oldLevel = m_level;

                m_level = value;

                if( OnPieceLevelChanged != null )
                {
                    OnPieceLevelChanged( this, oldLevel, m_level );
                }
            }
        }

        public BoardPiece( int x, int y, Board board )
        {
            m_x = x;
            m_y = y;

            m_board = board;

            m_level = 0;
        }

        public void TickLevel()
        {
            Level = ( Level + 1 ) % m_board.NumPieceLevels;
        }

        public override int GetHashCode()
        {
            return ( m_x << 16 ) | m_y;
        }

        public void Destroy()
        {
            if( OnPieceDestroyed != null )
            {
                OnPieceDestroyed( this );
            }
        }

        public void Highlight()
        {
            if( OnPieceHighlighted != null )
            {
                OnPieceHighlighted( this );
            }
        }

        public void Unhighlight()
        {
            if( OnPieceUnhighlighted != null )
            {
                OnPieceUnhighlighted( this );
            }
        }
    }
}
