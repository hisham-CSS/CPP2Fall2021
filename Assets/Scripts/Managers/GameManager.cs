using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // C# property to retrieve save/load manager
    public static LoadSaveManager SaveManager
    {
        get
        {
            if (!saveManager)
                saveManager = Instance.GetComponent<LoadSaveManager>();

            return saveManager;
        }
    }

    private static LoadSaveManager saveManager = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
                SceneManager.LoadScene("MainMenu");
            else
                SceneManager.LoadScene("SampleScene");
        }
    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void GoToEndScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
