using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateTest : MonoBehaviour
{
	public SteamVR_TrackedObject rightHand;

	// Start is called before the first frame update
	void Start()
    {
        
    }

	int i = 0;
    // Update is called once per frame
    void Update()
    {
		SteamVR_Controller.Input((int)rightHand.index).TriggerHapticPulse(3999);
		Debug.Log(i);
		i++;

	}
}
