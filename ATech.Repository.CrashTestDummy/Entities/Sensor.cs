using System;

namespace ATech.Repository.CrashTestDummy.Entities
{
    public class Sensor
    {
        public short Id { get; set; }

        public string Name { get; set; }
        
        public string Make { get; set; }

        public string Model { get; set; }
        
        public string SerialNumber { get; set; }

        public short PhysicalDimensionId { get; set; }
        public PhysicalDimension PhysicalDimension { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }        
    }
}
