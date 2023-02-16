using UnityEngine;

public class AdaptationManager {

	private bool usedHelp = false;
	private bool usedMagicNow = false;
	private bool usedMagicBefore = false;
	private bool characterWrittenNow = false;
	private bool characterWrittenBefore = false;

	public AdaptationManager(){
	}

	public void Reset(){
		usedHelp = false;
		usedMagicBefore = usedMagicNow;
		usedMagicNow = false;
		characterWrittenBefore = characterWrittenNow;
		characterWrittenNow = false;
	}

	public void OpenedCanvas(){
		Reset();
	}

	public void ClosedCanvas(){

	}

	public void UsedHelp(){
		usedHelp = true;
	}

	public void CharacterWritten(string character, float time){
		characterWrittenNow = true;
		//if (usedHelp || (!usedMagicBefore && characterWrittenBefore)){ // If player uses help or reopens canvas without using magic
		if (usedHelp){
			Global.GD.kt.UpdateKnowledge(character, false, 0.1f); // decrease confidence by 0.1
			//usedHelp = false; // Not necessary if we only allow the player to write one character on the canvas before closing it
		}
	}

	public void UsedMagic(){
		usedMagicNow = true;
	}

	public void EnemyKilled(string type){

	}

}