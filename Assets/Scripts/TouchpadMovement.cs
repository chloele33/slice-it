using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TouchpadMovement : MonoBehaviour
{
	[SerializeField]
	private Transform rig;

	[SerializeField]
	private float speed;

	private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private Vector2 axis = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
        	Debug.Log("Controller not initialised");
        	return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchPad))
        {
        	axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

        	if (rig != null)
        	{
        		rig.position += (transform.right * axis.x + transform.forward * axis.y) * speed * Time.deltaTime;
        		rig.position = new Vector3(rig.position.x, 0, rig.position.z);
        	}
        }
    }
}
