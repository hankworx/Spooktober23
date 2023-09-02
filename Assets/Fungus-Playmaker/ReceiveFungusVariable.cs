//A custom PlayMaker action to copy a PlayMaker variable to a Fungus command.
using UnityEngine;
using System.Collections;
using Fungus;
using HutongGames.Playmaker.Actions;
using HutongGames.PlayMaker;

namespace HutongGames.Playmaker.Actions
{

    [ActionCategory(ActionCategory.ScriptControl)]
    [PlayMaker.Tooltip("Copy a Fungus variable value to a Playmaker variable")]

    public class ReceiveFungusVariable : FsmStateAction
    { //Need Monobehaviour to attach to game object

        //[ActionCategory(ActionCategory.ScriptControl)]
        [RequiredField]

        //   [CheckForComponent(typeof(ChangeFungusVariable))]

        public FsmOwnerDefault gameObject;//Game object the script is on

        //Add more variable types as needed
        public FsmString stringDestination; //Name of the variable 
        public FsmString pmString; //Variable to be transferred to Fungus
        public FsmString intDestination;
        public FsmInt pmInt;
        public FsmString floatDestination;
        public FsmFloat pmFloat;
        public FsmString boolDestination;
        public FsmBool pmBool;
        private string dest;

        public FsmBool everyFrame;

        public Flowchart flowchart;

        // ChangeFungusVariable theScript; //Custom Variable with script type; might not be needed
        // Use this for initialization
        public override void Reset()
        {

            gameObject = null;
            pmString = null;

            everyFrame = true;
        }

        // Update is called once per frame
        public override void OnEnter()
        {
            // pmString.Value = flowchart.GetStringVariable("myName");
            if (stringDestination != null)
            {
                changeString(stringDestination, pmString);
            }
            //flowchart.SetStringVariable("myName", pmString.Value);
            //flowchart.SetStringVariable(destination.Value, pmString.Value);
            if (intDestination != null)
            {
                changeInt(intDestination, pmInt);
            }
            //flowchart.SetFloatVariable("myFloat", pmFloat.Value);
            if (floatDestination != null)
            {
                changeFloat(floatDestination, pmFloat);
            }
            //flowchart.SetBooleanVariable("myBool", pmBool.Value);
            if (boolDestination != null)
            {
                changeBoolean(boolDestination, pmBool);
            }

            //   var go = Fsm.GetOwnerDefaultTarget(gameObject);

            //  theScript = go.GetComponent<ChangeFungusVariable>();

            if (!everyFrame.Value)
            {
                Store_Variable();
                Finish();
            }
        }
        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                Store_Variable();
            }
        }

        public void Store_Variable() //Change name to StoreVariables
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            //pmString.Value = theScript.PMString;
            //  theScript.PMString = pmString.Value;
        }

        public FsmString changeString(FsmString destination, FsmString pmString)
        {
            dest = destination.Value;
            //flowchart.SetStringVariable(dest, pmString.Value);
            pmString.Value = flowchart.GetStringVariable(dest);
            return pmString;
        }

        public FsmInt changeInt(FsmString destination, FsmInt pmInt)
        {
            dest = destination.Value;
            //flowchart.SetIntegerVariable(dest, pmInt.Value);
            pmInt.Value = flowchart.GetIntegerVariable(dest);
            return pmInt;
        }

        public FsmFloat changeFloat(FsmString destination, FsmFloat pmFloat)
        {
            dest = destination.Value;
            //flowchart.SetFloatVariable(dest, pmFloat.Value);
            pmFloat.Value = flowchart.GetFloatVariable(dest);
            return pmFloat;
        }

        public FsmBool changeBoolean(FsmString destination, FsmBool pmBool)
        {
            dest = destination.Value;
            //flowchart.SetBooleanVariable(dest, pmBool.Value);
            pmBool.Value = flowchart.GetBooleanVariable(dest);
            return pmBool;
        }
    }
}


