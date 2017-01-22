using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VR;
using Random = UnityEngine.Random;

public enum SOUNDS {
    NO_SOUND,
    OUT_OF_AMMO,
	PISTOL_SHOT,
	CANNON_SHOT,
	EXPLOSION_BIG,
	EXPLOSION_SMALL,
	MAIN_GAME_AUDIO,
    DEPTH_CHARGE_DROP,
    BASE_EXPLOSION,
    SUB_EXPLOSION,
    WAVES,
    CANNON_EXPLOSION

    
    
}

[Serializable]
public class AudioToEnum
{
	public SOUNDS sound;
	public AudioClip clip; 
	//todo: frequency and volume
	public float frequencyVariance = 0.2f;
	public float volumeVariance = 0.2f;
    public float volume = 1.0f;
}
public class AudioManager : MonoBehaviour //TODO: onValidate to make sure all sounds are set
{
	private static AudioManager s_Instance;

	[FormerlySerializedAs("m_OutOfAmmo")]
	[SerializeField] private AudioSource m_templateAudio;

	public List<AudioToEnum> audioclips;
	private Dictionary<SOUNDS, AudioToEnum> enumToSoundDataMap; 

	void Awake()
	{
		s_Instance = this;
		enumToSoundDataMap = new Dictionary<SOUNDS, AudioToEnum>();
		foreach (var audioToEnum in audioclips)
		{
			try
			{
				enumToSoundDataMap.Add(audioToEnum.sound, audioToEnum);
			}
			catch (Exception e)
			{
				Debug.LogError("Error in soundmanager sound setup:");
				Debug.LogException(e);
			}
			
		}
	}

	void Start()
	{
		Play(SOUNDS.MAIN_GAME_AUDIO);
	}

	public static void Play(SOUNDS sound) //default position is at the vr players head
	{
		Play(sound, InputTracking.GetLocalPosition(VRNode.Head));
	}

	public static void Play(SOUNDS sound,Vector3 pointToPlayAt,bool killWhenDone = true) {
		try
		{
			var clipInfo = s_Instance.enumToSoundDataMap[sound];
			var tmpAudioSource = Instantiate(s_Instance.m_templateAudio) as AudioSource;
			tmpAudioSource.transform.position = pointToPlayAt;
			tmpAudioSource.clip = clipInfo.clip;
			tmpAudioSource.volume += Random.Range(-1*clipInfo.volumeVariance, 0f);
			tmpAudioSource.pitch += Random.Range(-1 * clipInfo.frequencyVariance, clipInfo.frequencyVariance);
			tmpAudioSource.Play();
			if(killWhenDone)
				s_Instance.StartCoroutine(s_Instance.killWhenDone(tmpAudioSource));
		}
		catch(Exception e) {
			Debug.LogWarning("Sound play error:" + e.Message);
		}

	}

	public IEnumerator killWhenDone(AudioSource source)
	{
		yield return null;
		while (source != null && source.isPlaying)
		{
			yield return null;
		}
		if(source != null)
			Destroy(source.gameObject);
	}

	[ContextMenu("test play OutOFAMMO")]
	public void OutOfAmmo()
	{
		Play(SOUNDS.OUT_OF_AMMO);
	}
}
