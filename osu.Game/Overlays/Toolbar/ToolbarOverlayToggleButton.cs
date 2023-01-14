// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Overlays.Toolbar
{
    public partial class ToolbarOverlayToggleButton : ToolbarButton
    {
        [Resolved]
        private OverlayColourProvider colourProvider { get; set; } = null!;

        private Box stateBackground = null!;

        private OverlayContainer? stateContainer;

        private readonly Bindable<Visibility> overlayState = new Bindable<Visibility>();

        public OverlayContainer? StateContainer
        {
            get => stateContainer;
            set
            {
                stateContainer = value;

                overlayState.UnbindBindings();

                if (stateContainer != null)
                {
                    Action = stateContainer.ToggleVisibility;
                    overlayState.BindTo(stateContainer.State);
                }

                if (stateContainer is not INamedOverlayComponent named) return;

                TooltipMain = named.Title;
                TooltipSub = named.Description;
                SetIcon(named.IconTexture);
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(stateBackground = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = colourProvider.Background1,
                Depth = 2,
            });

            overlayState.ValueChanged += stateChanged;
        }

        private void stateChanged(ValueChangedEvent<Visibility> state)
        {
            switch (state.NewValue)
            {
                case Visibility.Hidden:
                    stateBackground.FadeColour(colourProvider.Background2, 500, Easing.OutQuint);
                    break;

                case Visibility.Visible:
                    stateBackground.FadeColour(colourProvider.Background1, 500, Easing.OutQuint);
                    break;
            }
        }
    }
}
