using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public MaskType type;

    public MaskType GetMaskType()
    {
        return type;
    }

    public void SetMaskType(MaskType maskType)
    {
        type = maskType;
    }
}
