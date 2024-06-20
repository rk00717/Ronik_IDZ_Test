public class Rook : ChessPiece {
    protected override void Initialize() {
        base.Initialize();

        directions = new int[][]{
            new int[]{1, 0},   // Up
            new int[]{-1, 0},  // Down
            new int[]{0, 1},   // Right
            new int[]{0, -1},  // Left
        };

        doIterativeCheck = true;
    }
}