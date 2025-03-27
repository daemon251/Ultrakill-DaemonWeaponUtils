using System.IO;
using System.Reflection;
using UnityEngine;
using DaemonWeaponUtilsPlugin;

public class Crosshairs
{
    public static string DefaultParentFolder = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";
    public static Texture2D ClosedCircleTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D OpenCircleTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D ClosedCrossTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D OpenCrossTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D GrenadeTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D CrossBorderTexture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D Custom1Texture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D Custom2Texture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D Custom3Texture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static Texture2D Custom4Texture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
    public static void LoadImages()
    {
        ClosedCircleTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "ClosedCircle.png")}"));
        OpenCircleTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "OpenCircle.png")}"));
        ClosedCrossTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "ClosedCross.png")}"));
        OpenCrossTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "OpenCross.png")}"));
        GrenadeTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "GrenadeCross.png")}"));
        CrossBorderTexture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "BorderCross.png")}"));
        Custom1Texture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "Custom1Cross.png")}"));
        Custom2Texture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "Custom2Cross.png")}"));
        Custom3Texture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "Custom3Cross.png")}"));
        Custom4Texture.LoadImage(File.ReadAllBytes($"{Path.Combine(DefaultParentFolder!, "Custom4Cross.png")}"));
    }

    public static void RenderBaseCrosshair()
    {
        Color color3 = MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color;
        if(ModConfig.mainCrossHairEnum == ModConfig.CrossHairEnum.UltrakillBase) {MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color3.r, color3.g, color3.b, 1f); return;}
        if(Plugin.IsMenu() == true) 
        {
            //default behavior better than nothing
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color3.r, color3.g, color3.b, 1f);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(true);
            return;
        }
        //disable base crosshair

        Color color2 = MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color;
        MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color2.r, color2.g, color2.b, 0f);

        if(ModConfig.mainCrossHairEnum == ModConfig.CrossHairEnum.None) {return;}

        if(MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.activeSelf == true && ModConfig.mainCrossHairKeepInfo == false)
        {
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(false);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(false);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(false);
        }
        else if(MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.activeSelf == false && ModConfig.mainCrossHairKeepInfo == true)
        {
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(true);
        }
        //MonoSingleton<StatsManager>.Instance.crosshair.transform.parent.gameObject.SetActive(false);

        Texture2D tex = ConvertCrosshairEnumToTexture(ModConfig.mainCrossHairEnum);
        if(tex == null) {return;} //case None

        float x = Screen.width / 2;
        float y = Screen.height / 2;
        float width = (ModConfig.mainCrossHairScale / 4f) * tex.width;
        float height = (ModConfig.mainCrossHairScale / 4f) * tex.height;
        UnityEngine.GUI.DrawTexture(new Rect(x - width / 2, y - height / 2, width, height), tex, ScaleMode.StretchToFill, true, 0, ModConfig.mainCrossHairColor, 0, 0);
    }

    public static Texture2D ConvertCrosshairEnumToTexture(ModConfig.CrossHairEnum crosshairStyle)
    {
        Texture2D tex = null;
        switch(crosshairStyle)
        {
            case ModConfig.CrossHairEnum.UltrakillBase:
                //do nothing
                if(MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.activeSelf == false)
                {
                    //MonoSingleton<StatsManager>.Instance.crosshair.transform.parent.gameObject.SetActive(true);
                    MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(true);
                    MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(true);
                    MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(true);
                }
                Color color1 = MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color;
                MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color1.r, color1.g, color1.b, 1f);
                return null;
            //case ModConfig.CrossHairEnum.DefaultLarge:
            //    tex = DefaultLargeTexture;
            //    break;
            //case ModConfig.CrossHairEnum.DefaultSmall:
            //    tex = DefaultSmallTexture;
            //    break;
            case ModConfig.CrossHairEnum.None:
                tex = null;               
                break;
            case ModConfig.CrossHairEnum.ClosedCircle:
                tex = ClosedCircleTexture;
                break;
            case ModConfig.CrossHairEnum.OpenCircle:
                tex = OpenCircleTexture;
                break;
            case ModConfig.CrossHairEnum.ClosedCross:
                tex = ClosedCrossTexture;
                break;
            case ModConfig.CrossHairEnum.OpenCross:
                tex = OpenCrossTexture;
                break;
            case ModConfig.CrossHairEnum.Grenade:
                tex = GrenadeTexture;
                break;
            case ModConfig.CrossHairEnum.CrossBorder:
                tex = CrossBorderTexture;
                break;
            case ModConfig.CrossHairEnum.Custom1:
                tex = Custom1Texture;
                break;
            case ModConfig.CrossHairEnum.Custom2:
                tex = Custom2Texture;
                break;
            case ModConfig.CrossHairEnum.Custom3:
                tex = Custom3Texture;
                break;
            case ModConfig.CrossHairEnum.Custom4:
                tex = Custom4Texture;
                break;
        }
        return tex;
    }

    /*public static void SetMainCrossHair()
    {
        Texture2D tex = ConvertCrosshairEnumToTexture(ModConfig.mainCrossHairEnum);
        if(tex == null) {return;}
        MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().material.mainTexture = tex;
        MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = ModConfig.mainCrossHairColor;
    }*/

    public static void DrawCrosshair(int i, int j)
    {
        if(i > 5 || i < 0) {RenderBaseCrosshair(); return;}
        if (MonoSingleton<StatsManager>.Instance == null ||
            MonoSingleton<StatsManager>.Instance.crosshair == null ||
            MonoSingleton<StatsManager>.Instance.crosshair.transform == null ||
            MonoSingleton<StatsManager>.Instance.crosshair.transform.parent == null ||
            MonoSingleton<StatsManager>.Instance.crosshair.transform.parent.gameObject == null) {return;}
        i += -1;
        ModConfig.CrossHairEnum crosshairStyle = ModConfig.crosshairStyles[i, j];
        float crosshairScale = ModConfig.crosshairScales[i, j];
        Color crosshairColor = ModConfig.crosshairColors[i, j];
        crosshairColor = new Color(crosshairColor.r, crosshairColor.g, crosshairColor.b, ModConfig.crosshairOpacities[i, j]);

        Texture2D tex = ConvertCrosshairEnumToTexture(crosshairStyle);
        if(tex == null) {RenderBaseCrosshair(); return;}

        if(Plugin.IsMenu() == true) 
        {
            //default behavior better than nothing
            Color color3 = MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color;
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color3.r, color3.g, color3.b, 1f);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(true);
            return;
        }
        //disable base crosshair
        Color color2 = MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color;
        MonoSingleton<StatsManager>.Instance.crosshair.transform.GetComponent<UnityEngine.UI.Image>().color = new Color(color2.r, color2.g, color2.b, 0f);
        if(MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.activeSelf == true && ModConfig.keepCrosshairInfo[i,j] == false)
        {
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(false);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(false);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(false);
        }
        else if(MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.activeSelf == false && ModConfig.keepCrosshairInfo[i,j] == true)
        {
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(4).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(5).gameObject.SetActive(true);
            MonoSingleton<StatsManager>.Instance.crosshair.transform.GetChild(6).gameObject.SetActive(true);
        }
        //MonoSingleton<StatsManager>.Instance.crosshair.transform.parent.gameObject.SetActive(false);

        if(tex == null) {return;} //case None

        float x = Screen.width / 2;
        float y = Screen.height / 2;
        float width = (crosshairScale / 4f) * tex.width;
        float height = (crosshairScale / 4f) * tex.height;
        UnityEngine.GUI.DrawTexture(new Rect(x - width / 2, y - height / 2, width, height), tex, ScaleMode.StretchToFill, true, 0, crosshairColor, 0, 0);
    }
    
}