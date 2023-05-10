﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    public partial object? Map(object? source, global::System.Type targetType)
    {
        return source switch
        {
            int x when targetType.IsAssignableFrom(typeof(int)) => MapIntToInt(x),
            string x when targetType.IsAssignableFrom(typeof(int)) => MapStringToInt(x),
            global::A x when targetType.IsAssignableFrom(typeof(global::B)) => MapToB(x),
            global::C x when targetType.IsAssignableFrom(typeof(global::D)) => MapToD(x),
            null => default,
            _ => throw new System.ArgumentException($"Cannot map {source.GetType()} to {targetType} as there is no known type mapping", nameof(source)),
        };
    }

    private partial global::B MapToB(global::A source)
    {
        var target = new global::B();
        target.Value = source.Value;
        return target;
    }

    private partial global::D? MapToD(global::C? source)
    {
        if (source == null)
            return default;
        var target = new global::D();
        target.Value2 = source.Value2;
        return target;
    }

    private partial int? MapStringToInt(string? source)
    {
        return source == null ? default : int.Parse(source);
    }

    private partial int? MapIntToInt(int source)
    {
        return (int?)source;
    }
}
