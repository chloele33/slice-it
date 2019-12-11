using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateTest : MonoBehaviour
{
	public SteamVR_TrackedObject rightHand;

	public SteamVR_TrackedObject leftHand;
	// Start is called before the first frame update
	void Start()
    {
        
    }

	int i = 0;
	int ten = 100;
	// Update is called once per frame
	void Update()
	{
		if (ten > 0)
		{ 
		SteamVR_Controller.Input((int)rightHand.index).TriggerHapticPulse(3999);
		SteamVR_Controller.Input((int)leftHand.index).TriggerHapticPulse(3999);
		}
		ten--;
		Debug.Log(i);
		i++;

	}
}
