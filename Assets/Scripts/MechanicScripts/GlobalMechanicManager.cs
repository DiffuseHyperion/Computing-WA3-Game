using System.Collections.Generic;
using UnityEngine;

namespace MechanicScripts
{
    public class GlobalMechanicManager : MonoBehaviour
    {
        private readonly Dictionary<GlobalMechanicNames, Mechanic> _mechanics = new();

        public GlobalMechanicManager()
        {
            _mechanics.Add(GlobalMechanicNames.ELECTRICITY, new ElectricityMechanic());
            _mechanics.Add(GlobalMechanicNames.RUST, new RustMechanic());
        }
        
        public T GetMechanic<T>(GlobalMechanicNames mechanicName) where T : Mechanic
        {
            return (T) _mechanics[mechanicName];
        }

        public static GlobalMechanicManager GetGlobalMechanicManager()
        {
            return GameObject.FindWithTag("GlobalMechanicManager").GetComponent<GlobalMechanicManager>();
        }
    }
}