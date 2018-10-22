using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
	readonly string charaId = "b008";
	enum Facial
	{
		Natural = 001,
		Smiling = 002,
	}

	[SerializeField] UnityEngine.UI.Image eyes, mouth;

	[SerializeField] UnityEngine.U2D.SpriteAtlas atlas;

	IEnumerator Start()
	{
		Debug.Log("衝撃のファーストブリット");

		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		eyes.sprite = atlas.GetSprite("b008_000_03_e1");
		mouth.sprite = atlas.GetSprite("b008_001_03_m3");

		Debug.Log("撃滅のセカンドブリット");
		yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		yield return null;

		Debug.Log("抹殺のラストブリット");
	}
}
