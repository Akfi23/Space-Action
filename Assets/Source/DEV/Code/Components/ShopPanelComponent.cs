using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Akfi
{
    public class ShopPanelComponent : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;
        [SerializeField] protected UpgradeOwner owner;

        public Button CloseButton => closeButton;
        public UpgradeOwner Owner => owner;


        public virtual void InitScreen()
        {
            closeButton.onClick.AddListener(() => ToggleScreen(false));
        }

        public virtual void ToggleScreen(bool status)
        {
            if (status)
            {
                transform.localScale = Vector3.one * 0.5f;
                gameObject.SetActive(status);
                transform.DOScale(Vector3.one, 0.15f);
            }
            else
            {
                transform.DOScale(Vector3.one * 0.5f, 0.05f).OnComplete(() => gameObject.SetActive(status));
            }
        }
    }
}
