using UnityEngine;
using DaemonWeaponUtilsPlugin;
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
        ModConfig.WeaponDefaultLocations();
        int index = -1;
        if (weapon.GetComponent<Revolver>() != null && weapon.GetComponent<Revolver>().wpos != null)
        {
            Revolver revolver = weapon.GetComponent<Revolver>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Piercer_Revolver) { index = 0; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Marksman_Revolver) { index = 1; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sharpshooter_Revolver) { index = 2; }
            revolver.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            revolver.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ModConfig.weaponDefaultAngles[index].x + ModConfig.globalAngle.x,
                                                                        ModConfig.weaponAngles[index].z + ModConfig.weaponDefaultAngles[index].y + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].y + ModConfig.weaponDefaultAngles[index].z + -ModConfig.globalAngle.y);
            revolver.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * ModConfig.weaponDefaultScales[index].x * ModConfig.globalScale.z,
                                                                            ModConfig.weaponScales[index].y * ModConfig.weaponDefaultScales[index].y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].x * ModConfig.weaponDefaultScales[index].z * ModConfig.globalScale.x);

            revolver.transform.localPosition = revolver.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            revolver.transform.localEulerAngles = revolver.wpos.defaultRot;
            revolver.transform.localScale = revolver.wpos.defaultScale;
        }
        if (weapon.GetComponent<Shotgun>() != null && weapon.GetComponent<Shotgun>().wpos != null)
        {
            Shotgun shotgun = weapon.GetComponent<Shotgun>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Core_Eject_Shotgun) { index = 3; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Pump_Charge_Shotgun) { index = 4; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sawed_On_Shotgun) { index = 5; }

            shotgun.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            shotgun.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ModConfig.weaponDefaultAngles[index].x + ModConfig.globalAngle.x,
                                                                        ModConfig.weaponAngles[index].z + ModConfig.weaponDefaultAngles[index].y + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].y + ModConfig.weaponDefaultAngles[index].z + -ModConfig.globalAngle.y);
            shotgun.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * ModConfig.weaponDefaultScales[index].x * ModConfig.globalScale.z,
                                                                            ModConfig.weaponScales[index].y * ModConfig.weaponDefaultScales[index].y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].x * ModConfig.weaponDefaultScales[index].z * ModConfig.globalScale.x);

            shotgun.transform.localPosition = shotgun.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            shotgun.transform.localEulerAngles = shotgun.wpos.defaultRot;
            shotgun.transform.localScale = shotgun.wpos.defaultScale;
        }
        if (weapon.GetComponent<ShotgunHammer>() != null && weapon.GetComponent<ShotgunHammer>().wpos != null)
        {
            ShotgunHammer shotgunHammer = weapon.GetComponent<ShotgunHammer>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Core_Eject_Shotgun) { index = 3; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Pump_Charge_Shotgun) { index = 4; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Sawed_On_Shotgun) { index = 5; }

            shotgunHammer.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                            ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                            ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            shotgunHammer.GetComponent<WeaponPos>().defaultRot = new Vector3(ModConfig.weaponAngles[index].x + ModConfig.weaponDefaultAngles[index].x + ModConfig.globalAngle.x,
                                                                                ModConfig.weaponAngles[index].z + ModConfig.weaponDefaultAngles[index].y + ModConfig.globalAngle.z,
                                                                                -ModConfig.weaponAngles[index].y + ModConfig.weaponDefaultAngles[index].z + -ModConfig.globalAngle.y);
            shotgunHammer.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].z * 1 * ModConfig.globalScale.z,
                                                                                ModConfig.weaponScales[index].y * 1 * ModConfig.globalScale.y,
                                                                                ModConfig.weaponScales[index].x * 1 * ModConfig.globalScale.x);

            shotgunHammer.transform.localPosition = shotgunHammer.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            shotgunHammer.transform.localEulerAngles = shotgunHammer.wpos.defaultRot;
            shotgunHammer.transform.localScale = shotgunHammer.wpos.defaultScale;
        }
        if (weapon.GetComponent<Nailgun>() != null && weapon.GetComponent<Nailgun>().wpos != null)
        {
            Nailgun nailgun = weapon.GetComponent<Nailgun>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Attractor_Nailgun) { index = 6; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Overheat_Nailgun) { index = 7; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Jumpstart_Nailgun) { index = 8; }

            nailgun.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            nailgun.GetComponent<WeaponPos>().defaultRot = new Vector3(-ModConfig.weaponAngles[index].y + 0 + -ModConfig.globalAngle.y,
                                                                        ModConfig.weaponAngles[index].z + 0 + ModConfig.globalAngle.z,
                                                                        -ModConfig.weaponAngles[index].x + 0 + -ModConfig.globalAngle.x);
            nailgun.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].x * ModConfig.weaponDefaultScales[index].x * ModConfig.globalScale.x,
                                                                            ModConfig.weaponScales[index].y * ModConfig.weaponDefaultScales[index].y * ModConfig.globalScale.y,
                                                                            ModConfig.weaponScales[index].z * ModConfig.weaponDefaultScales[index].z * ModConfig.globalScale.z);

            nailgun.transform.localPosition = nailgun.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            nailgun.transform.localEulerAngles = nailgun.wpos.defaultRot;
            nailgun.transform.localScale = nailgun.wpos.defaultScale;
        }
        if (weapon.GetComponent<Railcannon>() != null && weapon.GetComponent<Railcannon>().wpos != null)
        {
            Railcannon railcannon = weapon.GetComponent<Railcannon>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Electric_Railcannon) { index = 9; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Screwdriver) { index = 10; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Malicious_Railcannon) { index = 11; }

            railcannon.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                            ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                            ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            railcannon.GetComponent<WeaponPos>().defaultRot = new Vector3(-ModConfig.weaponAngles[index].y + ModConfig.weaponDefaultAngles[index].x + -ModConfig.globalAngle.y,
                                                                            ModConfig.weaponAngles[index].z + ModConfig.weaponDefaultAngles[index].y + ModConfig.globalAngle.z,
                                                                            -ModConfig.weaponAngles[index].x + ModConfig.weaponDefaultAngles[index].z + -ModConfig.globalAngle.x);
            railcannon.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].x * ModConfig.weaponDefaultScales[index].x * ModConfig.globalScale.x,
                                                                                ModConfig.weaponScales[index].y * ModConfig.weaponDefaultScales[index].y * ModConfig.globalScale.y,
                                                                                ModConfig.weaponScales[index].z * ModConfig.weaponDefaultScales[index].z * ModConfig.globalScale.z);

            railcannon.transform.localPosition = railcannon.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            railcannon.transform.localEulerAngles = railcannon.wpos.defaultRot;
            railcannon.transform.localScale = railcannon.wpos.defaultScale;
        }
        if (weapon.GetComponent<RocketLauncher>() != null && weapon.GetComponent<RocketLauncher>().wpos != null)
        {
            RocketLauncher rocketLauncher = weapon.GetComponent<RocketLauncher>();
            if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Freezeframe_Rocket_Launcher) { index = 12; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.SRS_Rocket_Launcher) { index = 13; }
            else if (convertWeaponToWeaponEnum(weapon) == WeaponEnum.Firestarter_Rocket_Launcher) { index = 14; }

            rocketLauncher.wpos.currentDefault = new Vector3(ModConfig.weaponOffsets[index].x + ModConfig.weaponDefaultPositions[index].x + ModConfig.globalOffset.x,
                                                        ModConfig.weaponOffsets[index].y + ModConfig.weaponDefaultPositions[index].y + ModConfig.globalOffset.y,
                                                        ModConfig.weaponOffsets[index].z + ModConfig.weaponDefaultPositions[index].z + ModConfig.globalOffset.z);
            //revolver.GetComponent<WeaponPos>().currentDefault = revolver.wpos.currentDefault; //doesnt do anything?
            rocketLauncher.GetComponent<WeaponPos>().defaultRot = new Vector3(-ModConfig.weaponAngles[index].y + ModConfig.weaponDefaultAngles[index].x + -ModConfig.globalAngle.y,
                                                                                ModConfig.weaponAngles[index].z + ModConfig.weaponDefaultAngles[index].y + ModConfig.globalAngle.z,
                                                                                -ModConfig.weaponAngles[index].x + ModConfig.weaponDefaultAngles[index].z + -ModConfig.globalAngle.x);
            rocketLauncher.GetComponent<WeaponPos>().defaultScale = new Vector3(ModConfig.weaponScales[index].x * ModConfig.weaponDefaultScales[index].x * ModConfig.globalScale.x,
                                                                                    ModConfig.weaponScales[index].y * ModConfig.weaponDefaultScales[index].y * ModConfig.globalScale.y,
                                                                                    ModConfig.weaponScales[index].z * ModConfig.weaponDefaultScales[index].z * ModConfig.globalScale.z);

            rocketLauncher.transform.localPosition = rocketLauncher.wpos.currentDefault; //we have to set it ourselves... probably doesnt need to be on tick
            rocketLauncher.transform.localEulerAngles = rocketLauncher.wpos.defaultRot;
            rocketLauncher.transform.localScale = rocketLauncher.wpos.defaultScale;
        }
    }
    
}