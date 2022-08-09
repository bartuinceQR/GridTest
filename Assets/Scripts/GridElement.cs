using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int row;
    public int col;

    private GridHandler _gridHandler;
    
    [SerializeField] private Sprite EmptyImage;
    [SerializeField] private Sprite ClickedImage;

    private GridState state;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        state = GridState.Disabled;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCoords(int x, int y)
    {
        row = x;
        col = y;
    }

    public void Enable()
    {
        if (state == GridState.Enabled) return;
        _spriteRenderer.sprite = ClickedImage;
        state = GridState.Enabled;
        
        Debug.Log(GetCoordinates());

        _gridHandler.TileEnabled(row, col);
    }
    
    public void Disable()
    {
        _spriteRenderer.sprite = EmptyImage;
        state = GridState.Disabled;
    }

    public void SetGrid(GridHandler grid)
    {
        _gridHandler = grid;
    }

    public GridState GetState()
    {
        return state;
    }

    public Vector2 GetCoordinates()
    {
        return new(row, col);
    }

    public enum GridState
    {
        Disabled = 0,
        Enabled = 1

    }
}
