﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial global::System.Linq.IQueryable<global::B> Map(global::System.Linq.IQueryable<global::A> source)
    {
#nullable disable
        return System.Linq.Queryable.Select(source, x => new global::B() { Values = global::System.Linq.Enumerable.Select(x.Values, x1 => (int)x1) });
#nullable enable
    }
}