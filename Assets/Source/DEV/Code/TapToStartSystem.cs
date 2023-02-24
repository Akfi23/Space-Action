using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace Akfi
{
    public class TapToStartSystem : GameSystem
    {
        public override void OnStateEnter()
        {
            game.Rocket.StartFX.Play();
            game.Player.gameObject.SetActive(false);
            game.Player.transform.localScale = Vector3.one * 0.3f;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(game.Rocket.transform.DOMove(game.LandingPos.transform.position+Vector3.up*4,3f));
            sequence.AppendCallback(()=>game.Player.gameObject.SetActive(true));
            sequence.Append(game.Player.transform.DOScale(Vector3.one, 0.5f));
            sequence.AppendCallback(()=>PrepareGameState());
        }

        private void PrepareGameState()
        {
            cameraController.RocketCamera.gameObject.SetActive(false);
            Bootstrap.Instance.ChangeGameState(GameStateID.Game);
        }
    }
}