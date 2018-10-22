using MiniJSON;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ScenarioWriter : EditorWindow
{

	const string URL = "https://script.google.com/macros/s/AKfycbyt_Er9hHJgVcqV8-kfFDb2l-33pB847JMpqrHA2PlRpmlJh80/exec";
	string _sheetName;
	string _message;

	[MenuItem("Window/Scenario Writer")]
	static void Open()
	{
		GetWindow<ScenarioWriter>();
	}

	void OnGUI()
	{
		EditorGUILayout.Space();
		_sheetName = EditorGUILayout.TextField(
			"sheet name",
			_sheetName,
			GUILayout.Height(20.0f));
		EditorGUILayout.Space();

		if (GUILayout.Button("Create", GUILayout.Width(50.0f)))
		{
			EditorCoroutine.Start(Run());
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField(_message);
	}

	IEnumerator Run()
	{
		var download = new WWW(URL + "?sheetName=" + _sheetName);
		_message = "download";
		while (!download.isDone)
		{
			_message += ".";
			yield return new EditorCoroutine.WaitForSeconds(0.1f);
		}
		Debug.Log(download.text);

		string path = "Scenarios/" + _sheetName;
		string fullPath = "Assets/Resources/" + path + ".asset";
		var json = (List<object>)Json.Deserialize(download.text);

		Debug.Log(json);

		if (json == null)
		{
			Debug.LogError(download.text);
		}
		else
		{
			// ファイルを取得
			Scenario scenario = null;
			scenario = Resources.Load<Scenario>(path);

			if (!scenario)
			{
				// 存在しない場合作成
				scenario = CreateScriptableObject<Scenario>(fullPath);
			}
			else
			{
				scenario.ReSet();
			}

			foreach (var data in json)
			{
				var dic = data as Dictionary<string, object>;
				scenario.charas.Add(dic["character"] as string);
				scenario.texts.Add(dic["contents"] as string);
			}

			// ScriptableObjectのEditor編集を無効
			//scenario.hideFlags = HideFlags.NotEditable;
			// テキスト設定
			Debug.Log("complete.");
		}
		_message = "";
	}

	T CreateScriptableObject<T>(string output) where T : ScriptableObject
	{
		var res = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset((ScriptableObject)res, output);
		return res;
	}
}