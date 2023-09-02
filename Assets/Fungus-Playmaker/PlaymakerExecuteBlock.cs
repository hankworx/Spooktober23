//A custom PlayMaker action to copy a PlayMaker variable to a Fungus command.
using UnityEngine;
using System.Collections;
using Fungus;
using HutongGames.Playmaker.Actions;
using HutongGames.PlayMaker;

//Find out why this holds the last entered block name and executes it when blockName is empty
namespace HutongGames.Playmaker.Actions
{

    [ActionCategory(ActionCategory.ScriptControl)]
    [PlayMaker.Tooltip("Execute a named Fungus Block")]

    public class PlaymakerExecuteBlock : FsmStateAction
    { //Need Monobehaviour to attach to game object

        //[ActionCategory(ActionCategory.ScriptControl)]
        [RequiredField]

      //  [CheckForComponent(typeof(ChangeFungusVariable))]

        public FsmOwnerDefault gameObject;//Game object the script is on

        //Add more variable types as needed
        public FsmString blockName; //Name of Block to be execueted

        public FsmBool everyFrame;

        public Flowchart flowchart;

        // ChangeFungusVariable theScript; //Custom Variable with script type
        // Use this for initialization
        public override void Reset()
        {

            gameObject = null;
            blockName = null;

            everyFrame = true;
        }

        // Update is called once per frame
        public override void OnEnter()
        {

            flowchart.ExecuteBlock(blockName.Value);


           // var go = Fsm.GetOwnerDefaultTarget(gameObject);

            //  theScript = go.GetComponent<ChangeFungusVariable>();

            if (!everyFrame.Value)
            {
                StoreVariable();
                Finish();
            }
        }
        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                StoreVariable();
            }
        }

        public void StoreVariable() //Change name to StoreVariables
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            //pmString.Value = theScript.PMString;
            //   theScript.PMString = blockName.Value;
        }
    }
}