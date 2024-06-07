using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextscreen : MonoBehaviour
{
    private float levelLoaDelat = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            StartCoroutine(LoadNextLevel());

        }
    }
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoaDelat);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}