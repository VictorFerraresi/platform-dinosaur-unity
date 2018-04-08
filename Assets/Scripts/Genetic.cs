using UnityEngine;
using System;
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
}
	