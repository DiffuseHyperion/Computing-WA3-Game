using System.Collections.Generic;
using PlayerScripts.PlayerActions;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public PlayerBuildMenu.PlayerBuildMenu buildMenu;
        public PlayerActions.PlayerUI uiMenu;
        public PlayerMachineStats machineStats;
        
        public static List<Player> GetAllPlayers()
        {
            List<Player> list = new();
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                list.Add(gameObject.GetComponent<Player>());
            }

            return list;
        }
    }
}