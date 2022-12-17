using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
    [SerializeField]
    private float minStandingY = 1f;
    bool fallen = false;
    private void Start() {
        StartCoroutine(CheckIfFallen());
    }
    private IEnumerator CheckIfFallen() {
        while(!fallen) {
            yield return new WaitForSecondsRealtime(0.5f);
            if(transform.position.y <= minStandingY) {
                fallen = true;
                GameManager.BlockFallenEvent.Invoke();
            }
        }
    }
}
