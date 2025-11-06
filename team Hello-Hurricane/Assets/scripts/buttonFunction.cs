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

    public void onRestart()     //i know this is not great but i'm tired
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.stateUnpause();
    }

    //public void onTitle()
    //{
    //    SceneManager.LoadScene("Kathryn-Scene");
    //}


    //public void loadLevel(int lvl)
    //{
    //    SceneManager.LoadScene(lvl);
    //    //gameManager.instance.stateUnpause();

    //}
}
