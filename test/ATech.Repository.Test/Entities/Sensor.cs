using System;

namespace ATech.Repository.Test.Entities;

internal sealed class Sensor
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public short PhysicalDimensionId { get; set; }
    public PhysicalDimension PhysicalDimension { get; set; } = null!;

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;
}
