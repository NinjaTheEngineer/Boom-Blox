using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour {
    public UIManager UIManager;
    public delegate void BlockFallen();
    public delegate void BallShot();
    public static BlockFallen BlockFallenEvent;
    public static BallShot BallShotEvent;
    private int amountOfFallenBlocks = 0;
    private int amountOfBallsShot = 0;
    private void Start() {
        BlockFallenEvent+=AddBlockFallen;
        BallShotEvent+=AddBallShot;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }

    private void AddBlockFallen() {
        amountOfFallenBlocks++;
        UIManager.UpdateFallenBlocks(amountOfFallenBlocks);
    }
    private void AddBallShot() {
        amountOfBallsShot++;
        UIManager.UpdateBallsShot(amountOfBallsShot);
    }
    private void OnDestroy() {
        BlockFallenEvent-=AddBlockFallen;
        BallShotEvent-=AddBallShot;
    }
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
