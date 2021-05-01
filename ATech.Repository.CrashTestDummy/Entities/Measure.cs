using System;

namespace ATech.Repository.CrashTestDummy.Entities
{
    public class Measure
    {
        public long Id { get; set; }

        public short SensorId { get; set; }

        public double Value { get; set; }

        public DateTime Created { get; set; }
    }
}
