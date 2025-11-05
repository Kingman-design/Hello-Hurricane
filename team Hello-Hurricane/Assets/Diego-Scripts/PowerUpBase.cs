using UnityEngine;
using System.Collections;

public class PowerUpBase : MonoBehaviour
{
    public static PowerUpBase instance;

    private void Awake()
    {
        instance = this;
    }


    public void PowerUse(NewMonoBehaviourScript player, int powerType, float duration = 5f)
    {
        

        switch (powerType)
        {
            case 1:
                //Power high jump
                StartCoroutine(HighJump(player, duration));
                break;
            case 2:
                //Power invincibility
                break;
        }

    }


    IEnumerator HighJump(NewMonoBehaviourScript player, float duration)
    {
        int oldStat = player.GetJumpSpeed();
        player.SetJumpSpeed(oldStat * 2);
        yield return new WaitForSeconds(duration);
        player.SetJumpSpeed(oldStat);
    }










}
