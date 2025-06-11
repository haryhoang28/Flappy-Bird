using UnityEngine;
using UnityEngine.Rendering;

public class Background : MonoBehaviour
{
    [Header("Background Settings")]
    [Tooltip("")]
    private MeshRenderer meshRenderer;
    [Tooltip("The speed of the background animation.")]
    public float animationSpeed = 0.5f; 
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

}
