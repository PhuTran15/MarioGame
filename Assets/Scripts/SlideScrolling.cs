using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate() {
        Vector3 cameraPosition = transform.position;
        // cameraPosition.x = player.position.x; // Người chơi có thể quay lại
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // Người chơi không thể quay lại
        transform.position = cameraPosition;
    }
}
