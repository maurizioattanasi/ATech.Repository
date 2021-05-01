using System;

namespace ATech.Repository.CrashTestDummy.Entities
{
    public class PhysicalDimension
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public string Scale { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
    }
}
