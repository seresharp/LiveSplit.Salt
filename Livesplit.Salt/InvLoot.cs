using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LiveSplit.Salt
{
    // Yes, this is terrible
    // Idk how to handle the pointer -> string conversion better
    // I'm open to ideas, please help me fix this
    public struct InvLoot
    {
        // ReSharper disable InconsistentNaming
        // Copying field names from actual InvLoot class
        public readonly string name;
        public readonly int category;
        public readonly int catalogIdx;
        public readonly int count;
        public readonly float durability;
        public readonly int upgrade;
        public readonly int elemental;
        public readonly int sanctuaryConsumableCreed;
        public readonly bool equipped;


        public InvLoot(Process program, IntPtr address)
        {
            InvLootActual data = program.Read<InvLootActual>(address, 0x0);

            name = program.GetString((IntPtr) data.name);
            category = data.category;
            catalogIdx = data.catalogIdx;
            count = data.count;
            durability = data.durability;
            upgrade = data.upgrade;
            elemental = data.elemental;
            sanctuaryConsumableCreed = data.sanctuaryConsumableCreed;
            equipped = data.equipped;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct InvLootActual
        {
            private readonly uint Vtable;
            public readonly uint name;
            private readonly uint title;

            public readonly int category;
            public readonly int catalogIdx;
            public readonly int count;
            public readonly float durability;
            public readonly int upgrade;
            public readonly int elemental;
            public readonly int sanctuaryConsumableCreed;
            public readonly bool equipped;
        }
    }
}
