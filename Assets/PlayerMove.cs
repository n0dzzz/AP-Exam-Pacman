using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public Tilemap elevationTilemap;
    public TMP_Text scoreText;
    public float moveSpeed = 5f; // Adjust this to control the speed of movement
    public SpriteRenderer playerSpriteRenderer;
    public Sprite[] sprites;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float playerScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SwapSpritesCoroutine());
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            movementDirection = Vector2.up; 
            playerSpriteRenderer.sprite = sprites[0];
        }
        
        if(Input.GetKey(KeyCode.A))
        {    
            movementDirection = Vector2.left; 
            playerSpriteRenderer.sprite = sprites[1];
        }

        if(Input.GetKey(KeyCode.S))
        {   
            movementDirection = Vector2.down; 
            playerSpriteRenderer.sprite = sprites[2];
        }

        if(Input.GetKey(KeyCode.D))
        {    
            movementDirection = Vector2.right; 
            playerSpriteRenderer.sprite = sprites[3];
        }  

        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    IEnumerator SwapSpritesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            // Check the current sprite and swap to the other one
            if (playerSpriteRenderer.sprite == sprites[4])
            {
                if(movementDirection == Vector2.up)
                    playerSpriteRenderer.sprite = sprites[0];
                if(movementDirection == Vector2.left)
                    playerSpriteRenderer.sprite = sprites[1];
                if(movementDirection == Vector2.down)
                    playerSpriteRenderer.sprite = sprites[2];
                if(movementDirection == Vector2.right)
                    playerSpriteRenderer.sprite = sprites[3];    
            }
            else
            {
                playerSpriteRenderer.sprite = sprites[4];
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3Int closestCell = elevationTilemap.WorldToCell(gameObject.transform.position);
        if(elevationTilemap.HasTile(closestCell)) // Eats ball
        {    
            elevationTilemap.SetTile(closestCell, null);
            playerScore += 10;
            scoreText.text = "Score: " + playerScore;
        }
    }
}
