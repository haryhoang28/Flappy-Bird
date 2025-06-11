using UnityEngine;

public class Pipes : MonoBehaviour
{
    [Header("Pipe Settings")]

    [Tooltip("Global speed for all pipes.")]
    public static float globalSpeed = 5f;
    [Tooltip("The left egde of the screen in order to deactivate the pipes")]
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f; // Adjust the left edge based on camera view
    }
    private void Update()
    {
        transform.position += Vector3.left * globalSpeed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            gameObject.SetActive(false); // Deactivate the pipe when it goes off-screen
        }
    }

}
