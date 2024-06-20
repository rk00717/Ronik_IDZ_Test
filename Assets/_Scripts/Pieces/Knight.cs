public class Knight : ChessPiece {
    protected override void Initialize() {
        base.Initialize();

        directions = new int[][]{
            new int[]{2, 1},   // Up 2, Right 1
            new int[]{1, 2},   // Up 1, Right 2
            new int[]{-1, 2},  // Down 1, Right 2
            new int[]{-2, 1},  // Down 2, Right 1
            new int[]{-2, -1}, // Down 2, Left 1
            new int[]{-1, -2}, // Down 1, Left 2
            new int[]{1, -2},  // Up 1, Left 2
            new int[]{2, -1},  // Up 2, Left 1
        };
    }
}