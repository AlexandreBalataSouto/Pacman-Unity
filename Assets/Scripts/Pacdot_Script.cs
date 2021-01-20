using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if(otherCollider.tag == "Player"){
            UIManager_Script.staticUIManager.ScorePoints(100);
            Destroy(this.gameObject);
        }
    }
}
