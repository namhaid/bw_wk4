
using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Boid : MonoBehaviour {
	
	public Vector3 looky;
	public float sight;
	public float sepDist;
	public float coh;
	public float sep;
	public float ali;
	public float speed;
	public float mapSize;
	public float turnSpeed;
	public Vector3 v;
	
	private float dist;
	private Vector3 targetRotation;
	private float aliCount;
	private Vector3 targetCohesion;
	private float cohCount;
	private Vector3 targetSeparation;
	private float sepCount;
	
	public Vector3 targetSum;
	
	internal BoidControl controller;
	
	void FixedUpdate() {
		v = rigidbody.velocity;
		// Clear all
		targetSum = transform.position;
		looky = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		targetRotation = transform.position;
		targetCohesion = transform.position;
		targetSeparation = transform.position;
		aliCount = 0;
		cohCount = 0;
		sepCount = 0;
				
				// perform the following on every boid
				foreach (Boid boid in controller.boids) {
					// "dist" is the distance between this boid and the object this script is attached to
					dist = Vector3.Distance (boid.transform.position, transform.position);
					// if that distance is within sight range
					if (dist <= sight) {
						// access the boids script of this particular boid
						Boid targetScript = boid.GetComponent<Boid>();
						// add the directional information to "target Rotation"
						targetRotation += (targetScript.looky);
						// and count it in the "alignment" counter*
						aliCount++;
						// if it is within sight range but far enough away
//						if (dist >= sepDist) {
							// add its location information to "target cohesion"
							targetCohesion += (boid.transform.position);
							// and count it in the "cohesion" counter*
							cohCount++;
						// else, if it is within sight range and within the desired separation distance
//						} else 
						if (dist <= sepDist) {
							// subtract its position data from "target Separation"
							targetSeparation -= (boid.transform.position);
							// and count it in the "separation" counter*
							sepCount++;
						}
					}
					// *these counters are used later as a means of knowing how much to divide by in order
					// to obtain the "average" across all boids.
				}
				
				
				// divide everything in order to find true averages
				targetRotation /= aliCount;
				targetCohesion /= cohCount;
				targetSeparation /= sepCount;
				
				// find actual relative positions
				// (target position - local position = target position relative to us)
				targetCohesion -= transform.position;
				targetSeparation -= transform.position;
				
				// normalize everything
				// ie, turn everything into directions, instead of points.
//				rigidbody.velocity = Vector3.Normalize (rigidbody.velocity);
//				rigidbody.velocity *= speed;
				targetCohesion = Vector3.Normalize(targetCohesion);
				targetSeparation = Vector3.Normalize (targetSeparation);
				targetRotation = Vector3.Normalize (targetRotation);
				
				// weight each force
				targetCohesion *= coh;
				targetSeparation *= sep;
				targetRotation *= ali;
				
				// add all forces together into one universal vector
				targetSum += targetCohesion;
				targetSum += targetSeparation;
				targetSum += targetRotation;
				
				// make sure that targetSum is not underground
				if (targetSum.y < 0) {
					targetSum.y = transform.position.y;
				}
				// look at target sum
				transform.LookAt( targetSum );
				// and then move forward
				rigidbody.AddForce (transform.forward);
				
				//adjust "looky" so others know where to look.
				looky = transform.TransformPoint (0, 0, 5000);
				
				// make sure you're not exceeding the speed limit.
				// normaize the existing velocity, so that it equals 1
				rigidbody.velocity = Vector3.Normalize (rigidbody.velocity);
				// angle boid in direction of new velocity
				transform.forward = new Vector3 ( rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z );
				// and make the boid go the speed limit.
				rigidbody.velocity *= speed;
				
				// Look in the right direction again!
				//transform.LookAt(rigidbody.velocity);
				
				// oh, and borders. derp.
				// x axis
				if (transform.position.x >= (mapSize/2)) {
					transform.position = new Vector3(-(mapSize/2), transform.position.y, transform.position.z);
				} else if (transform.position.x <= -(mapSize/2)) {
					transform.position = new Vector3(mapSize/2, transform.position.y, transform.position.z);
				}
				// z axis
				if (transform.position.z >= (mapSize/2)) {
					transform.position = new Vector3(transform.position.x, transform.position.y, -(mapSize/2));
				} else if (transform.position.z <= -(mapSize/2)) {
					transform.position = new Vector3(transform.position.x, transform.position.y, mapSize/2);
				}
				// y axis
				if (transform.position.y >= (mapSize/3)) {
					transform.position = new Vector3(transform.position.x, mapSize/3, transform.position.z);
				} else if (transform.position.y <= 0f) {
					transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
				}

		}
				
			
	}