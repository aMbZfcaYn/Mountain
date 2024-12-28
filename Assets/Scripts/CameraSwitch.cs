using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Camera Cam_1;
    [SerializeField] private Camera Cam_2;

    void Start()
    {
        Transform transform = GetComponent<Transform>();
        Cam_1.enabled = true;
        Cam_2.enabled = false;
    }

    void Update()
    {
        if( player.rb.position.x < transform.position.x && Cam_1.enabled )
            SwitchCam();

        if( player.rb.position.x > transform.position.x && Cam_2.enabled )
            SwitchCam();
    }

    private void SwitchCam ()
    {
        Cam_1.enabled = !Cam_1.enabled;
        Cam_2.enabled = !Cam_2.enabled;
    }
}
