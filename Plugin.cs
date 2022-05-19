using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slipstream
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;

        public void OnPlayerSpawned(Exiled.Events.EventArgs.SpawnedEventArgs ev)
        {
            ev.Player.ReferenceHub.gameObject.AddComponent<Slipstream>();
        }

        public void OnPlayerDied(Exiled.Events.EventArgs.DiedEventArgs ev)
        {
            if (ev.Target.ReferenceHub.gameObject.TryGetComponent(out Slipstream slipstream))
                slipstream.Destroy();

            ChangeWalkingSpeedPatch.WalkSpeeds.Remove(ev.Target);
            ChangeRunningSpeedPatch.RunSpeeds.Remove(ev.Target);
        }

        public override void OnEnabled()
        {
            Instance = this;
            Instance.Config.DistanceToSlipSqr = Instance.Config.DistanceToSlip*Instance.Config.DistanceToSlip;
            Instance.Config.WalkPercentIncreaseValue = 1 + Instance.Config.WalkPercentIncrease / 100f;
            Instance.Config.RunPercentIncreaseValue = 1 + Instance.Config.RunPercentIncrease / 100f;
            Exiled.Events.Handlers.Player.Spawned += OnPlayerSpawned;
            Exiled.Events.Handlers.Player.Died += OnPlayerDied;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Player.Spawned -= OnPlayerSpawned;
            Exiled.Events.Handlers.Player.Died -= OnPlayerDied;
            ChangeWalkingSpeedPatch.WalkSpeeds.Clear();
            ChangeRunningSpeedPatch.RunSpeeds.Clear();
            base.OnDisabled();
        }
    }
}
