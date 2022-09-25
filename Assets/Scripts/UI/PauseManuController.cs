using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManuController : MonoBehaviour
{
    public GameObject pauseMenu;

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 当用户按下Esc键时，显示或退出暂停菜单
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        // 显示暂停菜单
        pauseMenu.SetActive(true);
        // 通过将timeScale设为0，所有与timeScale有关的行为都会暂停（比如Time.time不会再发生变化，Time.deltaTime会保持为0）
        Time.timeScale = 0f;
        // 暂停背景音乐的播放
        AudioManager.instance.PauseBGM();
        // 告诉levelManager游戏已暂停，便于其他的一些相关操作也能暂停
        LevelManager.instance.IsGameRunning = false;
    }
    
    void ResumeGame()
    {
        // 恢复游戏
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.instance.ResumeBGM();
        LevelManager.instance.IsGameRunning = true;
    }
    
    public void OnResumeButtonClicked()
    {
        ResumeGame();
    }

    public void OnMainMenuButtonClicked()
    {
        /*
         * 恢复timeScale为1
         * pauseMenu.ActiveSelf、BGM和IsGameRunning这些都不用恢复，
         * 因为从主菜单再重新加载回游戏场景时，这些都会重置为默认值
         */
        Time.timeScale = 1f;
        
        // 加载主菜单场景
        SceneManager.LoadScene(mainMenuScene);
    }
}
