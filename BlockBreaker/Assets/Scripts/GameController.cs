using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Ready, Play
}
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject ball;
    GameStates state;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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
        }
    }

}
