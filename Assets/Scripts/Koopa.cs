using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 20f;
    private bool shelled;
    private bool pushed;
    private void OnCollisionEnter2D(Collision2D other) {
        if ( !shelled && other.gameObject.CompareTag("Player")){
            Player player = other.gameObject.GetComponent<Player>();

            if (other.transform.DotTest(transform, Vector2.down)){
                EnterShell();
            }else{
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (shelled && other.CompareTag("Player")){
            if (!pushed){
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }else{
                Player player = other.GetComponent<Player>();
                player.Hit();
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")){
            Hit();
        }
    }

    private void EnterShell(){
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimationSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
    private void PushShell(Vector2 direction){
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit(){
        GetComponent<AnimationSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible() {
        if (pushed){
            Destroy(gameObject);
        }
    }
}
