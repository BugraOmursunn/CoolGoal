/*
    Projectile Manager
    ©︎   2020 Shuji Hori
 */

using UnityEngine;

namespace ProjectileManager
{
    public class TouchManager : MonoBehaviour
    {
        public bool useDefaultSetting;
        public GameObject projectile;
        public GameObject shootMgr;
        private Vector3 markPos;

        ProjectileThrow projectileThrow;
        TouchState info;

        public Vector3 MarkPos
        {
            get { return markPos; }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (useDefaultSetting)
            {
                shootMgr = GameObject.Find("ShootPoint");
            }
            projectileThrow = shootMgr.GetComponent<ProjectileThrow>();
        }

        // Update is called once per frame
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
                    
                    projectileThrow.ShootObject(projectile, touchPos);
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

            if (Physics.Raycast(ray, out hit, 60f, layerMask))
            {
                markPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, hit.distance));
            }
            projectileThrow.CheckVector(markPos);
        }
    }
}
