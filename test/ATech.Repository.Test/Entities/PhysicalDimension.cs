using System;

namespace ATech.Repository.Test.Entities;

internal sealed class PhysicalDimension
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Scale { get; set; } = null!;

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;
}
