using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int row;
    public int col;
    
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

    public void SetTexture(bool isSet)
    {
        _spriteRenderer.sprite = isSet ? ClickedImage : EmptyImage;
        state = isSet ? GridState.Enabled : GridState.Disabled;

    }

    public enum GridState
    {
        Disabled = 0,
        Enabled = 1

    }
}
