using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class RotateCamzaxis : MonoBehaviour
    {
        //  VARIABLES
        public SteamVR_Action_Pose pose;
        public Hand hand;
        //public GameObject steam_camera;
        public GameObject joueur;
        private float rot;
        public GameObject indiccontainer;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            rot = pose.GetLocalRotation(hand.handType).z;
            print("rot="+ rot);
            indiccontainer.transform.rotation = Quaternion.Euler(0, 0, rot * 90);   
        }


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (pose == null)
            {
                Debug.LogError("No pose action assigned");
                return;
            }

            pose.AddOnChangeListener(OnRotationCameraActionChange, hand.handType);
        }
        private void OnRotationCameraActionChange(SteamVR_Action_In action_In) { }

        private void OnDisable()
        {
            if (pose != null)
            {
                pose.RemoveOnChangeListener(OnRotationCameraActionChange, hand.handType);
            }
        }
    }
}
