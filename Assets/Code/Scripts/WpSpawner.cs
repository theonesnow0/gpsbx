using Fusion;
using UnityEngine;

namespace Wp.Network {
    public class WpSpawner : SimulationBehaviour, IPlayerJoined {
        [field: SerializeField] GameObject m_playerPrefab;

        void IPlayerJoined.PlayerJoined(PlayerRef player) {
            if (Runner.LocalPlayer == player)
                _ = Runner.Spawn(m_playerPrefab, new(0, 7f, 0), Quaternion.identity);
        }
    }
}