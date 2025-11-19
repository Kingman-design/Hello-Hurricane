using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] GameObject hit1, hit2, hit3;
    [SerializeField] GameObject jump,invincible,fly,explosion;
   
    public NewMonoBehaviourScript PlayerScript;
    public PowerUpBase PlayerPowerUp;
    public GameManager Score;
    public TMP_Text ScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerScript = GetComponent<NewMonoBehaviourScript>();
        PlayerPowerUp = GetComponent<PowerUpBase>();
        Score = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
        UpdateScore();
    }

    void UpdateHearts()
    {
        int hp = PlayerScript.GetHP();
        hit1.SetActive(hp >= 2);
        hit2.SetActive(hp >= 1);
    }

    //void UpdatePowerUp()
    //{

    //}

    void UpdateScore()
    {
        ScoreText.text = Score.GetScoreText();
    }
}
