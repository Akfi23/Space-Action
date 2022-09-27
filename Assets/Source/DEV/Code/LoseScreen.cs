using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Akfi
{
    public class LoseScreen : UIScreen
    {
        [SerializeField] private Image losePanel;
        [SerializeField] private Image loseLabel;
        [SerializeField] private TMP_Text loseText;
        [SerializeField] private Button restartButton;

        public Image Panel => losePanel;
        public Image Label => loseLabel;
        public TMP_Text Text => loseText;
        public Button RestartButton => restartButton;
    }
}
