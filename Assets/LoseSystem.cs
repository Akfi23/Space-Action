using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace Akfi
{
    public class LoseSystem : GameSystemWithScreen<LoseScreen>
    {
        public override void OnInit()
        {
            screen.Panel.DOFade(0.65f, 0.5f);
            screen.Label.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.4f);
            screen.Text.transform.DOPunchRotation(Vector3.forward * 30, 0.5f).SetDelay(0.4f);
            screen.RestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            screen.Panel.DOKill();
            screen.Text.DOKill();
            screen.Label.DOKill();

            screen.RestartButton.interactable = false;
            Bootstrap.Instance.GameRestart(0);
        }
    }
}
