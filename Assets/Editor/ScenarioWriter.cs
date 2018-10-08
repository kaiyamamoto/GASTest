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

		var json = (List<object>)Json.Deserialize(download.text);
		if (json != null)
		{
			var scenario = CreateScriptableObject<Scenario>("Assets/Scenarios/" + _sheetName + ".asset");
			scenario.hideFlags = HideFlags.NotEditable;
			scenario.texts = json.Select(j => j.ToString()).ToArray();
			Debug.Log("complete.");
		}
		else
		{
			Debug.LogError(download.text);
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