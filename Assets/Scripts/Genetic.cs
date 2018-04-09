using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Genetic
{

	public static int calculateFitness(List<Jumped> jumps, List<Cactus> cactus) {
		int jumpedCactus = 0;
		Cactus nextCactus = cactus.Find(c => c.position.x == GameObject.Find("cactus_1_first").transform.position.x);

		foreach (Jumped jump in jumps) {	
			if (nextCactus != jump.nearestCactus) {
				nextCactus = jump.nearestCactus;
				jumpedCactus++;
			}
		}

		return jumpedCactus;
	}

	public static Genome crossOver(Genome a, Genome b) {
		int greaterSize = a.jumps.Count > b.jumps.Count ? a.jumps.Count : b.jumps.Count;

		Genome c = new Genome {
			fitness = -1,
			jumps = new List<Jumped>()
		};				

		for (int i = 0; i < greaterSize; i++) {			
			int rand = UnityEngine.Random.Range (0, 2);
				
			if ((rand == 1 && i < b.jumps.Count) || (rand == 0 && i >= a.jumps.Count)) {
				c.jumps.Add (b.jumps [i]);
			} else {
				c.jumps.Add (a.jumps [i]);
			}
		}

		return c;
	}

	public static Genome mutate(Genome a) {		
		int rand = UnityEngine.Random.Range (0, a.jumps.Count);
		float avg = 0;

		foreach (Jumped j in a.jumps) {
			avg += j.distanceToNearestCactus.x;
		}

		if (avg == 0) {
			avg = 0.7f;
		} else {
			avg /= a.jumps.Count;
		}
		Jumped randJump = new Jumped {
			nearestCactus = null,
			distanceToNearestCactus = new Vector2(avg + UnityEngine.Random.Range(-0.5f, 1.0f), 0.0f)
		};

		a.jumps.Insert (rand, randJump);

		return a;
	}
}
	