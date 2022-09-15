using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUDComponent : MonoBehaviour
{
    [SerializeField] private Canvas hud;
    [SerializeField] private Image hpBar;

    public Canvas HUD => hud;
    public Image HPBar => hpBar;

    public void InitHUD()
    {
        hud = GetComponentInChildren<Canvas>();
        hud.gameObject.SetActive(false);
    }
}
