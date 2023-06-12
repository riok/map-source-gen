using Riok.Mapperly.Diagnostics;

namespace Riok.Mapperly.Tests.Mapping;

public class EnumFallbackValueTest
{
    [Fact]
    public void EnumByNameWithFallbackShouldSwitch()
    {
        var source = TestSourceBuilder.MapperWithBodyAndTypes(
            "[MapEnum(EnumMappingStrategy.ByName, IgnoreCase = true, FallbackValue = E2.Unknown)] partial E2 ToE1(E1 source);",
            "enum E1 {A, B, C, D}",
            "enum E2 {Unknown = -1, A = 100, B, C, d}"
        );

        TestHelper
            .GenerateMapper(source)
            .Should()
            .HaveSingleMethodBody(
                """
                return source switch
                {
                    global::E1.A => global::E2.A,
                    global::E1.B => global::E2.B,
                    global::E1.C => global::E2.C,
                    global::E1.D => global::E2.d,
                    _ => global::E2.Unknown,
                };
                """
            );
    }

    [Fact]
    public void EnumByValueCheckDefinedWithFallback()
    {
        var source = TestSourceBuilder.MapperWithBodyAndTypes(
            "[MapEnum(EnumMappingStrategy.ByValueCheckDefined, FallbackValue = E2.Unknown)] partial E2 ToE1(E1 source);",
            "enum E1 {A, B, C, D, E}",
            "enum E2 {Unknown = -1, A, B, C, D, E}"
        );

        TestHelper
            .GenerateMapper(source)
            .Should()
            .HaveSingleMethodBody(
                "return global::System.Enum.IsDefined(typeof(global::E2), (global::E2)source) ? (global::E2)source : global::E2.Unknown;"
            );
    }

    [Fact]
    public void EnumByValueWithFallbackShouldDiagnostic()
    {
        var source = TestSourceBuilder.MapperWithBodyAndTypes(
            "[MapEnum(EnumMappingStrategy.ByValue, FallbackValue = E2.Unknown)] partial E2 ToE1(E1 source);",
            "enum E1 {A, B, C, D, E}",
            "enum E2 {Unknown = -1, A, B, C, D, E}"
        );

        TestHelper
            .GenerateMapper(source, TestHelperOptions.AllowDiagnostics)
            .Should()
            .HaveSingleMethodBody(
                "return global::System.Enum.IsDefined(typeof(global::E2), (global::E2)source) ? (global::E2)source : global::E2.Unknown;"
            )
            .HaveDiagnostic(
                DiagnosticDescriptors.EnumFallbackValueRequiresByValueCheckDefinedStrategy,
                "Enum fallback values are only supported for the ByName and ByValueCheckDefined strategies, but not for the ByValue strategy"
            )
            .HaveAssertedAllDiagnostics();
    }

    [Fact]
    public void StringToEnumFallbackValueShouldSwitchIgnoreCase()
    {
        var source = TestSourceBuilder.MapperWithBodyAndTypes(
            "[MapEnum(EnumMappingStrategy.ByName, IgnoreCase = true, FallbackValue = E1.Unknown)] partial E1 ToE1(string source);",
            "enum E1 {Unknown = -1, A, B, C}"
        );
        TestHelper
            .GenerateMapper(source)
            .Should()
            .HaveSingleMethodBody(
                """
                return source switch
                {
                    { } s when s.Equals(nameof(global::E1.A), System.StringComparison.OrdinalIgnoreCase) => global::E1.A,
                    { } s when s.Equals(nameof(global::E1.B), System.StringComparison.OrdinalIgnoreCase) => global::E1.B,
                    { } s when s.Equals(nameof(global::E1.C), System.StringComparison.OrdinalIgnoreCase) => global::E1.C,
                    _ => global::E1.Unknown,
                };
                """
            );
    }

    [Fact]
    public void StringToEnumShouldSwitch()
    {
        var source = TestSourceBuilder.MapperWithBodyAndTypes(
            "[MapEnum(EnumMappingStrategy.ByName, FallbackValue = E1.Unknown)] partial E1 ToE1(string source);",
            "enum E1 {Unknown = -1, A, B, C}"
        );
        TestHelper
            .GenerateMapper(source)
            .Should()
            .HaveSingleMethodBody(
                """
                return source switch
                {
                    nameof(global::E1.A) => global::E1.A,
                    nameof(global::E1.B) => global::E1.B,
                    nameof(global::E1.C) => global::E1.C,
                    _ => global::E1.Unknown,
                };
                """
            );
    }
}
