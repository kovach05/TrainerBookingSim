namespace Common.Models;

public class PassportId : IEquatable<PassportId>
{
    public string Value { get; set; }

    public PassportId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Passport ID cannot be null or empty.", nameof(value));
        }

        Value = value;
    }
    
    public bool Equals(PassportId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is PassportId other)
        {
            return Value == other.Value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}