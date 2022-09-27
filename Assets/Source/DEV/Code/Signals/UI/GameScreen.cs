using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen
{
    [Header("Jetpack UI")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private Slider jetpackFuelFill;
    [SerializeField] private Color emptyFuelColor;
    [SerializeField] private Image jetpackFuelIcon;

    [Header("Player Health UI")]
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private TargetIndicator playeryHealthBar;
    [SerializeField] private Image healthIndicator;
    [SerializeField] private TMP_Text playerHealthText;

    [Header("Enemy Health UI")]
    [HorizontalLine(color: EColor.Red)]
    [SerializeField] private TargetIndicator enemyHealthBar;
    [SerializeField] private TargetIndicator bossIndicator;

    [Header("Money")]
    [HorizontalLine(color: EColor.Yellow)]
    public TMP_Text moneyText;

    public Image HealthIndicator => healthIndicator;
    public TargetIndicator PlayerHealthBar => playeryHealthBar;
    public TargetIndicator EnemyHealthBar => enemyHealthBar;
    public TargetIndicator BossIndicator => bossIndicator;
    public TMP_Text PlayerHealthText => playerHealthText;
    public Slider JetpackFuelFill => jetpackFuelFill;
    public Color EmptyFuelColor => emptyFuelColor;
    public Image JetpackFuelIcon => jetpackFuelIcon;

    public void UpdateMoney(int money)
    {
        moneyText.text = money.ToString();
    }

    public async UniTask UpdateHPBar(CharacterComponent character, TargetIndicator indicator, float damage)
    {
        indicator.CurrentHP.fillAmount = 1f / character.MaxHealth * (character.CurrentHealth - damage);

        if (character.CurrentHealth > 0)
        {
            indicator.FakeHP.fillAmount = 1f / character.MaxHealth * (character.CurrentHealth + 1);
        }
        else
        {
            indicator.FakeHP.fillAmount = 1f;
        }

        indicator.FakeHP.DOFillAmount(indicator.CurrentHP.fillAmount, 0.3f).SetEase(Ease.Linear);
    }

    public void ShowDamageText(CharacterComponent character, TargetIndicator indicator, float damage)
    {
        indicator.DamageText.gameObject.SetActive(true);
        indicator.DamageText.text = damage.ToString();
        indicator.DamageText.transform.DOKill();
        indicator.DamageText.transform.localPosition = new Vector3(58, 0, 0);
        indicator.DamageText.transform.DOPunchRotation(Vector3.forward * 0.15f, 0.2f, 10, 1);
        indicator.DamageText.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => indicator.DamageText.transform.DOScale(Vector3.one * 1.2f, 1.5f));
        indicator.DamageText.DOFade(1, 0.5f).OnComplete(() => indicator.DamageText.DOFade(0, 1.5f));
        indicator.DamageText.transform.DOLocalMoveY(10f, 0.5f);
    }
}
