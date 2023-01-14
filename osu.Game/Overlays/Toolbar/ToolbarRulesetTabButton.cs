// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Rulesets;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK;

namespace osu.Game.Overlays.Toolbar
{
    public partial class ToolbarRulesetTabButton : TabItem<RulesetInfo>
    {
        private readonly RulesetButton ruleset;

        public ToolbarRulesetTabButton(RulesetInfo value)
            : base(value)
        {
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;
            Child = ruleset = new RulesetButton
            {
                Active = false,
            };

            var rInstance = value.CreateInstance();

            ruleset.TooltipMain = rInstance.Description;
            ruleset.TooltipSub = $"play some {rInstance.Description}";
            ruleset.SetIcon(rInstance.CreateIcon());
        }

        protected override void OnActivated() => ruleset.Active = true;

        protected override void OnDeactivated() => ruleset.Active = false;

        private partial class RulesetButton : ToolbarButton
        {
            [Resolved]
            private OverlayColourProvider colourProvider { get; set; } = null!;

            public bool Active
            {
                set => IconContainer.Colour = value ? colourProvider.Highlight1 : Colour4.White;
            }

            protected override bool OnClick(ClickEvent e)
            {
                Parent.TriggerClick();
                return base.OnClick(e);
            }

            public RulesetButton()
            {
                IconContainer.Size = new Vector2(20);
            }
        }
    }
}
