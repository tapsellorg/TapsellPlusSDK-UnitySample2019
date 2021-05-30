using System;
using UnityEngine;

namespace TapsellPlus
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