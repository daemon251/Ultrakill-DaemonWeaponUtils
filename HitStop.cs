using BepInEx.Logging;
using HarmonyLib;
using DaemonWeaponUtilsPlugin;

public class HitstopPatches
{
    [HarmonyPatch(typeof(TimeController), "TrueStop")]
    public class TrueStopPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ref float length)
        {
            length *= ModConfig.truestopMult + NextHitStopMultAdd;
            NextHitStopMultAdd = 0f;
        }
    }

    public static float NextHitStopMultAdd = 0.0f;

    [HarmonyPatch(typeof(ShotgunHammer), "Impact")]
    public class ShotgunHammerImpactPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            NextHitStopMultAdd = ModConfig.jackhammerAddTimeStop;
            //Plugin.logger.LogInfo(NextHitStopMultAdd);
        }

        //hitstop happens between these

        [HarmonyPostfix]
        private static void Postfix() //now reset it
        {
            NextHitStopMultAdd = 0.0f;
        }
    }

    [HarmonyPatch(typeof(TimeController), "HitStop")]
    public class HitStopPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ref float length)
        {
            length *= ModConfig.hitstopMult;
        }
    }

    [HarmonyPatch(typeof(TimeController), "SlowDown")]
    public class SlowDownPatch
    {   
        [HarmonyPrefix]
        private static void Prefix(ref float amount)
        {
            amount *= ModConfig.slowdownMult;
        }
    }
}