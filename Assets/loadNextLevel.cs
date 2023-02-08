using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class loadNextLevel : MonoBehaviour
{

    public GameObject levelComplete;
    public GameObject allEnemys;
    public TextMeshProUGUI enemyCount;

    bool _allowInvoke = true;
    private void Update()
    {
        Transform[] a = allEnemys.GetComponentsInChildren<Transform>();
        if (a == null) return;
        enemyCount.text = "Enemys Left : " + a.Length / 5;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _allowInvoke)
        {
            _allowInvoke = false;
            levelComplete.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
            Invoke("LoadNextLevel", 2f);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
