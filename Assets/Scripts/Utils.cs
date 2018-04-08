using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Utils
{
	public static int actualGenome = 0;

	public static List<Genome> loadAllGenomes() {
		List<Genome> genomes = new List<Genome> ();
		foreach(string file in Directory.GetFiles("Genomes/CrossedOvers", "*.json")) {
			String json = File.ReadAllText(file);
			genomes.Add(JsonUtility.FromJson<Genome>(json));
		}

		if (genomes.Count == 0) {
			foreach(string file in Directory.GetFiles("Genomes", "*.json")) {
				String json = File.ReadAllText(file);
				genomes.Add(JsonUtility.FromJson<Genome>(json));
				Utils.actualGenome = 5;
			}
		}

		return genomes;
	}

	public static void clearCrossOversFolder() {
		foreach(string file in Directory.GetFiles("Genomes/CrossedOvers", "*.json")) {
			File.Delete(file);
		}
	}

	public static void clearGenomesFolder() {
		foreach(string file in Directory.GetFiles("Genomes", "*.json")) {
			File.Delete(file);
		}
	}

	public static List<Genome> naturalSelection(List<Genome> genomes, int amount) {
		List<Genome> bestGenomes = new List<Genome> ();

		genomes.Sort((a, b) => b.fitness.CompareTo (a.fitness));

		for (int i = 0; i < amount; i++) { 
			bestGenomes.Add (genomes [i]);
		}
			
		return bestGenomes;
	}

	public static void persistInJson(Genome g, String path) {		
		String filename = path + DateTime.Now.ToString("dd-MM-yy-HHmmss") + ".json";
		if (File.Exists(filename))
		{
			Debug.Log(filename+" already exists.");
			return;
		}
		var sr = File.CreateText(filename);

		sr.WriteLine (JsonUtility.ToJson (g));
		sr.Close();
	}
}
