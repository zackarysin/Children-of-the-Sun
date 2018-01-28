using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCamera : Zac.ZacGObj {

    protected Camera camera_;
    public Camera Camera
    {
        get
        {
            return camera_;
        }
    }

    protected float crtDepth;

    [SerializeField]
    protected Transform camHolder;
    [SerializeField]
    protected Transform camRotPivot;
    [SerializeField]
    protected Transform camRefPos;

    [SerializeField]
    protected Transform camXAxis;
    [SerializeField]
    protected Transform camZAxis;

    [SerializeField]
    protected float midMouseBtnDragMul = 10.0f;

    protected Vector3 mv_humord_previousPoint;
    protected bool mv_humord_isHasPreviousPoint;

    protected override void set()
    {
        camera_ = GetComponent<Camera>();

        crtDepth = camHolder.localPosition.magnitude;
    }

    // Use this for initialization
    void Start () {
		
	}

    protected Quaternion camHolderRotCache;


    protected void handleUIModuleOnMiddleDrag(Vector2 _dragNomDelta)
    {

        Vector2 _drag = _dragNomDelta;
        _drag.x = _dragNomDelta.x * Screen.width * Time.deltaTime * midMouseBtnDragMul;
        _drag.y = _dragNomDelta.y * Screen.height * Time.deltaTime * midMouseBtnDragMul;

        float newX = camXAxis.localEulerAngles.x;
        float newZ = camZAxis.localEulerAngles.z;

        newX %= 360.0f;

        newX += _drag.y;// * Time.deltaTime;
        newZ += _drag.x;

        camXAxis.localEulerAngles = new Vector3(newX, camXAxis.localEulerAngles.y, camXAxis.localEulerAngles.z);
        camZAxis.localEulerAngles = new Vector3(camZAxis.localEulerAngles.x, camZAxis.localEulerAngles.y, newZ);

        //constraintCamAngle();
    }

    Vector2 previousMousePos;// = Input.mousePosition;

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButton(2))
        {
            Vector2 currentMousePos = Input.mousePosition;
            handleUIModuleOnMiddleDrag((previousMousePos - currentMousePos));
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currentMousePos = Input.mousePosition;
            Ray dragRay = camera_.ScreenPointToRay(currentMousePos);

            RaycastHit dragRayHit;
            if (Physics.Raycast(dragRay, out dragRayHit, Vector3.Magnitude(camHolder.position - Planet.Instance.transform.position)))
            {
                //Vector3 hitPt = transform.InverseTransformPoint(dragRayHit.point);
                Vector3 hitPt = dragRayHit.point - Planet.Instance.transform.position;

                //hitPt = Quaternion.Euler(-GameModule.StarSystem.GetPlanet(0).transform.localEulerAngles) * hitPt;

                if (!mv_humord_isHasPreviousPoint)
                {
                    //mv_humord_startPoint = hitPt;
                    mv_humord_previousPoint = hitPt;
                    mv_humord_isHasPreviousPoint = true;
                    //my_hymord_startRot = 

                }
                else
                {

                    if (mv_humord_isHasPreviousPoint)
                    {

                        Quaternion rot = Quaternion.FromToRotation(mv_humord_previousPoint, hitPt);

                        Quaternion inverseRot = Quaternion.Inverse(rot);

                        camRotPivot.rotation = inverseRot * camRotPivot.rotation;

                        camHolder.position = (camRefPos.transform.position - camRotPivot.transform.position).normalized * crtDepth + camRotPivot.transform.position;

                        // so that the planet is being look at by camera

                        camHolderRotCache = inverseRot * camHolder.rotation;
                        camHolder.rotation = camHolderRotCache;

                    }

                }

            }
            else
            {
                mv_humord_isHasPreviousPoint = false;
            }
        }
        else
        {
            mv_humord_isHasPreviousPoint = false;
        }

        crtDepth += Input.GetAxis("Mouse ScrollWheel");
        crtDepth -= Input.GetAxis("Vertical");
        camHolder.localPosition = camHolder.localPosition.normalized * crtDepth;

        previousMousePos = Input.mousePosition;
    }
}
