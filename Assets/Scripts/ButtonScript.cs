using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    public void ExitMenu()
    {
        SceneManager.LoadScene(0);
        GetComponent<SoundScript>().Background();
    }
    public void PlayGame()
    {
        
        _mainMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
