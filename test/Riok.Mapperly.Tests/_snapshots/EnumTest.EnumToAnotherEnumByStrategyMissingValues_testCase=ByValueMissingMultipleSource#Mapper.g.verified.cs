﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial global::B Map(global::A source)
    {
        var target = new global::B();
        target.Value = (global::D)source.Value;
        return target;
    }
}