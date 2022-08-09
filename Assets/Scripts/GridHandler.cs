using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    private Grid _mainGrid;
    private Vector2 _cellSize;

    private const int CHAIN_COUNT = 3;
    
    [SerializeField]
    private AudioClip placeSound;
    [SerializeField]
    private AudioClip poofSound;


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
                element.SetGrid(this);
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

    public void TileEnabled(int row, int col)
    {
        AudioController.Instance.PlaySound(placeSound);
        if (CheckGrid(row, col))
        {
            MainController.Instance.SetInput(false);
            StartCoroutine(StartFlush(row, col));
        }
    }
    

    public bool CheckGrid(int row, int col)
    {
        bool foundMatch = false;
        int rowChain = 0;
        for (int i = row-2; i <= row+2; i++)
        {
            if (i < 0 || i >= _mainGrid.Width) continue;
            Debug.Log(_mainGrid.GridElements[i, col].GetState());
            if (_mainGrid.GridElements[i, col].GetState() == GridElement.GridState.Enabled)
            {
                rowChain++;
                if (rowChain >= CHAIN_COUNT) { foundMatch = true;}
            }
            else
            {
                rowChain = 0;
            }
        }
        

        if (!foundMatch)
        {
            int colChain = 0;
            for (int j = col - 2; j <= col + 2; j++)
            {
                if (j < 0 || j >= _mainGrid.Height) continue;
                if (_mainGrid.GridElements[row, j].GetState() == GridElement.GridState.Enabled)
                {
                    colChain++;
                    if (colChain >= CHAIN_COUNT) { foundMatch = true;}
                }
                else
                {
                    colChain = 0;
                }
            }
        }
        
        return foundMatch;
    }

    public IEnumerator StartFlush(int row, int col)
    {
        yield return new WaitForSeconds(1f);
        FlushConnectedGrid(row, col);
        yield return new WaitForSeconds(0.25f);
        MainController.Instance.SetInput(true);
    }


    public void FlushConnectedGrid(int row, int col)
    {
        HashSet<Vector2> checkedPositions = new HashSet<Vector2>();
        Queue<GridElement> listPositions = new Queue<GridElement>();

        GridElement firstElement = _mainGrid.GridElements[row, col];
        listPositions.Enqueue(firstElement); //sanity add, I'm not doing BFS from memory without training wheels lmao

        while (listPositions.Count > 0)
        {
            GridElement currentElement = listPositions.Dequeue();
            Vector2 coords = currentElement.GetCoordinates();
            if (!checkedPositions.Contains(coords))
            {
                checkedPositions.Add(coords);

                row = (int) coords.x;
                col = (int) coords.y;

                if (currentElement.GetState() == GridElement.GridState.Enabled)
                {
                    currentElement.Disable();
                    for (int i = row-1; i <= row+1; i+=2)
                    {
                        if (i < 0 || i >= _mainGrid.Width) continue;
                        GridElement neighbor = _mainGrid.GridElements[i, col];
                        if (!checkedPositions.Contains(neighbor.GetCoordinates()))
                        {
                            listPositions.Enqueue(neighbor);
                        }
                    }
                    for (int j = col - 1; j <= col + 1; j+=2)
                    {
                        if (j < 0 || j >= _mainGrid.Height) continue;
                        GridElement neighbor = _mainGrid.GridElements[row, j];
                        if (!checkedPositions.Contains(neighbor.GetCoordinates()))
                        {
                            listPositions.Enqueue(neighbor);
                        }
                    }
                }
            }
        }
        
        AudioController.Instance.PlaySound(poofSound);
    }

    Vector3 GetTileOffset(int i, int j, int size, Vector2 _cellSize, int padding)
    {
        return new Vector2(
            _cellSize.x * (1 + (padding+1) / 10f) * (j - size/2f + 0.5f),
            -_cellSize.y * (1 + (padding+1) / 10f) * (i - size/2f + 0.5f)); 
    }

    void GetTileDetails()
    {
        SpriteRenderer tileRenderer = gridTilePrefab.GetComponent<SpriteRenderer>();
        //apparently this is for sliced sprites but I got too lost in the offset sauce
        _cellSize = tileRenderer.size * tileRenderer.transform.localScale;
    }
    
}
