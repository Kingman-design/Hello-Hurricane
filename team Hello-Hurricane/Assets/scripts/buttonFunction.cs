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
        SceneManager.LoadScene("Prototype");
        
    }
    //public void onMedium()
    //{
       
    //    UIManager.instance.stateUnpause();
    //}
    //public void onHard()
    //{
    //    UIManager.instance.stateUnpause();
    //}
    public void onBack() 
    {
        UIManager.instance.stateTitle();
    }

    public void onResume()
    {
        UIManager.instance.stateUnpause();
    }

    public void onRestart()     //i know this is not great but i'm tired
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIManager.instance.stateUnpause();
    }

    //public void onTitle()
    //{
    //    SceneManager.LoadScene("Kathryn-Scene");
    //}


    //public void loadLevel(int lvl)
    //{
    //    SceneManager.LoadScene(lvl);
    //    //UIManager.instance.stateUnpause();

    //}
}
