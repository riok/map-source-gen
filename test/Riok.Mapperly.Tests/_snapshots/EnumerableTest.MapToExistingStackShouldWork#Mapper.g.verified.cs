﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial void Map(global::System.Collections.Generic.List<global::A>? source, global::System.Collections.Generic.Stack<global::B> target)
    {
        if (source == null)
            return;
        target.EnsureCapacity(source.Count + target.Count);
        foreach (var item in source)
        {
            target.Push(MapToB(item));
        }
    }

    private global::B MapToB(global::A source)
    {
        var target = new global::B();
        target.Value = source.Value;
        return target;
    }
}
