using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject playerObject;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public GameObject lostText;
    
    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Vector2.left;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchDirectionCoroutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movementDirection == Vector2.left)
            spriteRenderer.flipX = true;
        if(movementDirection == Vector2.right)
            spriteRenderer.flipX = false;

        rb.MovePosition(rb.position + movementDirection * 2f * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Player" && !PlayerMove.Instance.IsPowered())
        {
            playerObject.SetActive(false);
            lostText.SetActive(true);
        }
    }

    IEnumerator SwitchDirectionCoroutine()
    {
        while(true)
        {
            float randomTime = Random.Range(1f, 2f);

            yield return new WaitForSeconds(randomTime);
        
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    movementDirection = Vector2.up;
                break;
                case 1:
                    movementDirection = Vector2.left;
                break;
                case 2:
                    movementDirection = Vector2.down;
                break;
                case 3:
                    movementDirection = Vector2.right;
                break;
            }
        }
    }
}

