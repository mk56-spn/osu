// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Screens.Select;

namespace osu.Game.Tests.Visual.SongSelect
{
    public partial class TestSceneDifficultyRangeSlider : OsuTestScene
    {
        public TestSceneDifficultyRangeSlider()
        {
            Children = new Drawable[]
            {
                new DifficultyRangeSlider
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 700
                },
                new DifficultyRangeSlider
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 50,
                    Width = 500
                },
                new DifficultyRangeSlider
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 100,
                    Width = 300
                }
            };
        }
    }
}
