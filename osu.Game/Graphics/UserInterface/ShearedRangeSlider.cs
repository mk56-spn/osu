// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Graphics.UserInterface
{
    public partial class ShearedRangeSlider : CompositeDrawable
    {
        /// <summary>
        /// The lower limiting value
        /// </summary>
        public Bindable<double> LowerBound
        {
            get => LowerSlider.Current;
            set => LowerSlider.Current = value;
        }

        /// <summary>
        /// The upper limiting value
        /// </summary>
        public Bindable<double> UpperBound
        {
            get => UpperSlider.Current;
            set => UpperSlider.Current = value;
        }

        /// <summary>
        /// Text that describes this RangeSlider's functionality
        /// </summary>
        public string Label
        {
            set => label.Text = value;
        }

        public float NubWidth
        {
            set => LowerSlider.NubWidth = UpperSlider.NubWidth = value;
        }

        /// <summary>
        /// Minimum difference between the lower bound and higher bound
        /// </summary>
        public float MinRange
        {
            set => minRange = value;
        }

        /// <summary>
        /// lower bound display for when it is set to its default value
        /// </summary>
        public string DefaultStringLowerBound
        {
            set => LowerSlider.DefaultString = value;
        }

        /// <summary>
        /// upper bound display for when it is set to its default value
        /// </summary>
        public string DefaultStringUpperBound
        {
            set => UpperSlider.DefaultString = value;
        }

        public LocalisableString DefaultTooltipLowerBound
        {
            set => LowerSlider.DefaultTooltip = value;
        }

        public LocalisableString DefaultTooltipUpperBound
        {
            set => UpperSlider.DefaultTooltip = value;
        }

        public string TooltipSuffix
        {
            set => UpperSlider.TooltipSuffix = LowerSlider.TooltipSuffix = value;
        }

        private float minRange = 0.1f;

        private readonly OsuSpriteText label;

        protected readonly LowerBoundSlider LowerSlider;
        protected readonly UpperBoundSlider UpperSlider;

        public ShearedRangeSlider()
        {
            const float vertical_offset = 13;

            InternalChildren = new Drawable[]
            {
                label = new OsuSpriteText
                {
                    Font = OsuFont.GetFont(size: 14),
                },
                UpperSlider = new UpperBoundSlider
                {
                    KeyboardStep = 0.1f,
                    RelativeSizeAxes = Axes.X,
                    Y = vertical_offset,
                },
                LowerSlider = new LowerBoundSlider
                {
                    KeyboardStep = 0.1f,
                    RelativeSizeAxes = Axes.X,
                    Y = vertical_offset,
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            LowerSlider.Current.ValueChanged += min => UpperSlider.Current.Value = Math.Max(min.NewValue + minRange, UpperSlider.Current.Value);
            UpperSlider.Current.ValueChanged += max => LowerSlider.Current.Value = Math.Min(max.NewValue - minRange, LowerSlider.Current.Value);
        }

        public partial class LowerBoundSlider : BoundSlider
        {
            protected override void LoadComplete()
            {
                base.LoadComplete();

                AccentColour = BackgroundColour;
                BackgroundColour = Color4.Transparent;
            }

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) =>
                base.ReceivePositionalInputAt(screenSpacePos)
                && screenSpacePos.X <= Nub.ScreenSpaceDrawQuad.TopRight.X;
        }

        public partial class UpperBoundSlider : BoundSlider
        {
            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) =>
                base.ReceivePositionalInputAt(screenSpacePos)
                && screenSpacePos.X >= Nub.ScreenSpaceDrawQuad.TopLeft.X;
        }

        public partial class BoundSlider : ShearedSliderBar<double>
        {
            public string? DefaultString;
            public LocalisableString? DefaultTooltip;
            public string? TooltipSuffix;
            public new ShearedNub Nub => base.Nub;
            public float NubWidth { get; set; } = ShearedNub.HEIGHT;

            public override LocalisableString TooltipText =>
                (Current.IsDefault ? DefaultTooltip : Current.Value.ToString($@"0.## {TooltipSuffix}")) ?? Current.Value.ToString($@"0.## {TooltipSuffix}");

            protected override bool OnHover(HoverEvent e)
            {
                base.OnHover(e);
                return true; // Make sure only one nub shows hover effect at once.
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                Nub.Width = NubWidth;
                RangePadding = Nub.Width / 2;

                OsuSpriteText currentDisplay;

                // Nesting the text in a sheared container allows it to be adequately centered in the nub.
                Nub.Add(new Container
                {
                    Shear = ShearedNub.SHEAR,
                    RelativeSizeAxes = Axes.Both,
                    Child = currentDisplay = new OsuSpriteText
                    {
                        Shear = -ShearedNub.SHEAR,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Y = -0.5f,
                        Colour = Color4.White,
                        Font = OsuFont.Torus.With(size: 16),
                    }
                });

                Current.BindValueChanged(current =>
                {
                    currentDisplay.Text = (current.NewValue != Current.Default ? current.NewValue.ToString("N1") : DefaultString) ?? current.NewValue.ToString("N1");
                }, true);
            }

            [BackgroundDependencyLoader(true)]
            private void load(OverlayColourProvider? colourProvider)
            {
                if (colourProvider == null) return;

                AccentColour = colourProvider.Background2;
                Nub.AccentColour = colourProvider.Background2;
                Nub.GlowingAccentColour = colourProvider.Background1;
                Nub.GlowColour = colourProvider.Background2;
            }
        }
    }
}
