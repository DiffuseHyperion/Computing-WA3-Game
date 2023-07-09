using System;
using System.Collections.Generic;
using BuildableObjects.Nodes;
using UtilClasses;

namespace BuildableObjects
{
    public abstract class MachineObject : LinkableObject, ITickableObject
    {
        private int _moveAmount;
        private readonly WaterStorageObject _waterStorageObject;
        private readonly CountdownObject _countdownObject;

        protected MachineObject(string name, string description, int cost, int maxStorage, int moveRate, int moveAmount, BuildableObjectTypes type) : base(name, description, cost, type)
        {
            _moveAmount = moveAmount;
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

                for (int i = 0; i < _moveAmount; i++)
                {
                    if (GetWaterStorage().IsEmpty())
                    {
                        return;
                    }

                    WaterObject transferredWater = GetWaterStorage().RemoveWater();
                    machineObject.GetWaterStorage().AddWater(transferredWater);
                }
            }
        }
        
        public void MoveWaterTick(Action<WaterObject> lambda)
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

                for (int i = 0; i < _moveAmount; i++)
                {
                    if (GetWaterStorage().IsEmpty())
                    {
                        return;
                    }

                    WaterObject transferredWater = GetWaterStorage().RemoveWater();
                    lambda.Invoke(transferredWater);
                    machineObject.GetWaterStorage().AddWater(transferredWater);
                }
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

        public abstract void Tick();
    }
}