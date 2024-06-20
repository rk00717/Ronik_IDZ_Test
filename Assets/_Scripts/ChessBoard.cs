using UnityEngine;

/// <summary>
/// Class representing the chess board. Manages the placement and movement of chess pieces.
/// </summary>
public class ChessBoard : MonoBehaviour {
    internal static ChessBoard Instance;
    private ChessPiece[,] _board;
    private int maxGridSize = 7;

    private void Awake() {
        Instance = this;
        Initialize();
    }

    public void Initialize(){
        _board = new ChessPiece[8, 8];
    }

    /// <summary>
    /// Registers a chess piece on the board at the specified position.
    /// </summary>
    /// <param name="piece">The chess piece to register.</param>
    /// <param name="row">The row index to place the piece.</param>
    /// <param name="col">The column index to place the piece.</param>
    internal void Register(ChessPiece piece, int row, int col){
        if(IsInBounds(row, col)){
            _board[row, col] = piece;
            SetPiecePosition(piece, row, col);
        }
    }

    /// <summary>
    /// Moves a piece from one position to another on the board.
    /// </summary>
    /// <param name="row">The starting row index.</param>
    /// <param name="col">The starting column index.</param>
    /// <param name="newRow">The destination row index.</param>
    /// <param name="newCol">The destination column index.</param>
    internal void Move(int row, int col, int newRow, int newCol){
        if(!(IsInBounds(row, col) && IsInBounds(newRow, newCol))) 
            return;
        
        var piece = _board[row, col];
        _board[newRow, newCol] = piece;
    
        SetPiecePosition(piece, newRow, newCol);

        _board[row, col] = null;
    }

    /// <summary>
    /// Highlights the possible moves for a selected piece.
    /// </summary>
    /// <param name="piece">The chess piece to highlight moves for.</param>
    internal void Highlight(ChessPiece piece){
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        var moves = piece.GetPossibleMoves(_board);

        foreach(var move in moves){
            // move.Item1 : row || x
            // move.Item2 : column || y
            // move.Item3 : Indicates if the highlight is normal (0) or not

            Debug.Log($"{move.Item1}, {move.Item2}");
            ChessBoardPlacementHandler.Instance.Highlight(move.Item1, move.Item2, move.Item3 == 0);
        }
    }

    /// <summary>
    /// Checks if the specified position is within the bounds of the chess board.
    /// </summary>
    /// <param name="x">The row index to check.</param>
    /// <param name="y">The column index to check.</param>
    /// <returns>True if the position is within bounds, otherwise false.</returns>
    public bool IsInBounds(int x, int y){
        if(x<0 || x>maxGridSize){
            return false;
        }

        if(y<0 || y>maxGridSize){
            return false;
        }

        return true;
    }

    /// <summary>
    /// Sets the position of a chess piece on the board.
    /// </summary>
    /// <param name="piece">The chess piece to position.</param>
    /// <param name="row">The row index for the position.</param>
    /// <param name="col">The column index for the position.</param>
    private void SetPiecePosition(ChessPiece piece, int row, int col){
        piece.transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, col).transform.position;
    }
}