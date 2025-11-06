using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
   public void onStart()
    {
        gameManager.instance.stateDifficulty();
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
        gameManager.instance.stateUnpause(); 
        SceneManager.LoadScene("Prototype");
        
    }
    //public void onMedium()
    //{
       
    //    gameManager.instance.stateUnpause();
    //}
    //public void onHard()
    //{
    //    gameManager.instance.stateUnpause();
    //}
    public void onBack() 
    {
        gameManager.instance.stateTitle();
    }

    public void onResume()
    {
        gameManager.instance.stateUnpause();
    }

    public void onRestart()
    {

    }

    public void onTitle()
    {
        SceneManager.LoadScene("Kathryn-Scene");
    }


    //public void loadLevel(int lvl)
    //{
    //    SceneManager.LoadScene(lvl);
    //    //gameManager.instance.stateUnpause();

    //}
}
