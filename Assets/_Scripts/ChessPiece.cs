using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Enumeration for the different types of chess pieces. 
/// </summary>
public enum PieceType{
    none,
    pawn,
    rook,
    knight,
    bishop,
    queen,
    king,
}

/// <summary> 
/// Enumeration for the different teams (colors) of chess pieces. 
/// </summary>
public enum PieceTeam{
    none,
    white,
    black,
}

/// <summary>
/// Abstract class representing a generic chess piece.
/// </summary>
public abstract class ChessPiece : MonoBehaviour {
    [SerializeField] protected PieceType type;
    [SerializeField] protected PieceTeam team;
    [SerializeField, Range(0, 7)] protected int row, column;
    protected int[][] directions;   // Directions in which the piece can move
    protected bool doIterativeCheck = false;    // Flag to toggle between single-step and multi-step move checks

    public bool highlight;  // ONLY FOR DEBUG : Flag to indicate if the piece should be highlighted

    protected void Start() {
        Initialize();

        // ONLY FOR DEBUG
        if(highlight)
            Invoke("OnSelect", 1f);
    }

    protected virtual void Initialize(){
        ChessBoard.Instance.Register(this, row, column);
    }

    protected virtual void OnSelect(){
        ChessBoard.Instance.Highlight(this);
    }

    /// <summary>
    /// Gets the possible moves for the Pawn.
    /// </summary>
    /// <param name="board">The current state of the chess board.</param>
    /// <returns>
    /// A list of tuples representing possible moves. Each tuple contains:
    /// - int row: The row index of the move.
    /// - int column: The column index of the move.
    /// - int canTakedown: 0 if the move is safe, 1 if it can capture an opponent piece.
    /// </returns>
    public virtual List<(int, int, int)> GetPossibleMoves(ChessPiece[,] board){
        var positions = new List<(int, int, int)>();

        foreach(var direction in directions){
            int currentRow = row;
            int currentCol = column;

            while(true){
                int newRow = currentRow + direction[0];
                int newCol = currentCol + direction[1];

                // Break if the new position is out of bounds
                if(!ChessBoard.Instance.IsInBounds(newRow, newCol))
                    break;

                // Check if the new position is occupied by another piece
                if(board[newRow, newCol] != null){
                    // If the piece in the new position is an opponent piece, add the move with canTakedown = 1
                    if(board[newRow, newCol].GetPieceTeam() != team)
                        positions.Add((newRow, newCol, 1));

                    break;
                }

                positions.Add((newRow, newCol, 0));

                // Break if iterative checking is disabled (only check one step in this direction)
                if(!doIterativeCheck)
                    break;

                currentRow = newRow;
                currentCol = newCol;
            }
        }

        return positions;
    }

    public PieceType GetPieceType() => type;
    public PieceTeam GetPieceTeam() => team;
}