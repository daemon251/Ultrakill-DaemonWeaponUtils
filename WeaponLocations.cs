using UnityEngine;
using DaemonWeaponUtilsPlugin;
using HarmonyLib;
using UnityEngine.SceneManagement;
using System;
public class WeaponLocations
{
    public enum WeaponEnum
    {
        None,
        Piercer_Revolver, Marksman_Revolver, Sharpshooter_Revolver,
        Core_Eject_Shotgun, Pump_Charge_Shotgun, Sawed_On_Shotgun,
        Attractor_Nailgun, Overheat_Nailgun, Jumpstart_Nailgun,
        Electric_Railcannon, Screwdriver, Malicious_Railcannon,
        Freezeframe_Rocket_Launcher, SRS_Rocket_Launcher, Firestarter_Rocket_Launcher
    }
    public static WeaponEnum convertWeaponToWeaponEnum(GameObject weapon)
    {
        WeaponEnum weaponEnum = WeaponEnum.None;
        if (weapon.GetComponent<Revolver>() != null)
        {
            if (weapon.GetComponent<Revolver>().gunVariation == 0) { weaponEnum = WeaponEnum.Piercer_Revolver; }
            if (weapon.GetComponent<Revolver>().gunVariation == 1) { weaponEnum = WeaponEnum.Marksman_Revolver; }
            if (weapon.GetComponent<Revolver>().gunVariation == 2) { weaponEnum = WeaponEnum.Sharpshooter_Revolver; }
        }
        if (weapon.GetComponent<Shotgun>() != null)
        {
            if (weapon.GetComponent<Shotgun>().variation == 0) { weaponEnum = WeaponEnum.Core_Eject_Shotgun; }
            if (weapon.GetComponent<Shotgun>().variation == 1) { weaponEnum = WeaponEnum.Pump_Charge_Shotgun; }
            if (weapon.GetComponent<Shotgun>().variation == 2) { weaponEnum = WeaponEnum.Sawed_On_Shotgun; }
        }
        if (weapon.GetComponent<ShotgunHammer>() != null)
        {
            if (weapon.GetComponent<ShotgunHammer>().variation == 0) { weaponEnum = WeaponEnum.Core_Eject_Shotgun; }
            if (weapon.GetComponent<ShotgunHammer>().variation == 1) { weaponEnum = WeaponEnum.Pump_Charge_Shotgun; }
            if (weapon.GetComponent<ShotgunHammer>().variation == 2) { weaponEnum = WeaponEnum.Sawed_On_Shotgun; }
        }
        if (weapon.GetComponent<Nailgun>() != null)
        {
            if (weapon.GetComponent<Nailgun>().variation == 1) { weaponEnum = WeaponEnum.Attractor_Nailgun; }
            if (weapon.GetComponent<Nailgun>().variation == 0) { weaponEnum = WeaponEnum.Overheat_Nailgun; }
            if (weapon.GetComponent<Nailgun>().variation == 2) { weaponEnum = WeaponEnum.Jumpstart_Nailgun; }
        }
        if (weapon.GetComponent<Railcannon>() != null)
        {
            if (weapon.GetComponent<Railcannon>().variation == 0) { weaponEnum = WeaponEnum.Electric_Railcannon; }
            if (weapon.GetComponent<Railcannon>().variation == 1) { weaponEnum = WeaponEnum.Screwdriver; }
            if (weapon.GetComponent<Railcannon>().variation == 2) { weaponEnum = WeaponEnum.Malicious_Railcannon; }
        }
        if (weapon.GetComponent<RocketLauncher>() != null)
        {
            if (weapon.GetComponent<RocketLauncher>().variation == 0) { weaponEnum = WeaponEnum.Freezeframe_Rocket_Launcher; }
            if (weapon.GetComponent<RocketLauncher>().variation == 1) { weaponEnum = WeaponEnum.SRS_Rocket_Launcher; }
            if (weapon.GetComponent<RocketLauncher>().variation == 2) { weaponEnum = WeaponEnum.Firestarter_Rocket_Launcher; }
        }
        return weaponEnum;
    }
    public static void UpdateWeaponLocations(GameObject weapon)
    {
       // ModConfig.WeaponDefaultLocations();
        int index = -1;
        int middleHanded = 0;
        if (MonoSingleton<PrefsManager>.Instance.GetInt("weaponHoldPosition") == 1 && (!(bool) (UnityEngine.Object) MonoSingleton<PowerUpMeter>.Instance || (double) MonoSingleton<PowerUpMeter>.Instance.juice <= 0.0))
        {
            middleHanded = 1;
        }
        if (weapon.GetComponent<Revolver>() != null && weapon.GetComponent<Revolver>().wpos != null)
        {
            Revolver revolver = weapon.GetComponent<Revolver>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Piercer_Revolver) {index = 0;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Marksman_Revolver) {index = 1;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sharpshooter_Revolver) {index = 2;}

            Vector3 pos = ModConfig.revolverPosition[middleHanded];
            Vector3 ang = ModConfig.revolverAng[middleHanded];
            Vector3 size = ModConfig.shotgunSize[middleHanded];

            if(revolver.altVersion) {pos = ModConfig.revolverAltPosition[middleHanded]; ang = ModConfig.revolverAltAng[middleHanded]; size = ModConfig.revolverAltSize[middleHanded];}

            revolver.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            revolver.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ang.x + ModConfig.globalAngle.x,
                                                                        ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].y + ang.z + -ModConfig.globalAngle.y);
            revolver.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * size.x * ModConfig.globalScale.z,
                                                                            ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].x * size.z * ModConfig.globalScale.x);

            revolver.transform.localPosition = revolver.wpos.currentDefault; 
            revolver.transform.localEulerAngles = revolver.wpos.defaultRot;
            revolver.transform.localScale = revolver.wpos.defaultScale;
        }
        if (weapon.GetComponent<Shotgun>() != null && weapon.GetComponent<Shotgun>().wpos != null)
        {
            Shotgun shotgun = weapon.GetComponent<Shotgun>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Core_Eject_Shotgun) {index = 3;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Pump_Charge_Shotgun) {index = 4;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sawed_On_Shotgun) {index = 5;}

            Vector3 pos = ModConfig.shotgunPosition[middleHanded];
            Vector3 ang = ModConfig.shotgunAng[middleHanded];
            Vector3 size = ModConfig.shotgunSize[middleHanded];

            shotgun.wpos.currentDefault = new Vector3(  ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            shotgun.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ang.x + ModConfig.globalAngle.x,
                                                                        ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].y + ang.z + -ModConfig.globalAngle.y);
            shotgun.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * size.x * ModConfig.globalScale.z,
                                                                            ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].x * size.z * ModConfig.globalScale.x);

            shotgun.transform.localPosition = shotgun.wpos.currentDefault; 
            shotgun.transform.localEulerAngles = shotgun.wpos.defaultRot;
            shotgun.transform.localScale = shotgun.wpos.defaultScale;
        }
        if (weapon.GetComponent<ShotgunHammer>() != null && weapon.GetComponent<ShotgunHammer>().wpos != null)
        {
            ShotgunHammer shotgunHammer = weapon.GetComponent<ShotgunHammer>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Core_Eject_Shotgun) {index = 3;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Pump_Charge_Shotgun) {index = 4;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sawed_On_Shotgun) {index = 5;}

            Vector3 pos = ModConfig.shotgunAltPosition[middleHanded];
            Vector3 ang = ModConfig.shotgunAltAng[middleHanded];
            Vector3 size = ModConfig.shotgunAltSize[middleHanded];

            shotgunHammer.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                            ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                            ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            shotgunHammer.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ang.x + ModConfig.globalAngle.x,
                                                                                ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                                -ModConfig.weaponAngles[index].y + ang.z + -ModConfig.globalAngle.y);
            shotgunHammer.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * size.x * ModConfig.globalScale.z,
                                                                                ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                                ModConfig.weaponScales[index].x * size.z * ModConfig.globalScale.x);

            shotgunHammer.transform.localPosition = shotgunHammer.wpos.currentDefault; 
            shotgunHammer.transform.localEulerAngles = shotgunHammer.wpos.defaultRot;
            shotgunHammer.transform.localScale = shotgunHammer.wpos.defaultScale;
        }
        if (weapon.GetComponent<Nailgun>() != null && weapon.GetComponent<Nailgun>().wpos != null)
        {
            Nailgun nailgun = weapon.GetComponent<Nailgun>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Attractor_Nailgun) { index = 6; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Overheat_Nailgun) { index = 7; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Jumpstart_Nailgun) { index = 8; }

            Vector3 pos = ModConfig.nailgunPosition[middleHanded];
            Vector3 ang = ModConfig.nailgunAng[middleHanded];
            Vector3 size = ModConfig.nailgunSize[middleHanded];
            if(nailgun.altVersion) {pos = ModConfig.nailgunAltPosition[middleHanded]; ang = ModConfig.nailgunAltAng[middleHanded]; size = ModConfig.nailgunAltSize[middleHanded];}

            nailgun.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            nailgun.GetComponent<WeaponPos>().defaultRot = new Vector3(-ModConfig.weaponAngles[index].y + ang.x + -ModConfig.globalAngle.y,
                                                                        ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].x + ang.z + -ModConfig.globalAngle.x);
            nailgun.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].x * size.x * ModConfig.globalScale.x,
                                                                            ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].z * size.z * ModConfig.globalScale.z);

            nailgun.transform.localPosition = nailgun.wpos.currentDefault; 
            nailgun.transform.localEulerAngles = nailgun.wpos.defaultRot;
            nailgun.transform.localScale = nailgun.wpos.defaultScale;
        }
        if (weapon.GetComponent<Railcannon>() != null && weapon.GetComponent<Railcannon>().wpos != null)
        {
            Railcannon railcannon = weapon.GetComponent<Railcannon>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Electric_Railcannon) {index = 9;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Screwdriver) {index = 10;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Malicious_Railcannon) {index = 11;}

            Vector3 pos = ModConfig.railcannonPosition[middleHanded];
            Vector3 ang = ModConfig.railcannonAng[middleHanded];
            Vector3 size = ModConfig.railcannonSize[middleHanded];

            railcannon.wpos.currentDefault = new Vector3(   ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                            ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                            ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            railcannon.GetComponent<WeaponPos>().defaultRot = new Vector3(  -ModConfig.weaponAngles[index].y + ang.x + -ModConfig.globalAngle.y,
                                                                            ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                            -ModConfig.weaponAngles[index].x + ang.z + -ModConfig.globalAngle.x);
            railcannon.GetComponent<WeaponPos>().defaultScale = new Vector3(    ModConfig.weaponScales[index].x * size.x * ModConfig.globalScale.x,
                                                                                ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                                ModConfig.weaponScales[index].z * size.z * ModConfig.globalScale.z);

            railcannon.transform.localPosition = railcannon.wpos.currentDefault;
            railcannon.transform.localEulerAngles = railcannon.wpos.defaultRot;
            railcannon.transform.localScale = railcannon.wpos.defaultScale;
        }
        if (weapon.GetComponent<RocketLauncher>() != null && weapon.GetComponent<RocketLauncher>().wpos != null)
        {
            RocketLauncher rocketLauncher = weapon.GetComponent<RocketLauncher>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Freezeframe_Rocket_Launcher) {index = 12;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.SRS_Rocket_Launcher) {index = 13;}
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Firestarter_Rocket_Launcher) {index = 14;}

            Vector3 pos = ModConfig.rocketLauncherPosition[middleHanded];
            Vector3 ang = ModConfig.rocketLauncherAng[middleHanded];
            Vector3 size = ModConfig.rocketLauncherSize[middleHanded];

            rocketLauncher.wpos.currentDefault = new Vector3(   ModConfig.weaponOffsets[index].x + pos.x + ModConfig.globalOffset.x,
                                                                ModConfig.weaponOffsets[index].y + pos.y + ModConfig.globalOffset.y,
                                                                ModConfig.weaponOffsets[index].z + pos.z + ModConfig.globalOffset.z);
            rocketLauncher.GetComponent<WeaponPos>().defaultRot = new Vector3(  -ModConfig.weaponAngles[index].y + ang.x + -ModConfig.globalAngle.y,
                                                                                ModConfig.weaponAngles[index].z + ang.y + ModConfig.globalAngle.z,
                                                                                -ModConfig.weaponAngles[index].x + ang.z + -ModConfig.globalAngle.x);
            rocketLauncher.GetComponent<WeaponPos>().defaultScale = new Vector3(    ModConfig.weaponScales[index].x * size.x * ModConfig.globalScale.x,
                                                                                    ModConfig.weaponScales[index].y * size.y * ModConfig.globalScale.y,
                                                                                    ModConfig.weaponScales[index].z * size.z * ModConfig.globalScale.z);

            rocketLauncher.transform.localPosition = rocketLauncher.wpos.currentDefault;
            rocketLauncher.transform.localEulerAngles = rocketLauncher.wpos.defaultRot;
            rocketLauncher.transform.localScale = rocketLauncher.wpos.defaultScale;
        }
    }


    public static bool blueArmVisible = true;
    public static bool redArmVisible = true;
    public static bool greenArmVisible = true;

    [HarmonyPatch(typeof(Punch), "Start")]
    public class PunchStartPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            changeArmVisiblity();
        }
    }

    public static void changeArmVisiblity()
    {
        if(MonoSingleton<FistControl>.Instance != null)
        {
            //GameObject mainCameraObj = guns.transform.parent.gameObject;
            GameObject punch = MonoSingleton<FistControl>.Instance.gameObject;//mainCameraObj.transform.GetChild(3).gameObject;

            GameObject hookArm = punch.transform.GetChild(1).gameObject;
            GameObject blueArm = punch.transform.GetChild(2).gameObject;
            GameObject redArm = punch.transform.GetChild(3).gameObject;

            GameObject blueArmObj1 = blueArm.transform.GetChild(0).gameObject;
            GameObject feedBacker = blueArmObj1.transform.GetChild(1).gameObject;
            SkinnedMeshRenderer blueArmRenderer = feedBacker.GetComponent<SkinnedMeshRenderer>();
            blueArmRenderer.enabled = blueArmVisible;

            GameObject redArm__lp = redArm.transform.GetChild(0).gameObject;
            SkinnedMeshRenderer redArmRenderer = redArm__lp.GetComponent<SkinnedMeshRenderer>();
            redArmRenderer.enabled = redArmVisible;

            GameObject greenArm = hookArm.transform.GetChild(0).gameObject;
            GameObject greenArmFinal = greenArm.transform.GetChild(0).gameObject;
            SkinnedMeshRenderer greenArmRenderer = greenArmFinal.GetComponent<SkinnedMeshRenderer>();
            greenArmRenderer.enabled = greenArmVisible;
        }
    }

    /*[HarmonyPatch(typeof(SceneHelper), "OnSceneLoaded")]
    public class SceneHelperPatch
    {
        [HarmonyPostfix]
        private static void Prefix(ref float length)
        {
            
            
        }
    }*/
}