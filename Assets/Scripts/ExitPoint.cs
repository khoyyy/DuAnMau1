using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    public float levelLoadDelay = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    // Couroutine : là một hàm chạy độc lập với các hàm khác 
    // có thể chạy trong nền, có thể dùng lại và tiếp tục sau đó 
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 ); 
    }
}
