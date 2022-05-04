using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Extensions;
using MEC;

namespace Slipstream
{
    public class Slipstream : MonoBehaviour
    {
        public bool Enabled = true;
        public Player Player;

        public bool CanSlip = true;

        public void Awake()
        {
            Player = Player.Get(gameObject);
        }

        public void Update()
        {
            if (!Enabled || !CanSlip)
                return;

            foreach (Player player in Player.List)
            {
                if (player != Player && player.IsHuman && (player.Position - Player.Position).sqrMagnitude <= Plugin.Instance.Config.DistanceToSlipSqr)
                {
                    CanSlip = false;

                    float previousWalk = ChangeWalkingSpeedPatch.WalkSpeeds.TryGetValue(player, out float walkSpeed) ? walkSpeed : 1f;
                    float previousRun = ChangeRunningSpeedPatch.RunSpeeds.TryGetValue(player, out float runSpeed) ? runSpeed : 1f;

                    player.ReferenceHub.playerMovementSync.AddSafeTime(Plugin.Instance.Config.SlipDuratation);
                    Player.ChangeWalkingSpeed(previousWalk + Plugin.Instance.Config.WalkPercentIncreaseValue, false);
                    Player.ChangeRunningSpeed(previousRun + Plugin.Instance.Config.RunPercentIncreaseValue, false);

                    Timing.CallDelayed(Plugin.Instance.Config.SlipDuratation, () =>
                    {
                        Player.ChangeWalkingSpeed(previousWalk, false);
                        Player.ChangeRunningSpeed(previousRun, false);
                    });

                    Timing.CallDelayed(Plugin.Instance.Config.Cooldown, () =>
                    {
                        CanSlip = true;
                    });
                    
                    break;
                }
            }
        }

        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this);
        }
    }
}
