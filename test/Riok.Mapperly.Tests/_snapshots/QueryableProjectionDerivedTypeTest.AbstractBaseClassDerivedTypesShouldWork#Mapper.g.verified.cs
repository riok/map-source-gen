﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial global::System.Linq.IQueryable<global::B> Map(global::System.Linq.IQueryable<global::A> source)
    {
#nullable disable
        return System.Linq.Queryable.Select(source, x => (global::B)(x is global::ASubType1 ? new global::BSubType1() { Value1 = ((global::ASubType1)x).Value1, BaseValue = ((global::ASubType1)x).BaseValue, StringValue = ((global::ASubType1)x).StringValue } : x is global::ASubType2 ? new global::BSubType2() { Value2 = ((global::ASubType2)x).Value2, BaseValue = ((global::ASubType2)x).BaseValue, StringValue = ((global::ASubType2)x).StringValue } : default));
#nullable enable
    }

    private partial global::B Map(global::A src)
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
        target.StringValue = source.StringValue;
        return target;
    }

    private global::BSubType2 MapToBSubType2(global::ASubType2 source)
    {
        var target = new global::BSubType2();
        target.Value2 = source.Value2;
        target.BaseValue = source.BaseValue;
        target.StringValue = source.StringValue;
        return target;
    }
}