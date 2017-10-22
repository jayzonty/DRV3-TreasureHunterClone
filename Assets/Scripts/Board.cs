using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindMineClone
{
    public class Board
    {
        private int m_width;
        private int m_height;

        private int m_numPieceLevels;
        public int NumPieceLevels
        {
            get
            {
                return m_numPieceLevels;
            }
        }
        
        /*
         * The board is represented such that the lower-left corner
         * is ( 0, 0 ) and the upper-right corner is at ( width + 1, height + 1 )
         */
        private BoardPiece[,] m_pieces;

        public Board( int width, int height ) : this( width, height, 3 )
        {
        }

        public Board( int width, int height, int numPieceLevels )
        {
            m_width = width;
            m_height = height;

            m_numPieceLevels = numPieceLevels;

            m_pieces = new BoardPiece[width + 2, height + 2];
            for( int x = 0; x < m_pieces.GetLength( 0 ); x++ )
            {
                for( int y = 0; y < m_pieces.GetLength( 1 ); y++ )
                {
                    m_pieces[x, y] = new BoardPiece( x, y, this );
                    m_pieces[x, y].Level = -1;
                }
            }
        }

        public void InitializeBoard()
        {
            for( int x = 1; x <= m_width; x++ )
            {
                for( int y = 1; y <= m_height; y++ )
                {
                    m_pieces[x, y].Level = Random.Range( 0, m_numPieceLevels );
                }
            }
        }

        public BoardPiece GetPiece( int x, int y )
        {
            if ( !IsWithinBounds( x, y ) )
            {
                return null;
            }

            return m_pieces[x, y];
        }

        public int GetPieceLevel( int x, int y )
        {
            BoardPiece piece = GetPiece( x, y );
            if( piece != null )
            {
                return piece.Level;
            }

            return -1;
        }

        public bool HasNeighbors( int x, int y )
        {
            BoardPiece currentPiece = GetPiece( x, y );
            if( currentPiece != null )
            {
                BoardPiece leftPiece = GetPiece( x - 1, y );
                if( ( leftPiece != null ) && ( leftPiece.Level == currentPiece.Level ) )
                {
                    return true;
                }

                BoardPiece rightPiece = GetPiece( x + 1, y );
                if( ( rightPiece != null ) && ( rightPiece.Level == currentPiece.Level ) )
                {
                    return true;
                }

                BoardPiece topPiece = GetPiece( x, y + 1 );
                if( ( topPiece != null ) && ( topPiece.Level == currentPiece.Level ) )
                {
                    return true;
                }

                BoardPiece bottomPiece = GetPiece( x, y - 1 );
                if( ( bottomPiece != null ) && ( bottomPiece.Level == currentPiece.Level ) )
                {
                    return true;
                }
            }

            return false;
        }

        public List<BoardPiece> GetConnectedPieces( int x, int y, out List<BoardPiece> outline )
        {
            List<BoardPiece> ret = new List<BoardPiece>();

            outline = new List<BoardPiece>();

            BoardPiece currentPiece = GetPiece( x, y );

            HashSet<BoardPiece> visited = new HashSet<BoardPiece>();

            Stack<BoardPiece> stack = new Stack<BoardPiece>();
            stack.Push( currentPiece );

            while( stack.Count > 0 )
            {
                BoardPiece temp = stack.Pop();
                if( visited.Contains( temp ) )
                {
                    continue;
                }
                visited.Add( temp );

                if( temp.Level == currentPiece.Level )
                {
                    ret.Add( temp );
                }
                else
                {
                    outline.Add( temp );
                    continue;
                }

                BoardPiece left = GetPiece( temp.X - 1, temp.Y );
                if( ( left != null ) )
                {
                    stack.Push( left );
                }

                BoardPiece right = GetPiece( temp.X + 1, temp.Y );
                if( ( right != null ) )
                {
                    stack.Push( right );
                }

                BoardPiece top = GetPiece( temp.X, temp.Y + 1 );
                if( ( top != null ) )
                {
                    stack.Push( top );
                }

                BoardPiece bottom = GetPiece( temp.X, temp.Y - 1 );
                if( ( bottom != null ) )
                {
                    stack.Push( bottom );
                }
            }

            return ret;
        }

        public int ClearPieces( int x, int y )
        {
            List<BoardPiece> outline;

            List<BoardPiece> connectedPieces = GetConnectedPieces( x, y, out outline );
            if ( connectedPieces.Count > 1 )
            {
                for ( int i = 0; i < connectedPieces.Count; i++ )
                {
                    int x2 = connectedPieces[i].X;
                    int y2 = connectedPieces[i].Y;

                    connectedPieces[i].Destroy();

                    m_pieces[x2, y2] = null;
                }

                for( int i = 0; i < outline.Count; i++ )
                {
                    outline[i].TickLevel();
                }

                return connectedPieces.Count;
            }

            return 0;
        }

        public bool HasValidMoves()
        {
            for( int x = 1; x <= m_width; x++ )
            {
                for( int y = 1; y <= m_height; y++ )
                {
                    if( HasNeighbors( x, y ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsWithinBounds( int x, int y )
        {
            return ( ( 1 <= x && x <= m_width ) && ( 1 <= y && y <= m_height ) );
        }
    }
}
