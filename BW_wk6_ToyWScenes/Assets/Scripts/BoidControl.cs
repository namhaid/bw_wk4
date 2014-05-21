using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidControl : MonoBehaviour {
	
	public int flockSize = 20;
	
	public Boid prefab;
	
	public List<Boid> boids = new List<Boid>();
	
	void Start()
	{
		for (int i = 0; i < flockSize; i++)
		{
			Boid boid = Instantiate(prefab, transform.position, transform.rotation) as Boid;
			boid.transform.parent = transform;
			Boid targetScript = boid.GetComponent<Boid>();
			boid.transform.localPosition = new Vector3(
				(Random.value * targetScript.mapSize) - (2/targetScript.mapSize),
				Random.value * targetScript.mapSize,
				(Random.value * targetScript.mapSize) - (2/targetScript.mapSize));
			boid.transform.rotation = Random.rotation;
			boid.controller = this;
			boids.Add(boid);
		}
	}
	
}

