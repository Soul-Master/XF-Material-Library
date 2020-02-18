using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialChip : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialChip), default(Command));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialChip), default(object));        
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChip), default(Color));        
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChip), Color.FromHex("#DE000000"));
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChip), string.Empty);
        public static readonly BindableProperty BadgeProperty = BindableProperty.Create(nameof(Badge), typeof(string), typeof(MaterialChip), string.Empty);

        public MaterialChip()
        {
            this.InitializeComponent();
        }
        
        public Command Command
        {
            get => (Command)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => (object)this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        public string Badge
        {
            get => (string)this.GetValue(BadgeProperty);
            set => this.SetValue(BadgeProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.BackgroundColor))
            {
                ChipContainer.BackgroundColor = this.BackgroundColor;
            }
            else
            {
                base.OnPropertyChanged(propertyName);

                switch (propertyName)
                {
                    case nameof(this.Text):
                        ChipLabel.Text = this.Text;
                        break;
                    case nameof(this.TextColor):
                        ChipLabel.TextColor = this.TextColor;
                        break;
                    case nameof(this.Badge):
                        if (!string.IsNullOrEmpty(this.Badge))
                        {
                            BadgeLabel.Text = this.Badge;
                            BadgeContainer.IsVisible = true;
                        }
                        else
                        {
                            BadgeLabel.Text = string.Empty;
                            BadgeContainer.IsVisible = false;
                        }
                        break;
                    case nameof(this.Command):
                    {
                        if (this.Command != null && ChipContainer.GestureRecognizers.Count <= 0)
                        {
                            ChipContainer.GestureRecognizers.Add(new TapGestureRecognizer { Command = Command, CommandParameter = CommandParameter });
                        }
                        break;
                    }
                }
            }
        }
    }
}