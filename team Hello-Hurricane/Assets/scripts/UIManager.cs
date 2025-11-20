using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //menus
    [SerializeField] GameObject menuActive;     //set the menu active or inactive
    [SerializeField] GameObject menuTitle;      //title menu
    [SerializeField] GameObject menuDifficulty; //difficulty select
    [SerializeField] GameObject menuPause;      //pause state
    [SerializeField] GameObject menuLose;       //lose state
    //scene specific ui containters
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameUI;         //in game UI
    [SerializeField] GameObject playerUI;       //hud      
    //player ui images
    [SerializeField] Image hit1, hit2, hit3;
    [SerializeField] Image jump, invincible, fly, explosion;

    public bool isPaused;
    float timeScaleOrig;

    public NewMonoBehaviourScript playerScript;   //new
    public TMP_Text ScoreText;

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
        playerScript = GetComponent<NewMonoBehaviourScript>();
    }
    void Start()
    {
        Time.timeScale = 1.0f;
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
    //public void UpdateHearts()
    //{
    //    int hp = playerScript.GetHP();
        
    //}
}
