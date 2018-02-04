using System;
namespace QuantConnect.Queues
{
    /// <summary>
    /// Code modified from https://github.com/colinmxs/CodenameGenerator
    /// </summary>
    public enum Casing
    {
        /// <summary>
        /// example: annoyedWombat
        /// </summary>
        CamelCase,

        /// <summary>
        /// example: AnnoyedWombat
        /// </summary>
        PascalCase,

        /// <summary>
        /// example: ANNOYEDWOMBAT
        /// </summary>
        UpperCase,

        /// <summary>
        /// example: annoyedwombat
        /// </summary>
        LowerCase
    }
}
