﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    public partial global::B Map(global::A src)
    {
        return src switch
        {
            global::ASubType1 x => MapToBSubType1(x),
            global::ASubType2 x => MapToBSubType2(x),
            _ => throw new System.ArgumentException($"Cannot map {src.GetType()} to B as there is no known derived type mapping", nameof(src)),
        };
    }

    private global::BSubType1 MapToBSubType1(global::ASubType1 source)
    {
        var target = new global::BSubType1();
        target.Value1 = source.Value1;
        target.BaseValue = source.BaseValue;
        return target;
    }

    private global::BSubType2 MapToBSubType2(global::ASubType2 source)
    {
        var target = new global::BSubType2();
        target.Value2 = source.Value2;
        target.BaseValue = source.BaseValue;
        return target;
    }
}