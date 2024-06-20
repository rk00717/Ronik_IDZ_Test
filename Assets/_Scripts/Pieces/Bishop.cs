public class Bishop : ChessPiece {
    protected override void Initialize() {
        base.Initialize();

        directions = new int[][]{
            new int[]{1, 1},   // Up-Right
            new int[]{-1, -1}, // Down-Left
            new int[]{-1, 1},  // Down-Right
            new int[]{1, -1},  // Up-Left
        };

        doIterativeCheck = true;
    }
}