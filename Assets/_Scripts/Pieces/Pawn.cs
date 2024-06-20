using System.Collections.Generic;

public class Pawn : ChessPiece {
    protected override void Initialize() {
        base.Initialize();

        directions = new int[][]{
            new int[] {1, 0}, // Up
            new int[] {2, 0}, // Up two squares
        };
    }

    public override List<(int, int, int)> GetPossibleMoves(ChessPiece[,] board) {
        var positions = new List<(int, int, int)>();

        // Check each direction the piece can move in
        foreach(var direction in directions){
            int newRow = row + direction[0];
            int newCol = column + direction[1];

            // Break if the new position is out of bounds
            if(!ChessBoard.Instance.IsInBounds(newRow, newCol))
                return null;

            var piece = board[newRow, newCol];
            // Checks if the current position in empty or not.
            if(piece == null){
                positions.Add((newRow, newCol, 0));
                // Break if the move goes out of bounds after moving two squares up
                if(ChessBoard.Instance.IsInBounds(row + -2, column))
                    break;
            }else{
                break;
            }
        }

        // Check for opponent pieces in diagonal positions
        int nxtRow = row+1;
        int rightCol = column+1;
        int leftCol = column-1;
        if(IsOpponentFound(board, nxtRow, rightCol))
            positions.Add((nxtRow, rightCol, 1));

        if(IsOpponentFound(board, nxtRow, leftCol))
            positions.Add((nxtRow, leftCol, 1));

        return positions;
    }

    private bool IsOpponentFound(ChessPiece[,] board, int row, int col){
        var piece = board[row, col];

        if(ChessBoard.Instance.IsInBounds(row, col) && piece && piece.GetPieceTeam() != team){
            return true;
        }

        return false;
    }
}