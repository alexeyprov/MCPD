using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPermission
{
    // Enumerated type for permission states.
    [Serializable]
    public enum SoundPermissionState
    {
        NoSound = 0,
        PlaySystemSounds = 1,
        PlayAnySound = 2
    }
}
