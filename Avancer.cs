//  Created by Guillaume and Guillaume
//              Schuster      Roux


//  Où le trouver ?
//      Sur Unity / Player / SteamVRObjects / RightHand


//  Assignation des entités de Unity aux paramètres de ce fichiers :
//      Paramètres :    Entités :
//      Avancer         \actions\default\in\AvanceAction    (configuration Window / SteamVR Input)
//      Pose            \actions\default\in\Pose            (configuration Window / SteamVR Input)
//      Hand            Player / SteamVRObjects / LeftHand (Hand)
//      Indic           PLayer / VRCamera / RotationIndicatorContainer / Indicator Container / Indicator
//      Joueur          Player


using System .Collections ;
using System .Collections .Generic ;
using UnityEngine ;
using Valve .VR .InteractionSystem ;


namespace Valve .VR .InteractionSystem .Sample
{


    public class Avancer : MonoBehaviour
    {


        //  VARIABLES   public
        public SteamVR_Action_Boolean avancer ; //  AvanceAction
        public SteamVR_Action_Pose pose ;       //  Pose
        public Hand hand ;                      //  LeftHand
        public GameObject indic,dir ;        //  Indicator Container
        public GameObject joueur ;              //  Player


        //  VARIABLES   private
        private Vector3 pose_before ;   // position de la main avant
        private Vector3 pose_now;       // position de la main maintenant
        private Vector3 pose_delta ;    // mouvement de la main
        private Vector3 angl_vector ;   // rotation de la main
        private int en_avance = 0 ;     // tant que la gachette est appuyé = 1
        private float speed = 5000 ;     // vitesse du coloscope / déplacement de la main


        //  VARIABLES de calculs
        private float cst ;             //
        private float translate_x;      //  x = cst * sin (angle_y) * sin (angle_z)         // sin (angle_y)        
        private float translate_y;      //  y = cst * 1             * cos (angle_z)         // 0                    ATTENTION, y correspond au z conventionnel ou repère indirect
        private float translate_z;      //  z = cst * sin (angle_y) * sin (angle_z)         // cos (angle_y)        ATTENTION, z correspond au y conventionnel ou repère indirect


        //private Quaternion


        private void Start() { }


        private void Update()
        {


            //  Déplacement du joueur
            if (en_avance == 1)
            {
                pose_now = pose.GetLocalPosition(hand.handType);
                pose_delta = pose_now - pose_before ;

                cst = speed * Vector3 .Dot ( angl_vector , pose_delta ) ;
                print(cst);

                // v1 : le mouvement suit la visée de la caméra VR.
                //
                //if ( true )     //  2D (angle_y)
                //{
                //    translate_x = cst * ( Mathf .Sin ( ( indic .transform .rotation .eulerAngles .y * ( Mathf .PI ) ) / 180 ) ) ;
                //    translate_y = cst ;
                //    translate_z = cst * ( Mathf .Cos ( ( indic .transform .rotation .eulerAngles .y * ( Mathf .PI ) ) / 180 ) ) ;
                //}
                //if ( true )     //  2D (angle_x)
                //{
                //    translate_x *= ( Mathf .Cos ( ( indic .transform .rotation .eulerAngles .x * ( Mathf.PI ) ) / 180 ) ) ;
                //    translate_y *= - ( Mathf .Sin ( ( indic .transform .rotation .eulerAngles .x * ( Mathf.PI ) ) / 180 ) ) ;
                //    translate_z *= ( Mathf .Cos ( ( indic .transform .rotation .eulerAngles .x * ( Mathf.PI ) ) / 180 ) ) ;
                //}


                //v2 : le mouvement suit la visée de l'indicateur --> indic = indic
                //
                //translate_x = cst * -(Mathf.Cos((indic.transform.rotation.eulerAngles.z % 180 * (Mathf.PI)) / 180));
                //translate_z = cst;
                //translate_y = cst * (Mathf.Cos((indic.transform.rotation.eulerAngles.z * (Mathf.PI)) / 180));

                //translate_x *= (Mathf.Cos((indic.transform.rotation.eulerAngles.x * (Mathf.PI)) / 180));
                //translate_z *= (Mathf.Sin((indic.transform.rotation.eulerAngles.x * (Mathf.PI)) / 180));
                //translate_y *= (Mathf.Cos((indic.transform.rotation.eulerAngles.x * (Mathf.PI)) / 180));


                //v3 : le mouvement suit la visée de l'indicateur --> indic = indic

                //  Pointe vers le -y ==> repasse en repère direct
                //      Rotation en x dans le sens trigo

                //translate_y = - cst * (Mathf.Cos((indic.transform.rotation.eulerAngles.x * (Mathf.PI)) / 180));
                //print(indic.transform.rotation.eulerAngles.x);
                //print("IMP : "+ indic.transform.rotation.x*180);
                //print(-Mathf.Cos(indic.transform.rotation.x * 180));

                //translate_x = cst * (Mathf.Sin((indic.transform.rotation.eulerAngles.z%180 * (Mathf.PI)) / 180));
                //translate_z = cst ;
                //translate_y = cst * ( Mathf .Cos ( ( indic .transform .rotation .eulerAngles .z * ( Mathf .PI ) ) / 180 ) ) ;

                //translate_x *= (Mathf.Cos((indic.transform.rotation.eulerAngles.x * (Mathf.PI)) / 180));
                //translate_z *= - ( Mathf .Sin ( ( indic .transform .rotation .eulerAngles .x * ( Mathf.PI ) ) / 180 ) ) ;
                //translate_y *= ( Mathf .Cos ( ( indic .transform .rotation .eulerAngles .x * ( Mathf.PI ) ) / 180 ) ) ;

                //print(indic.transform.rotation.eulerAngles.z%180);





                //joueur.transform.Translate(translate_x, translate_y, translate_z);
                joueur.transform.Translate(cst*(dir.transform.position - indic.transform.position)*Time.deltaTime);

                pose_before = pose_now;
                angl_vector = new Vector3(Mathf.Sin((pose.GetLastLocalRotation(hand.handType).eulerAngles.y * (Mathf.PI)) / 180), 0, Mathf.Cos((pose.GetLastLocalRotation(hand.handType).eulerAngles.y * (Mathf.PI)) / 180));
            }
            //print(angl_vector);
            //print((hand.GetComponent<SteamVR_Behaviour_Pose>().poseAction.GetLocalPosition(hand.GetComponent<SteamVR_Behaviour_Pose>().inputSource)));
        }


        public void avance_On() {
            //joueur .transform .Translate ( Mathf.Sin((indic.transform.rotation.eulerAngles.y*(Mathf.PI))/180), 0, Mathf.Cos((steam_camera.transform.rotation.eulerAngles.y * (Mathf.PI)) / 180));
            //print("enableTranslation");
            en_avance = 1 ;
            pose_before = pose.GetLocalPosition(hand.handType) ;
            angl_vector = new Vector3(Mathf.Sin((pose.GetLastLocalRotation(hand.handType).eulerAngles.y * (Mathf.PI)) / 180), 0, Mathf.Cos((pose.GetLastLocalRotation(hand.handType).eulerAngles.y * (Mathf.PI)) / 180));
        }


        public void avance_Off()
        {
            en_avance = 0;
        }


        private void OnAvanceActionChange (SteamVR_Action_In actionIn) {
            if (avancer.GetState(hand.handType)) {
                avance_On();
            }
            else
            {
                avance_Off();
            }
        }


        private void OnPoseActionUpdate(SteamVR_Action_In actionIn) {
           // print("=====") ;
            //print(pose.GetLocalPosition(hand.handType));
            //print(pose.GetLastLocalRotation(hand.handType));
        }


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (avancer == null)    //  voir ligne 10
            {
                Debug.LogError("No AvanceAction action assigned");
                return;
            }

            if (pose == null)       //  voir ligne 11
            {
                Debug.LogError("No Pose action assigned");
                return;
            }

            avancer.AddOnChangeListener(OnAvanceActionChange, hand.handType);
            pose.AddOnUpdateListener(OnPoseActionUpdate, hand.handType);
        }


        private void OnDisable()
        {
            if (avancer != null)
            {
                avancer.RemoveOnChangeListener(OnAvanceActionChange, hand.handType) ;
                pose.RemoveOnChangeListener(OnPoseActionUpdate, hand.handType) ;
            }
        }
    }
}