using UnityEngine;
using System.Collections;
using System;

public class PowerUpBase : MonoBehaviour
{
    public static PowerUpBase instance;
    public bool Invincibility;


    [SerializeField] Renderer model;
    [SerializeField] GameObject Destroyer;
    
    Color colorOrigin;

    public enum powerType { SuperJump, Invincibility, Explosion, Flight }

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

                //show UI icon
                UIManager.instance.ShowUI_Icon(Type);
                break;
            case powerType.Invincibility:
                //Power invincibility
                StartCoroutine(PowerInvincibility(player, duration));

                //show UI icon
                UIManager.instance.ShowUI_Icon(Type);
                break;
            case powerType.Explosion:
                //Power Explosion
                StartCoroutine(PowerExplosion(player, duration));

                //show UI icon
                UIManager.instance.ShowUI_Icon(Type);
                break;
            case powerType.Flight:
                //Power flight
                StartCoroutine(PowerFlight(player, duration));

                //show UI icon
                UIManager.instance.ShowUI_Icon(Type);
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

        //destory UI icon
        Destroy(UIManager.instance.jumpGO);
    }


    IEnumerator PowerInvincibility(NewMonoBehaviourScript player, float duration)
    {
        model.material.color = Color.blue;
        Invincibility = true;
        yield return new WaitForSeconds(duration);
        Invincibility = false;
        model.material.color = colorOrigin;

        //destory UI icon
        Destroy(UIManager.instance.invincibleGO);
    }

    IEnumerator PowerExplosion(NewMonoBehaviourScript player, float duration)
    {
        Destroyer.SetActive(true);
        yield return new WaitForSeconds(duration);
        Destroyer.SetActive(false);

        //destory UI icon
        Destroy(UIManager.instance.explosionGO);
    }

    IEnumerator PowerFlight(NewMonoBehaviourScript player, float duration)
    {
        model.material.color = Color.cyan;
        int oldGrav = player.GetGravity();
        player.SetGravity(0);

        yield return new WaitForSeconds(duration);
        model.material.color = colorOrigin;
        player.SetGravity(oldGrav);

        //destory UI icon
        Destroy(UIManager.instance.flyGO);
    }

}
