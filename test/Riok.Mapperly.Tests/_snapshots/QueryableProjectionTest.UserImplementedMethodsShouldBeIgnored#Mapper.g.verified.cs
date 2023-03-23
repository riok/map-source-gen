﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial System.Linq.IQueryable<B> Map(System.Linq.IQueryable<A> source)
    {
#nullable disable
        return System.Linq.Queryable.Select(source, x => new B() { StringValue = x.StringValue, NestedValue = new D() { Value = (int)x.NestedValue.Value } });
#nullable enable
    }

    private partial B MapToB(A source)
    {
        var target = new B();
        target.StringValue = source.StringValue;
        target.NestedValue = MapToD(source.NestedValue);
        return target;
    }
}