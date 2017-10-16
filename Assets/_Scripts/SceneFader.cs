using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public Image fadeImage;
	public AnimationCurve curve;

	void Start ()
	{
		fadeImage.color = new Color (0f,0f,0f,1f);
		StartCoroutine(FadeInRoutine());
	}

	public void FadeTo (int scene)
	{
		StartCoroutine(FadeOutRoutine(scene));
	}

	IEnumerator FadeInRoutine ()
	{
		float t = 1f;
		float alpha;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			alpha = curve.Evaluate(t);
			fadeImage.color = new Color (0f,0f,0f,alpha);
			yield return null;
		}
	}

	IEnumerator FadeOutRoutine (int scene)
	{
		float t = 0f;
		float alpha;
		while (t < 1f)
		{
			t += Time.deltaTime;
			alpha = curve.Evaluate(t);
			fadeImage.color = new Color (0f,0f,0f,alpha);
			yield return null;
		}
		SceneManager.LoadScene(scene);
	}

}
