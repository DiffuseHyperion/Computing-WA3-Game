﻿using System.Collections.Generic;
using BuildableObjects.LinkPorts;
using UtilClasses;

namespace BuildableObjects
{
    public abstract class MachineObject : LinkableObject
    {
        private readonly WaterStorageObject _waterStorageObject;
        private readonly CountdownObject _countdownObject;

        protected MachineObject(string name, string description, int cost, int maxStorage, int moveRate, BuildableObjectTypes type) : base(name, description, cost, type)
        {
            _waterStorageObject = new WaterStorageObject(maxStorage);
            _countdownObject = new CountdownObject(moveRate);
        }

        public WaterStorageObject GetWaterStorage()
        {
            return _waterStorageObject;
        }

        public void MoveWaterTick()
        {
            if (!_countdownObject.Countdown())
            {
                return;
            }
            List<LinkInput> inputs = GetInputs();
            foreach (var input in inputs)
            { 
                if (GetWaterStorage().IsEmpty())
                {
                    return;
                }
                if (input.GetLinkableObject() != this)
                {
                    continue;
                }

                LinkOutput output = GetLinkOutput(input);
                MachineObject machineObject = output.gameObject.transform.parent.gameObject.GetComponent<MachineObject>();
                if (machineObject == null)
                {
                    return;
                }
                if (machineObject.GetWaterStorage().IsFull())
                {
                    return;
                }
                WaterObject transferredWater = GetWaterStorage().RemoveWater();
                machineObject.GetWaterStorage().AddWater(transferredWater);
            }
        }

        public void OnMouseEnter()
        {
            GetPlayer().GetMachineStatsMenu().EnablePanel(this);
        }

        public void OnMouseExit()
        {
            GetPlayer().GetMachineStatsMenu().DisablePanel();
        }
    }
}