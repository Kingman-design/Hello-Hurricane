using UnityEngine;
using System.Collections;

public class PowerUpBase : MonoBehaviour
{
    public static PowerUpBase instance;
    public enum powerType { SuperJump, Invincibility }

    private void Awake()
    {
        instance = this;
    }


    public void PowerUse(NewMonoBehaviourScript player, powerType Type, float duration = 5f)
    {
        

        switch (Type)
        {
            case powerType.SuperJump:
                //Power Super Jump
                StartCoroutine(SuperJump(player, duration));
                break;
            case powerType.Invincibility:
                //Power invincibility
                break;
        }

    }


    IEnumerator SuperJump(NewMonoBehaviourScript player, float duration)
    {
        int oldStat = player.GetJumpSpeed();
        player.SetJumpSpeed(oldStat * 2);
        yield return new WaitForSeconds(duration);
        player.SetJumpSpeed(oldStat);
    }










}
