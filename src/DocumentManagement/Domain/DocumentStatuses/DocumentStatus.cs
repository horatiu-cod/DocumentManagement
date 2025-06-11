using Ardalis.SmartEnum;

namespace DocumentManagement.Domain.DocumentStatuses;

internal class DocumentStatus : ValueObject
{
    private DocumentStatusEnum? _status;
    public string Value
    {
        get => _status?.Name ?? string.Empty;
        private set
        {
            if (!DocumentStatusEnum.TryFromName(value, true, out var parsed))
                throw new Exception(nameof(Value));

            _status = parsed;
        }
    }
    public DocumentStatus(string value)
    {
        Value = value;
    }
    public static DocumentStatus Of(string value) => new(value);
    public static implicit operator string(DocumentStatus value) => value.Value;
    public static List<string> ListNames() => DocumentStatusEnum.List.Select(x => x.Name).ToList();
    public static DocumentStatus Created() => new(DocumentStatusEnum.Created.Name);
    public static DocumentStatus AllocatedToOwner() => new(DocumentStatusEnum.Allocated.Name);
    public static DocumentStatus TransmittedToOwner() => new(DocumentStatusEnum.Transmitted.Name);
    public static DocumentStatus SignedByOwner() => new(DocumentStatusEnum.Signed.Name);
    public static DocumentStatus VerifiedByOwner() => new(DocumentStatusEnum.Verified.Name);

}
internal abstract class DocumentStatusEnum : SmartEnum<DocumentStatusEnum>
{
    public static readonly DocumentStatusEnum Created = new CreatedType();
    public static readonly DocumentStatusEnum Allocated = new AllocatedType();
    public static readonly DocumentStatusEnum Signed = new SignedType();
    public static readonly DocumentStatusEnum Verified = new VerifiedType();
    public static readonly DocumentStatusEnum Transmitted = new TransmittedType();
    public static readonly DocumentStatusEnum Approved = new ApprovedType();

    private DocumentStatusEnum(string name, int value) : base(name, value)
    {
    }
    public sealed class CreatedType : DocumentStatusEnum
    {
        public CreatedType() : base("Created", 0)
        {
        }
    }
    private sealed class AllocatedType : DocumentStatusEnum
    {
        public AllocatedType() : base("Allocated", 1)
        {
        }
    }
    public sealed class SignedType : DocumentStatusEnum
    {
        public SignedType() : base("Signed", 2)
        {
        }
    }

    public class VerifiedType : DocumentStatusEnum
    {
        public VerifiedType() : base("Verified", 3)
        {
        }
    }

    public class TransmittedType : DocumentStatusEnum
    {
        public TransmittedType() : base("Transmitted", 4)
        {
        }
    }

    public class ApprovedType : DocumentStatusEnum
    {
        public ApprovedType() : base("Approved", 5)
        {
        }
    }
}
