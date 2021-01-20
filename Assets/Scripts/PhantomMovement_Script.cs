using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMovement_Script : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed = 6f;
    public bool waitHome = false;
    public static bool pacmanIsDead = false;

    private void Update()
    {
        if (GameManager_Script.staticGameManager.invincibleTime > 0)
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", false);
        }
    }

    private void FixedUpdate()
    {

        if (GameManager_Script.staticGameManager.gameStarted && !GameManager_Script.staticGameManager.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.1f;

            if (!waitHome)
            {
                float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position, (Vector2)waypoints[currentWaypoint].position);

                if (distanceToWaypoint < 0.1f)
                {
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                    Vector2 newDirection = waypoints[currentWaypoint].position - this.transform.position; //Punto de origen menos punto de destino

                    GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                    GetComponent<Animator>().SetFloat("DirY", newDirection.y);
                }
                else
                {
                    Vector2 newPos = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

                    GetComponent<Rigidbody2D>().MovePosition(newPos);
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameManager_Script.staticGameManager.invincibleTime <= 0)
            {
                GameManager_Script.staticGameManager.gameStarted = false;

                other.GetComponent<Animator>().SetBool("isDead",true);
                
                pacmanIsDead = true;

                StartCoroutine("RestartGame",other);
            }else
            {
                UIManager_Script.staticUIManager.ScorePoints(500);
                GameObject home = GameObject.Find("Ghost Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                this.waitHome = true;
                StartCoroutine("AwakeFromHome");
            }
        }
    }

    IEnumerator RestartGame(Collider2D other){
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(other.gameObject);
        GameManager_Script.staticGameManager.RestartGame();
    }
    IEnumerator AwakeFromHome(){
        yield return new WaitForSecondsRealtime(4.0f);
        this.waitHome = false;
        this.speed *= 1.2f;
    }
}
