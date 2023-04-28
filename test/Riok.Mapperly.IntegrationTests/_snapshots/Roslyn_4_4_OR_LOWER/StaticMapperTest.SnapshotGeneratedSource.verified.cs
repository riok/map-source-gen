﻿#nullable enable
namespace Riok.Mapperly.IntegrationTests.Mapper
{
    public static partial class StaticTestMapper
    {
        public static partial int DirectInt(int value)
        {
            return value;
        }

        public static partial long ImplicitCastInt(int value)
        {
            return (long)value;
        }

        public static partial int ExplicitCastInt(uint value)
        {
            return (int)value;
        }

        public static partial int? CastIntNullable(int value)
        {
            return (int?)value;
        }

        public static partial global::System.Guid ParseableGuid(string id)
        {
            return global::System.Guid.Parse(id);
        }

        public static partial int ParseableInt(string value)
        {
            return int.Parse(value);
        }

        public static partial global::System.DateTime DirectDateTime(global::System.DateTime dateTime)
        {
            return dateTime;
        }

        public static partial global::System.Collections.Generic.IEnumerable<global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto> MapAllDtos(global::System.Collections.Generic.IEnumerable<global::Riok.Mapperly.IntegrationTests.Models.TestObject> objects)
        {
            return global::System.Linq.Enumerable.Select(objects, x => MapToDtoExt(x));
        }

        public static partial global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto MapToDtoExt(this global::Riok.Mapperly.IntegrationTests.Models.TestObject src)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto(DirectInt(src.CtorValue), ctorValue2: DirectInt(src.CtorValue2))
            {IntInitOnlyValue = DirectInt(src.IntInitOnlyValue), RequiredValue = DirectInt(src.RequiredValue)};
            if (src.NullableFlattening != null)
            {
                target.NullableFlatteningIdValue = CastIntNullable(src.NullableFlattening.IdValue);
            }

            if (src.NestedNullable != null)
            {
                target.NestedNullableIntValue = DirectInt(src.NestedNullable.IntValue);
                target.NestedNullable = MapToTestObjectNestedDto(src.NestedNullable);
            }

            if (src.NestedNullableTargetNotNullable != null)
            {
                target.NestedNullableTargetNotNullable = MapToTestObjectNestedDto(src.NestedNullableTargetNotNullable);
            }

            if (src.StringNullableTargetNotNullable != null)
            {
                target.StringNullableTargetNotNullable = src.StringNullableTargetNotNullable;
            }

            if (src.RecursiveObject != null)
            {
                target.RecursiveObject = MapToDtoExt(src.RecursiveObject);
            }

            if (src.NullableReadOnlyObjectCollection != null)
            {
                target.NullableReadOnlyObjectCollection = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(src.NullableReadOnlyObjectCollection, x => MapToTestObjectNestedDto(x)));
            }

            if (src.SubObject != null)
            {
                target.SubObject = MapToInheritanceSubObjectDto(src.SubObject);
            }

            target.IntValue = DirectInt(src.IntValue);
            target.StringValue = src.StringValue;
            target.FlatteningIdValue = DirectInt(src.Flattening.IdValue);
            target.SourceTargetSameObjectType = src.SourceTargetSameObjectType;
            target.StackValue = new global::System.Collections.Generic.Stack<int>(global::System.Linq.Enumerable.Select(src.StackValue, x => ParseableInt(x)));
            target.QueueValue = new global::System.Collections.Generic.Queue<int>(global::System.Linq.Enumerable.Select(src.QueueValue, x => ParseableInt(x)));
            target.ImmutableArrayValue = global::System.Collections.Immutable.ImmutableArray.ToImmutableArray(global::System.Linq.Enumerable.Select(src.ImmutableArrayValue, x => ParseableInt(x)));
            target.ImmutableListValue = global::System.Collections.Immutable.ImmutableList.ToImmutableList(global::System.Linq.Enumerable.Select(src.ImmutableListValue, x => ParseableInt(x)));
            target.ImmutableHashSetValue = global::System.Collections.Immutable.ImmutableHashSet.ToImmutableHashSet(global::System.Linq.Enumerable.Select(src.ImmutableHashSetValue, x => ParseableInt(x)));
            target.ImmutableQueueValue = global::System.Collections.Immutable.ImmutableQueue.CreateRange(global::System.Linq.Enumerable.Select(src.ImmutableQueueValue, x => ParseableInt(x)));
            target.ImmutableStackValue = global::System.Collections.Immutable.ImmutableStack.CreateRange(global::System.Linq.Enumerable.Select(src.ImmutableStackValue, x => ParseableInt(x)));
            target.ImmutableSortedSetValue = global::System.Collections.Immutable.ImmutableSortedSet.ToImmutableSortedSet(global::System.Linq.Enumerable.Select(src.ImmutableSortedSetValue, x => ParseableInt(x)));
            target.ImmutableDictionaryValue = global::System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(src.ImmutableDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.ImmutableSortedDictionaryValue = global::System.Collections.Immutable.ImmutableSortedDictionary.ToImmutableSortedDictionary(src.ImmutableSortedDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.EnumValue = (global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue)src.EnumValue;
            target.EnumName = MapToEnumDtoByName(src.EnumName);
            target.EnumRawValue = (byte)src.EnumRawValue;
            target.EnumStringValue = MapToString(src.EnumStringValue);
            target.EnumReverseStringValue = MapToTestEnumDtoByValue(src.EnumReverseStringValue);
            target.DateTimeValueTargetDateOnly = global::System.DateOnly.FromDateTime(src.DateTimeValueTargetDateOnly);
            target.DateTimeValueTargetTimeOnly = global::System.TimeOnly.FromDateTime(src.DateTimeValueTargetTimeOnly);
            return target;
        }

        private static partial global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto MapToDtoInternal(global::Riok.Mapperly.IntegrationTests.Models.TestObject testObject)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto(DirectInt(testObject.CtorValue), ctorValue2: DirectInt(testObject.CtorValue2))
            {IntInitOnlyValue = DirectInt(testObject.IntInitOnlyValue), RequiredValue = DirectInt(testObject.RequiredValue)};
            if (testObject.NullableFlattening != null)
            {
                target.NullableFlatteningIdValue = CastIntNullable(testObject.NullableFlattening.IdValue);
            }

            if (testObject.NullableUnflatteningIdValue != null)
            {
                target.NullableUnflattening ??= new();
                target.NullableUnflattening.IdValue = DirectInt(testObject.NullableUnflatteningIdValue.Value);
            }

            if (testObject.NestedNullable != null)
            {
                target.NestedNullableIntValue = DirectInt(testObject.NestedNullable.IntValue);
                target.NestedNullable = MapToTestObjectNestedDto(testObject.NestedNullable);
            }

            if (testObject.NestedNullableTargetNotNullable != null)
            {
                target.NestedNullableTargetNotNullable = MapToTestObjectNestedDto(testObject.NestedNullableTargetNotNullable);
            }

            if (testObject.StringNullableTargetNotNullable != null)
            {
                target.StringNullableTargetNotNullable = testObject.StringNullableTargetNotNullable;
            }

            if (testObject.RecursiveObject != null)
            {
                target.RecursiveObject = MapToDtoExt(testObject.RecursiveObject);
            }

            if (testObject.NullableReadOnlyObjectCollection != null)
            {
                target.NullableReadOnlyObjectCollection = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(testObject.NullableReadOnlyObjectCollection, x => MapToTestObjectNestedDto(x)));
            }

            if (testObject.SubObject != null)
            {
                target.SubObject = MapToInheritanceSubObjectDto(testObject.SubObject);
            }

            target.IntValue = DirectInt(testObject.IntValue);
            target.StringValue = testObject.StringValue;
            target.RenamedStringValue2 = testObject.RenamedStringValue;
            target.FlatteningIdValue = DirectInt(testObject.Flattening.IdValue);
            target.Unflattening.IdValue = DirectInt(testObject.UnflatteningIdValue);
            target.SourceTargetSameObjectType = testObject.SourceTargetSameObjectType;
            target.StackValue = new global::System.Collections.Generic.Stack<int>(global::System.Linq.Enumerable.Select(testObject.StackValue, x => ParseableInt(x)));
            target.QueueValue = new global::System.Collections.Generic.Queue<int>(global::System.Linq.Enumerable.Select(testObject.QueueValue, x => ParseableInt(x)));
            target.ImmutableArrayValue = global::System.Collections.Immutable.ImmutableArray.ToImmutableArray(global::System.Linq.Enumerable.Select(testObject.ImmutableArrayValue, x => ParseableInt(x)));
            target.ImmutableListValue = global::System.Collections.Immutable.ImmutableList.ToImmutableList(global::System.Linq.Enumerable.Select(testObject.ImmutableListValue, x => ParseableInt(x)));
            target.ImmutableHashSetValue = global::System.Collections.Immutable.ImmutableHashSet.ToImmutableHashSet(global::System.Linq.Enumerable.Select(testObject.ImmutableHashSetValue, x => ParseableInt(x)));
            target.ImmutableQueueValue = global::System.Collections.Immutable.ImmutableQueue.CreateRange(global::System.Linq.Enumerable.Select(testObject.ImmutableQueueValue, x => ParseableInt(x)));
            target.ImmutableStackValue = global::System.Collections.Immutable.ImmutableStack.CreateRange(global::System.Linq.Enumerable.Select(testObject.ImmutableStackValue, x => ParseableInt(x)));
            target.ImmutableSortedSetValue = global::System.Collections.Immutable.ImmutableSortedSet.ToImmutableSortedSet(global::System.Linq.Enumerable.Select(testObject.ImmutableSortedSetValue, x => ParseableInt(x)));
            target.ImmutableDictionaryValue = global::System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(testObject.ImmutableDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.ImmutableSortedDictionaryValue = global::System.Collections.Immutable.ImmutableSortedDictionary.ToImmutableSortedDictionary(testObject.ImmutableSortedDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.EnumValue = (global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue)testObject.EnumValue;
            target.EnumName = MapToEnumDtoByName(testObject.EnumName);
            target.EnumRawValue = (byte)testObject.EnumRawValue;
            target.EnumStringValue = MapToString(testObject.EnumStringValue);
            target.EnumReverseStringValue = MapToTestEnumDtoByValue(testObject.EnumReverseStringValue);
            target.DateTimeValueTargetDateOnly = global::System.DateOnly.FromDateTime(testObject.DateTimeValueTargetDateOnly);
            target.DateTimeValueTargetTimeOnly = global::System.TimeOnly.FromDateTime(testObject.DateTimeValueTargetTimeOnly);
            return target;
        }

        public static partial global::Riok.Mapperly.IntegrationTests.Models.TestObject MapFromDto(global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto dto)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.TestObject(DirectInt(dto.CtorValue), ctorValue2: DirectInt(dto.CtorValue2))
            {IntInitOnlyValue = DirectInt(dto.IntInitOnlyValue), RequiredValue = DirectInt(dto.RequiredValue)};
            if (dto.NullableUnflattening != null)
            {
                target.NullableUnflatteningIdValue = CastIntNullable(dto.NullableUnflattening.IdValue);
            }

            if (dto.NestedNullable != null)
            {
                target.NestedNullable = MapToTestObjectNested(dto.NestedNullable);
            }

            if (dto.RecursiveObject != null)
            {
                target.RecursiveObject = MapFromDto(dto.RecursiveObject);
            }

            if (dto.NullableReadOnlyObjectCollection != null)
            {
                target.NullableReadOnlyObjectCollection = MapToIReadOnlyCollection(dto.NullableReadOnlyObjectCollection);
            }

            if (dto.SubObject != null)
            {
                target.SubObject = MapToInheritanceSubObject(dto.SubObject);
            }

            target.IntValue = DirectInt(dto.IntValue);
            target.StringValue = dto.StringValue;
            target.UnflatteningIdValue = DirectInt(dto.Unflattening.IdValue);
            target.NestedNullableTargetNotNullable = MapToTestObjectNested(dto.NestedNullableTargetNotNullable);
            target.StringNullableTargetNotNullable = dto.StringNullableTargetNotNullable;
            target.SourceTargetSameObjectType = dto.SourceTargetSameObjectType;
            target.StackValue = new global::System.Collections.Generic.Stack<string>(global::System.Linq.Enumerable.Select(dto.StackValue, x => x.ToString()));
            target.QueueValue = new global::System.Collections.Generic.Queue<string>(global::System.Linq.Enumerable.Select(dto.QueueValue, x => x.ToString()));
            target.ImmutableArrayValue = global::System.Collections.Immutable.ImmutableArray.ToImmutableArray(global::System.Linq.Enumerable.Select(dto.ImmutableArrayValue, x => x.ToString()));
            target.ImmutableListValue = global::System.Collections.Immutable.ImmutableList.ToImmutableList(global::System.Linq.Enumerable.Select(dto.ImmutableListValue, x => x.ToString()));
            target.ImmutableHashSetValue = global::System.Collections.Immutable.ImmutableHashSet.ToImmutableHashSet(global::System.Linq.Enumerable.Select(dto.ImmutableHashSetValue, x => x.ToString()));
            target.ImmutableQueueValue = global::System.Collections.Immutable.ImmutableQueue.CreateRange(global::System.Linq.Enumerable.Select(dto.ImmutableQueueValue, x => x.ToString()));
            target.ImmutableStackValue = global::System.Collections.Immutable.ImmutableStack.CreateRange(global::System.Linq.Enumerable.Select(dto.ImmutableStackValue, x => x.ToString()));
            target.ImmutableSortedSetValue = global::System.Collections.Immutable.ImmutableSortedSet.ToImmutableSortedSet(global::System.Linq.Enumerable.Select(dto.ImmutableSortedSetValue, x => x.ToString()));
            target.ImmutableDictionaryValue = global::System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(dto.ImmutableDictionaryValue, x => x.Key.ToString(), x => x.Value.ToString());
            target.ImmutableSortedDictionaryValue = global::System.Collections.Immutable.ImmutableSortedDictionary.ToImmutableSortedDictionary(dto.ImmutableSortedDictionaryValue, x => x.Key.ToString(), x => x.Value.ToString());
            target.EnumValue = (global::Riok.Mapperly.IntegrationTests.Models.TestEnum)dto.EnumValue;
            target.EnumName = (global::Riok.Mapperly.IntegrationTests.Models.TestEnum)dto.EnumName;
            target.EnumRawValue = (global::Riok.Mapperly.IntegrationTests.Models.TestEnum)dto.EnumRawValue;
            target.EnumStringValue = MapToTestEnum(dto.EnumStringValue);
            target.EnumReverseStringValue = MapToString1(dto.EnumReverseStringValue);
            return target;
        }

        public static partial global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByName MapToEnumDtoByName(global::Riok.Mapperly.IntegrationTests.Models.TestEnum v)
        {
            return v switch
            {
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value10 => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByName.Value10,
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value20 => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByName.Value20,
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value30 => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByName.Value30,
                _ => throw new System.ArgumentOutOfRangeException(nameof(v), v, "The value of enum TestEnum is not supported"),
            };
        }

        public static partial void UpdateDto(global::Riok.Mapperly.IntegrationTests.Models.TestObject source, global::Riok.Mapperly.IntegrationTests.Dto.TestObjectDto target)
        {
            if (source.NullableFlattening != null)
            {
                target.NullableFlatteningIdValue = CastIntNullable(source.NullableFlattening.IdValue);
            }

            if (source.NestedNullable != null)
            {
                target.NestedNullableIntValue = DirectInt(source.NestedNullable.IntValue);
                target.NestedNullable = MapToTestObjectNestedDto(source.NestedNullable);
            }

            if (source.NestedNullableTargetNotNullable != null)
            {
                target.NestedNullableTargetNotNullable = MapToTestObjectNestedDto(source.NestedNullableTargetNotNullable);
            }

            if (source.StringNullableTargetNotNullable != null)
            {
                target.StringNullableTargetNotNullable = source.StringNullableTargetNotNullable;
            }

            if (source.RecursiveObject != null)
            {
                target.RecursiveObject = MapToDtoExt(source.RecursiveObject);
            }

            if (source.NullableReadOnlyObjectCollection != null)
            {
                target.NullableReadOnlyObjectCollection = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(source.NullableReadOnlyObjectCollection, x => MapToTestObjectNestedDto(x)));
            }

            if (source.SubObject != null)
            {
                target.SubObject = MapToInheritanceSubObjectDto(source.SubObject);
            }

            target.CtorValue = DirectInt(source.CtorValue);
            target.CtorValue2 = DirectInt(source.CtorValue2);
            target.IntValue = DirectInt(source.IntValue);
            target.StringValue = source.StringValue;
            target.FlatteningIdValue = DirectInt(source.Flattening.IdValue);
            target.SourceTargetSameObjectType = source.SourceTargetSameObjectType;
            target.StackValue = new global::System.Collections.Generic.Stack<int>(global::System.Linq.Enumerable.Select(source.StackValue, x => ParseableInt(x)));
            target.QueueValue = new global::System.Collections.Generic.Queue<int>(global::System.Linq.Enumerable.Select(source.QueueValue, x => ParseableInt(x)));
            target.ImmutableArrayValue = global::System.Collections.Immutable.ImmutableArray.ToImmutableArray(global::System.Linq.Enumerable.Select(source.ImmutableArrayValue, x => ParseableInt(x)));
            target.ImmutableListValue = global::System.Collections.Immutable.ImmutableList.ToImmutableList(global::System.Linq.Enumerable.Select(source.ImmutableListValue, x => ParseableInt(x)));
            target.ImmutableHashSetValue = global::System.Collections.Immutable.ImmutableHashSet.ToImmutableHashSet(global::System.Linq.Enumerable.Select(source.ImmutableHashSetValue, x => ParseableInt(x)));
            target.ImmutableQueueValue = global::System.Collections.Immutable.ImmutableQueue.CreateRange(global::System.Linq.Enumerable.Select(source.ImmutableQueueValue, x => ParseableInt(x)));
            target.ImmutableStackValue = global::System.Collections.Immutable.ImmutableStack.CreateRange(global::System.Linq.Enumerable.Select(source.ImmutableStackValue, x => ParseableInt(x)));
            target.ImmutableSortedSetValue = global::System.Collections.Immutable.ImmutableSortedSet.ToImmutableSortedSet(global::System.Linq.Enumerable.Select(source.ImmutableSortedSetValue, x => ParseableInt(x)));
            target.ImmutableDictionaryValue = global::System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(source.ImmutableDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.ImmutableSortedDictionaryValue = global::System.Collections.Immutable.ImmutableSortedDictionary.ToImmutableSortedDictionary(source.ImmutableSortedDictionaryValue, x => ParseableInt(x.Key), x => ParseableInt(x.Value));
            target.EnumValue = (global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue)source.EnumValue;
            target.EnumName = MapToEnumDtoByName(source.EnumName);
            target.EnumRawValue = (byte)source.EnumRawValue;
            target.EnumStringValue = MapToString(source.EnumStringValue);
            target.EnumReverseStringValue = MapToTestEnumDtoByValue(source.EnumReverseStringValue);
            target.DateTimeValueTargetDateOnly = global::System.DateOnly.FromDateTime(source.DateTimeValueTargetDateOnly);
            target.DateTimeValueTargetTimeOnly = global::System.TimeOnly.FromDateTime(source.DateTimeValueTargetTimeOnly);
        }

        private static partial int PrivateDirectInt(int value)
        {
            return value;
        }

        public static partial object DerivedTypes(object source)
        {
            return source switch
            {
                string x => ParseableInt(x),
                int x => x.ToString(),
                _ => throw new System.ArgumentException($"Cannot map {source.GetType()} to object as there is no known derived type mapping", nameof(source)),
            };
        }

        private static global::Riok.Mapperly.IntegrationTests.Dto.TestObjectNestedDto MapToTestObjectNestedDto(global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Dto.TestObjectNestedDto();
            target.IntValue = DirectInt(source.IntValue);
            return target;
        }

        private static string MapToString(global::Riok.Mapperly.IntegrationTests.Models.TestEnum source)
        {
            return source switch
            {
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value10 => nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value10),
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value20 => nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value20),
                global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value30 => nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value30),
                _ => source.ToString(),
            };
        }

        private static global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue MapToTestEnumDtoByValue(string source)
        {
            return source switch
            {
                nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue1) => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue1,
                nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue2) => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue2,
                nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue3) => global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue3,
                _ => System.Enum.Parse<global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue>(source, false),
            };
        }

        private static global::Riok.Mapperly.IntegrationTests.Dto.InheritanceSubObjectDto MapToInheritanceSubObjectDto(global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Dto.InheritanceSubObjectDto();
            target.SubIntValue = DirectInt(source.SubIntValue);
            target.BaseIntValue = DirectInt(source.BaseIntValue);
            return target;
        }

        private static global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested MapToTestObjectNested(global::Riok.Mapperly.IntegrationTests.Dto.TestObjectNestedDto source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested();
            target.IntValue = DirectInt(source.IntValue);
            return target;
        }

        private static global::System.Collections.Generic.IReadOnlyCollection<global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested> MapToIReadOnlyCollection(global::Riok.Mapperly.IntegrationTests.Dto.TestObjectNestedDto[] source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.TestObjectNested[source.Length];
            for (var i = 0; i < source.Length; i++)
            {
                target[i] = MapToTestObjectNested(source[i]);
            }

            return target;
        }

        private static global::Riok.Mapperly.IntegrationTests.Models.TestEnum MapToTestEnum(string source)
        {
            return source switch
            {
                nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value10) => global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value10,
                nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value20) => global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value20,
                nameof(global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value30) => global::Riok.Mapperly.IntegrationTests.Models.TestEnum.Value30,
                _ => System.Enum.Parse<global::Riok.Mapperly.IntegrationTests.Models.TestEnum>(source, false),
            };
        }

        private static string MapToString1(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue source)
        {
            return source switch
            {
                global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue1 => nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue1),
                global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue2 => nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue2),
                global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue3 => nameof(global::Riok.Mapperly.IntegrationTests.Dto.TestEnumDtoByValue.DtoValue3),
                _ => source.ToString(),
            };
        }

        private static global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject MapToInheritanceSubObject(global::Riok.Mapperly.IntegrationTests.Dto.InheritanceSubObjectDto source)
        {
            var target = new global::Riok.Mapperly.IntegrationTests.Models.InheritanceSubObject();
            target.SubIntValue = DirectInt(source.SubIntValue);
            target.BaseIntValue = DirectInt(source.BaseIntValue);
            return target;
        }
    }
}