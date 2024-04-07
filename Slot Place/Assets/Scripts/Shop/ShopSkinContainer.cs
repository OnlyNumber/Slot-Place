using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkinContainer : MonoBehaviour
{
    public List<Sprite> Backgrounds;

    public List<BuildingsInfoFabric> BuildingsInfoFabrics;

    public List<Material> LineMaterials;

    public List<AudioClip> BackgroundMusics;

    public int GetCount(SkinType type)
    {
        switch (type)
        {
            case SkinType.background:
                {
                    return Backgrounds.Count;
                }
            case SkinType.music:
                {
                    return BackgroundMusics.Count;
                }
            case SkinType.buildings:
                {
                    return BuildingsInfoFabrics.Count;
                }
            case SkinType.line:
                {
                    return LineMaterials.Count;
                }

        }

        return 0;
    }

}
