using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject menuActive;     //set the menu active or inactive
    [SerializeField] GameObject menuTitle;      //title menu
    [SerializeField] GameObject menuDifficulty; //difficulty select
    [SerializeField] GameObject menuPause;      //pause state
    [SerializeField] GameObject menuLose;       //lose state
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameUI;         //in game UI
    [SerializeField] GameObject playerUI;       //hud      

    public bool isPaused;
    float timeScaleOrig;
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;

        if (SceneManager.GetActiveScene().name == "TitleScene")         //ensure that only the desired menus active 
        {                                                               //for the appropriate scenes
            titleUI.SetActive(true);                                    //when loading a new scene in prototype
        }                                                               //it wont get stuck on the title screen
        else if (SceneManager.GetActiveScene().name == "Prototype")
        {
            gameUI.SetActive(true);
            playerUI.SetActive(true);
        }

    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
    }
    public void statePause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void stateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        if(SceneManager.GetActiveScene().name == "Prototype")       //trying to fix the timing issue
        {                                                           //on onTitle;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        menuActive.SetActive(false);
        menuActive = null;
    }
    public void stateLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }
    public void stateTitle()
    {
        menuDifficulty.SetActive(false);
        menuActive = menuTitle;
        menuActive.SetActive(true);
    }
    public void stateDifficulty()
    {
        menuTitle.SetActive(false);
        menuActive = menuDifficulty;
        menuActive.SetActive(true);
    }
}
