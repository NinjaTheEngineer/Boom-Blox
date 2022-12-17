using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {
    [SerializeField]
    private BallController ballPrefab;
    [SerializeField]
    private Transform hitMark;
    [SerializeField]
    private float defaultThrowSpeed = 25;
    [SerializeField]
    private float maxChargeTime = 3f;
    private float chargeTime;
    private int layerMask;
    private void Awake() {
        hitMark.gameObject.SetActive(false);
        layerMask = (1 << LayerMask.NameToLayer("Block"));
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            SetTarget();   
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && hitMark.gameObject.activeInHierarchy) {
            chargeTime = Time.time;
        }
        bool reachedChargeMaxTime = (chargeTime>0 && Time.time - chargeTime>=maxChargeTime);
        if(reachedChargeMaxTime || (!Input.GetKey(KeyCode.Space) && chargeTime>0)) {
            ShotBall();
            chargeTime = 0;
            hitMark.gameObject.SetActive(false);
        }
    }
    private void ShotBall() {
        string logId = "ShotBall";
        float throwSpeed = defaultThrowSpeed + (Time.time-chargeTime);
        Vector3 throwDirection = hitMark.position - Camera.main.transform.position;
        Debug.Log(logId + ":: throwSpeed="+throwSpeed+" throwDirection="+throwDirection+" chargeTime="+chargeTime);

        BallController ball = Instantiate(ballPrefab, Camera.main.transform.position, Quaternion.FromToRotation(Vector3.forward, throwDirection));
        ball.SetVelocity(throwDirection.normalized * throwSpeed);
        GameManager.BallShotEvent.Invoke();
    }
    private void SetTarget() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, layerMask)) {
            AddHitMark(hit);
        }
    }

    private void AddHitMark(RaycastHit hit) {
        hitMark.gameObject.SetActive(true);
        hitMark.localPosition = hit.point + (hit.normal * 0.01f);
        hitMark.localRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
    }
}
