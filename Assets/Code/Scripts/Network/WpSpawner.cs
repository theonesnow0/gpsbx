using Fusion;
using UnityEngine;

namespace Wp.Network {
    public class WpSpawner : SimulationBehaviour, IPlayerJoined {
        [field: SerializeField] private GameObject m_PlayerPrefab;

        public void PlayerJoined(PlayerRef player) {
            if (Runner.LocalPlayer == player)
                _ = Runner.Spawn(m_PlayerPrefab, new(3F, 10F, 3F), Quaternion.identity);
        }
    }
}