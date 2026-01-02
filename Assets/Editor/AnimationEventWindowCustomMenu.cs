#if UNITY_EDITOR
using UnityEngine;
using System;
using UnityEditor;

public static class AnimationEventWindowCustomMenu
{
    static void setAnimationWindowEventSendMessageOption(SendMessageOptions sendMessageOption)
    {
        Type typeAnimationWindowEvent = Selection.activeObject.GetType();
        AnimationClip clip =
            (AnimationClip)typeAnimationWindowEvent.GetField("clip") //this field is public
            .GetValue(Selection.activeObject)
        ;
        AnimationEvent[] aAnimationEvent = AnimationUtility.GetAnimationEvents(clip);
        for (int i = 0; i < Selection.objects.Length; ++i)
        {
            int eventIndex =
                (int)typeAnimationWindowEvent.GetField("eventIndex").GetValue(Selection.objects[i]);
            aAnimationEvent[eventIndex].messageOptions = sendMessageOption;
        }
        AnimationUtility.SetAnimationEvents(clip, aAnimationEvent);
    }

    static SendMessageOptions getAnimationWindowEventSendMessageOption()
    {
        Type typeAnimationWindowEvent = Selection.activeObject.GetType();
        AnimationClip clip =
            (AnimationClip)typeAnimationWindowEvent.GetField("clip") //this field is public
            .GetValue(Selection.activeObject)
        ;
        int eventIndexActive =
            (int)typeAnimationWindowEvent.GetField("eventIndex").GetValue(Selection.activeObject);
        AnimationEvent[] aAnimationEvent = AnimationUtility.GetAnimationEvents(clip);
        return aAnimationEvent[eventIndexActive].messageOptions;
    }

    [MenuItem("CONTEXT/AnimationWindowEvent/Set Require Receiver")]
    static void animationWindowEventSetRequireReceiver()
    {
        setAnimationWindowEventSendMessageOption(SendMessageOptions.RequireReceiver);
    }

    [MenuItem("CONTEXT/AnimationWindowEvent/Set Require Receiver", true)]
    static bool animationWindowEventSetRequireReceiverValidate()
    {
        return getAnimationWindowEventSendMessageOption() != SendMessageOptions.RequireReceiver;
    }

    [MenuItem("CONTEXT/AnimationWindowEvent/Clear Require Receiver")]
    static void animationWindowEventClearRequireReceiver()
    {
        setAnimationWindowEventSendMessageOption(SendMessageOptions.DontRequireReceiver);
    }

    [MenuItem("CONTEXT/AnimationWindowEvent/Clear Require Receiver", true)]
    static bool animationWindowEventClearRequireReceiverValidate()
    {
        return getAnimationWindowEventSendMessageOption() != SendMessageOptions.DontRequireReceiver;
    }
}
#endif
