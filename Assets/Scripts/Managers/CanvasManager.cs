using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject pausePanel;
    bool gamePaused = false;

    // Start is called before the first frame update
    public void LoadGame()
    {
        PersistentSaveObject[] saveObjectList = GameObject.FindObjectsOfType<PersistentSaveObject>();

        foreach(PersistentSaveObject p in saveObjectList)
        {
            p.LoadGameComplete();
        }

        GameManager.SaveManager.Load(Application.persistentDataPath + "/SaveGame.xml");
    }

    public void SaveGame()
    {
        PersistentSaveObject[] saveObjectList = GameObject.FindObjectsOfType<PersistentSaveObject>();

        foreach (PersistentSaveObject p in saveObjectList)
        {
            p.SaveGamePrepare();
        }

        GameManager.SaveManager.Save(Application.persistentDataPath + "/SaveGame.xml");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        gamePaused = !gamePaused;
        pausePanel.SetActive(gamePaused);

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
