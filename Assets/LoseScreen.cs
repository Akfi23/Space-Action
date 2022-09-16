using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Akfi
{
    public class LoseScreen : UIScreen
    {
        [SerializeField] private Image losePanel;
        [SerializeField] private Button restartButton;

        public Image LosePanel => losePanel;
        public Button RestartButton => restartButton;
    }
}
