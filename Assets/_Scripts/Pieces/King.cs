public class King : ChessPiece {
    protected override void Initialize() {
        base.Initialize();

        directions = new int[][]{
            new int[]{1, 0},   // Up
            new int[]{-1, 0},  // Down
            new int[]{0, 1},   // Right
            new int[]{0, -1},  // Left
            // Diagonals
            new int[]{1, 1},   // Up-Right
            new int[]{-1, -1}, // Down-Left
            new int[]{-1, 1},  // Down-Right
            new int[]{1, -1},  // Up-Left
        };
    }
}