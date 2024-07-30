using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Ready, Play, Less, Result
}
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public UnityEvent OnLost;
    public UnityEvent OnWin;

    public TMP_Text chancesTxt;
    public TMP_Text scoreTxt;
    public GameObject ball;
    public GameObject paddle;

    [SerializeField] int chances, score, bricksNumber;
    GameStates state;
    Vector2 ballInitialPos;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ballInitialPos = ball.transform.localPosition;
        chancesTxt.text = "Lives "+chances;
        scoreTxt.text = "Score " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameStates GetGameState()
    {
        return state;
    }
    public void SetGameState(GameStates newState)
    {
        state = newState;
        switch (state)
        {
            case GameStates.Ready:
                break;
            case GameStates.Play:
                break;
            case GameStates.Less:
                chances--;
                chancesTxt.text = "Lives " + chances;
                if (chances <= 0)
                {
                    OnLost.Invoke();
                    SetGameState(GameStates.Result);
                } else
                {
                    ResetBall();
                }
                break;
            case GameStates.Result:
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
        }
    }
    public void ResetBall()
    {
        ball.transform.SetParent(paddle.transform);
        ball.transform.localPosition = ballInitialPos;
        ball.SetActive(true);
        ball.GetComponent<Ball>().ActiveTrail(false);
        SetGameState(GameStates.Ready);
    }
    public void AddScore(int points)
    {
        score += points;
        scoreTxt.text = "Score " + score;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetBricksNumber(int bricks)
    {
        bricksNumber = bricks;
    }
    public void SubtractBricksNumber()
    {
        bricksNumber--;
        if(bricksNumber <= 0)
        {
            OnWin.Invoke();
            SetGameState(GameStates.Result);
        }
    }
}
