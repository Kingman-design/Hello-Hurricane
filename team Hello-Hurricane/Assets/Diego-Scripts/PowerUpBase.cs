using UnityEngine;
using System.Collections;
using System;

public class PowerUpBase : MonoBehaviour
{
    public static PowerUpBase instance;


    [SerializeField] Renderer model;
    Color colorOrigin;

    public enum powerType { SuperJump, Invincibility, Explosion }

    private void Awake()
    {
        instance = this;
        colorOrigin = model.material.color;
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
                StartCoroutine(PowerInvincibility(player, duration));
                break;
            case powerType.Explosion:
                //Power Explosion
                break;
        }

    }


    IEnumerator SuperJump(NewMonoBehaviourScript player, float duration)
    {
        int oldStat = player.GetJumpSpeed();
        player.SetJumpSpeed(oldStat * 2);
        
        model.material.color = Color.green;
        yield return new WaitForSeconds(duration);
        player.SetJumpSpeed(oldStat);
        model.material.color = colorOrigin;
    }


    IEnumerator PowerInvincibility(NewMonoBehaviourScript player, float duration)
    {
        model.material.color = Color.blue;
        yield return new WaitForSeconds(duration);
        model.material.color = colorOrigin;
    }


}
