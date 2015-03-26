using UnityEngine;
using System.Collections;

public class ParticleSelector : MonoBehaviour {

	public ParticleSystem particleSystem1;
	public ParticleSystem particleSystem2;
	public ParticleSystem particleSystem3;
	public ParticleSystem particleSystem4;
	public ParticleSystem particleSystem5;

	public void PlayColor(Color color, Vector3 emitLocation)
	{
		if (!particleSystem1.isPlaying) 
		{
			particleSystem1.transform.localPosition = new Vector3(emitLocation.x, emitLocation.y, particleSystem1.transform.localPosition.z);
			particleSystem1.startColor = color;
			particleSystem1.Play();
		}
		else if (!particleSystem2.isPlaying)
		{
			particleSystem2.transform.localPosition = new Vector3(emitLocation.x, emitLocation.y, particleSystem2.transform.localPosition.z);
			particleSystem2.startColor = color;
			particleSystem2.Play();
		}
		else if (!particleSystem3.isPlaying)
		{
			particleSystem3.transform.localPosition = new Vector3(emitLocation.x, emitLocation.y, particleSystem3.transform.localPosition.z);
			particleSystem3.startColor = color;
			particleSystem3.Play();
		}
		else if (!particleSystem4.isPlaying)
		{
			particleSystem4.transform.localPosition = new Vector3(emitLocation.x, emitLocation.y, particleSystem4.transform.localPosition.z);
			particleSystem4.startColor = color;
			particleSystem4.Play();
		}
		else if (!particleSystem5.isPlaying)
		{
			particleSystem5.transform.localPosition = new Vector3(emitLocation.x, emitLocation.y, particleSystem5.transform.localPosition.z);
			particleSystem5.startColor = color;
				particleSystem5.Play();
		}
	}
	

}
