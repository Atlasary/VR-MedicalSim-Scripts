//  Created by Guillaume and Guillaume
//              Schuster      Roux

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class RotationCamera : MonoBehaviour
    {
        //  VARIABLES
        public SteamVR_Action_Vector2 rotationCamera;
        public SteamVR_Action_Pose pose;
        public Hand hand1,hand2;
        //public GameObject steam_camera;
        public GameObject joueur;
        private Quaternion before;
        public GameObject indic;
        private Vector3 pos ;

        // Use this for initialization
        void Start ( )
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            
            if ( rotationCamera .GetAxis ( hand1 .handType ) .y !=  0 )
            {          
                pos = rotationCamera.GetAxis(hand1.handType);
            }
           
            //print(pos.y);
            indic.transform.rotation = Quaternion.Euler(-pos.y*180,0, pose.GetLocalRotation(hand2.handType).z*180);   
        }


        private void OnEnable()
        {
            if ( hand1 == null )
                hand1 = this .GetComponent<Hand> ( ) ;

            if ( rotationCamera == null )rotationCamera .GetAxis ( hand1 .handType ) ;
            {
                Debug .LogError ( "No action assigned" ) ;
                return ;
            }
            if (pose == null)
            {
                Debug.LogError("No pose action assigned");
                return;
            }

            rotationCamera .AddOnChangeListener ( OnRotationCameraActionChange , hand1.handType);
            rotationCamera.AddOnChangeListener(OnRotationCameraActionChange, hand2.handType);
        }
        private void OnRotationCameraActionChange(SteamVR_Action_In action_In) { }

        private void OnDisable ( )
        {
            if ( rotationCamera != null)
            {
                rotationCamera .RemoveOnChangeListener ( OnRotationCameraActionChange , hand1 .handType ) ;
            }
            if (rotationCamera != null)
            {
                rotationCamera.RemoveOnChangeListener(OnRotationCameraActionChange, hand2.handType);
            }
        }
    }
}
