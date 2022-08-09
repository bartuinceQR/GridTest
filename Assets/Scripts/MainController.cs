using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private GridHandler gridHandler;
    
    public static MainController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GenerateGrid(int size, int padding)
    {
        gridHandler.Clean();
        gridHandler.Init(size, padding);
    }
}
