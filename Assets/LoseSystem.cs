using DG.Tweening;
using Kuhpik;
using Kuhpik.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class LoseSystem : GameSystemWithScreen<LoseScreen>
    {
        public override void OnInit()
        {
            screen.LosePanel.DOFade(0.65f, 1f);
            screen.RestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            screen.LosePanel.DOKill();
            screen.RestartButton.interactable = false;
            //PoolingSystem.Clear();
            Bootstrap.Instance.GameRestart(0);
        }
    }
}
