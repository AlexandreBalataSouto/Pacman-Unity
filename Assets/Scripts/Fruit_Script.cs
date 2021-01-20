using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){

            UIManager_Script.staticUIManager.ScorePoints(200);

            GameManager_Script.staticGameManager.MakeInvincible(10.0f);

            Destroy(this.gameObject);
        }
    }
}
