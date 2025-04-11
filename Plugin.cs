using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using UnityEngine;
using HarmonyLib;
using System;
using BepInEx.Logging;

//TODO
//hit particles
//clean code
//srs
//railcannon particle color
//srs charge colors
//explosion colors
//rocket body color
//swordsmachine shotgun color?


//piercer and sharpshooter altfire color attribute
//shotgun shooting more efficent
//PulseBlack and PulseWhite now work with revolver muzzleflashes properly
//increased limit of custom crosshairs to ten 

namespace DaemonWeaponUtilsPlugin;

[BepInPlugin("DaemonWeaponUtils", "DaemonWeaponUtils", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static bool modEnabled = true;
    private readonly Harmony harmony = new Harmony("daemon.DaemonWeaponUtils");
    public static ManualLogSource logger;
    private void Awake()
    {
        logger = new ManualLogSource("DaemonWeaponUtils"); BepInEx.Logging.Logger.Sources.Add(logger);
        ModConfig.CreateConfig();
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        Logger.LogInfo("Plugin DaemonWeaponUtils is loaded!");
    }
    public static string DefaultParentFolder = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";

    public void Start()
    {
        Colors.LoadImages();
        Crosshairs.LoadImages();
    }
    public static GameObject weapon = null;
    
    public static bool IsMenu()
    {
        if(MonoSingleton<OptionsManager>.Instance != null && !MonoSingleton<OptionsManager>.Instance.paused && !MonoSingleton<FistControl>.Instance.shopping && GameStateManager.Instance != null && !GameStateManager.Instance.PlayerInputLocked)
        {
            return false;
        }
        return true;
    }
    private void OnGUI()
    {
        if(modEnabled == true && weapon != null)
        {
            int index = MonoSingleton<GunControl>.Instance.currentSlotIndex;
            int variation = -1;
            if(index == 1) {variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Revolver>().gunVariation;}
            else if(index == 2) 
            {
                if(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>() != null)
                {
                    variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().variation;
                }
                if(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<ShotgunHammer>() != null)
                {
                    variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<ShotgunHammer>().variation;
                }
            }
            else if(index == 3) {variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Nailgun>().variation;}
            else if(index == 4) {variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Railcannon>().variation;}
            else if(index == 5) {variation = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<RocketLauncher>().variation;}
            //fixes nailgun order jank
            if(index == 3 && variation == 0) {variation = 1;}
            else if(index == 3 && variation == 1) {variation = 0;}
            Crosshairs.DrawCrosshair(index, variation);
        }
    }
    
    private void Update()
    {
        if(modEnabled == false) {return;}
        if(MonoSingleton<GunControl>.Instance != null)
        {
            weapon = MonoSingleton<GunControl>.Instance.currentWeapon;
            if(Colors.ColoredPiercerAltTexture == null) {Colors.MakeColoredPiercerAltTexture(Color.white);}
        }
        else{weapon = null;}
        if(weapon != null)
        {
            WeaponLocations.UpdateWeaponLocations(weapon);

            DualWield[] dualwields = MonoSingleton<GunControl>.Instance.GetComponentsInChildren<DualWield>();
            for(int i = 0; i < dualwields.Length; i++)
            {
                if(dualwields[i].currentWeapon != null)
                {
                    WeaponLocations.UpdateWeaponLocations(dualwields[i].currentWeapon);
                }
            }
            Colors.OtherColorEffects();    
        }
        Colors.ColorEverything();    
    }
}
