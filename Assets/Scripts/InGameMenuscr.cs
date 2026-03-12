using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject winMenu;

    private void Start()
    {
        optionsPanel.SetActive(false);
        winMenu.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool isActive = optionsPanel.activeSelf;
            optionsPanel.SetActive(!isActive);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void OpenWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void CloseWinMenu()
    {
        winMenu.SetActive(false);
    }
}