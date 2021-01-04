using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    Vector3 targetPos;

    public GameObject followTarget;

    private void Update() {
        //targetPos = MoveCamera;
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        targetPos = new Vector3(followTarget.transform.position.x, 10f, followTarget.transform.position.z - 3f);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        /*float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);*/
    }

    public void MoveCamera(Vector3 movePoint) {
        //targetPos = movePoint;
    }
}
