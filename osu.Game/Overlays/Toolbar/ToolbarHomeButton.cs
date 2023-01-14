// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Game.Input.Bindings;

namespace osu.Game.Overlays.Toolbar
{
    public partial class ToolbarHomeButton : ToolbarButton
    {
        public ToolbarHomeButton()
        {
            Width *= 1.4f;
            Hotkey = GlobalAction.Home;
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            Add(new Box
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
                Colour = colourProvider.Background2
            });
            RelativeSizeAxes = Axes.None;
            Margin = new MarginPadding { Top = -10 };
            Height = 70;
            Masking = true;
            CornerRadius = 5;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 4,
                Colour = Colour4.Black.Opacity(0.1f),
            };
            Flow.Padding = new MarginPadding { Horizontal = 12 };
            TooltipMain = "home";
            TooltipSub = "return to the main menu";
            SetIcon("Icons/Hexacons/home");
        }
    }
}
