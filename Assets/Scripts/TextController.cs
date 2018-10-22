using UnityEngine;
using System.Collections;
using UnityEngine.UI;   // uGUIの機能を使うお約束

public class TextController : MonoBehaviour
{
	public Scenario scenarios; // シナリオを格納する
	public Text uiText;
	public Text charaText;

	int currentLine = 0;

	void Start()
	{
		TextUpdate();
	}

	void Update()
	{
		// 現在の行番号がラストまで行ってない状態でクリックすると、テキストを更新する
		if (scenarios.texts.Count <= currentLine) return;
		if (Input.GetMouseButtonDown(0)
			|| Input.GetKeyDown(KeyCode.Z))
		{
			TextUpdate();
		}
	}

	// テキストを更新する
	void TextUpdate()
	{
		// 現在の行のテキストをuiTextに流し込み、現在の行番号を一つ追加する
		charaText.text = scenarios.charas[currentLine];
		uiText.text = scenarios.texts[currentLine];
		currentLine++;
	}
}