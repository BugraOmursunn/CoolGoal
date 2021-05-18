using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileManager
{
    public class Canon : MonoBehaviour
    {
        TouchState info;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            Vector3 touchPos;
            info = TouchControl.GetTouchState();
            switch (info)
            {
                case TouchState.Start:
                    touchPos = TouchControl.GetTouchPosition();
                    CheckHitPosition(touchPos);
                    break;

                case TouchState.Moved:
                    touchPos = TouchControl.GetTouchPosition();
                    CheckHitPosition(touchPos);
                    break;

                case TouchState.Ended:
                    touchPos = TouchControl.GetTouchPosition();
                    CheckHitPosition(touchPos);
                    break;

                default:
                    break;
            }
        }

        void CheckHitPosition(Vector3 pos)
        {
            int layerMask = -1;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Vector3 worldPos = new Vector3();

            if (Physics.Raycast(ray, out hit, 60f, layerMask))
            {
                worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, hit.distance));
            }
            RotateCanon(worldPos);
        }

        public void RotateCanon(Vector3 hitPos)
        {
            hitPos.y = transform.position.y;
            
            transform.LookAt(hitPos);
        }
    }
}