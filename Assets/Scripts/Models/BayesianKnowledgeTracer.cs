using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static System.Math;
using UnityEngine;

[Serializable]
public class BayesianKnowledgeTracer {
	// private Dictionary<string, Dictionary<string, float>> KCDict;
	// private Dictionary<string, float> kt;
	private Hashtable KCDict;
	private Hashtable kt;

	// :param KCDict: dictionary with key: name of the KC, and value: dictionary containing p(L) and p(T) probabilities
	public BayesianKnowledgeTracer(Hashtable KCDict){
		this.KCDict = KCDict;
		this.Reset();
	}

	public void Reset(){
		
		// (Re)initialisation of the knowledge tracer with prior probabilities.
		this.kt = new Hashtable();
		foreach (string kc in this.KCDict.Keys){
			this.kt[kc] = ((Hashtable)KCDict[kc])["prior"];
		}
	}

/*	// :param kc_attempted: name of the KC 
	// :param success: boolean to indicate whether the question was answered correctly
	// :return: Change in probability of mastery after attempt
	public float UpdateKnowledge(string kc_attempted, bool success){

		float p_mastered = this.kt[kc_attempted];
		float p_obs = 0;

		if (success) {
			float p_success = p_mastered * (1 - this.KCDict[kc_attempted]["slip"]);
			float p_guess = (1 - p_mastered) * this.KCDict[kc_attempted]["guess"];
			p_obs = p_success / p_success + p_guess;
		} else {
			float p_slip = p_mastered * this.KCDict[kc_attempted]["slip"];
			float p_failedguess = (1 - p_mastered) * (1 - this.KCDict[kc_attempted]["guess"]);
			p_obs = p_slip / p_slip + p_failedguess;
		}

		this.kt[kc_attempted] = Min(1, p_obs + (1 - p_obs) * this.KCDict[kc_attempted]["learn"]);

		return this.kt[kc_attempted] - p_mastered; // difference between mastery before and after resource was used
	}*/

	public void UpdateKnowledge(string KC, bool success, float value = -1f){
		if (success){
			if (value == -1f){
				value = (float)((Hashtable)KCDict[KC])["learn"];
			}
			this.kt[KC] = Min(1, (float)this.kt[KC] + value);
			Debug.Log("increased "+KC+" by"+value);
		} else {
			if (value == -1f){
				this.kt[KC] = Max(0, (float)this.kt[KC] - (float)((Hashtable)KCDict[KC])["learn"]);
			} else {
				this.kt[KC] = Max(0, (float)this.kt[KC] - value);
				Debug.Log("decreased "+KC+" by"+value);
			}
		}
	}

	// Careful: this exposes the KT object itself
	public List<float> GetCurrentState()
	{
		string toPrint = "";
		foreach (string kc in this.KCDict.Keys){
			toPrint += kc + ": " + this.kt[kc] + ", ";
		}
		Debug.Log(toPrint);
		return kt.Values.Cast<float>().ToList();
	}

	public float GetMasteryOf(string kc){
		return (float)kt[kc];
	}
}