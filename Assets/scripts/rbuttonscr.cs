using UnityEngine;
using UnityEngine.SceneManagement;
//only for the test level, not ingame

public class rbuttonscr : MonoBehaviour
{
    public void ResetScene()
    {
        //Debug.Log("press");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("start");
    }
}
