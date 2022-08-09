using UnityEngine;

public class Grid
{
    public int Width;
    public int Height;
    public int Padding;

    public Vector2 CellSize;
    public GridElement[,] GridElements;

    public Grid(int width, int height, Vector2 cellSize, int padding)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        Padding = padding;
        GridElements = new GridElement[Height, Width];
    }

    public GridElement GetCoord(int row, int col)
    {
        return GridElements[row, col];
    }
    
}