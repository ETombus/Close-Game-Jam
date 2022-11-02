using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuControlls : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject helpPanel;

    public GameObject menuFirstButton, helpFirstButton, creditsFirstButton;

    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false);
        creditsPanel.SetActive(false);
        SetFirstSelected(menuFirstButton);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleHelp()
    {
        if (helpPanel.activeSelf)
        {
            helpPanel.SetActive(false);
            SetFirstSelected(menuFirstButton);
        }
        else
        {
            helpPanel.SetActive(true);
            SetFirstSelected(helpFirstButton);
        }
        creditsPanel.SetActive(false);
    }
    

    public void ToggleCredits()
    {
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
            SetFirstSelected(menuFirstButton);
        }
        else
        {
            creditsPanel.SetActive(true);
            SetFirstSelected(creditsFirstButton);
        }
        helpPanel.SetActive(false);
    }

    void SetFirstSelected(GameObject selectedButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
