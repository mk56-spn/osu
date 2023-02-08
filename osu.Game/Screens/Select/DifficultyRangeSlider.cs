// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;

namespace osu.Game.Screens.Select
{
    public sealed partial class DifficultyRangeSlider : ShearedRangeSlider
    {
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
        }
    }
}
