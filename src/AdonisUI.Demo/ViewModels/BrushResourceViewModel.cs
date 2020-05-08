using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class BrushResourceViewModel
        : ViewModel
    {
        public enum BrushGroup
        {
            Foreground,
            Accent,
            Layer0,
            Layer1,
            Layer2,
            Layer3,
            Layer4,
            Disabled,
            Status,
            Controls,
        }

        private ComponentResourceKey _resourceKey;

        public ComponentResourceKey ResourceKey
        {
            get => _resourceKey;
            set
            {
                SetProperty(ref _resourceKey, value);
                SetProperty(ref _group, DetermineGroup());
                RaisePropertyChanged(nameof(ShortName));
            }
        }

        public string Name
        {
            get => _resourceKey.ResourceId.ToString();
        }

        public string ShortName
        {
            get
            {
                if (Name.EndsWith("Brush"))
                    return Name.Substring(0, Name.Length - "Brush".Length);

                return Name;
            }
        }

        public string ColorName
        {
            get
            {
                if (!_isBasedOnColorResource || !Name.EndsWith("Brush"))
                    return null;

                return ShortName + "Color";
            }
        }

        private bool _isBasedOnColorResource;

        public bool IsBasedOnColorResource
        {
            get => _isBasedOnColorResource;
            set => SetProperty(ref _isBasedOnColorResource, value);
        }

        private BrushGroup _group;

        public BrushGroup Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        private BrushGroup DetermineGroup()
        {
            string brushName = _resourceKey.ResourceId.ToString();

            foreach (BrushGroup availableGroup in Enum.GetValues(typeof(BrushGroup)))
            {
                if (brushName.StartsWith(availableGroup.ToString()))
                    return availableGroup;
            }

            string[] statusKeywords = { "success", "error", "alert" };

            if (statusKeywords.Any(keyword => brushName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)))
                return BrushGroup.Status;

            return BrushGroup.Controls;
        }
    }
}
