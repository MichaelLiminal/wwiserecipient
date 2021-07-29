using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnityEngine.AddComponentMenu("Wwise/CustomEvent")]
[UnityEngine.RequireComponent(typeof(AkGameObj))]
public class NewBehaviourScript : MonoBehaviour
{
    /// Replacement action.  See AK::SoundEngine::ExecuteEventOnAction()
    public AkActionOnEventType actionOnEventType = AkActionOnEventType.AkActionOnEventType_Stop;

    /// Fade curve to use with the new Action.  See AK::SoundEngine::ExecuteEventOnAction()
    public AkCurveInterpolation curveInterpolation = AkCurveInterpolation.AkCurveInterpolation_Linear;
    public float transitionDuration;
    public AK.Wwise.Event data = new AK.Wwise.Event();

    [System.Serializable]
    public class CallbackData
    {
        public AK.Wwise.CallbackFlags Flags;
        public string FunctionName;
        public UnityEngine.GameObject GameObject;

        public void CallFunction(AkEventCallbackMsg eventCallbackMsg)
        {
            if (((uint)eventCallbackMsg.type & Flags.value) != 0 && GameObject)
                GameObject.SendMessage(FunctionName, eventCallbackMsg);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hey());
    }

    IEnumerator hey() 
    {
        while(true)
        {
            print(data.Name);
            AkSoundEngine.PostEvent(data.Name, gameObject);
            yield return new WaitForSeconds(2f);
        }
    }
}
