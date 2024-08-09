using System.Diagnostics;
using Riok.Mapperly.Configuration;
using Riok.Mapperly.Symbols.Members;

namespace Riok.Mapperly.Descriptors.Mappings.MemberMappings;

[DebuggerDisplay("{DebuggerDisplay}")]
public record MemberMappingInfo(
    SourceMemberPath? SourceMember,
    NonEmptyMemberPath TargetMember,
    MemberValueMappingConfiguration? ValueConfiguration = null,
    MemberMappingConfiguration? Configuration = null
)
{
    public MemberMappingInfo(SourceMemberPath? sourceMember, NonEmptyMemberPath targetMember, MemberMappingConfiguration? configuration)
        : this(sourceMember, targetMember, null, configuration) { }

    public MemberMappingInfo(NonEmptyMemberPath targetMember, MemberValueMappingConfiguration configuration)
        : this(null, targetMember, configuration) { }

    public bool IsSourceNullable => SourceMember?.MemberPath.IsAnyNullable() ?? ValueConfiguration?.Value?.ConstantValue.IsNull ?? true;

    private string DebuggerDisplay =>
        $"{SourceMember?.MemberPath.FullName ?? ValueConfiguration?.DescribeValue()} => {TargetMember.FullName}";

    /// <summary>
    /// Returns <c>true</c> if the <see cref="SourceMember"/> and <see cref="TargetMember"/>
    /// were matched automatically by Mapperly without any additional user configuration.
    /// </summary>
    public bool IsAutoMatch => Configuration == null && ValueConfiguration == null;

    public TypeMappingKey ToTypeMappingKey()
    {
        if (SourceMember == null)
            throw new InvalidOperationException($"{SourceMember} and {TargetMember} need to be set to create a {nameof(TypeMappingKey)}");

        return new TypeMappingKey(SourceMember.MemberPath.MemberType, TargetMember.MemberType, Configuration?.ToTypeMappingConfiguration());
    }

    public string DescribeSource()
    {
        return SourceMember?.MemberPath.ToDisplayString(includeMemberType: false) ?? ValueConfiguration?.DescribeValue() ?? string.Empty;
    }
}
