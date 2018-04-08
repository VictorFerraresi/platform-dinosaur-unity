using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Utils
{
	public static List<Genome> loadAllGenomes() {
		List<Genome> genomes = new List<Genome> ();
		foreach(string file in Directory.GetFiles("Genomes", "*.json")) {
			String json = File.ReadAllText(file);
			genomes.Add(JsonUtility.FromJson<Genome>(json));
		}

		return genomes;
	}
}
