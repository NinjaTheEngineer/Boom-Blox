using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    [SerializeField] 
    private float autoDestroyTime = 2f;
    [SerializeField]
    private Rigidbody ballRb;
    private float spawnTime;
    
    private void Awake() {
        spawnTime = Time.time;
    }

    private void Update() {
        if(Time.time - spawnTime >= autoDestroyTime) {
            Destroy(gameObject);
        }
    }
    public void SetVelocity(Vector3 velocity) => ballRb.velocity = velocity;
}
