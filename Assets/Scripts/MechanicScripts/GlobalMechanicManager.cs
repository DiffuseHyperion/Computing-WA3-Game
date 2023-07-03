using System.Collections.Generic;
using UnityEngine;

namespace MechanicScripts
{
    public class GlobalMechanicManager : MonoBehaviour
    {
        public Dictionary<GlobalMechanicNames, Mechanic> Mechanics = new();

        public GlobalMechanicManager()
        {
            Mechanics.Add(GlobalMechanicNames.ELECTRICITY, new ElectricityMechanic());
        }
        
        public T GetMechanic<T>(GlobalMechanicNames mechanicName) where T : Mechanic
        {
            return (T) Mechanics[mechanicName];
        }

        public static GlobalMechanicManager GetGlobalMechanicManager()
        {
            return GameObject.FindWithTag("GlobalMechanicManager").GetComponent<GlobalMechanicManager>();
        }
    }
}