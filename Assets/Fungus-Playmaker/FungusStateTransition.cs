using UnityEngine;
using System.Collections;
using Fungus;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[CommandInfo("Playmaker",
             "Global State Transition",
             "Send an event from Fungus to Playmaker")]

public class FungusStateTransition :Command {

    public PlayMakerFSM FSM;
    private string destination;
    private FsmString destString;
    public string destinationEvent;
    private FsmString destEvent;

	// Use this for initialization
	public override void OnEnter() {
        /*
        destString.Value = destination;
        destEvent.Value = destinationEvent;
        FsmString FunString = FSM.FsmVariables.GetFsmString(destString.Value);
        FunString.Value = "Successfully pushed String to playmaker variable";
        //FSM.SendEvent(destEvent.Value);
        */
        FSM.SendEvent(destinationEvent);
        Continue();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CallMe()
    {
        Debug.Log("Hi there!");
    }
}
