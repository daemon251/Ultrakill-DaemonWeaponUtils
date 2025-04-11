using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using UnityEngine;
using DaemonWeaponUtilsPlugin;
using Vector3 = UnityEngine.Vector3;

public class ModConfig
{
    public enum SpecialColorEnum {Default, RandomRainbow, TimeBasedRainbow, Confetti, PulseWhite, PulseBlack, HSVRandom, RGBRandom}
    /*public static Vector3[] weaponDefaultPositionsRight = {  new Vector3(0.5f, -0.7f, 1.49f), new Vector3(0.5f, -0.7f, 1.49f), new Vector3(0.5f, -0.7f, 1.49f),
                                                        new Vector3(0.42f, -0.60f, 1.49f), new Vector3(0.42f, -0.60f, 1.49f), new Vector3(0.42f, -0.60f, 1.49f),
                                                        new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
                                                        new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
                                                        new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};

    public static Vector3[] weaponDefaultAnglesRight = { new Vector3(0f, 92f, 20f), new Vector3(0f, 92f, 20f), new Vector3(0f, 92f, 20f),
                                                    new Vector3(0f, 90f, 20f), new Vector3(0f, 90f, 20f), new Vector3(0f, 90f, 20f),
                                                    new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
                                                    new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
                                                    new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};                                                       

    public static Vector3[] weaponDefaultPositionsMiddle = {  new Vector3(0.09f, -0.9f, 1.49f), new Vector3(0.09f, -0.9f, 1.49f), new Vector3(0.09f, -0.9f, 1.49f),
                                                        new Vector3(0.00f, -0.85f, 1.65f), new Vector3(0.00f, -0.85f, 1.65f), new Vector3(0.00f, -0.85f, 1.65f),
                                                        new Vector3(-0.385f, -0.1f, 0.2f), new Vector3(-0.385f, -0.1f, 0.2f), new Vector3(-0.385f, -0.1f, 0.2f),
                                                        new Vector3(-0.6f, -0.4f, 0.1f), new Vector3(-0.6f, -0.4f, 0.1f), new Vector3(-0.6f, -0.4f, 0.1f),
                                                        new Vector3(-0.375f, -0.15f, 0.2f), new Vector3(-0.375f, -0.15f, 0.2f), new Vector3(-0.375f, -0.15f, 0.2f)};

    public static Vector3[] weaponDefaultAnglesMiddle = { new Vector3(0f, 93f, 25f), new Vector3(0f, 93f, 25f), new Vector3(0f, 93f, 25f),
                                                    new Vector3(0f, 91.5f, 20f), new Vector3(0f, 91.5f, 20f), new Vector3(0f, 91.5f, 20f),
                                                    new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
                                                    new Vector3(0f, 0.5f, 0f), new Vector3(0f, 0.5f, 0f), new Vector3(0f, 0.5f, 0f),
                                                    new Vector3(5f, 5f, 0f), new Vector3(5f, 5f, 0f), new Vector3(5f, 5f, 0f)}; */

    public static string DefaultParentFolder = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";     
    public static Vector3 globalOffset = new Vector3(0,0,0);
    public static Vector3[] weaponOffsets = new Vector3[15];   

    public static Vector3[] revolverPosition = {new Vector3(0.42f, -0.60f, 1.49f), new Vector3(0.09f, -0.90f, 1.49f)};
    public static Vector3[] revolverAltPosition = {new Vector3(0.5f, -0.7f, 1.49f), new Vector3(0.09f, -0.90f, 1.49f)};
    public static Vector3[] shotgunPosition = {new Vector3(0.42f, -0.60f, 1.49f), new Vector3(0.00f, -0.85f, 1.65f)};
    public static Vector3[] shotgunAltPosition = {new Vector3(0.42f, -0.60f, 1.49f), new Vector3(-0.18f, -0.80f, 1.65f)};
    public static Vector3[] nailgunPosition = {new Vector3(0f, 0f, 0f), new Vector3(-0.39f, -0.10f, 0.20f)};
    public static Vector3[] nailgunAltPosition = {new Vector3(0f, 0f, 0f), new Vector3(-0.50f, -0.10f, 0.20f)};
    public static Vector3[] railcannonPosition = {new Vector3(0f, 0f, 0f), new Vector3(-0.60f, -0.40f, 0.10f)};
    public static Vector3[] rocketLauncherPosition = {new Vector3(0f, 0f, 0f), new Vector3(-0.375f, -0.15f, 0.20f)};

    public static Vector3[] revolverAng = {new Vector3(0f, 90f, 20f), new Vector3(0f, 93f, 30f)};
    public static Vector3[] revolverAltAng = {new Vector3(0f, 92f, 20f), new Vector3(0f, 93f, 25f)};
    public static Vector3[] shotgunAng = {new Vector3(0f, 90f, 20f), new Vector3(0f, 91.5f, 20f)};
    public static Vector3[] shotgunAltAng = {new Vector3(0f, 90f, 20f), new Vector3(0f, 90f, 25f)};
    public static Vector3[] nailgunAng = {new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};
    public static Vector3[] nailgunAltAng = {new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};
    public static Vector3[] railcannonAng = {new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};
    public static Vector3[] rocketLauncherAng = {new Vector3(0f, 0f, 0f), new Vector3(5f, 5f, 0f)};

    public static Vector3[] revolverSize = {new Vector3(0.29f, 0.29f, 0.33f), new Vector3(0.29f, 0.29f, 0.33f)};
    public static Vector3[] revolverAltSize = {new Vector3(0.30f, 0.30f, 0.40f), new Vector3(0.30f, 0.30f, 0.40f)};
    public static Vector3[] shotgunSize = {new Vector3(0.29f, 0.29f, 0.33f), new Vector3(0.29f, 0.29f, 0.33f)};
    public static Vector3[] shotgunAltSize = {new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)};
    public static Vector3[] nailgunSize = {new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)};
    public static Vector3[] nailgunAltSize = {new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)};
    public static Vector3[] railcannonSize = {new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)};
    public static Vector3[] rocketLauncherSize = {new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1.20f)};



    public static Vector3 globalScale = new Vector3(1f,1f,1f);
    public static Vector3[] weaponScales = new Vector3[15];     


    public static Vector3 globalAngle = new Vector3(0f,0f,0f);
    public static Vector3[] weaponAngles = new Vector3[15];     

    public enum CrossHairEnum {UltrakillBase, None, 
    //DefaultSmall, DefaultLarge, 
    ClosedCircle, OpenCircle, ClosedCross, OpenCross, Grenade, CrossBorder,
    Custom1, Custom2, Custom3, Custom4, Custom5, Custom6, Custom7, Custom8, Custom9, Custom10};

    public static CrossHairEnum[,] crosshairStyles = new CrossHairEnum[5, 3];
    public static float[,] crosshairScales = new float[5, 3];
    public static Color[,] crosshairColors = new Color[5, 3];
    public static bool[,] keepCrosshairInfo = new bool[5, 3];
    public static float[,] crosshairOpacities = new float[5, 3];

    public static void OpenFolder() {Application.OpenURL(Plugin.DefaultParentFolder);}

    //public static bool nailFollowCurrentRainbow = false;
    public static void CreateBaseFields(ConfigPanel cp, int i)
    {
        ConfigPanel positionPanel = new ConfigPanel(cp, "Position / Scale / Angle", "positionPanel" + i);

        FloatField xPosField = new FloatField(positionPanel, "X Position Offset", "xPosOffset" + i, 0.0f, -100f, 100f);
        xPosField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponOffsets[i].x = e.value;}; 
        weaponOffsets[i].x = xPosField.value;

        FloatField yPosField = new FloatField(positionPanel, "Y Position Offset", "yPosOffset" + i, 0.0f, -100f, 100f);
        yPosField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponOffsets[i].y = e.value;}; 
        weaponOffsets[i].y = yPosField.value;

        FloatField zPosField = new FloatField(positionPanel, "Z Position Offset", "zPosOffset" + i, 0.0f, -100f, 100f);
        zPosField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponOffsets[i].z = e.value;}; 
        weaponOffsets[i].z = zPosField.value;

        new ConfigHeader(positionPanel, "------------------------------------------------------").textSize = 16;

        FloatField rollAngField = new FloatField(positionPanel, "Roll Offset", "rollAngOffset" + i, 0.0f, -180f, 180f);
        rollAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponAngles[i].x = e.value;}; 
        weaponAngles[i].x = rollAngField.value;

        FloatField pitchAngField = new FloatField(positionPanel, "Pitch Offset", "pitchAngOffset" + i, 0.0f, -180f, 180f);
        pitchAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponAngles[i].y = e.value;}; 
        weaponAngles[i].y = pitchAngField.value;

        FloatField yawAngField = new FloatField(positionPanel, "Yaw Offset", "yawAngOffset" + i, 0.0f, -180f, 180f);
        yawAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponAngles[i].z = e.value;}; 
        weaponAngles[i].z = yawAngField.value;

        new ConfigHeader(positionPanel, "------------------------------------------------------").textSize = 16;

        FloatField xScaleField = new FloatField(positionPanel, "X Scale", "xScale" + i, 1.0f, -100f, 100f);
        xScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponScales[i].x = e.value;}; 
        weaponScales[i].x = xScaleField.value;

        FloatField yScaleField = new FloatField(positionPanel, "Y Scale", "yScale" + i, 1.0f, -100f, 100f);
        yScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponScales[i].y = e.value;}; 
        weaponScales[i].y = yScaleField.value;

        FloatField zScaleField = new FloatField(positionPanel, "Z Scale", "zScale" + i, 1.0f, -100f, 100f);
        zScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {weaponScales[i].z = e.value;}; 
        weaponScales[i].z = zScaleField.value;

        ConfigPanel crosshairPanel = new ConfigPanel(cp, "Crosshair", "CrosshairSettingsPanel" + i);

        EnumField<CrossHairEnum> crosshairStyleField = new EnumField<CrossHairEnum>(crosshairPanel, "Crosshair Style", "crosshairStyle" + i, CrossHairEnum.UltrakillBase);
        ConfigDivision crosshairDivision = new ConfigDivision(crosshairPanel, "crosshairDivision" + i);
        crosshairStyleField.onValueChange += (EnumField<CrossHairEnum>.EnumValueChangeEvent e) => {crosshairStyles[i / 3, i % 3] = e.value; if(e.value == CrossHairEnum.UltrakillBase) {crosshairDivision.interactable = false;} else {{crosshairDivision.interactable = true;}}};
        crosshairStyles[i / 3, i % 3] = crosshairStyleField.value; if(crosshairStyleField.value == CrossHairEnum.UltrakillBase) {crosshairDivision.interactable = false;} else {{crosshairDivision.interactable = true;}}

        new ConfigHeader(crosshairDivision, "------------------------------------------------------").textSize = 16;

        FloatField crosshairScaleField = new FloatField(crosshairDivision, "Crosshair Scale", "crosshairScale" + i, 1f, 0f, 100f);
        crosshairScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {crosshairScales[i / 3, i % 3] = e.value;};
        crosshairScales[i / 3, i % 3] = crosshairScaleField.value;

        ColorField crosshairColorField = new ColorField(crosshairDivision, "Crosshair Color", "crosshairColor" + i, Color.white);
        crosshairColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {crosshairColors[i / 3, i % 3] = e.value;};
        crosshairColors[i / 3, i % 3] = crosshairColorField.value;

        FloatField crosshairOpacityField = new FloatField(crosshairDivision, "Crosshair Opacity", "crosshairOpacity" + i, 1f, 0f, 1f);
        crosshairOpacityField.onValueChange += (FloatField.FloatValueChangeEvent e) => {crosshairOpacities[i / 3, i % 3] = e.value;};
        crosshairOpacities[i / 3, i % 3] = crosshairOpacityField.value;

        BoolField keepCrosshairInfoField = new BoolField(crosshairDivision, "Keep Crosshair Info", "keepCrossHairInfo" + i, true);
        keepCrosshairInfoField.onValueChange += (BoolField.BoolValueChangeEvent e) => {keepCrosshairInfo[i / 3, i % 3] = e.value;};
        keepCrosshairInfo[i / 3, i % 3] = keepCrosshairInfoField.value;

        ConfigHeader crosshairHeader = new ConfigHeader(crosshairPanel, "Custom crosshairs can be changed by changing Custom1.png, Custom2.png, etc. in the mod directory");
        crosshairHeader.textSize = 12;

        ButtonField openSoundsFolderField = new ButtonField(crosshairPanel, "Open Mod Directory", "button.openfolder" + i);
        openSoundsFolderField.onClick += new ButtonField.OnClick(OpenFolder);
    }

    
    public static Color[] revolverBeamColors = new Color[3];
    public static SpecialColorEnum[,] weaponSpecialColors = new SpecialColorEnum[5,3];
    public static bool useColorableTexturePiercer = false;
    public static Color piercerAltBeamColor = new Color();
    public static SpecialColorEnum piercerAltBeamSpecialColor = SpecialColorEnum.Default;
    public static Color PiercerChargedAltHUDColor = new Color();
    public static SpecialColorEnum PiercerChargedAltHUDSpecialColor = SpecialColorEnum.Default;
    public static Color sharpshooterAltBeamColor = new Color();
    public static bool coinSettingsToggle = false;
    public static Color marksmanCoinChargedColor = new Color();
    public static SpecialColorEnum marksmanCoinChargedSpecialColor = SpecialColorEnum.Default;
    public static Color marksmanCoinFlashColor = new Color();
    public static SpecialColorEnum marksmanCoinFlashSpecialColor = SpecialColorEnum.Default;
    public static Color marksmanCoinChargingColor = new Color();
    public static Color coinColor = new Color();
    public static Color coinTrailColor = new Color();
    public static SpecialColorEnum coinTrailSpecialColor = SpecialColorEnum.Default;
    public static SpecialColorEnum coinEntitySpecialColor = SpecialColorEnum.Default;
    public static bool[,] enableColors = new bool[5, 3];
    public static float[] revolverBeamWidths = new float[3];
    public static float piercerAltBeamWidth = new float();
    public static float sharpshooterAltBeamWidth = new float();
    public static SpecialColorEnum sharpshooterAltBeamSpecialColor = SpecialColorEnum.Default;
    public static Color piercerChargeEffectColor = new Color();
    public static SpecialColorEnum piercerChargeEffectSpecialColor = SpecialColorEnum.Default;

    public static void CreateRevolverFields(ConfigPanel cp, int i)
    {
        ConfigPanel colorPanel = new ConfigPanel(cp, "Color", "RevolverColorPanel" + i);
        ConfigHeader warningHeader = new ConfigHeader(colorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField enabledBeamColorField = new BoolField(colorPanel, "Custom Colors For This Weapon", "RevolverEnabledBeamColor" + i, false);
        ConfigDivision division = new ConfigDivision(colorPanel, "RevolverColorDivision" + i);
        enabledBeamColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {enableColors[0, i] = e.value; division.interactable = e.value;};
        enableColors[0, i] = enabledBeamColorField.value; division.interactable = enabledBeamColorField.value;

        new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

        ColorField beamColorField = new ColorField(division, "Beam Color", "RevolverBeamColor" + i, Color.white);
        beamColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {revolverBeamColors[i] = e.value;};
        revolverBeamColors[i] = beamColorField.value;

        FloatField beamWidthField = new FloatField(division, "Beam Width / Time", "revolverBeamWidth" + i, 1.0f, 0.0f, 100f);
        beamWidthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {revolverBeamWidths[i] = e.value;};
        revolverBeamWidths[i] = beamWidthField.value;

        EnumField<SpecialColorEnum> revolverSpecialColorField = new EnumField<SpecialColorEnum>(division, "Color Attribute", "revolverSpecialColor" + i, SpecialColorEnum.Default);
        revolverSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {weaponSpecialColors[0, i] = e.value;};
        weaponSpecialColors[0, i] = revolverSpecialColorField.value;

        if(i == 0)
        {
            new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

            ColorField beamAltColorField = new ColorField(division, "Alternate Fire Beam Color", "RevolverAltBeamColor" + i, Color.white);
            beamAltColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {piercerAltBeamColor = e.value; Colors.MakeColoredPiercerAltTexture(Color.white);};
            piercerAltBeamColor = beamAltColorField.value; 

            EnumField<SpecialColorEnum> piercerAltBeamSpecialColorField = new EnumField<SpecialColorEnum>(division, "Alt Fire Charged Beam Color Attribute", "ChargedAltBeamSpecialColor" + i, SpecialColorEnum.Default);
            piercerAltBeamSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {piercerAltBeamSpecialColor = e.value;};
            piercerAltBeamSpecialColor = piercerAltBeamSpecialColorField.value;

            FloatField altFireBeamWidthField = new FloatField(division, "Alt Fire Beam Width (Stacks)", "piercerAltFireBeamWidth" + i, 1.0f, 0.0f, 100f);
            altFireBeamWidthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {piercerAltBeamWidth = e.value;};
            piercerAltBeamWidth = altFireBeamWidthField.value;

            BoolField useColorableTexturePiercerField = new BoolField(division, "Use Alterate Charging Texture", "PiercerAlternateTexture", true);
            useColorableTexturePiercerField.onValueChange += (BoolField.BoolValueChangeEvent e) => {useColorableTexturePiercer = e.value;};
            useColorableTexturePiercer = useColorableTexturePiercerField.value;

            ColorField chargedAltEffectColorField = new ColorField(division, "AltFire Charged Effect Color", "ChargedAltEffectColor" + i, Color.white);
            chargedAltEffectColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {piercerChargeEffectColor = e.value;};
            piercerChargeEffectColor = chargedAltEffectColorField.value; 

            EnumField<SpecialColorEnum> chargedAltEffectSpecialColorField = new EnumField<SpecialColorEnum>(division, "AltFire Charged Effect Color Attribute", "ChargedAltEffectSpecialColor" + i, SpecialColorEnum.Default);
            chargedAltEffectSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {piercerChargeEffectSpecialColor = e.value;};
            piercerChargeEffectSpecialColor = chargedAltEffectSpecialColorField.value;

            ColorField chargedAltHUDColorField = new ColorField(division, "Alternate Fire Charged HUD Color", "ChargedAltHUDColor" + i, Color.white);
            chargedAltHUDColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {PiercerChargedAltHUDColor = e.value;};
            PiercerChargedAltHUDColor = chargedAltHUDColorField.value; 

            EnumField<SpecialColorEnum> chargedAltHUDSpecialColorField = new EnumField<SpecialColorEnum>(division, "Alt Fire Charged HUD Color Attribute", "ChargedAltHUDSpecialColor" + i, SpecialColorEnum.Default);
            chargedAltHUDSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {PiercerChargedAltHUDSpecialColor = e.value;};
            PiercerChargedAltHUDSpecialColor = chargedAltHUDSpecialColorField.value;
        }
        else if(i == 1)
        {
            new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

            BoolField customCoinColorsField = new BoolField(colorPanel, "Custom Coin Settings", "customCoinSettings", false);
            ConfigDivision coinDivision = new ConfigDivision(colorPanel, "coinDivision");
            customCoinColorsField.onValueChange += (BoolField.BoolValueChangeEvent e) => {coinSettingsToggle = e.value; coinDivision.interactable = e.value;};
            coinSettingsToggle = customCoinColorsField.value; coinDivision.interactable = customCoinColorsField.value;

            ColorField coinColorField = new ColorField(coinDivision, "Coin Entity Color", "CoinColor" + i, Color.white);
            coinColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {coinColor = e.value;};
            coinColor = coinColorField.value;

            EnumField<SpecialColorEnum> revolverCoinSpecialColorField = new EnumField<SpecialColorEnum>(coinDivision, "Coin Entity Color Attribute", "CoinEntitySpecialColor" + i, SpecialColorEnum.Default);
            revolverCoinSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {coinEntitySpecialColor = e.value;};
            coinEntitySpecialColor = revolverCoinSpecialColorField.value;

            ColorField coinTrailColorField = new ColorField(coinDivision, "Coin Trail Color", "CoinTrailColor" + i, Color.white);
            coinTrailColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {coinTrailColor = e.value;};
            coinTrailColor = coinTrailColorField.value;

            EnumField<SpecialColorEnum> revolverCoinTrailSpecialColorField = new EnumField<SpecialColorEnum>(coinDivision, "Coin Trail Color Attribute", "CoinTrailSpecialColor" + i, SpecialColorEnum.Default);
            revolverCoinTrailSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {coinTrailSpecialColor = e.value;};
            coinTrailSpecialColor = revolverCoinTrailSpecialColorField.value;

            ColorField coinFlashColorField = new ColorField(coinDivision, "Coin Flash Color", "CoinFlashColor" + i, Color.white);
            coinFlashColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {marksmanCoinFlashColor = e.value;};
            marksmanCoinFlashColor = coinFlashColorField.value;

            EnumField<SpecialColorEnum> revolverCoinFlashSpecialColorField = new EnumField<SpecialColorEnum>(coinDivision, "Coin Flash Color Attribute", "CoinFlashSpecialColor" + i, SpecialColorEnum.Default);
            revolverCoinFlashSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {marksmanCoinFlashSpecialColor = e.value;};
            marksmanCoinFlashSpecialColor = revolverCoinFlashSpecialColorField.value;

            ColorField coinChargedColorField = new ColorField(coinDivision, "HUD Coin Charged Color", "CoinChargedColor" + i, Color.white);
            coinChargedColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {marksmanCoinChargedColor = e.value;};
            marksmanCoinChargedColor = coinChargedColorField.value;

            EnumField<SpecialColorEnum> revolverCoinChargedSpecialColorField = new EnumField<SpecialColorEnum>(coinDivision, "HUD Coin Charged Color Attribute", "CoinChargedSpecialColor" + i, SpecialColorEnum.Default);
            revolverCoinChargedSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {marksmanCoinChargedSpecialColor = e.value;};
            marksmanCoinChargedSpecialColor = revolverCoinChargedSpecialColorField.value;

            ColorField coinChargingColorField = new ColorField(coinDivision, "HUD Coin Charging Color", "CoinChargingColor" + i, Color.white);
            coinChargingColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {marksmanCoinChargingColor = e.value;};
            marksmanCoinChargingColor = coinChargingColorField.value;
        }
        else if(i == 2)
        {
            new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

            FloatField altFireBeamWidthField = new FloatField(division, "Alt Fire Beam Width (Stacks)", "sharpshooterAltFireBeamWidth" + i, 1.0f, 0.0f, 100f);
            altFireBeamWidthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {sharpshooterAltBeamWidth = e.value;};
            sharpshooterAltBeamWidth = altFireBeamWidthField.value;

            ColorField beamAltColorField = new ColorField(division, "Alternate Fire Beam Color", "RevolverAltBeamColor" + i, Color.white);
            beamAltColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {sharpshooterAltBeamColor = e.value;};
            sharpshooterAltBeamColor = beamAltColorField.value;

            EnumField<SpecialColorEnum> sharpshooterAltBeamSpecialColorField = new EnumField<SpecialColorEnum>(division, "Alt Fire Charged Beam Color Attribute", "ChargedAltBeamSpecialColor" + i, SpecialColorEnum.Default);
            sharpshooterAltBeamSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {sharpshooterAltBeamSpecialColor = e.value;};
            sharpshooterAltBeamSpecialColor = sharpshooterAltBeamSpecialColorField.value;
        }
    }

    public static Color[] shotgunPelletColors = new Color[3];
    public static Color coreEjectColor = new Color();
    public static SpecialColorEnum coreEjectSpecialColor = SpecialColorEnum.Default;
    public static Color coreEjectExplosionColor = new Color();
    
    public static void CreateShotgunFields(ConfigPanel cp, int i)
    {
        ConfigPanel colorPanel = new ConfigPanel(cp, "Color", "ShotgunColorPanel" + i);
        ConfigHeader warningHeader = new ConfigHeader(colorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField enabledPelletColorField = new BoolField(colorPanel, "Custom Colors For This Weapon", "ShotgunEnabledColor" + i, false);
        ConfigDivision division = new ConfigDivision(colorPanel, "ShotgunColorFieldDivision" + i);
        enabledPelletColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {enableColors[1, i] = e.value; division.interactable = e.value;};
        enableColors[1, i] = enabledPelletColorField.value; division.interactable = enabledPelletColorField.value;

        new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

        ConfigDivision specificColorDivision = new ConfigDivision(division, "ShotgunSpecificColorDivision");
        ColorField pelletColorField = new ColorField(specificColorDivision, "Pellet Color", "ShotgunPelletColor" + i, Color.white);
        pelletColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {shotgunPelletColors[i] = e.value;};
        shotgunPelletColors[i] = pelletColorField.value;

        EnumField<SpecialColorEnum> shotgunSpecialColorField = new EnumField<SpecialColorEnum>(division, "Pellet Color Attribute", "shotgunSpecialColor" + i, SpecialColorEnum.Default);
        shotgunSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {weaponSpecialColors[1, i] = e.value;};
        weaponSpecialColors[1, i] = shotgunSpecialColorField.value;

        if(i == 0)
        {
            new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

            ConfigDivision coreEjectColorDivision = new ConfigDivision(division, "coreEjectColorDivision");
            ColorField coreEjectColorField = new ColorField(coreEjectColorDivision, "Core Eject Grenade Color", "CoreEjectGrenadeColor", Color.white);
            coreEjectColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {coreEjectColor = e.value;};
            coreEjectColor = coreEjectColorField.value;

            EnumField<SpecialColorEnum> coreEjectSpecialColorField = new EnumField<SpecialColorEnum>(division, "Core Eject Color Attribute", "coreEjectSpecialColor", SpecialColorEnum.Default);
            coreEjectSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {coreEjectSpecialColor = e.value;};
            coreEjectSpecialColor = coreEjectSpecialColorField.value;

            /*ColorField explosionColorField = new ColorField(division, "Core Eject Explosion Color", "CoreEjectExplosionColor", Color.white);
            explosionColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {coreEjectExplosionColor = e.value;};
            coreEjectExplosionColor = explosionColorField.value;*/
        }
    }

    public static Color[] nailgunNailColors = new Color[3];

    public static void CreateNailgunFields(ConfigPanel cp, int i)
    {
        ConfigPanel colorPanel = new ConfigPanel(cp, "Color", "NailgunColorPanel" + i);
        ConfigHeader warningHeader = new ConfigHeader(colorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField enabledNailColorField = new BoolField(colorPanel, "Custom Colors For This Weapon", "NailgunEnabledColor" + i, false);
        ConfigDivision division = new ConfigDivision(colorPanel, "NailgunColorFieldDivision" + i);
        enabledNailColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {enableColors[2, i] = e.value; division.interactable = e.value;};
        enableColors[2, i] = enabledNailColorField.value; division.interactable = enabledNailColorField.value;

        new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

        ConfigDivision specificColorDivision = new ConfigDivision(division, "NailgunSpecificColorDivision");
        ColorField nailColorField = new ColorField(specificColorDivision, "Nail Color", "NailgunNailColor" + i, Color.white);
        nailColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {nailgunNailColors[i] = e.value;};
        nailgunNailColors[i] = nailColorField.value;

        EnumField<SpecialColorEnum> nailgunSpecialColorField = new EnumField<SpecialColorEnum>(division, "Color Attribute", "nailgunSpecialColor" + i, SpecialColorEnum.Default);
        nailgunSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {weaponSpecialColors[2, i] = e.value;};
        weaponSpecialColors[2, i] = nailgunSpecialColorField.value;
    }

    public static Color[] railcannonBeamColors = new Color[3];
    public static float[] railcannonBeamWidths = new float[3];
    public static bool electricRailcannonEffectColorChange = true;

    public static void CreateRailcannonFields(ConfigPanel cp, int i)
    {
        ConfigPanel colorPanel = new ConfigPanel(cp, "Color", "railcannonColorPanel" + i);
        ConfigHeader warningHeader = new ConfigHeader(colorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField enabledBeamColorField = new BoolField(colorPanel, "Custom Colors For This Weapon", "RailcannonEnabledBeamColor" + i, false);
        ConfigDivision division = new ConfigDivision(colorPanel, "RailcannonColorDivision" + i);
        enabledBeamColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {enableColors[3, i] = e.value; division.interactable = e.value;};
        enableColors[3, i] = enabledBeamColorField.value; division.interactable = enabledBeamColorField.value;

        new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

        if(i != 1)
        {
            FloatField beamWidthField = new FloatField(division, "Beam Width / Time", "railcannonBeamWidth" + i, 1.0f, 0.0f, 100f);
            beamWidthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {railcannonBeamWidths[i] = e.value;};
            railcannonBeamWidths[i] = beamWidthField.value;
        }

        ColorField beamColorField = new ColorField(division, "Beam Color", "RailcannonBeamColor" + i, Color.white);
        beamColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {railcannonBeamColors[i] = e.value;};
        railcannonBeamColors[i] = beamColorField.value;

        EnumField<SpecialColorEnum> railcannonSpecialColorField = new EnumField<SpecialColorEnum>(division, "Color Attribute", "railcannonSpecialColor" + i, SpecialColorEnum.Default);
        railcannonSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {weaponSpecialColors[3, i] = e.value;};
        weaponSpecialColors[3, i] = railcannonSpecialColorField.value;

        if(i == 0)
        {
            new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

            BoolField electricRailcannonEffectColorChangeField = new BoolField(division, "Electric Effect Color Change", "electricEffectColorChange", true);
            electricRailcannonEffectColorChangeField.onValueChange += (BoolField.BoolValueChangeEvent e) => {electricRailcannonEffectColorChange = e.value;};
            electricRailcannonEffectColorChange = electricRailcannonEffectColorChangeField.value;
        }
    }

    public static Color[] rocketLauncherRocketColors = new Color[3];
    //public static Color[] rocketLauncherExplosionColors = new Color[3];
    public static Color freezeframeColor = new Color();
    public static SpecialColorEnum freezeframeSpecialColor = SpecialColorEnum.Default;
    public static Color cannonballColor = new Color();
    public static SpecialColorEnum cannonballSpecialColor = SpecialColorEnum.Default;

    public static void CreateRocketLauncherFields(ConfigPanel cp, int i)
    {
        ConfigPanel colorPanel = new ConfigPanel(cp, "Color", "RocketLauncherColorPanel" + i);
        ConfigHeader warningHeader = new ConfigHeader(colorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField enabledRocketColorField = new BoolField(colorPanel, "Custom Colors For This Weapon", "RocketLauncherEnabledColor" + i, false);
        ConfigDivision division = new ConfigDivision(colorPanel, "RocketLauncherColorFieldDivision" + i);
        enabledRocketColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {enableColors[4, i] = e.value; division.interactable = e.value;};
        enableColors[4, i] = enabledRocketColorField.value; division.interactable = enabledRocketColorField.value;

        new ConfigHeader(division, "------------------------------------------------------").textSize = 16;

        ConfigDivision specificColorDivision = new ConfigDivision(division, "RocketLauncherSpecificColorDivision");
        ColorField rocketColorField = new ColorField(specificColorDivision, "Rocket Color", "RocketLauncherRocketColor" + i, Color.white);
        rocketColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {rocketLauncherRocketColors[i] = e.value;};
        rocketLauncherRocketColors[i] = rocketColorField.value;

        EnumField<SpecialColorEnum> rocketLauncherSpecialColorField = new EnumField<SpecialColorEnum>(division, "Color Attribute", "rocketLauncherSpecialColor" + i, SpecialColorEnum.Default);
        rocketLauncherSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {weaponSpecialColors[4, i] = e.value;};
        weaponSpecialColors[4, i] = rocketLauncherSpecialColorField.value;

        if(i == 0)
        {
            ColorField freezeframeRocketColorField = new ColorField(division, "Freeze Effect Color", "FrozenRocketColor" + i, Color.white);
            freezeframeRocketColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {freezeframeColor = e.value;};
            freezeframeColor = freezeframeRocketColorField.value; 

            EnumField<SpecialColorEnum> freezeframeRocketSpecialColorField = new EnumField<SpecialColorEnum>(division, "Frozen Rocket Color Attribute", "FrozenRocketSpecialColor" + i, SpecialColorEnum.Default);
            freezeframeRocketSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {freezeframeSpecialColor = e.value;};
            freezeframeSpecialColor = freezeframeRocketSpecialColorField.value;
        }
        else if(i == 1)
        {
            /*ColorField cannonballColorField = new ColorField(colorPanel, "Cannonball Color", "CannonballColor" + i, Color.black);
            cannonballColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {cannonballColor = e.value;};
            cannonballColor = cannonballColorField.value; 

            EnumField<SpecialColorEnum> cannonballSpecialColorField = new EnumField<SpecialColorEnum>(colorPanel, "Cannonball Color Attribute", "cannonballSpecialColor" + i, SpecialColorEnum.Default);
            cannonballSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {cannonballSpecialColor = e.value;};
            cannonballSpecialColor = cannonballSpecialColorField.value;*/
        }

        /*ColorField explosionColorField = new ColorField(division, "Explosion Color", "RocketLauncherExplosionColor" + i, Color.white);
        explosionColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {rocketLauncherExplosionColors[i] = e.value;};
        rocketLauncherExplosionColors[i] = explosionColorField.value;*/
    }
    public static bool customColors = true;
    public static Color parryProjectileColor = new Color();
    public static bool parryProjectileColored = false;
    public static SpecialColorEnum parrySpecialColor = SpecialColorEnum.Default;
    public static bool colorMuzzleFlashes = true;
    public static float truestopMult = 1.0f;
    public static float hitstopMult = 1.0f;
    public static float slowdownMult = 1.0f;
    public static float colorEffectSpeed = 1.0f;
    public static float jackhammerAddTimeStop = 0.0f;
    public static Color jackhammerBeamColor = new Color();
    public static SpecialColorEnum jackhammerBeamSpecialColor = SpecialColorEnum.Default;
    public static bool jackhammerCustomBeamColor = false;

    public static CrossHairEnum mainCrossHairEnum = CrossHairEnum.UltrakillBase;
    public static float mainCrossHairScale = 1f;
    public static bool mainCrossHairKeepInfo = true;
    public static Color mainCrossHairColor = new Color();
    public static float mainCrossHairOpacity = 1f;

    public static void CreateConfig()
    {
        var config = PluginConfigurator.Create("Daemon's Weapon Utils", "DaemonWeaponUtils");
        config.SetIconWithURL($"{Path.Combine(DefaultParentFolder!, "icon.png")}");
        BoolField enabledField = new BoolField(config.rootPanel, "Mod Enabled", "modEnabled", true);
        ConfigDivision division = new ConfigDivision(config.rootPanel, "division");
        enabledField.onValueChange += (BoolField.BoolValueChangeEvent e) => {Plugin.modEnabled = e.value; division.interactable = Plugin.modEnabled;};
        Plugin.modEnabled = enabledField.value; division.interactable = Plugin.modEnabled;

        ConfigPanel globalSettingsPanel = new ConfigPanel(division, "Global Settings", "globalSettingsPanel");

        ConfigPanel globalPositionPanel = new ConfigPanel(globalSettingsPanel, "Position / Scale / Angle", "globalPositionPanel");
        ConfigPanel globalColorPanel = new ConfigPanel(globalSettingsPanel, "Color", "globalColorPanel");
        ConfigPanel globalHitstopPanel = new ConfigPanel(globalSettingsPanel, "Hitstop", "globalHitstopPanel");
        ConfigPanel globalCrosshairPanel = new ConfigPanel(globalSettingsPanel, "Base Crosshair Settings", "globalCrosshairPanel");

        FloatField xPosGlobalField = new FloatField(globalPositionPanel, "X Position Offset", "xPosGlobalOffset", 0.0f, -100f, 100f);
        xPosGlobalField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalOffset.x = e.value;}; 
        globalOffset.x = xPosGlobalField.value;

        FloatField yPosGlobalField = new FloatField(globalPositionPanel, "Y Position Offset", "yPosGlobalOffset", 0.0f, -100f, 100f);
        yPosGlobalField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalOffset.y = e.value;}; 
        globalOffset.y = yPosGlobalField.value;

        FloatField zPosGlobalField = new FloatField(globalPositionPanel, "Z Position Offset", "zPosGlobalOffset", 0.0f, -100f, 100f);
        zPosGlobalField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalOffset.z = e.value;}; 
        globalOffset.z = zPosGlobalField.value;

        new ConfigHeader(globalPositionPanel, "------------------------------------------------------").textSize = 16;

        FloatField rollAngField = new FloatField(globalPositionPanel, "Roll Offset", "rollAngGlobalOffset", 0.0f, -180f, 180f);
        rollAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalAngle.x = e.value;}; 
        globalAngle.x = rollAngField.value;

        FloatField pitchAngField = new FloatField(globalPositionPanel, "Pitch Offset", "pitchAngGlobalOffset", 0.0f, -180f, 180f);
        pitchAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalAngle.y = e.value;}; 
        globalAngle.y = pitchAngField.value;

        FloatField yawAngField = new FloatField(globalPositionPanel, "Yaw Offset", "yawAngGlobalOffset", 0.0f, -180f, 180f);
        yawAngField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalAngle.z = e.value;}; 
        globalAngle.z = yawAngField.value;

        new ConfigHeader(globalPositionPanel, "------------------------------------------------------").textSize = 16;

        FloatField xScaleField = new FloatField(globalPositionPanel, "X Scale", "xGlobalScale", 1.0f, -100f, 100f);
        xScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalScale.x = e.value;}; 
        globalScale.x = xScaleField.value;

        FloatField yScaleField = new FloatField(globalPositionPanel, "Y Scale", "yGlobalScale", 1.0f, -100f, 100f);
        yScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalScale.y = e.value;}; 
        globalScale.y = yScaleField.value;

        FloatField zScaleField = new FloatField(globalPositionPanel, "Z Scale", "zGlobalScale", 1.0f, -100f, 100f);
        zScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {globalScale.z = e.value;}; 
        globalScale.z = zScaleField.value;



        ConfigHeader warningHeader = new ConfigHeader(globalColorPanel, "May need to restart level after disabling any value to see change in effect.");
        warningHeader.textSize = 12;

        BoolField customVisualsField = new BoolField(globalColorPanel, "Enable Custom Colors Globally", "customVisualsGlobal", true);
        customVisualsField.onValueChange += (BoolField.BoolValueChangeEvent e) => {customColors = e.value;};
        customColors = customVisualsField.value;

        new ConfigHeader(globalColorPanel, "------------------------------------------------------").textSize = 16;

        BoolField enabledParryColorField = new BoolField(globalColorPanel, "Custom Parry Color", "ParryColorEnabled", false);
        ConfigDivision colorDivision = new ConfigDivision(globalColorPanel, "ParryColorFieldDivision");
        enabledParryColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {parryProjectileColored = e.value; colorDivision.interactable = e.value;};
        parryProjectileColored = enabledParryColorField.value; colorDivision.interactable = enabledParryColorField.value;

        ColorField parryColorField = new ColorField(colorDivision, "Parry Color", "ParryColor", Color.white);
        parryColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {parryProjectileColor = e.value;};
        parryProjectileColor = parryColorField.value;

        EnumField<SpecialColorEnum> parrySpecialColorField = new EnumField<SpecialColorEnum>(colorDivision, "Parry Color Attribute", "parrySpecialColor", SpecialColorEnum.Default);
        parrySpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {parrySpecialColor = e.value;};
        parrySpecialColor = parrySpecialColorField.value;

        new ConfigHeader(globalColorPanel, "------------------------------------------------------").textSize = 16;

        BoolField enabledShotgunHammerBeamColorField = new BoolField(globalColorPanel, "Custom Jackhammer Beam Color", "ShotgunHammerBeamColorEnabled", false);
        ConfigDivision colorDivision2 = new ConfigDivision(globalColorPanel, "ParryColorFieldDivision");
        enabledShotgunHammerBeamColorField.onValueChange += (BoolField.BoolValueChangeEvent e) => {jackhammerCustomBeamColor = e.value; colorDivision2.interactable = e.value;};
        jackhammerCustomBeamColor = enabledShotgunHammerBeamColorField.value; colorDivision2.interactable = enabledShotgunHammerBeamColorField.value;

        ColorField shotgunHammerColorField = new ColorField(colorDivision2, "Jackhammer Beam Color", "ShotgunHammerBeamColor", Color.white);
        shotgunHammerColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {jackhammerBeamColor = e.value;};
        jackhammerBeamColor = shotgunHammerColorField.value;

        EnumField<SpecialColorEnum> shotgunHammerSpecialColorField = new EnumField<SpecialColorEnum>(colorDivision2, "Jackhammer Beam Color Attribute", "shotgunHammerBeamSpecialColor", SpecialColorEnum.Default);
        shotgunHammerSpecialColorField.onValueChange += (EnumField<SpecialColorEnum>.EnumValueChangeEvent e) => {jackhammerBeamSpecialColor = e.value;};
        jackhammerBeamSpecialColor = shotgunHammerSpecialColorField.value;

        new ConfigHeader(globalColorPanel, "------------------------------------------------------").textSize = 16;

        BoolField muzzleFlashColorEnabledField = new BoolField(globalColorPanel, "Enable Muzzle Flash Color Changes", "muzzleFlashColorEnabled", true);
        muzzleFlashColorEnabledField.onValueChange += (BoolField.BoolValueChangeEvent e) => {colorMuzzleFlashes = e.value;};
        colorMuzzleFlashes = muzzleFlashColorEnabledField.value;

        //BoolField nailFollowCurrentRainbowField = new BoolField(globalColorPanel, "Nails follow current rainbow color", "nailFollowCurrentRainbow", true);
        //nailFollowCurrentRainbowField.onValueChange += (BoolField.BoolValueChangeEvent e) => {nailFollowCurrentRainbow = e.value;};
        //nailFollowCurrentRainbow = nailFollowCurrentRainbowField.value;

        FloatField colorEffectRotationSpeedField = new FloatField(globalColorPanel, "Color Effect Rotation Freq (Hz)", "colorEffectRotationFrequency", 1.0f, 0.001f, 1000f);
        colorEffectRotationSpeedField.onValueChange += (FloatField.FloatValueChangeEvent e) => {colorEffectSpeed = e.value;};
        colorEffectSpeed = colorEffectRotationSpeedField.value;



        FloatField hitstopLengthField = new FloatField(globalHitstopPanel, "Hitstop Length Mult", "hitstopLength", 1.0f, 0.0f, 100.0f);
        hitstopLengthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {hitstopMult = e.value;};
        hitstopMult = hitstopLengthField.value;

        FloatField truestopLengthField = new FloatField(globalHitstopPanel, "Truestop Length Mult", "truestopLength", 1.0f, 0.0f, 100.0f);
        truestopLengthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {truestopMult = e.value;};
        truestopMult = truestopLengthField.value;

        FloatField slowdownLengthField = new FloatField(globalHitstopPanel, "Slowdown Mult", "slowdownMult", 1.0f, 0.0f, 100.0f);
        slowdownLengthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {slowdownMult = e.value;};
        slowdownMult = slowdownLengthField.value;

        new ConfigHeader(globalHitstopPanel, "------------------------------------------------------").textSize = 16;

        FloatField jackhammerTruestopLengthField = new FloatField(globalHitstopPanel, "Jackhammer Truestop Mult Add", "jackhammerAddTimeStop", 0.0f, -100.0f, 100.0f);
        jackhammerTruestopLengthField.onValueChange += (FloatField.FloatValueChangeEvent e) => {jackhammerAddTimeStop = e.value;};
        jackhammerAddTimeStop = jackhammerTruestopLengthField.value;


        ConfigHeader crosshairWarningHeader = new ConfigHeader(globalCrosshairPanel, "May need to restart game to restore base game crosshair. UltrakillBase will keep the base crosshair as default.");
        crosshairWarningHeader.textSize = 12;

        EnumField<CrossHairEnum> crosshairStyleField = new EnumField<CrossHairEnum>(globalCrosshairPanel, "Crosshair Style", "mainCrosshairStyle", CrossHairEnum.UltrakillBase);
        ConfigDivision crosshairDivision = new ConfigDivision(globalCrosshairPanel, "mainCrosshairDivision");
        crosshairStyleField.onValueChange += (EnumField<CrossHairEnum>.EnumValueChangeEvent e) => {mainCrossHairEnum = e.value; if(e.value == CrossHairEnum.UltrakillBase) {crosshairDivision.interactable = false;} else {{crosshairDivision.interactable = true;}}};
        mainCrossHairEnum = crosshairStyleField.value; if(crosshairStyleField.value == CrossHairEnum.UltrakillBase) {crosshairDivision.interactable = false;} else {{crosshairDivision.interactable = true;}}

        FloatField crosshairScaleField = new FloatField(crosshairDivision, "Crosshair Scale", "mainCrosshairScale", 1f);
        crosshairScaleField.onValueChange += (FloatField.FloatValueChangeEvent e) => {mainCrossHairScale = e.value;};
        mainCrossHairScale = crosshairScaleField.value;

        BoolField crosshairKeepInfoField = new BoolField(crosshairDivision, "Keep Crosshair Info", "keepMainCrosshairInfo", true);
        crosshairKeepInfoField.onValueChange += (BoolField.BoolValueChangeEvent e) => {mainCrossHairKeepInfo = e.value;};
        mainCrossHairKeepInfo = crosshairKeepInfoField.value;

        ColorField crosshairColorField = new ColorField(crosshairDivision, "Crosshair Color", "mainCrosshairColor", Color.white);
        crosshairColorField.onValueChange += (ColorField.ColorValueChangeEvent e) => {mainCrossHairColor = e.value;};
        mainCrossHairColor = crosshairColorField.value;

        FloatField crosshairOpacityField = new FloatField(crosshairDivision, "Crosshair Opacity", "mainCrosshairOpacity", 1f, 0f, 1f);
        crosshairOpacityField.onValueChange += (FloatField.FloatValueChangeEvent e) => {mainCrossHairOpacity = e.value;};
        mainCrossHairOpacity = crosshairOpacityField.value;




        ConfigPanel revolverPanel = new ConfigPanel(division, "Revolver", "revolverPanel");
        revolverPanel.fieldColor = new Color(0f, 0f, 0.2f);

        ConfigPanel revolver0Panel = new ConfigPanel(revolverPanel, "Piercer", "revolver0Panel");
        CreateBaseFields(revolver0Panel, 0);
        CreateRevolverFields(revolver0Panel, 0);

        ConfigPanel revolver1Panel = new ConfigPanel(revolverPanel, "Marksman", "revolver1Panel");
        CreateBaseFields(revolver1Panel, 1);
        CreateRevolverFields(revolver1Panel, 1);

        ConfigPanel revolver2Panel = new ConfigPanel(revolverPanel, "Sharpshooter", "revolver2Panel");
        CreateBaseFields(revolver2Panel, 2);
        CreateRevolverFields(revolver2Panel, 2);

        ConfigPanel shotgunPanel = new ConfigPanel(division, "Shotgun", "shotgunPanel");
        shotgunPanel.fieldColor = new Color(0.2f, 0f, 0f);

        ConfigPanel shotgun0Panel = new ConfigPanel(shotgunPanel, "Core Eject", "shotgun0Panel");
        CreateBaseFields(shotgun0Panel, 3);
        CreateShotgunFields(shotgun0Panel, 0);

        ConfigPanel shotgun1Panel = new ConfigPanel(shotgunPanel, "Pump Charge", "shotgun1Panel");
        CreateBaseFields(shotgun1Panel, 4);
        CreateShotgunFields(shotgun1Panel, 1);

        ConfigPanel shotgun2Panel = new ConfigPanel(shotgunPanel, "Sawed-On", "shotgun2Panel");
        CreateBaseFields(shotgun2Panel, 5);
        CreateShotgunFields(shotgun2Panel, 2);

        ConfigPanel nailgunPanel = new ConfigPanel(division, "Nailgun", "nailgunPanel");
        nailgunPanel.fieldColor = new Color(0f, 0.2f, 0f);

        ConfigPanel nailgun0Panel = new ConfigPanel(nailgunPanel, "Attractor", "nailgun0Panel");
        CreateBaseFields(nailgun0Panel, 6);
        CreateNailgunFields(nailgun0Panel, 0);

        ConfigPanel nailgun1Panel = new ConfigPanel(nailgunPanel, "Overheat", "nailgun1Panel");
        CreateBaseFields(nailgun1Panel, 7);
        CreateNailgunFields(nailgun1Panel, 1);

        ConfigPanel nailgun2Panel = new ConfigPanel(nailgunPanel, "Jumpstart", "nailgun2Panel");
        CreateBaseFields(nailgun2Panel, 8);
        CreateNailgunFields(nailgun2Panel, 2);

        ConfigPanel railcannonPanel = new ConfigPanel(division, "Railcannon", "railcannonPanel");
        railcannonPanel.fieldColor = new Color(0.12f, 0.0f, 0.12f);

        ConfigPanel railcannon0Panel = new ConfigPanel(railcannonPanel, "Electric", "railcannonPanel");
        CreateBaseFields(railcannon0Panel, 9);
        CreateRailcannonFields(railcannon0Panel, 0);

        ConfigPanel railcannon1Panel = new ConfigPanel(railcannonPanel, "Screwdriver", "railcannonPanel");
        CreateBaseFields(railcannon1Panel, 10);
        CreateRailcannonFields(railcannon1Panel, 1);

        ConfigPanel railcannon2Panel = new ConfigPanel(railcannonPanel, "Malicious", "railcannonPanel");
        CreateBaseFields(railcannon2Panel, 11);
        CreateRailcannonFields(railcannon2Panel, 2);

        ConfigPanel rocket_launcherPanel = new ConfigPanel(division, "Rocket Launcher", "rocket_launcherPanel");
        rocket_launcherPanel.fieldColor = new Color(0.12f, 0.12f, 0.0f);

        ConfigPanel rocket_launcher0Panel = new ConfigPanel(rocket_launcherPanel, "Freezeframe", "rocket_launcherPanel");
        CreateBaseFields(rocket_launcher0Panel, 12);
        CreateRocketLauncherFields(rocket_launcher0Panel, 0);

        ConfigPanel rocket_launcher1Panel = new ConfigPanel(rocket_launcherPanel, "SRS", "rocket_launcherPanel");
        CreateBaseFields(rocket_launcher1Panel, 13);
        CreateRocketLauncherFields(rocket_launcher1Panel, 1);

        ConfigPanel rocket_launcher2Panel = new ConfigPanel(rocket_launcherPanel, "Firestarter", "rocket_launcherPanel");
        CreateBaseFields(rocket_launcher2Panel, 14);
        CreateRocketLauncherFields(rocket_launcher2Panel, 2);
    }
}