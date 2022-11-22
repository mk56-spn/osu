﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class PracticeOverlayStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.PracticeOverlay";

        /// <summary>
        /// "Practice Overlay"
        /// </summary>
        public static LocalisableString PracticeOverlayHeaderTitle => new TranslatableString(getKey(@"practice_overlay"), @"Practice Overlay");

        /// <summary>
        /// "Practice any segment of a map!"
        /// </summary>
        public static LocalisableString PracticeOverlayHeaderDescription => new TranslatableString(getKey(@"practice_any_segment_of_a"), @"Practice any segment of a map!");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
