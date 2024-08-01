using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnGenerator : MonoBehaviour
{
    public static ColumnGenerator instance;
    public GameObject brickPrefab;
    List<GameObject> columnsList = new List<GameObject>();
    public int rows;
    public int columns;
    Color[] colors = {Color.blue, Color.green, Color.magenta, Color.cyan, Color.white };
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        GenerateColumns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateColumns()
    {
        for (int column = 0; column < columns; column++)
        {
            GameObject temp = new GameObject();
            temp.transform.position = new Vector2(transform.position.x + (2.1f * column), transform.position.y);
            for (int row = 0; row < rows; row++)
            {
                Vector2 pos = new Vector2(temp.transform.position.x, transform.position.y + (-0.65f * row));
                GameObject brick = Instantiate(brickPrefab, pos, brickPrefab.transform.rotation);
                brick.GetComponent<SpriteRenderer>().color = colors[row];
                brick.transform.SetParent(temp.transform);
            }
            columnsList.Add(temp);
        }
        GameController.instance.SetBricksNumber(rows * columns);
    }
    public void ResetColumns()
    {
        foreach (var item in columnsList)
        {
            Destroy(item);
        }
        columnsList = new List<GameObject>();
    }
}
