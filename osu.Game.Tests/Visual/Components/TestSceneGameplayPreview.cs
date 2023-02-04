// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Graphics.UserInterface;
using osu.Game.Screens.Play;

namespace osu.Game.Tests.Visual.Components
{
    public partial class TestSceneGameplayPreview : OsuTestScene
    {
        private readonly TestGameplayPreview gameplayPreview;

        public TestSceneGameplayPreview()
        {
            Child = gameplayPreview = new TestGameplayPreview(Beatmap)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        [Test]
        public void TestBeatMapChange()
        {
            AddStep("Change beatmap to dummy", () => Beatmap.Value = new DummyWorkingBeatmap(Audio, null));
            AddStep("Change beatmap to Kuba Oms", () => Beatmap.Value = CreateWorkingBeatmap(Ruleset.Value));
        }

        [Test]
        public void TestPause()
        {
            AddStep("Pause", () => gameplayPreview.GameplayClockContainer.Stop());
            AddStep("Start", () => gameplayPreview.GameplayClockContainer.Start());
        }

        private partial class TestGameplayPreview : GameplayPreview
        {
            public new MasterGameplayClockContainer GameplayClockContainer => base.GameplayClockContainer;

            public TestGameplayPreview(IBindable<WorkingBeatmap> beatmap)
                : base(beatmap)
            {
            }
        }
    }
}
