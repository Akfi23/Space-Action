using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Akfi
{
    public class UpgradeBarComponent : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text upgradeName;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite inactive;

        public Button Button => upgradeButton;

        public void Init(Sprite icon, string name, int price, int money)
        {
            this.icon.sprite = icon;
            UpdateStats(price, money);
            upgradeName.text = name;
        }

        public void UpdateStats(int price, int playerMoney)
        {
            this.price.text = price.ToString();

            if (playerMoney >= price) buttonImage.sprite = active;
            else buttonImage.sprite = inactive;
        }
    }
}
