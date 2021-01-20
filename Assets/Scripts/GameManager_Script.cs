using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script staticGameManager;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public float invincibleTime = 0.0f;
    public AudioClip pauseAudio, startAudio, missAudio;

    private void Awake() {
        if(staticGameManager == null){
            staticGameManager = this;
        }

        StartCoroutine("StartGame");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            gamePaused = !gamePaused;
            if(gamePaused){
                PlayPauseMusic();
            }else{
                StopPauseMusic();
            }
        }

        if(invincibleTime > 0){
            invincibleTime -= Time.deltaTime;
        }

        if(PhantomMovement_Script.pacmanIsDead){
            PhantomMovement_Script.pacmanIsDead = false;
            PlayMissMusic();
        }
    }

    void PlayPauseMusic(){
        AudioSource source = GetComponent<AudioSource>();
        source.clip = pauseAudio;
        source.loop = true;
        source.Play();
    }

    void StopPauseMusic(){
        GetComponent<AudioSource>().Stop();
    }

    void PlayMissMusic(){
        AudioSource source = GetComponent<AudioSource>();
        source.clip = missAudio;
        source.Play();
    }

    IEnumerator StartGame(){
        yield return new WaitForSecondsRealtime(4.0f);
        gameStarted = true;
    }

    public void MakeInvincible(float numberOfSeconds){
        this.invincibleTime += numberOfSeconds;
    }

    public void RestartGame(){
       SceneManager.LoadScene("MainScene");
    }
}
