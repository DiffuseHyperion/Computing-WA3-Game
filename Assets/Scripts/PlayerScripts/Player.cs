using System.Collections.Generic;
using PlayerScripts.PlayerActions;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerBuildMenu.PlayerBuildMenu buildMenu;
        [SerializeField] private PlayerActions.PlayerUI uiMenu;
        [SerializeField] private PlayerMachineStats machineStats;
        
        public static List<Player> GetAllPlayers()
        {
            List<Player> list = new();
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                list.Add(gameObject.GetComponent<Player>());
            }

            return list;
        }

        public PlayerBuildMenu.PlayerBuildMenu GetBuildMenu()
        {
            return buildMenu;
        }
        
        public PlayerActions.PlayerUI GetUIMenu()
        {
            return uiMenu;
        }
        
        public PlayerMachineStats GetMachineStatsMenu()
        {
            return machineStats;
        }
    }
}