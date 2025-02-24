using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject trail;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        trail = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Brick"))
        {
            GameController.instance.PlaySoundEffect(SoundTypes.Hit);
        } else
        {
            rb.velocity *= 1.015f;
        }
    }
    public void ActiveTrail(bool active)
    {
        trail.SetActive(active);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        GameController.instance.SetGameState(GameStates.Less);
    }
}
