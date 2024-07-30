using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rows; i++)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + (-0.7f * i));
            Instantiate(brickPrefab, pos, brickPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
