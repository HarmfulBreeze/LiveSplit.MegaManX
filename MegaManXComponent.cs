using LiveSplit.ASL;
using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.TimeFormatters;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using LiveSplit.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.MegaManX
{
    class MegaManXComponent : LogicComponent
    {
        //public MegaManXSettings Settings { get; set; }

        protected Process Game { get; set; }

        protected TimerModel Model { get; set; }
        public ASLState OldState { get; set; }
        public ASLState State { get; set; }

        protected DeepPointer<byte> GameTimer { get; set; }

        public override string ComponentName
        {
            get { return "Mega Man X Auto Splitter"; }
        }

        public MegaManXComponent(LiveSplitState state)
        {
            State = new ASLState();
        }

        private void Rebuild()
        {
            State.ValueDefinitions.Clear();
            var gameVersion = GameVersion.NTSC10;

            switch (gameVersion)
            {
                case GameVersion.NTSC10: RebuildNTSC10(); break;
                default: Game = null; break;
            }
        }

        private void RebuildNTSC10()
        {
            State.ValueDefinitions.Add(new ASLValueDefinition()
            {
                Identifier = "scene",
                Pointer = new DeepPointer<byte>(1, Game, "snes9x.exe", 0x00A787D2)
            });
            State.ValueDefinitions.Add(new ASLValueDefinition()
            {
                Identifier = "selectedOption",
                Pointer = new DeepPointer<byte>(1, Game, "snes9x.exe", 0x027966FC)
            });
            State.ValueDefinitions.Add(new ASLValueDefinition()
            {
                Identifier = "bossHealth",
                Pointer = new DeepPointer<byte>(1, Game, snes9x.exe, )
            }
        }

        protected void TryConnect()
        {
            if (Game == null || Game.HasExited)
            {
                Game = Process.GetProcessesByName("snes9x").FirstOrDefault();
                if (Game != null)
                {
                    Rebuild();
                    State.RefreshValues();
                    OldState = State;
                }
            }
        }

        public bool Start(LiveSplitState timer, dynamic old, dynamic current)
        {
            
        }

    }
}
