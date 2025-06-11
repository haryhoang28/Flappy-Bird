using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]

    [Tooltip("The sprite renderer of the player. Used to change the sprite based on the player's state.")]
    private SpriteRenderer spriteRenderer;
    [Tooltip("The sprites to use for the player. The first sprite is used for the default state, and the second sprite is used when the player jumps.")]
    public Sprite[] sprites;
    [Tooltip("The index of the current sprite being displayed. Used to switch between sprites based on the player's state.")]
    private int currentSpriteIndex = 0; 
    [Tooltip("Movement direction of the player.")]
    private Vector3 direction;
    [Tooltip("The gravity force player has to take.")]
    public float gravity = -9.81f;
    [Tooltip("The strength to lift up of the player movement.")]
    public float strength = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimatedSprites), 0.15f, 0.15f); // Call AnimatedSprites every 0.5 seconds
    }

    private void OnEnable()
    {
        Vector3 position = transform.position; // Get the current position of the player
        position.y = 0f; // Set the y position to 0
        transform.position = position; // Update the player's position to the new position
        direction = Vector3.zero; // Reset the direction to zero when the player is enabled
    } 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // Check for space key or mouse button press
        {
            direction = Vector3.up * strength; // Jump force
        }

        if (Input.touchCount > 0) // Check if there is at least one touch input
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // Check if the touch just started
            {
                direction = Vector3.up * strength; // Jump force
            }
        }
        direction.y += gravity * Time.deltaTime; // Apply gravity to the direction
        transform.position += direction * Time.deltaTime;
    }

    private void AnimatedSprites()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex >= sprites.Length)
        {
            currentSpriteIndex = 0; // Reset to the first sprite if we reach the end
        }
        spriteRenderer.sprite = sprites[currentSpriteIndex]; // Update the sprite renderer with the current sprite
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle") // Check if the player collides with a obstacle (pipe, ground)
        {
            FindObjectOfType<GameManager>().GameOver(); // Call GameOver method from GameManager
        }
        else if (other.gameObject.tag == "Scoring") // Check if the player collides with a score object
        {
            FindObjectOfType<GameManager>().IncreaseScore(); // Call IncreaseScore method from GameManager
            
        }
    }
}
