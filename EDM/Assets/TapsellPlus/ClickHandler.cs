using System;
using UnityEngine;

namespace TapsellPlusSDK
{
    public class ClickHandler : MonoBehaviour
    {
        public event Action ONClick;
        private void OnMouseUpAsButton()
        {
            ONClick?.Invoke();
        }
    }
}