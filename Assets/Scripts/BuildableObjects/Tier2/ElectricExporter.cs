﻿using BuildableObjects.BaseMachineClasses;
using MechanicScripts;

namespace BuildableObjects.Tier2
{
    public class ElectricExporter : CollectorObject, ITickableObject, IPoweredObject
    {
        public ElectricExporter() : base(
            "Electric Exporter", 
            "Sell big chunks of water off slowly.", 
            100, 
            MachineObjectConstants.ExporterDefaultWaterStorageSize * 2, 
            MachineObjectConstants.ExporterDefaultWaterMoveRate, 
            1,
            5,
            5,
            1f)
        {
        }

        public override bool CanBuild()
        {
            return OnLand();
        }

        public override void Tick()
        {
            if (!GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IsPowered())
            {
                return;
            }
            CollectWaterTick();
        }

        public int GetPowerConsumption()
        {
            return 5;
        }
    }
}