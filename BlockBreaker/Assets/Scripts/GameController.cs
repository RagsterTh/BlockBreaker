using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.RestService;

public enum GameStates
{
    Ready, Play, Less, Result
}
public enum SoundTypes
{
    Hit, LaunchBall, DestroyBrick, Select, GameOver, LostBall, Win
}
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public UnityEvent OnLost;
    public UnityEvent OnWin;
    public UnityEvent OnLostBall;

    public TMP_Text chancesTxt;
    public TMP_Text scoreTxt;
    public GameObject ball;
    public GameObject paddle;

    AudioSource soundEffectSource;
    [SerializeField] SoundEffects[] soundEffects;
    public static PlayerData playerData;
    [SerializeField] int chances, score, bricksNumber;
    GameStates state;
    Vector2 ballInitialPos;
    Dictionary<SoundTypes, AudioClip> soundEffectsRegister = new Dictionary<SoundTypes, AudioClip>();
    [System.Serializable]
    public class PlayerData
    {
        public int score;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ballInitialPos = ball.transform.localPosition;
        chancesTxt.text = "Lives "+chances;
        scoreTxt.text = "Score " + score;
        OnWin.AddListener(delegate
        {
            PlaySoundEffect(SoundTypes.Win);
            SaveToJson();
        });
        OnLost.AddListener(delegate 
        {
            PlaySoundEffect(SoundTypes.GameOver);
        });
        OnLostBall.AddListener(delegate
        {
            PlaySoundEffect(SoundTypes.LostBall);
        });
        foreach (var item in soundEffects)
        {
            soundEffectsRegister.Add(item.soundType, item.soundClip);
        }
        soundEffectSource = GetComponent<AudioSource>();
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
                    OnLostBall.Invoke();
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
    public void SaveToJson()
    {
        GameController.playerData = new PlayerData();
        GameController.playerData.score = score;
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        string playerData = JsonUtility.ToJson(GameController.playerData);
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, playerData);
    }

    #region Sound Effect

    [System.Serializable]
    public struct SoundEffects
    {
        public SoundTypes soundType;
        public AudioClip soundClip;
    }
    public void PlaySoundEffect(SoundTypes soundType)
    {
        soundEffectSource.PlayOneShot(soundEffectsRegister[soundType]);
    }
    #endregion
}
