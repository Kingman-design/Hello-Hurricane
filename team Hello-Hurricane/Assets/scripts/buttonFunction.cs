using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
   public void onStart()
    {
        UIManager.instance.stateDifficulty();
    }

    public void onQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void onEasy()
    {
        //GameManager.instance.SetToEasy();
        SceneManager.LoadScene("Prototype");
        Time.timeScale = 1.0f;
        
    }
    public void onMedium()
    {
        //GameManager.instance.SetToMedium();
        SceneManager.LoadScene("Prototype");
        Time.timeScale = 1.0f;


    }
    public void onHard()
    {
        //GameManager.instance.SetToHard();
        SceneManager.LoadScene("Prototype");
        Time.timeScale = 1.0f;

    }
    public void onBack() 
    {
        UIManager.instance.stateTitle();
    }

    public void onResume()
    {
        UIManager.instance.stateUnpause();
    }

    public void onRestart()   
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIManager.instance.stateUnpause();

    }

    public void onTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }


}
