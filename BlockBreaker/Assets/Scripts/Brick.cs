using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.instance.AddScore(points);
        GameController.instance.SubtractBricksNumber();
        collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 1.03f;
        Destroy(gameObject);
    }
}
