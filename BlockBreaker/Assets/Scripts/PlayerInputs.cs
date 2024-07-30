using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    [SerializeField] Transform paddle;
    [SerializeField] Transform limitLeft, limitRight;
    Rigidbody2D ball;
    public float paddleSpeed = 5;
    public float ballSpeed = 5;
    Vector2 mousePos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
    // Start is called before the first frame update
    void Start()
    {
        ball = GameController.instance.ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.GetGameState().Equals(GameStates.Result))
            return;


        paddle.position = new Vector2(Mathf.Clamp(mousePos.x, limitLeft.position.x, limitRight.position.x), paddle.position.y);

        if (Input.GetButtonDown("Fire1"))
        {
            if (GameController.instance.GetGameState().Equals(GameStates.Ready))
            {
                ball.transform.SetParent(null);
                ball.GetComponent<Ball>().ActiveTrail(true);
                int direction = Random.Range(-1, 2);
                direction = direction == 0 ? -1 : direction;
                ball.velocity = new Vector2(ballSpeed * direction, ballSpeed);
            }
        }
    }
}
