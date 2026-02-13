using UnityEngine;
using UnityEngine.SceneManagement;

public class rbuttonscr : MonoBehaviour
{
    public void ResetScene()
    {
        Debug.Log("press");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("start");
    }
}
