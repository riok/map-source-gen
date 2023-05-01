﻿#nullable enable
namespace Riok.Mapperly.IntegrationTests.Mapper
{
    public static partial class DeepCloningMapper
    {
        public static partial global::Riok.Mapperly.IntegrationTests.Models.IdObject Copy(global::Riok.Mapperly.IntegrationTests.Models.IdObject src)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.IdObject();
            target.IdValue = src.IdValue;
            return target;
        }

        public static partial global::Riok.Mapperly.IntegrationTests.Models.TestObject Copy(global::Riok.Mapperly.IntegrationTests.Models.TestObject src)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.TestObject(src.CtorValue, ctorValue2: src.CtorValue2)
            {IntInitOnlyValue = src.IntInitOnlyValue, RequiredValue = src.RequiredValue};
            if (src.NullableFlattening != null)
            {
                target.NullableFlattening = Copy(src.NullableFlattening);
            }

            if (src.NestedNullable != null)
            {
                target.NestedNullable = MapToTestObjectNested(src.NestedNullable);
            }

            if (src.NestedNullableTargetNotNullable != null)
            {
                target.NestedNullableTargetNotNullable = MapToTestObjectNested(src.NestedNullableTargetNotNullable);
            }

            if (src.RecursiveObject != null)
            {
                target.RecursiveObject = Copy(src.RecursiveObject);
            }

            if (src.SourceTargetSameObjectType != null)
            {
                target.SourceTargetSameObjectType = Copy(src.SourceTargetSameObjectType);
            }

            if (src.NullableReadOnlyObjectCollection != null)
            {
                target.NullableReadOnlyObjectCollection = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(src.NullableReadOnlyObjectCollection, x => MapToTestObjectNested(x)));
            }

            if (src.SubObject != null)
            {
                target.SubObject = MapToInheritanceSubObject(src.SubObject);
            }

            target.IntValue = src.IntValue;
            target.StringValue = src.StringValue;
            target.RenamedStringValue = src.RenamedStringValue;
            target.Flattening = Copy(src.Flattening);
            target.UnflatteningIdValue = src.UnflatteningIdValue;
            target.NullableUnflatteningIdValue = src.NullableUnflatteningIdValue;
            target.StringNullableTargetNotNullable = src.StringNullableTargetNotNullable;
            target.StackValue = new global::System.Collections.Generic.Stack<string>(src.StackValue);
            target.QueueValue = new global::System.Collections.Generic.Queue<string>(src.QueueValue);
            target.ImmutableArrayValue = global::System.Collections.Immutable.ImmutableArray.ToImmutableArray(src.ImmutableArrayValue);
            target.ImmutableListValue = global::System.Collections.Immutable.ImmutableList.ToImmutableList(src.ImmutableListValue);
            target.ImmutableQueueValue = global::System.Collections.Immutable.ImmutableQueue.CreateRange(src.ImmutableQueueValue);
            target.ImmutableStackValue = global::System.Collections.Immutable.ImmutableStack.CreateRange(src.ImmutableStackValue);
            target.ImmutableSortedSetValue = global::System.Collections.Immutable.ImmutableSortedSet.ToImmutableSortedSet(src.ImmutableSortedSetValue);
            target.ImmutableDictionaryValue = global::System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(src.ImmutableDictionaryValue);
            target.ImmutableSortedDictionaryValue = global::System.Collections.Immutable.ImmutableSortedDictionary.ToImmutableSortedDictionary(src.ImmutableSortedDictionaryValue);
            target.EnumValue = src.EnumValue;
            target.EnumName = src.EnumName;
            target.EnumRawValue = src.EnumRawValue;
            target.EnumStringValue = src.EnumStringValue;
            target.EnumReverseStringValue = src.EnumReverseStringValue;
            target.DateTimeValueTargetDateOnly = src.DateTimeValueTargetDateOnly;
            target.DateTimeValueTargetTimeOnly = src.DateTimeValueTargetTimeOnly;
            return target;
        }

        private static global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested MapToTestObjectNested(global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested();
            target.IntValue = source.IntValue;
            return target;
        }

        private static global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject MapToInheritanceSubObject(global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject();
            target.SubIntValue = source.SubIntValue;
            target.BaseIntValue = source.BaseIntValue;
            return target;
        }
    }
}