using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameVisible() { // Hoạt động khi trong tầm nhìn camera
        enabled = true;
    }

    private void OnBecameInvisible() { // Không trong tầm nhìn camera
        enabled = false;
    }

    private void OnEnable() {
        rigidbody.WakeUp();
    }

    private void OnDisable() {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
    private void FixedUpdate() { // Tốc độ khung hình luôn cố định không thay đổi, thường quan trọng cho đối tượng vật lý, có sự nhất quán trong vật lý. Update() tốc độ khung hình khác nhau
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; // Lực hấp dẫn

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (rigidbody.Raycast(direction)){
            direction = -direction;
        }
        if (rigidbody.Raycast(Vector2.down)){
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
}
