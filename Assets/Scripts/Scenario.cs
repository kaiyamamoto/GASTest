using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class Scenario : ScriptableObject
{
	public List<string> charas = new List<string>();
	public List<string> texts = new List<string>();

	public void ReSet()
	{
		this.texts = new List<string>();
		this.charas = new List<string>();
	}
}
