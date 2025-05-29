using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the scene when player touches lava
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}