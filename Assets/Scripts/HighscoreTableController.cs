using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighscoreTableController : MonoBehaviour
{
    public GameObject tableContainter;
    public HighscoreTableRow recordPrefab;
    private List<HighscoreTableRow> spawnedRows = new List<HighscoreTableRow>();

    private void Start() {
        LoadHighscoreTable();
    }

    public void LoadHighscoreTable()
    {
        foreach (var row in spawnedRows)
        {
            Destroy(row);
        }
        spawnedRows.Clear();
        
        int rank = 1;
        foreach(var scoreData in ScoreSavingManager.scoreDatas.OrderByDescending(e => e.Score) ) {
            var row = Instantiate(recordPrefab, tableContainter.transform);
            row.Show(scoreData, rank++);
            spawnedRows.Add(row);
        }
    }
}
