using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement_Script : MonoBehaviour
{
    public float speed = 6f;
    Vector2 destination = Vector2.zero;
    Rigidbody2D m_Rigibody;

    void Start()
    {
        destination = this.transform.position;
        m_Rigibody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (GameManager_Script.staticGameManager.gameStarted && !GameManager_Script.staticGameManager.gamePaused)
        {
            
            GetComponent<AudioSource>().volume = 0.2f;
            m_Rigibody.constraints = RigidbodyConstraints2D.None;
            m_Rigibody.constraints = RigidbodyConstraints2D.FreezeRotation;
            

            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPos);

            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);

            if (distanceToDestination < 2.0f)
            {
                if (Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + Vector2.up;
                }
                if (Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + Vector2.right;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + Vector2.left;
                }
                if (Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + Vector2.down;
                }
            }
            Vector2 direction = destination - (Vector2)this.transform.position;
            GetComponent<Animator>().SetFloat("DirX", direction.x);
            GetComponent<Animator>().SetFloat("DirY", direction.y);

        }else{
            GetComponent<AudioSource>().volume = 0.0f;
            m_Rigibody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    bool CanMoveTo(Vector2 direction)
    {
        Vector2 pacmanPosition = this.transform.position;

        //Raycast desde el objetivo que queremos ir hacia pacman
        RaycastHit2D hit = Physics2D.Linecast(pacmanPosition + direction, pacmanPosition);

        Debug.DrawLine(pacmanPosition, direction, Color.green, 2);

        return hit.collider == GetComponent<Collider2D>();
    }
}
