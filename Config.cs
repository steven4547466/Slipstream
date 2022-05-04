using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slipstream
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public float WalkPercentIncrease { get; set; } = 10f;
        public float WalkPercentIncreaseValue = 0f;
        public float RunPercentIncrease { get; set; } = 5f;
        public float RunPercentIncreaseValue = 0f;
        
        public float SlipDuratation { get; set; } = 2f;

        public float Cooldown { get; set; } = 4f;

        public float DistanceToSlip { get; set; } = 0.3f;

        public float DistanceToSlipSqr = 0f;
    }
}
