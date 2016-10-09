using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BossrMobile.Controls
{
    public enum AlertType
    {
        None,
        Error,
        Warning,
        Success
    }

    public enum AlertPreset
    {
        None,
        NoConnection
    }

    public partial class Alert : ContentView
    {
        public Alert()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public AlertType Type
        {
            get { return (AlertType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public AlertPreset Preset
        {
            get { return (AlertPreset)GetValue(PresetProperty); }
            set { SetValue(PresetProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(string), "");
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(AlertType), typeof(string), AlertType.None);
        public static readonly BindableProperty PresetProperty = BindableProperty.Create(nameof(Preset), typeof(AlertPreset), typeof(string), AlertPreset.None);

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == nameof(Text))
                Label.Text = Text;
            if (propertyName == nameof(Type))
            {
                if (Type == AlertType.None)
                {
                    Hide();
                }
                else if (Type == AlertType.Warning)
                {
                    BackgroundColor = Color.FromHex("#FFEB3B");
                    Show();
                }
                else if (Type == AlertType.Success)
                {
                    BackgroundColor = Color.FromHex("#4CAF50");
                    Show();
                }
                else if (Type == AlertType.Error)
                {
                    BackgroundColor = Color.FromHex("#F44336");
                    Show();
                }
            }
            if (propertyName == nameof(Preset))
            {
                if (Preset == AlertPreset.None)
                {
                    Hide();
                }
                else if (Preset == AlertPreset.NoConnection)
                {
                    Text = "No Connection";
                    Type = AlertType.Error;
                    Show();
                }
            }
        }

        private void Show()
        {
            Opacity = 0;
            IsVisible = true;
            this.FadeTo(1, 400, Easing.CubicInOut);
        }

        private void Hide()
        {
            IsVisible = false;
        }
    }
}
