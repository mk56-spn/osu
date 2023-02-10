// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;

namespace osu.Game.Screens.Select
{
    public sealed partial class DifficultyRangeSlider : ShearedRangeSlider
    {
        [Resolved]
        private OsuColour colours { get; set; } = null!;

        public DifficultyRangeSlider()
        {
            Height = 70;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            DefaultStringLowerBound = "0";
            DefaultStringUpperBound = "∞";
            DefaultTooltipUpperBound = UserInterfaceStrings.NoLimit;
            TooltipSuffix = "stars";
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            LowerBound = config.GetBindable<double>(OsuSetting.DisplayStarsMinimum);
            UpperBound = config.GetBindable<double>(OsuSetting.DisplayStarsMaximum);

            LowerSlider.KeyboardStep = 0.01f;
            LowerSlider.KeyboardStep = 0.01f;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            LowerBound.BindValueChanged(l =>
                LowerSlider.Nub.AccentColour = LowerSlider.Nub.GlowingAccentColour = gradientForStarDifficulty(l.NewValue), true);
            UpperBound.BindValueChanged(l =>
                UpperSlider.Nub.AccentColour = UpperSlider.Nub.GlowingAccentColour = gradientForStarDifficulty(l.NewValue), true);
        }

        private ColourInfo gradientForStarDifficulty(double stars) =>
            ColourInfo.GradientHorizontal(colours.ForStarDifficulty(stars - 0.2f), colours.ForStarDifficulty(stars + 0.2f));
    }
}
