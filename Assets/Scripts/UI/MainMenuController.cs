using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnQuitButtonClicked()
    {
        // 退出游戏，但在Unity编辑器里运行游戏时，退出方法是无效的
        Application.Quit();
        Debug.Log("Quitting the game...");
    }
}
