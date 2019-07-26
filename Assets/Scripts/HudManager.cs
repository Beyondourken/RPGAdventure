namespace RPG.Control
{
    using UnityEngine;

    public class HudManager : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.UI;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            return true;
        }

    }
}

