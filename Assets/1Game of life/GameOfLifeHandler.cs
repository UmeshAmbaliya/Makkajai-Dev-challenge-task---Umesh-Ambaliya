using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLifeHandler : MonoBehaviour
{
    public static GameOfLifeHandler Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] GameObject cellPrefab;
    [SerializeField] List<Vector2> inputA;
    [SerializeField] List<Vector2> inputB;
    [SerializeField] List<Vector2> inputC;
    [SerializeField] List<Vector2> inputD;
    [SerializeField] List<CellStoreData> cells;
    [SerializeField] List<CellBehaviour> deadCells; 
    Vector2[] neighbourVectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 1), new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, -1) };

    void Start()
    {
        
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ExecuteInput(inputA);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ExecuteInput(inputB);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ExecuteInput(inputC);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ExecuteInput(inputD);
        }
    }

    public void ExecuteInput(List<Vector2> myPoints)
    {
        cells = new List<CellStoreData>();
        foreach (Transform item in this.transform)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < myPoints.Count; i++)
        {
            CreateCellAtPoint(myPoints[i]);
        }
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].cell.CheckNeighBours();
        }
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].cell.MakeAliveDead();
        }
    }

    void CreateCellAtPoint(Vector2 point)
    {
        GameObject go = Instantiate(cellPrefab, this.transform);
        go.transform.position = point;
        go.name = "Cell " + (cells.Count + 1);
        go.GetComponent<CellBehaviour>().Init(point);
        CellStoreData sd = new CellStoreData(point, go.GetComponent<CellBehaviour>());
        cells.Add(sd);
        Debug.Log("Create cell at " + point);
    }

    public List<CellBehaviour> GetNaighbourCells(Vector2 point)
    {
        List<CellBehaviour> neighbours = new List<CellBehaviour>();
        for (int i = 0; i < neighbourVectors.Length; i++)
        {   
            Vector2 targetPoint = point + neighbourVectors[i];
            CellStoreData sd = cells.Find(x => x.point == targetPoint);
            if (sd!=null)
            {
                neighbours.Add(sd.cell);
            }
        }
        return neighbours;
    }
}
[System.Serializable]
public class CellStoreData
{
    public Vector2 point;
    public CellBehaviour cell;
    public CellStoreData(Vector2 point,CellBehaviour cell) 
    { 
        this.point = point;
        this.cell = cell;
    }
}
