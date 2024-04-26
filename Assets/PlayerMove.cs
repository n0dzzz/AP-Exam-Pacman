using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;
    public Tilemap elevationTilemap;
    public TileBase pointBall;
    public TileBase bigPointBall;
    public TMP_Text scoreText;
    public GameObject WinText;
    public float moveSpeed = 5f; // Adjust this to control the speed of movement
    public SpriteRenderer playerSpriteRenderer;
    public Sprite[] sprites;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float playerScore;
    private bool isPowered;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SwapSpritesCoroutine());
    }

    // Update is called once per frame

    void Update()
    {
        if(isPowered)
            playerSpriteRenderer.color = Color.red;
        else
            playerSpriteRenderer.color = Color.white;
            
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

    IEnumerator PowerUp()
    {
        isPowered = true;
        yield return new WaitForSeconds(5f);
        isPowered = false;
    }

    public bool IsPowered()
    {
        return isPowered;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3Int closestCell = elevationTilemap.WorldToCell(gameObject.transform.position);
        if(elevationTilemap.HasTile(closestCell)) // Eats ball
        {    
            if(elevationTilemap.GetTile(closestCell) == bigPointBall)
                StartCoroutine(PowerUp());

            elevationTilemap.SetTile(closestCell, null);
            CheckForTiles();
            playerScore += 10;
            scoreText.text = "Score: " + playerScore;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isPowered && collision.gameObject.tag == "Ghost")
        {
            collision.gameObject.SetActive(false);   
            playerScore += 500;
            Debug.Log("hit ghost");         
        }
    }

    void CheckForTiles()
    {
        if(!elevationTilemap.ContainsTile(pointBall))
            WinText.SetActive(true);
    }
}
