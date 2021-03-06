﻿namespace Concept2.Commands.CSafe
{
    public class CadenceCommand : Command
    {
        public CadenceCommand() : base(Enums.Csafe.GETCADENCE_CMD, 3)
        {
            StrokeRate = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            StrokeRate = reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x54 - StrokesPerMinute
        }

        public uint StrokeRate { get; private set; }
    }
}
