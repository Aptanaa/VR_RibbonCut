using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Common
{
    public class PickupOptions : MonoBehaviour
    {
        public Vector3 OffsetPosition;
        public Vector3 OffsetRotation;
        public bool StayInHand;
        public UnityEvent OnGrab;
        public UnityEvent OnDrop;

        [HideInInspector]
        public SteamVR_Controller.Device Controller;
    }
}