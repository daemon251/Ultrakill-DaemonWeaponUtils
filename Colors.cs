using UnityEngine;
using HarmonyLib;
using System.IO;
using System.Reflection;
using DaemonWeaponUtilsPlugin;
using System.Collections.Generic;
using System;
using System.Linq;
public class Colors
{
    public static string DefaultParentFolder = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";
    //public static Texture2D DefaultSmallTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    //public static Texture2D DefaultLargeTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    
    public static Texture2D BigFlashTexture = new Texture2D(0, 0, TextureFormat.RGBAFloat, false);
    public static Texture2D SmallFlashTexture = new Texture2D(0, 0, TextureFormat.RGBAFloat, false);
    public static Texture2D PiercerAltChargeTexture = new Texture2D(0, 0, TextureFormat.RGBAFloat, false);
    
    public static Color HSVToColor(float h, float S, float V) //copied from internet somewhere
    {    
        float H = h;
        while (H < 0) { H += 360; };
        while (H >= 360) { H -= 360; };
        float R, G, B;
        if (V <= 0)
            { R = G = B = 0; }
        else if (S <= 0)
        {
            R = G = B = V;
        }
        else
        {
            float hf = H / 60.0f;
            int i = (int)Math.Floor(hf);
            float f = hf - i;
            float pv = V * (1 - S);
            float qv = V * (1 - S * f);
            float tv = V * (1 - S * (1 - f));
            switch (i)
            {

            // Red is the dominant color

            case 0:
                R = V;
                G = tv;
                B = pv;
                break;

            // Green is the dominant color

            case 1:
                R = qv;
                G = V;
                B = pv;
                break;
            case 2:
                R = pv;
                G = V;
                B = tv;
                break;

            // Blue is the dominant color

            case 3:
                R = pv;
                G = qv;
                B = V;
                break;
            case 4:
                R = tv;
                G = pv;
                B = V;
                break;

            // Red is the dominant color

            case 5:
                R = V;
                G = pv;
                B = qv;
                break;

            // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

            case 6:
                R = V;
                G = tv;
                B = pv;
                break;
            case -1:
                R = V;
                G = pv;
                B = qv;
                break;

            // The color is not defined, we should throw an error.

            default:
                //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                R = G = B = V; // Just pretend its black/white
                break;
            }
        }
        Color color = new Color(R,G,B);
        return color;
    }

    public static Color SpecialColorLogic(ModConfig.SpecialColorEnum sc, Color currentColor, Color settingColor, bool firstTime = false)
    {
        if(sc == ModConfig.SpecialColorEnum.Default) {return settingColor;}
        //check for if white in case color hasnt been set yet
        else if(sc == ModConfig.SpecialColorEnum.RandomRainbow && (currentColor == settingColor || currentColor == null || currentColor == new Color(1f, 1f, 1f) || firstTime == true)) {return GetRandomRainbowColor();}
        else if(sc == ModConfig.SpecialColorEnum.TimeBasedRainbow) {return GetCurrentRainbowColor();}
        else if(sc == ModConfig.SpecialColorEnum.Confetti) {return GetRandomRainbowColor();}
        else if(sc == ModConfig.SpecialColorEnum.PulseWhite) {return GetCurrentPulseColor(settingColor, Color.white);}
        else if(sc == ModConfig.SpecialColorEnum.PulseBlack) {return GetCurrentPulseColor(settingColor, Color.black);}
        else if(sc == ModConfig.SpecialColorEnum.HSVRandom && (currentColor == settingColor || currentColor == null || currentColor == new Color(1f, 1f, 1f) || firstTime == true)) {return GetRandomHSV();}
        else if(sc == ModConfig.SpecialColorEnum.RGBRandom && (currentColor == settingColor || currentColor == null || currentColor == new Color(1f, 1f, 1f) || firstTime == true)) {return GetRandomRGB();}
        else {return currentColor;}
    }

    public static Color GetCurrentPulseColor(Color settingColor, Color pulseColor) //pulses between settingColor and pulseColor, base 1Hz
    {
        //frequency 1Hz
        float currentPulseFraction = 0.5f + Convert.ToSingle(Math.Sin(Time.time * ModConfig.colorEffectSpeed * 2 * Math.PI)) / 2.0f;
        Color outputColor = new Color();
        outputColor.r = settingColor.r * (1f - currentPulseFraction) + pulseColor.r * currentPulseFraction;
        outputColor.g = settingColor.g * (1f - currentPulseFraction) + pulseColor.g * currentPulseFraction;
        outputColor.b = settingColor.b * (1f - currentPulseFraction) + pulseColor.b * currentPulseFraction;
        outputColor.a = 1f;
        return outputColor;
    }


    public static Color GetRandomRainbowColor()
    {
        System.Random rand = new System.Random();
        return HSVToColor(Convert.ToSingle(rand.NextDouble() * 360.0), 1.0f, 1.0f);
    }

    public static Color GetCurrentRainbowColor() //returns a color where the color is based on the current time. Frequency of 1Hz base
    {
        return HSVToColor(Convert.ToSingle(Time.time * 360.0 * ModConfig.colorEffectSpeed), 1.0f, 1.0f);
    }

    public static Color GetRandomHSV()
    {
        System.Random rand = new System.Random();
        return HSVToColor(Convert.ToSingle(rand.NextDouble() * 360.0), Convert.ToSingle(rand.NextDouble()), Convert.ToSingle(rand.NextDouble()));
    }

    public static Color GetRandomRGB()
    {
        System.Random rand = new System.Random();
        Color color = new Color(Convert.ToSingle(rand.NextDouble()), Convert.ToSingle(rand.NextDouble()), Convert.ToSingle(rand.NextDouble()));
        return color;
    }

    public static void LoadImages()
    {
        BigFlashTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "BigFlash.png")}"));
        SmallFlashTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "SmallFlash.png")}"));
        PiercerAltChargeTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "PiercerCharge.png")}"));

        BigFlashTexture.filterMode = FilterMode.Point;
        SmallFlashTexture.filterMode = FilterMode.Point;
        PiercerAltChargeTexture.filterMode = FilterMode.Point;
    }
    public static void ColorGameObject(GameObject gameObject, Color color)
    {
        MeshRenderer component2;
        if (gameObject.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
        {
            component2.material.SetColor("_Color", color);
        }
        TrailRenderer component3;
        if (gameObject.TryGetComponent<TrailRenderer>(out component3))
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[2]
            {
            new GradientColorKey(color, 0.0f),
            new GradientColorKey(color, 1f)
            }, new GradientAlphaKey[2]
            {
            new GradientAlphaKey(1f, 0.0f),
            new GradientAlphaKey(0.0f, 1f)
            });
            component3.colorGradient = gradient;
        }
        Light component4;
        if (gameObject.TryGetComponent<Light>(out component4)) {component4.color = color;}
    }
    
    public static List<RevolverBeam> revolverBeams = new List<RevolverBeam>();
    public static List<Projectile> projectiles = new List<Projectile>();
    public static List<Nail> nails = new List<Nail>();
    public static List<Harpoon> harpoons = new List<Harpoon>();

    public static void ColorEverything()
    {
        if(ModConfig.customColors == false || Plugin.modEnabled == false) {return;}

        Plugin.logger.LogInfo(projectiles.Count);
        for(int i = 0; i < projectiles.Count; i++)
        {
            Projectile projectile = projectiles[i];
            if(projectile == null) {projectiles.Remove(projectile); i += -1; continue;} 
            //if(projectiles.Contains(projectile)) {continue;} //continue if we've already seen this projectile //removed to make rainbow cooler
            if(projectile.parried)
            {
                if(ModConfig.parryProjectileColored == false) {continue;}
                Color color = ModConfig.parryProjectileColor;
                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (projectile.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.parrySpecialColor, currentColor, color);

                //copied from projectile parry code
                MeshRenderer component2;
                if (projectile.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    component2.material.SetColor("_Color", color);
                }

                //now set skull texture color
                if(projectile.gameObject.transform.childCount != 0)
                {
                    GameObject go1 = projectile.gameObject.transform.GetChild(0).gameObject;
                    MeshRenderer component2a; //homing projectile fix? doesnt work so well
                    if (go1.TryGetComponent<MeshRenderer>(out component2a) && (bool) (UnityEngine.Object) component2a.material && component2a.material.HasProperty("_Color"))
                    {
                        component2a.material.SetColor("_Color", color);
                    }
                    if(go1.transform.childCount != 0)
                    {
                        GameObject go2 = go1.transform.GetChild(0).gameObject;
                        MeshRenderer component2b; //normal projectile fix
                        if (go2.TryGetComponent<MeshRenderer>(out component2b) && (bool) (UnityEngine.Object) component2b.material && component2b.material.HasProperty("_Color"))
                        {
                            component2b.material.SetColor("_Color", color);
                        }
                    }
                }
                

                TrailRenderer component3;
                if (projectile.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (projectile.TryGetComponent<Light>(out component4)) {component4.color = color;}
            }
            else if(projectile.weaponType == "shotgun0" ||  projectile.weaponType == "shotgun1" || projectile.weaponType == "shotgun2")
            {
                int variation = -1;
                if(projectile.weaponType == "shotgun0") {variation = 0;}
                else if(projectile.weaponType == "shotgun1") {variation = 1;}
                else if(projectile.weaponType == "shotgun2") {variation = 2;}
                Color color = ModConfig.shotgunPelletColors[variation];
                if(ModConfig.enableColors[1, variation] == false) {break;}

                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (projectile.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[1, variation], currentColor, color);

                //copied from projectile parry code
                MeshRenderer component2;
                if (projectile.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    component2.material.SetColor("_Color", color);

                    component2.material.mainTexture = Texture2D.whiteTexture;
                }

                TrailRenderer component3;
                if (projectile.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (projectile.TryGetComponent<Light>(out component4)) {component4.color = color;}
                //muzzleflash
                ColorGameObject(projectile.sourceWeapon.GetComponent<Shotgun>().muzzleFlash, color);
            }
        }
        foreach(Grenade grenade in MonoSingleton<ObjectTracker>.Instance.grenadeList)
        {
            //if(grenades.Contains(grenade)) {continue;}
            if(grenade != null && grenade.name == "Grenade(Clone)")
            {
                Color color = ModConfig.coreEjectColor;
                if(ModConfig.enableColors[1, 0] == false) {break;}

                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (grenade.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.coreEjectSpecialColor, currentColor, color);

                //copied from projectile parry code
                MeshRenderer component2;
                if (grenade.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    component2.material.SetColor("_Color", color);
                }
                TrailRenderer component3;
                if (grenade.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (grenade.TryGetComponent<Light>(out component4)) {component4.color = color;}

                Color color2 = new Color(color.r, color.g, color.b, Convert.ToSingle(1.1f * Math.Sin(Time.time * 30 * 2 * Math.PI))); //attempts to replicate vanilla behavior
                GameObject muzzleFlash = grenade.transform.GetChild(1).gameObject;
                GameObject muzzleFlashReal = muzzleFlash.transform.GetChild(0).gameObject;
                SpriteRenderer muzzleFlashSpriteRenderer = muzzleFlashReal.GetComponent<SpriteRenderer>();
                //Texture2D desaturatedTexture = DesaturateTexture(flashTexture); //muzzleFlashSpriteRender.sprite.texture
                Sprite newSprite = Sprite.Create(BigFlashTexture, new Rect(0, 0, BigFlashTexture.width, BigFlashTexture.height), new Vector2(0.5f, 0.5f));
                muzzleFlashSpriteRenderer.sprite = newSprite;
                muzzleFlashSpriteRenderer.color = color2;
            }
            else if(grenade != null && grenade.name == "Rocket(Clone)")
            {
                int variation = -1;
                if     (grenade.sourceWeapon.GetComponent<RocketLauncher>().variation == 0) {variation = 0;}
                else if(grenade.sourceWeapon.GetComponent<RocketLauncher>().variation == 1) {variation = 1;}
                else if(grenade.sourceWeapon.GetComponent<RocketLauncher>().variation == 2) {variation = 2;}
                Color color = ModConfig.rocketLauncherRocketColors[variation];
                if(ModConfig.enableColors[4, variation] == false) {break;}

                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (grenade.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[4, variation], currentColor, color);

                //copied from projectile parry code
                MeshRenderer component2;
                if (grenade.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    component2.material.SetColor("_Color", color);
                }
                TrailRenderer component3;
                if (grenade.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (grenade.TryGetComponent<Light>(out component4)) {component4.color = color;}
                //ColorMuzzleFlash(grenade.sourceWeapon.GetComponent<RocketLauncher>().muzzleFlash, color);

                GameObject muzzleFlash = grenade.transform.GetChild(1).gameObject;
                GameObject muzzleFlashReal = muzzleFlash.transform.GetChild(0).gameObject;
                SpriteRenderer muzzleFlashSprite = muzzleFlashReal.GetComponent<SpriteRenderer>();
                muzzleFlashSprite.color = color;



                GameObject FreezeEffect = grenade.freezeEffect;
                GameObject Sprite = FreezeEffect.transform.GetChild(0).gameObject;
                GameObject RealSprite = Sprite.transform.GetChild(0).gameObject;

                Sprite.GetComponent<SpriteGetVariationColor>().enabled = false; //prevents auto color switch

                Color freezeframeColor = ModConfig.freezeframeColor;
                freezeframeColor = SpecialColorLogic(ModConfig.freezeframeSpecialColor, RealSprite.GetComponent<SpriteRenderer>().color, freezeframeColor);

                RealSprite.GetComponent<SpriteRenderer>().color = freezeframeColor;
                GameObject RealSprite1 = RealSprite.transform.GetChild(0).gameObject;
                GameObject RealSprite2 = RealSprite.transform.GetChild(1).gameObject;
                GameObject RealSprite3 = RealSprite.transform.GetChild(2).gameObject;
                GameObject RealSprite4 = RealSprite.transform.GetChild(3).gameObject;

                RealSprite1.GetComponent<SpriteRenderer>().color = freezeframeColor;
                RealSprite2.GetComponent<SpriteRenderer>().color = freezeframeColor;
                RealSprite3.GetComponent<SpriteRenderer>().color = freezeframeColor;
                RealSprite4.GetComponent<SpriteRenderer>().color = freezeframeColor;
            }
        }
        for(int i = 0; i < harpoons.Count; i++)
        {
            Harpoon harpoon = harpoons[i];
            if(harpoon == null) {harpoons.Remove(harpoon); i += -1; continue;}

            Color color = new Color();
            //if(harpoons.Contains(harpoon)) {continue;}
            if(harpoon.sourceWeapon != null && harpoon.sourceWeapon.GetComponent<Railcannon>() != null)
            {
                color = ModConfig.railcannonBeamColors[1];
                if(ModConfig.enableColors[3, 1] == false) {break;}

                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (harpoon.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[3, 1], currentColor, color);
            }
            /*else if(harpoon.sourceWeapon != null && harpoon.sourceWeapon.GetComponent<Nailgun>() != null)
            {
                color = ModConfig.nailgunNailColors[harpoon.sourceWeapon.GetComponent<Nailgun>().variation];
                if(ModConfig.enableColors[2, harpoon.sourceWeapon.GetComponent<Nailgun>().variation] == false) {break;}

                MeshRenderer componentMesh;
                Color currentColor = new Color(1f, 1f, 1f);
                if (harpoon.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                {
                    currentColor = componentMesh.material.GetColor("_Color");
                }
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[2, harpoon.sourceWeapon.GetComponent<Nailgun>().variation], currentColor, color);
            }*/
            else {continue;}
            

            //copied from projectile parry code
            MeshRenderer component2;
            if (harpoon.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
            {
                component2.material.SetColor("_Color", color);
            }
            TrailRenderer component3;
            if (harpoon.TryGetComponent<TrailRenderer>(out component3))
            {
                Gradient gradient = new Gradient();
                gradient.SetKeys(new GradientColorKey[2]
                {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1f)
                }, new GradientAlphaKey[2]
                {
                new GradientAlphaKey(1f, 0.0f),
                new GradientAlphaKey(0.0f, 1f)
                });
                component3.colorGradient = gradient;
            }
            Light component4;
            if (harpoon.TryGetComponent<Light>(out component4)) {component4.color = color;}
        }
        for(int i = 0; i < nails.Count; i++)
        {
            Nail nail = nails[i];
            if(nail == null) {nails.Remove(nail); i += -1; continue;}

            //if(nails.Contains(nail) && ModConfig.nailFollowCurrentRainbow == false){continue;} 
            int variation = -1;
            if(nail.sourceWeapon.GetComponent<Nailgun>() != null)
            {
                if(nail.sourceWeapon.GetComponent<Nailgun>().variation == 1) {variation = 0;}
                else if(nail.sourceWeapon.GetComponent<Nailgun>().variation == 0) {variation = 1;}
                else if(nail.sourceWeapon.GetComponent<Nailgun>().variation == 2) {variation = 2;}
                Color color = ModConfig.nailgunNailColors[variation];
                if(ModConfig.enableColors[2, variation] == false) {break;}
                
                //TrailRenderer componentTrail;
                //Color currentColor = new Color(1f, 1f, 1f);
                //if (nail.TryGetComponent<TrailRenderer>(out componentTrail) && (bool) (UnityEngine.Object) componentTrail.material && componentTrail.material.HasProperty("_Color"))
                //{
                //    currentColor = componentTrail.material.GetColor("_Color");
                //}
                Color currentColor = nail.GetComponent<TrailRenderer>().colorGradient.colorKeys[0].color;
                if(currentColor.r < 0.01f && Math.Abs(currentColor.g - 0.8745f) < 0.01f && currentColor.b > 0.99f) //default saw color
                {
                    currentColor = new Color(1f, 1f, 1f); //scuffed
                }
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[2, variation], currentColor, color);

                //copied from projectile parry code
                MeshRenderer component2;
                if (nail.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    component2.material.SetColor("_Color", color);
                }
                TrailRenderer component3;
                if (nail.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (nail.TryGetComponent<Light>(out component4)) {component4.color = color;}
                ColorGameObject(nail.sourceWeapon.GetComponent<Nailgun>().muzzleFlash, color);
                ColorGameObject(nail.sourceWeapon.GetComponent<Nailgun>().muzzleFlash2, color);
            }
        }
        foreach(Cannonball cannonball in MonoSingleton<ObjectTracker>.Instance.cannonballList)
        {
            /*Color color = ModConfig.cannonballColor;
            if(ModConfig.enableColors[4, 1] == false) {break;}

            MeshRenderer componentMesh;
            Color currentColor = new Color(1f, 1f, 1f);
            if (cannonball.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
            {
                currentColor = componentMesh.material.GetColor("_Color");
            }
            color = SpecialColorLogic(ModConfig.cannonballSpecialColor, currentColor, color);

            //copied from projectile parry code
            MeshRenderer component2;
            if (cannonball.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
            {
                component2.material.SetColor("_Color", color);

                component2.material.mainTexture = Texture2D.whiteTexture;
            }

            TrailRenderer component3;
            if (cannonball.transform.GetChild(0).gameObject.TryGetComponent<TrailRenderer>(out component3))
            {
                Gradient gradient = new Gradient();
                gradient.SetKeys(new GradientColorKey[2]
                {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1f)
                }, new GradientAlphaKey[2]
                {
                new GradientAlphaKey(1f, 0.0f),
                new GradientAlphaKey(0.0f, 1f)
                });
                component3.colorGradient = gradient;
            }
            Light component4;
            if (cannonball.TryGetComponent<Light>(out component4)) {component4.color = color;}*/
        }
        foreach(Coin coin in MonoSingleton<CoinList>.Instance.revolverCoinsList)
        {
            if(ModConfig.coinSettingsToggle == false || ModConfig.enableColors[0, 1] == false) {break;}
            Color coinColor = ModConfig.coinColor;
            Color trailColor = ModConfig.coinTrailColor;

            MeshRenderer componentMesh;
            Color currentCoinColor = new Color(1f, 1f, 1f);
            if (coin.transform.GetChild(0).gameObject.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
            {
                currentCoinColor = componentMesh.material.GetColor("_Color");
            }
            coinColor = SpecialColorLogic(ModConfig.coinEntitySpecialColor, currentCoinColor, coinColor);

            TrailRenderer componentTrail;
            Color currentCoinTrailColor = new Color(1f, 1f, 1f);
            if (coin.TryGetComponent<TrailRenderer>(out componentTrail))
            {
                currentCoinTrailColor = componentTrail.startColor;
            }
            trailColor = SpecialColorLogic(ModConfig.coinTrailSpecialColor, currentCoinTrailColor, trailColor);

            //copied from projectile parry code
            MeshRenderer component2;
            if (coin.transform.GetChild(0).gameObject.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
            {
                component2.material.SetColor("_Color", coinColor);
            }
            TrailRenderer component3;
            if (coin.TryGetComponent<TrailRenderer>(out component3))
            {
                Gradient gradient = new Gradient();
                gradient.SetKeys(new GradientColorKey[2]
                {
                new GradientColorKey(trailColor, 0.0f),
                new GradientColorKey(trailColor, 1f)
                }, new GradientAlphaKey[2]
                {
                new GradientAlphaKey(1f, 0.0f),
                new GradientAlphaKey(0.0f, 1f)
                });
                component3.colorGradient = gradient;
            }

            if(coin.transform.childCount > 2)
            {
                SpriteRenderer sr = coin.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
                sr.color = SpecialColorLogic(ModConfig.marksmanCoinFlashSpecialColor, sr.color, ModConfig.marksmanCoinFlashColor);
                Sprite newSprite = Sprite.Create(SmallFlashTexture, new Rect(0, 0, SmallFlashTexture.width, SmallFlashTexture.height), new Vector2(0.5f, 0.5f));
                sr.sprite = newSprite;
            }

        }
        for(int i = 0; i < revolverBeams.Count; i++)
        {
            RevolverBeam revolverBeam = revolverBeams[i];
            if(revolverBeam == null) {revolverBeams.Remove(revolverBeam); i += -1; continue;} //is this the only condition to remove it?
            ColorRevolverBeamStart(revolverBeam, false, false);
        }
    }
    public static void ColorLineRenderer(LineRenderer lineRenderer, Color color, float width = 1f)
    {
        if (ModConfig.customColors == false) {return;}
        if (lineRenderer == null) {return;}
        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[2]
        {
            new GradientColorKey(color, 0.0f),
            new GradientColorKey(color, 1f)
        }, new GradientAlphaKey[2]
        {
            new GradientAlphaKey(1f, 0.0f),
            new GradientAlphaKey(1f, 1f)
        });
        lineRenderer.colorGradient = gradient;
        if(width != 1f) {lineRenderer.widthMultiplier *= width;}
    }

    public static void ColorRevolverBeamStart(RevolverBeam revolverBeam, bool changeWidth, bool firstTime = false)
    {
        if(revolverBeam == null || revolverBeam.sourceWeapon == null || ModConfig.customColors == false || revolverBeam.beamType == BeamType.Enemy) {return;}
        if(firstTime == true) {revolverBeams.Add(revolverBeam);}
        Color color = Color.black;
        ModConfig.SpecialColorEnum specialColor = ModConfig.SpecialColorEnum.Default;
        int variation = -1;
        float width = 1f;
        if(revolverBeam.sourceWeapon.GetComponent<Revolver>() != null)
        {
            if(revolverBeam.sourceWeapon.GetComponent<Revolver>().gunVariation == 0) 
            {
                variation = 0;
                specialColor = ModConfig.weaponSpecialColors[0, variation];
                color = ModConfig.revolverBeamColors[variation];
                if(revolverBeam.maxHitsPerTarget > 2) {color = ModConfig.piercerAltBeamColor; specialColor = ModConfig.piercerAltBeamSpecialColor;}
            }
            else if(revolverBeam.sourceWeapon.GetComponent<Revolver>().gunVariation == 1) 
            {
                variation = 1;
                specialColor = ModConfig.weaponSpecialColors[0, variation];
                color = ModConfig.revolverBeamColors[variation];
            }
            else if(revolverBeam.sourceWeapon.GetComponent<Revolver>().gunVariation == 2) 
            {
                variation = 2;
                color = ModConfig.revolverBeamColors[variation];
                specialColor = ModConfig.weaponSpecialColors[0, variation];
                if(revolverBeam.ricochetAmount > 0 || revolverBeam.hasBeenRicocheter) {color = ModConfig.sharpshooterAltBeamColor; specialColor = ModConfig.sharpshooterAltBeamSpecialColor;}
            }
            if(ModConfig.enableColors[0, variation] == false) {return;}
            if(changeWidth == true) 
            {
                width = ModConfig.revolverBeamWidths[variation];
                if(revolverBeam.maxHitsPerTarget > 2 && revolverBeam.sourceWeapon.GetComponent<Revolver>().gunVariation == 0) {width *= ModConfig.piercerAltBeamWidth;}
                else if(revolverBeam.ricochetAmount > 0 || revolverBeam.hasBeenRicocheter) {width *= ModConfig.sharpshooterAltBeamWidth;}
            }

            Color currentColor = Color.white;
            if(revolverBeam.lr != null) {currentColor = revolverBeam.lr.startColor;}
            color = SpecialColorLogic(specialColor, currentColor, color, firstTime);
            if(revolverBeam.muzzleLight != null) {revolverBeam.muzzleLight.color = color; Plugin.logger.LogInfo("a");}
        }
        else if(revolverBeam.sourceWeapon.GetComponent<Railcannon>() != null)
        {
            if(revolverBeam.sourceWeapon.GetComponent<Railcannon>().variation == 0) 
            {
                variation = 0;
                if(changeWidth == true) 
                {
                    width = ModConfig.railcannonBeamWidths[variation];
                }
                color = ModConfig.railcannonBeamColors[variation];
                Color currentColor = Color.white;
                if(revolverBeam.lr != null) {currentColor = revolverBeam.lr.startColor;}
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[3, variation], currentColor, color, firstTime);

                GameObject effect1 = revolverBeam.transform.GetChild(1).gameObject;
                GameObject effect2 = revolverBeam.transform.GetChild(2).gameObject;

                if(ModConfig.electricRailcannonEffectColorChange == true)
                {
                    ColorLineRenderer(effect1.GetComponent<LineRenderer>(), color, width);
                    ColorLineRenderer(effect2.GetComponent<LineRenderer>(), color, width);
                }
                else
                {
                    if(width != 1f) 
                    {
                        effect1.GetComponent<LineRenderer>().widthMultiplier *= width;
                        effect2.GetComponent<LineRenderer>().widthMultiplier *= width;
                    }
                }
                
            }
            else if(revolverBeam.sourceWeapon.GetComponent<Railcannon>().variation == 2) 
            {
                variation = 2;
                if(changeWidth == true) 
                {
                    width = ModConfig.railcannonBeamWidths[variation];
                }
                color = ModConfig.railcannonBeamColors[variation];
                Color currentColor = revolverBeam.muzzleLight.color;
                color = SpecialColorLogic(ModConfig.weaponSpecialColors[3, variation], currentColor, color, firstTime);
            }
            if(ModConfig.enableColors[3, variation] == false) {return;}
            //children color
        }
        else if(revolverBeam.sourceWeapon.GetComponent<ShotgunHammer>() != null)
        {
            if(ModConfig.jackhammerCustomBeamColor == false) {return;}
            
            Color currentColor = revolverBeam.muzzleLight.color;
            color = ModConfig.jackhammerBeamColor;
            //set white so itll work with randomRainbow
            color = SpecialColorLogic(ModConfig.jackhammerBeamSpecialColor, currentColor, color, firstTime);

            revolverBeam.muzzleLight.color = color;
        }
        if(ModConfig.colorMuzzleFlashes)
        {
            if(revolverBeam.transform.childCount != 0)
            {
                GameObject muzzleFlash = revolverBeam.transform.GetChild(0).gameObject;
                if(muzzleFlash.transform.childCount != 0)
                {
                    GameObject muzzleFlashReal = muzzleFlash.transform.GetChild(0).gameObject;
                    SpriteRenderer muzzleFlashSpriteRenderer = muzzleFlashReal.GetComponent<SpriteRenderer>();
                    Sprite newSprite = Sprite.Create(SmallFlashTexture, new Rect(0, 0, SmallFlashTexture.width, SmallFlashTexture.height), new Vector2(0.5f, 0.5f));
                    muzzleFlashSpriteRenderer.sprite = newSprite;
                    muzzleFlashSpriteRenderer.color = color;
                    if(muzzleFlashReal.transform.childCount != 0)
                    {
                        GameObject MuzzleFlashULTRAReal = muzzleFlashReal.transform.GetChild(0).gameObject; //seriously what the fuck
                        SpriteRenderer muzzleFlashULTRASpriteRenderer = MuzzleFlashULTRAReal.GetComponent<SpriteRenderer>();
                        muzzleFlashULTRASpriteRenderer.sprite = newSprite;
                        muzzleFlashULTRASpriteRenderer.color = color;
                    }
                    //ColorMuzzleFlash(muzzleFlashReal, color);
                }
            }
        }
        ColorLineRenderer(revolverBeam.GetComponent<LineRenderer>(), color, width);
    }
    [HarmonyPatch(typeof(Shotgun), "Shoot")]
    public class ShotgunShootPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            if(ModConfig.customColors == false) {return;}
            if(Plugin.modEnabled == false) {return;}

            int variation = -1;
            if     (MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().variation == 0) {variation = 0;}
            else if(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().variation == 1) {variation = 1;}
            else if(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().variation == 2) {variation = 2;}
            Color color = ModConfig.shotgunPelletColors[variation];
            if(ModConfig.enableColors[1, variation] == false) {return;}

            MeshRenderer componentMesh;
            Color currentColor = new Color(1f, 1f, 1f);
            if (MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().muzzleFlash.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
            {
                currentColor = componentMesh.material.GetColor("_Color");
            }
            color = SpecialColorLogic(ModConfig.weaponSpecialColors[1, variation], currentColor, color);

            ColorGameObject(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().muzzleFlash, color);

            if(ModConfig.colorMuzzleFlashes)
            {
                GameObject muzzleFlash = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().muzzleFlash.transform.GetChild(0).gameObject;
                GameObject muzzleFlashReal = muzzleFlash.transform.GetChild(0).gameObject;
                SpriteRenderer muzzleFlashSpriteRenderer = muzzleFlashReal.GetComponent<SpriteRenderer>();
                Sprite newSprite = Sprite.Create(BigFlashTexture, new Rect(0, 0, BigFlashTexture.width, BigFlashTexture.height), new Vector2(0.5f, 0.5f));
                muzzleFlashSpriteRenderer.sprite = newSprite;
                muzzleFlashSpriteRenderer.color = color;
            }
        }
    
        /*[HarmonyPostfix]
        private static void Postfix()
        {
            if(ModConfig.customColors == false) {return;}
            if(Plugin.modEnabled == false) {return;}

            //slightly inefficent
            projectiles = (UnityEngine.Object.FindObjectsOfType(typeof(Projectile)) as Projectile[]).ToList<Projectile>();
        }*/
    }

    [HarmonyPatch(typeof(Projectile), "Start")]
    public class ProjectileStartpatch
    {
        [HarmonyPostfix]
        private static void Postfix(Projectile __instance)
        {
            //there isnt an OnDestroy or something so we null check and remove later if needed
            projectiles.Add(__instance);
        }
    }

    [HarmonyPatch(typeof(Nail), "Awake")]
    public class NailAwakePatch
    {
        [HarmonyPostfix]
        private static void Postfix(Nail __instance)
        {
            nails.Add(__instance);
        }
    }
    
    [HarmonyPatch(typeof(Nail), "OnDestroy")]
    public class NailDestroyPatch
    {
        [HarmonyPostfix]
        private static void Postfix(Nail __instance)
        {
            nails.Remove(__instance);
        }
    }

    [HarmonyPatch(typeof(Harpoon), "Start")]
    public class HarpoonStartPatch
    {
        [HarmonyPostfix]
        private static void Postfix(Harpoon __instance)
        {
            harpoons.Add(__instance);
        }
    }
    
    [HarmonyPatch(typeof(Harpoon), "OnDestroy")]
    public class HarpoonDestroyPatch
    {
        [HarmonyPostfix]
        private static void Postfix(Harpoon __instance)
        {
            harpoons.Remove(__instance);
        }
    }

    [HarmonyPatch(typeof(Shotgun), "ShootSinks")]
    public class ShotgunShootSinksPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Color color = ModConfig.coreEjectColor;
            if(ModConfig.customColors == false) {return;}
            if(Plugin.modEnabled == false) {return;}
            if(ModConfig.enableColors[1, 0] == false) {return;}
            ColorGameObject(MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().muzzleFlash, color);

            if(ModConfig.colorMuzzleFlashes)
            {
                GameObject muzzleFlash = MonoSingleton<GunControl>.Instance.currentWeapon.GetComponent<Shotgun>().muzzleFlash.transform.GetChild(0).gameObject;
                GameObject muzzleFlashReal = muzzleFlash.transform.GetChild(0).gameObject;
                SpriteRenderer muzzleFlashSpriteRenderer = muzzleFlashReal.GetComponent<SpriteRenderer>();
                Sprite newSprite = Sprite.Create(BigFlashTexture, new Rect(0, 0, BigFlashTexture.width, BigFlashTexture.height), new Vector2(0.5f, 0.5f));
                muzzleFlashSpriteRenderer.sprite = newSprite;
                muzzleFlashSpriteRenderer.color = color;
            }
        }
    }
    
    //this doesnt really do anything anymore but I'll keep it I guess
    [HarmonyPatch(typeof(Punch), "ParryProjectile")]
    public class ParryProjectilePatch
    {
        [HarmonyPostfix]
        private static void Postfix(Projectile proj)
        {
            Color color = ModConfig.parryProjectileColor;
            if(ModConfig.customColors == false) {return;}
            if(Plugin.modEnabled == false) {return;}
            if(ModConfig.parryProjectileColored == false) {return;}

            //slightly inefficent
            projectiles = (UnityEngine.Object.FindObjectsOfType(typeof(Projectile)) as Projectile[]).ToList<Projectile>();

            Color currentColor = new Color(1f, 1f, 1f); //first tick its active, of course its the default color
            color = SpecialColorLogic(ModConfig.parrySpecialColor, currentColor, color);

            //copied from projectile parry code
            MeshRenderer component2;
            if (proj.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
            {
                component2.material.SetColor("_Color", color);
            }
            TrailRenderer component3;
            if (proj.TryGetComponent<TrailRenderer>(out component3))
            {
                Gradient gradient = new Gradient();
                gradient.SetKeys(new GradientColorKey[2]
                {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1f)
                }, new GradientAlphaKey[2]
                {
                new GradientAlphaKey(1f, 0.0f),
                new GradientAlphaKey(0.0f, 1f)
                });
                component3.colorGradient = gradient;
            }
            Light component4;
            if (proj.TryGetComponent<Light>(out component4)) {component4.color = color;}
        }
    }

    /*[HarmonyPatch(typeof(Coin), "SpawnBeam")]
    public class CoinSpawnBeamPatch
    {
        [HarmonyPostfix]
        private static void Postfix(ref GameObject __result)
        {
            if(__result == null){return;}
            ColorRevolverBeam(__result.GetComponent<LineRenderer>(), __result.GetComponent<RevolverBeam>(), true, Color.white);
        }
    }*/

    //object reference not set to instance of an object
    [HarmonyPatch(typeof(RevolverBeam), "Start")]
    public class RevolverBeamStartPatch
    {
        [HarmonyPostfix]
        private static void Postfix(RevolverBeam __instance)
        {
            if(__instance == null) {return;}
            ColorRevolverBeamStart(__instance, true, true);
        }
    }

    [HarmonyPatch(typeof(Coin), "DelayedReflectRevolver")]
    public class CoinDelayedReflectRevolverPatch
    {
        [HarmonyPostfix]
        private static void Postfix(GameObject beam)
        {
            if(beam == null || beam.GetComponent<RevolverBeam>() == null) {return;}
            ColorRevolverBeamStart(beam.GetComponent<RevolverBeam>(), true, true);
        }
    }

    public static Texture2D ColoredPiercerAltTexture = null;
    public static Color LastPiercerAltTextureColor = Color.white;
    public static void MakeColoredPiercerAltTexture(Color color)
    {
        //white remains white, colors dark pixels with color based on how dark they are (dark -> color, no darkness remains)
        Texture2D outputTexture = new Texture2D(PiercerAltChargeTexture.width, PiercerAltChargeTexture.height);
        for(int x = 0; x < PiercerAltChargeTexture.width; x++)
        {
            for(int y = 0; y < PiercerAltChargeTexture.height; y++)
            {
                Color pxColor = PiercerAltChargeTexture.GetPixel(x, y);
                float darkness = 1f - ((pxColor.r + pxColor.g + pxColor.b) / 3f); //1 is very dark

                float red = 1f - darkness * (1f - color.r);
                float green = 1f - darkness * (1f - color.g);
                float blue = 1f - darkness * (1f - color.b);

                Color outputColor = new Color(red, green, blue, pxColor.a);
                outputTexture.SetPixel(x, y, outputColor);
            }
        }
        ColoredPiercerAltTexture = outputTexture;
        ColoredPiercerAltTexture.SetPixels(outputTexture.GetPixels());
        ColoredPiercerAltTexture.Apply();
        LastPiercerAltTextureColor = color;
    }

    public static float NextTimeUpdatePiercerAltTexture = 0f;
    public static void OtherColorEffects()
    {
        if(Plugin.modEnabled == false || ModConfig.customColors == false || ModConfig.enableColors[0,0] == false) {return;}
        if(Plugin.weapon.GetComponent<Revolver>() != null)
        {
            if(Plugin.weapon.GetComponent<Revolver>().gunVariation == 0)
            {
                GameObject RevolverRig = Plugin.weapon.transform.GetChild(0).gameObject;
                GameObject Armature = RevolverRig.transform.GetChild(0).gameObject;
                GameObject UpperArm = Armature.transform.GetChild(0).gameObject;
                GameObject Forearm = UpperArm.transform.GetChild(0).gameObject;
                GameObject Hand = Forearm.transform.GetChild(0).gameObject;
                GameObject RevolverBone = Hand.transform.GetChild(5).gameObject;
                GameObject ShootPoint = RevolverBone.transform.GetChild(2).gameObject;
                GameObject ChargeEffect = ShootPoint.transform.GetChild(0).gameObject;

                Color colorBeam = ModConfig.revolverBeamColors[0];
                Color colorEffect = ModConfig.piercerChargeEffectColor;
                //slightly inefficent but wtv
                if(Time.time >= NextTimeUpdatePiercerAltTexture) //ModConfig.weaponSpecialColors[0, 0] != ModConfig.SpecialColorEnum.Default && Time.time >= NextTimeUpdatePiercerAltTexture) 
                {
                    colorEffect = SpecialColorLogic(ModConfig.piercerChargeEffectSpecialColor, new Color(1f, 1f, 1f), colorEffect);
                    MakeColoredPiercerAltTexture(colorEffect);
                    NextTimeUpdatePiercerAltTexture = Time.time + 0.050f;
                }
                else
                {
                    MeshRenderer componentMesh;
                    Color currentColor = new Color(1f, 1f, 1f);
                    if (ChargeEffect.TryGetComponent<MeshRenderer>(out componentMesh) && (bool) (UnityEngine.Object) componentMesh.material && componentMesh.material.HasProperty("_Color"))
                    {
                        currentColor = componentMesh.material.GetColor("_Color");
                    }

                    colorBeam = SpecialColorLogic(ModConfig.weaponSpecialColors[0, 0], currentColor, colorBeam);
                }
                if(ModConfig.enableColors[0, 0] == false) {return;}

                //copied from parry projectile code
                MeshRenderer component2;
                if (ChargeEffect.TryGetComponent<MeshRenderer>(out component2) && (bool) (UnityEngine.Object) component2.material && component2.material.HasProperty("_Color"))
                {
                    if(ModConfig.useColorableTexturePiercer) {component2.material.mainTexture = ColoredPiercerAltTexture;}
                    else {component2.material.SetColor("_Color", colorBeam);}
                }
                TrailRenderer component3;
                if (ChargeEffect.TryGetComponent<TrailRenderer>(out component3))
                {
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(new GradientColorKey[2]
                    {
                    new GradientColorKey(colorBeam, 0.0f),
                    new GradientColorKey(colorBeam, 1f)
                    }, new GradientAlphaKey[2]
                    {
                    new GradientAlphaKey(1f, 0.0f),
                    new GradientAlphaKey(0.0f, 1f)
                    });
                    component3.colorGradient = gradient;
                }
                Light component4;
                if (ChargeEffect.TryGetComponent<Light>(out component4)) {component4.color = colorBeam;}
            }
        }
    }

    [HarmonyPatch(typeof(Revolver), "CheckCoinCharges")]
    public class CheckCoinChargesPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if(Plugin.modEnabled == false || ModConfig.customColors == false || ModConfig.enableColors[0,1] == false) {return;}
            GameObject mainWeapon = Plugin.weapon;
            DualWield[] dualwields = MonoSingleton<GunControl>.Instance.GetComponentsInChildren<DualWield>();
            List<GameObject> weapons = new List<GameObject>();
            weapons.Add(mainWeapon);
            for(int i = 0; i < dualwields.Length; i++)
            {
                if(dualwields[i].currentWeapon != null)
                {
                    weapons.Add(dualwields[i].currentWeapon);
                }
            }

            foreach(GameObject weapon in weapons)
            {
                if(weapon != null && weapon.GetComponent<Revolver>() != null && weapon.GetComponent<Revolver>().gunVariation == 1 && ModConfig.coinSettingsToggle == true)
                {
                    float sum = 0f;
                    for(int i = 0; i < weapon.GetComponent<Revolver>().coinPanels.Length; i++)
                    {
                        sum += weapon.GetComponent<Revolver>().coinPanels[i].fillAmount; 
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        Color newColor = new Color();
                        if(i == Math.Floor(sum)) {newColor = ModConfig.marksmanCoinChargingColor;}
                        else if(i < sum) {newColor = ModConfig.marksmanCoinChargedColor; newColor = SpecialColorLogic(ModConfig.marksmanCoinChargedSpecialColor, new Color(1f, 1f, 1f), newColor);}
                        else {newColor = Color.black;}
                        weapon.GetComponent<Revolver>().coinPanels[i].color = newColor;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(Revolver), "Update")]
    public class RevolverUpdatePatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if(Plugin.modEnabled == true && ModConfig.customColors == true && ModConfig.enableColors[0,0] == true && Plugin.weapon.GetComponent<Revolver>().gunVariation == 0)
            {
                List<GameObject> weapons = new List<GameObject>();
                weapons.Add(Plugin.weapon);
                DualWield[] dualwields = MonoSingleton<GunControl>.Instance.GetComponentsInChildren<DualWield>();
                for(int i = 0; i < dualwields.Length; i++)
                {
                    if(dualwields[i].currentWeapon != null)
                    {
                        weapons.Add(dualwields[i].currentWeapon);
                    }
                }
                foreach(GameObject weapon in weapons)
                {
                    if(weapon.GetComponent<Revolver>().pierceCharge >= 100f)
                    {
                        Color newColor = ModConfig.PiercerChargedAltHUDColor;
                        newColor = SpecialColorLogic(ModConfig.PiercerChargedAltHUDSpecialColor, new Color(1f, 1f, 1f), newColor);
                        weapon.GetComponent<Revolver>().screenProps.SetColor("_Color", newColor);
                        weapon.GetComponent<Revolver>().screenMR.SetPropertyBlock(Plugin.weapon.GetComponent<Revolver>().screenProps);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(RocketLauncher), "Update")]
    public class RocketLauncherUpdatePatch()
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if(Plugin.weapon == null || Plugin.weapon.GetComponent<RocketLauncher>() == null) {return;}
            RocketLauncher rl = Plugin.weapon.GetComponent<RocketLauncher>();
            if(ModConfig.customColors == false || Plugin.modEnabled == false) {return;}
            for (int index = 0; index < rl.variationColorables.Length; ++index)
            {
                if(ModConfig.enableColors[4,0] == true && rl.variation == 0)
                {
                    Color freezeframeColor = ModConfig.freezeframeColor;
                    freezeframeColor = SpecialColorLogic(ModConfig.freezeframeSpecialColor, new Color(1f, 1f, 1f), freezeframeColor);
                    rl.variationColorables[index].color = new Color(freezeframeColor.r, freezeframeColor.g, freezeframeColor.b, rl.colorablesTransparencies[index] * 1f);
                }
            }
                
        }
        
    }
}