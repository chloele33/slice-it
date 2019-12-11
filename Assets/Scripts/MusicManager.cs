using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	private static MusicManager _instance;

	public static MusicManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<MusicManager>();

				//Tell unity not to destroy this object when loading a new cene!
				DontDestroyOnLoad(_instance.gameObject);
			}

			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			Debug.Log("Null");
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if (this != _instance)
			{
				Play();
				Debug.Log("IsnotNull");
				Destroy(this.gameObject);
			}
		}

	}
	public void Update()
	{
		if (this != _instance)
		{
			_instance = null;
		}
	}
	public void Play()
	{
		GetComponent<AudioSource>().Play();
	}
}
