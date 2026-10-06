using System;

[Serializable]
public struct SerializableDamageByType
{
    public DamageType damageType;
    public int value;

    public SerializableDamageByType(DamageType damageType, int value)
    {
        this.damageType = damageType;
        this.value = value;
    }
}