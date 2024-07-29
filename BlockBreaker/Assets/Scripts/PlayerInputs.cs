using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    [SerializeField] Transform paddle;

    public float speed = 5;
    Vector2 mousePos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseOver()
    {
        paddle.position = new Vector2(mousePos.x, paddle.position.y);
    }
}
