using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float scrollSpeed = 10f;
    [SerializeField]
    private float rotationSpeedX = 3f;
    [SerializeField]
    private float rotationSpeedY = 0.5f;
    [SerializeField]
    private float maxRotationY = 90f;
    [SerializeField]
    private float minRotationY = -15f;
    private Vector3 priorViewportPosition = Vector3.zero;
    private Vector2 currentRotation = Vector2.zero;
    private Quaternion cameraRotation;
    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            SavePriorViewportPosition();
        }
        if(Input.GetMouseButton(1)) {    
            CalculateCameraRotation();
            ApplyCameraRotation();
        }
    }
    private void ApplyCameraRotation() {
        transform.localRotation = cameraRotation;
        SavePriorViewportPosition();
    }
    private void CalculateCameraRotation() {
        Vector3 rotationDirection = priorViewportPosition - mainCamera.ScreenToViewportPoint(Input.mousePosition);
        currentRotation.x += rotationDirection.y * (180 * rotationSpeedY);
        currentRotation.y += rotationDirection.x * (180 * rotationSpeedX);
        currentRotation = GetBoundedRotation(currentRotation);
        cameraRotation = Quaternion.Euler(
                                    Mathf.Lerp(transform.localRotation.x, currentRotation.x, rotationSpeedX),
                                    Mathf.Lerp(transform.localRotation.y, currentRotation.y, rotationSpeedY),
                                    0); 
    }

    private Vector2 GetBoundedRotation(Vector2 rotation) {
        if(rotation.x <= minRotationY) {
            rotation.x = minRotationY;
        } else if(rotation.x >= maxRotationY) {
            rotation.x = maxRotationY;
        }
        return rotation;
    }

    private void SavePriorViewportPosition() {
        priorViewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
    }


}
