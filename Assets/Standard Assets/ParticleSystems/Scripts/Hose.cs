using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hose : MonoBehaviour
{
	public float maxPower = 20;
	public float minPower = 5;
	public float changeSpeed = 5;
	public ParticleSystem[] hoseWaterSystems;
	public Renderer systemRenderer;

	private float m_Power;


	// Update is called once per frame
	void Update()
	{
		m_Power = Random.Range(minPower, maxPower);

		foreach (var system in hoseWaterSystems)
		{
			ParticleSystem.MainModule mainModule = system.main;
			mainModule.startSpeed = m_Power;
			var emission = system.emission;
			emission.enabled = true;
		}
	}
}
