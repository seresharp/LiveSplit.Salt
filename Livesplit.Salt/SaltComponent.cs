using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.Salt
{
    public class SaltComponent : IComponent
    {
        private static readonly string[] BossItemNames =
        {
            "dice_dread", "dice_cutqueen", "dice_bull", "dice_alchemist", "dice_fauxjester", "dice_dragon",
            "dice_torturetree", "dice_pirate", "dice_inquisitor", "dice_hippogriff", "dice_ruinaxe", "dice_lakewitch",
            "dice_monster", "dice_squiddragon", "dice_nameless", "dice_leviathon", "dice_cloak", "dice_gasbag",
            "dice_clay", "dice_mummy", "dice_butterfly"
        };

        private readonly TimerModel _model;
        private readonly SaltMemory _mem;

        private readonly Dictionary<string, InvLoot> _bossItems = new Dictionary<string, InvLoot>();
        private readonly List<EnemyHealthTracker> _enemyTrackers = new List<EnemyHealthTracker>();

        public string ComponentName { get; }

        public SaltComponent(LiveSplitState state, string name)
        {
            ComponentName = name;
            _mem = new SaltMemory();

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            _model = new TimerModel
            {
                CurrentState = state
            };

            _model.CurrentState.OnStart -= TimerStart;
            _model.CurrentState.OnStart += TimerStart;
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            _mem.Hook();
            if (!_mem.IsHooked)
            {
                return;
            }

            if (_model.CurrentState.CurrentSplitIndex == -1)
            {
                CheckStart();
                return;
            }

            if (_mem.GetGameState() != GameState.Playing)
            {
                return;
            }

            CheckItemSplits();

            // TODO: Generic enemy kill split system
            // Unspeakable Deep
            int len = _mem.GetCharCount();
            for (int i = 0; i < len; i++)
            {
                if (_mem.GetCharType(i) != EnemyType.Leviathon || _mem.GetCharHealth(i) <= 0f ||
                    _enemyTrackers.Any(tracker => tracker.CharType == EnemyType.Leviathon))
                {
                    continue;
                }

                _enemyTrackers.Add(new EnemyHealthTracker(_mem, i));
            }

            CheckCharKills();

            if (_model.CurrentState.CurrentSplitIndex == _model.CurrentState.Run.Count - 1 && _mem.IsGameEnding())
            {
                _model.Split();
            }
        }

        private void CheckStart()
        {
            if (_mem.GetMenuLevel(0) == MenuLevel.VentureForth &&
                _mem.GetMenuTransitionMode(0) == TransitionMode.AllOut)
            {
                _model.Start();
            }
        }

        private void CheckItemSplits()
        {
            // Boss items
            foreach (string itemName in BossItemNames)
            {
                // Skipping nameless for now, will add back in after I make config
                if (itemName == "dice_nameless")
                {
                    continue;
                }

                if (!_mem.TryGetPlayerItem(0, itemName, out InvLoot item))
                {
                    continue;
                }

                if (_bossItems.TryGetValue(itemName, out InvLoot existingItem) && item.count <= existingItem.count)
                {
                    continue;
                }

                _bossItems[itemName] = item;
                _model.Split();
            }
        }

        private void CheckCharKills()
        {
            foreach (EnemyHealthTracker tracker in _enemyTrackers)
            {
                tracker.Update();
                if (tracker.ShouldSplit)
                {
                    _model.Split();
                }
            }

            _enemyTrackers.RemoveAll(tracker => tracker.Done);
        }

        private void TimerStart(object sender, EventArgs e)
        {
            _bossItems.Clear();
            _enemyTrackers.Clear();
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("Settings");
        }

        public void SetSettings(XmlNode settings)
        {
        }

        public void Dispose()
        {
            _model.CurrentState.OnStart -= TimerStart;
            _mem.Dispose();
        }

        #region Unused interface stuff
        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
        }

        public float HorizontalWidth => 0;
        public float MinimumHeight => 0;
        public float VerticalHeight => 0;
        public float MinimumWidth => 0;
        public float PaddingTop => 0;
        public float PaddingBottom => 0;
        public float PaddingLeft => 0;
        public float PaddingRight => 0;
        public IDictionary<string, Action> ContextMenuControls => null;
#endregion
    }
}
