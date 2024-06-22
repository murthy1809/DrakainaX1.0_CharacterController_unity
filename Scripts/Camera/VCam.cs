using UnityEngine;
using Cinemachine;

public class VCam : MonoBehaviour
{
    [SerializeField] public  CinemachineFreeLook vCam;
    public VCameraObject vCO,vZO;
    [SerializeField] Transform LookAt,Follow,ZoomLook;
    public bool freeLook;
    [SerializeField] float freeLookTime;
    public bool TimerOn = false;
    public float TimeLeft;
    bool t;

    void Start()
    {
        
    }

    void Update()
    {
        if (ZoomLook == null || vZO == null)
        {
            return;
        }

        if (Input.GetButton("SecondaryAttack"))
        {

            vCam.LookAt = ZoomLook;
            vCam.Follow = ZoomLook;
            vCam.m_Orbits[0].m_Height = vZO.TopRigHeight;
            vCam.m_Orbits[0].m_Radius = vZO.TopRigRadius;
            vCam.m_Orbits[1].m_Height = vZO.MiddleRigHeight;
            vCam.m_Orbits[1].m_Radius = vZO.MiddleRigRadius;
            vCam.m_Orbits[2].m_Height = vZO.BottomRigHeight;
            vCam.m_Orbits[2].m_Radius = vZO.BotttomRigRadius;
            vCam.m_Lens.FieldOfView = vZO.VerticalFOV;
            vCam.m_Heading.m_Bias = 0;
            vCam.m_YAxis.m_MaxSpeed = 20;
            vCam.m_XAxis.m_MaxSpeed = 300;
            freeLook = true;
            TimeLeft = 1;
            TimerOn = false;



        }
        else /*if (Input.GetKeyDown(KeyCode.LeftAlt))*/
        {
            vCam.m_Orbits[0].m_Height = vCO.TopRigHeight;
            vCam.m_Orbits[0].m_Radius = vCO.TopRigRadius;
            vCam.m_Orbits[1].m_Height = vCO.MiddleRigHeight;
            vCam.m_Orbits[1].m_Radius = vCO.MiddleRigRadius;
            vCam.m_Orbits[2].m_Height = vCO.BottomRigHeight;
            vCam.m_Orbits[2].m_Radius = vCO.BotttomRigRadius;
            vCam.m_YAxis.m_MaxSpeed = 2;
            vCam.m_XAxis.m_MaxSpeed = 300;

            vCam.LookAt = LookAt;
            vCam.Follow = Follow;

            vCam.m_Lens.FieldOfView = vCO.VerticalFOV;
            vCam.m_Heading.m_Bias = 0;


            TimerOn = true;
        }

        if (TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
                vCam.m_RecenterToTargetHeading.m_enabled = true;
                vCam.m_YAxisRecentering.m_enabled = true;
            }
            else
            {
                vCam.m_RecenterToTargetHeading.m_enabled = false;
                vCam.m_YAxisRecentering.m_enabled = false;
                TimeLeft = 0;
                TimerOn = false;
                freeLook = false;
            }
        }
        else
        {
            vCam.m_RecenterToTargetHeading.m_enabled = false;
            vCam.m_YAxisRecentering.m_enabled = false;
        }

        //IEnumerator ExecuteAfterTime(float time)
        //{
        //    yield return new WaitForSeconds(time);
        //    vCam.m_RecenterToTargetHeading.m_enabled = false;
        //    vCam.m_YAxisRecentering.m_enabled = false;
        //}
        //StartCoroutine(ExecuteAfterTime(freeLookTime));
        
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
    }
}
