using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject menuActive;     //set the menu active or inactive
    [SerializeField] GameObject menuTitle;      //title menu
    [SerializeField] GameObject menuDifficulty; //difficulty select
    [SerializeField] GameObject menuPause;      //pause state
    //[SerializeField] GameObject menuWin;      //currently no win condition
    [SerializeField] GameObject menuLose;       //lose state

    public bool isPaused;
    float timeScaleOrig;
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
