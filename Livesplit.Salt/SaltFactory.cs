using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;

namespace LiveSplit.Salt
{
    // ReSharper disable once UnusedMember.Global
    public class SaltFactory : IComponentFactory
    {
        public IComponent Create(LiveSplitState state)
        {
            return new SaltComponent(state, ComponentName);
        }

        public string UpdateName => ComponentName;
        public string XMLURL => UpdateURL + "Updates.xml";
        public string UpdateURL => "https://raw.githubusercontent.com/seanpr96/Livesplit.Salt/master/";
        public Version Version => new Version(0, 0, 1);

        public string ComponentName => "Salt and Sanctuary Autosplitter v" + Version;
        public string Description => "Salt and Sanctuary Autosplitter";
        public ComponentCategory Category => ComponentCategory.Control;
    }
}
