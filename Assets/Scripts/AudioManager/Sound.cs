using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	// Name
	public string name;

	// Audio Clip
	public AudioClip clip;

	// Volume and variance
	// Play different levels of sound randomly
	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	// Pitch and variance
	// Play different levels of pitch randomly
	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	// 3D Sound
	// 0 = 2D sound, 1 = 3D
	// Only use values around 0.5 - 0.8 for realistic 3d effect
	[Range(0f, 1f)]
	public float SpatialBlend3D = 0f;

	public bool loop = false;

	//public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
