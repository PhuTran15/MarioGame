using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            Player player = other.gameObject.GetComponent<Player>();

            if (other.transform.DotTest(transform, Vector2.down)){
                Flatten();
            }else{
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")){
            Hit();
        }
    }

    private void Flatten(){
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimationSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit(){
        GetComponent<AnimationSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
