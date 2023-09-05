﻿using BuildableObjects.BaseMachineClasses;

namespace BuildableObjects.Tier1
{
    public class Exporter : CollectorObject
    {
        public Exporter() : base(
            "Exporter", 
            "Sell your water off for cash.", 
            50, 
            MachineObjectConstants.ExporterDefaultWaterStorageSize, 
            MachineObjectConstants.ExporterDefaultWaterMoveRate, 
            1,
            3,
            1,
            1f)
        {
        }
        
        public override IBuildCondition GetBuildCondition()
        {
            return new OnLandBuildCondition();
        }

        public override void Tick()
        {
            CollectWaterTick();
        }
    }
}