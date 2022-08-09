using UnityEngine;

public class GridHandler : MonoBehaviour
{
    private Grid _mainGrid;
    private Vector2 _cellSize;

    [SerializeField]
    private GameObject gridTilePrefab;
    
    public void Init(int size, int padding)
    {
        GetTileDetails();
        _mainGrid = new Grid(size, size, _cellSize, padding);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 offset = GetTileOffset(i, j, size, _cellSize, padding);

                GameObject gridTile = Instantiate(gridTilePrefab, transform.position + offset, Quaternion.identity,
                    transform);
                GridElement element = gridTile.GetComponent<GridElement>();
                element.SetCoords(i, j);
                _mainGrid.GridElements[i, j] = element;
;            }
        }
    }

    public void Clean()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (_mainGrid != null)
            _mainGrid.GridElements = new GridElement[1, 1];
    }

    Vector3 GetTileOffset(int i, int j, int size, Vector2 _cellSize, int padding)
    {
        Debug.Log(_cellSize);
        return new Vector2(
            _cellSize.x * (1 + (padding+1) / 10f) * (j - size/2f + 0.5f),
            _cellSize.y * (1 + (padding+1) / 10f) * (i - size/2f + 0.5f)); 
    }

    void GetTileDetails()
    {
        SpriteRenderer tileRenderer = gridTilePrefab.GetComponent<SpriteRenderer>();
        _cellSize = tileRenderer.size * tileRenderer.transform.localScale;
    }
    
}
