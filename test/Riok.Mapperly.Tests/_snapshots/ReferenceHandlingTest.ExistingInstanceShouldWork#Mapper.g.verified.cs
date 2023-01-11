﻿//HintName: Mapper.g.cs
#nullable enable
public partial class Mapper
{
    private partial void Map(A source, B target)
    {
        var refHandler = new Riok.Mapperly.Abstractions.ReferenceHandling.Internal.PreserveReferenceHandler();
        target.Parent = MapToB(source.Parent, refHandler);
        target.Value = MapToD(source.Value, refHandler);
    }

    private B MapToB(A source, Riok.Mapperly.Abstractions.ReferenceHandling.IReferenceHandler refHandler)
    {
        if (refHandler.TryGetReference<A, B>(source, out var existingTargetReference))
            return existingTargetReference;
        var target = new B();
        refHandler.SetReference<A, B>(source, target);
        target.Parent = MapToB(source.Parent, refHandler);
        target.Value = MapToD(source.Value, refHandler);
        return target;
    }

    private D MapToD(C source, Riok.Mapperly.Abstractions.ReferenceHandling.IReferenceHandler refHandler)
    {
        if (refHandler.TryGetReference<C, D>(source, out var existingTargetReference))
            return existingTargetReference;
        var target = new D();
        refHandler.SetReference<C, D>(source, target);
        target.StringValue = source.StringValue;
        return target;
    }
}