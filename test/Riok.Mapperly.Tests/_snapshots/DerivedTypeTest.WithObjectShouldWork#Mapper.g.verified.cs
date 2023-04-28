﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    public partial object Map(object src)
    {
        return src switch
        {
            string x => int.Parse(x),
            int x => x.ToString(),
            global::System.Collections.Generic.IEnumerable<string> x => global::System.Linq.Enumerable.Select(x, x1 => int.Parse(x1)),
            _ => throw new System.ArgumentException($"Cannot map {src.GetType()} to object as there is no known derived type mapping", nameof(src)),
        };
    }
}