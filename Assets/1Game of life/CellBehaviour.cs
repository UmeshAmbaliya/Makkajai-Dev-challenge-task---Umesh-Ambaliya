using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    [SerializeField] Vector2 currentPoint;
    [SerializeField] bool isDead;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] int totalLiveNeighbours = 0;
    [SerializeField] int totalDeadNeighbours = 0;
    public void Init(Vector2 point)
    {
        currentPoint = point;
        sr.color = Color.green;
    }

    public void CheckNeighBours()
    {
        List<CellBehaviour> neighBours = GameOfLifeHandler.Instance.GetNaighbourCells(currentPoint);
        totalLiveNeighbours = 0;
        for (int i = 0; i < neighBours.Count; i++)
        {
            if (!neighBours[i].isDead)
            {
                totalLiveNeighbours++;
            }
        }
        if (totalLiveNeighbours > 3 || totalLiveNeighbours < 2)
        {
            isDead = true;
            sr.color = Color.red;
        } 
    }

    public void MakeAliveDead()
    {
        List<CellBehaviour> neighBours = GameOfLifeHandler.Instance.GetNaighbourCells(currentPoint);
        if (isDead)
        {
            totalDeadNeighbours = 0;
            for (int i = 0; i < neighBours.Count; i++)
            {
                if (neighBours[i].isDead)
                {
                    totalDeadNeighbours++;
                }
            }
            if (totalDeadNeighbours == 3)
            {
                isDead = false;
                sr.color = Color.green;
            }
        }
    }
}
