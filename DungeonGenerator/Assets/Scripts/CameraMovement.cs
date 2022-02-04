using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Transform target;
    public float smoothing;

    void FixedUpdate() {
        if(transform.position != target.position) {
            Vector3 target_position = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target_position, smoothing);
        }
    }
}