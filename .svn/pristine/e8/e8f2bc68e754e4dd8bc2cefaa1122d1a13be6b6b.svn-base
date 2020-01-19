using System;

namespace Files.Activities
{
    [Flags]
    public enum AccessMask
    {
        Unknown = 0x0,
        Read = 0x1,
        Create = 0x2,

        Delete = 0x10000,
        ReadControl = 0x20000,
        WriteDac = 0x40000,
        WriteOwner = 0x80000,
        Synchronize = 0x100000,

        All = 0xFFFFFF
    }
}