// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Screens.Play;

namespace osu.Game.Overlays.Practice
{
    public partial class PracticePlayer : Player
    {
        private PracticeOverlay practiceOverlay = null!;

        public PracticePlayer(PlayerConfiguration? configuration = null)
            : base(configuration)
        {
        }

        [Resolved(CanBeNull = true)]
        internal IOverlayManager? OverlayManager { get; private set; }

        private IDisposable? practiceOverlayRegistration;

        [BackgroundDependencyLoader]
        private void load(OsuColour colour, IBindable<WorkingBeatmap> beatmap, PracticePlayerLoader loader)
        {
            var playableBeatmap = beatmap.Value.GetPlayableBeatmap(beatmap.Value.BeatmapInfo.Ruleset);

            SetGameplayStartTime(loader.CustomStart.Value * (playableBeatmap.HitObjects.Last().StartTime - playableBeatmap.HitObjects.First().StartTime));

            addButtons(colour);
            LoadComponent(practiceOverlay = new PracticeOverlay
            {
                State = { Value = Visibility.Visible },
                Restart = () => Restart()
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            practiceOverlayRegistration = OverlayManager?.RegisterBlockingOverlay(practiceOverlay);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            practiceOverlayRegistration?.Dispose();
        }

        protected override bool CheckModsAllowFailure() => false; // never fail. Todo: find a way to avoid instantly failing after initial seek

        private void addButtons(OsuColour colour)
        {
            PauseOverlay.AddButton("Practice", colour.Blue, () => practiceOverlay.Show());
            FailOverlay.AddButton("Practice", colour.Blue, () => practiceOverlay.Show());
        }
    }
}
