using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class MedicAnimation : MonoBehaviour
    {
        private bool forward, backward;
        private Vector3 init;
        public SteamVR_Action_Boolean press;
        public Hand hand;
        // Start is called before the first frame update
        void Start()
        {
            init = GetComponentInParent<Transform>().position;
        }

        // Update is called once per frame
        void Update()
        {
            if (press.GetState(hand.handType))
            {
                StartCoroutine(Medic());
            }
            if (forward)
            {
                transform.Translate(0, 0, Time.deltaTime);
            }
            if (backward)
            {
                transform.Translate(0, 0, -Time.deltaTime);
            }
        }
        private IEnumerator Medic()
        {
            forward = true;
            yield return new WaitForSeconds(0.5f);
            forward = false;
            backward = true;
            yield return new WaitForSeconds(0.5f);
            transform.position = GetComponentInParent<Transform>().position;
            backward = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "pathologie")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
