using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float transitionDelay = 0.25f;
    [SerializeField] private Color highlightColor = Color.yellow;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isTransitioning = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(TransitionToNextLevel());
        }
    }

    private IEnumerator TransitionToNextLevel()
    {
        isTransitioning = true;

        // Change door color to indicate activation
        spriteRenderer.color = highlightColor;

        // Wait for the specified delay
        yield return new WaitForSeconds(transitionDelay);

        // Get current scene index and load the next one
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there's a next scene, otherwise loop back to first scene
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to first scene
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            // Reset door color when player leaves
            spriteRenderer.color = originalColor;
        }
    }
}