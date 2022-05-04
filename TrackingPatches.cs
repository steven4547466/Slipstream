using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Extensions;
using HarmonyLib;

namespace Slipstream
{
    [HarmonyPatch(typeof(MirrorExtensions), nameof(MirrorExtensions.ChangeWalkingSpeed))]
    public class ChangeWalkingSpeedPatch
    {
        public static Dictionary<Player, float> WalkSpeeds = new Dictionary<Player, float>();
        
        public static void Prefix(Player player, float multiplier, bool useCap)
        {
            if (player.ReferenceHub.gameObject.TryGetComponent(out Slipstream slipstream) && !slipstream.CanSlip)
                return;
            
            if (useCap)
                multiplier = Mathf.Clamp(multiplier, -2f, 2f);
            
            WalkSpeeds[player] = multiplier;
        }
    }

    [HarmonyPatch(typeof(MirrorExtensions), nameof(MirrorExtensions.ChangeRunningSpeed))]
    public class ChangeRunningSpeedPatch
    {
        public static Dictionary<Player, float> RunSpeeds = new Dictionary<Player, float>();

        public static void Prefix(Player player, float multiplier, bool useCap)
        {
            if (player.ReferenceHub.gameObject.TryGetComponent(out Slipstream slipstream) && !slipstream.CanSlip)
                return;

            if (useCap)
                multiplier = Mathf.Clamp(multiplier, -2f, 2f);

            RunSpeeds[player] = multiplier;
        }
    }
}
