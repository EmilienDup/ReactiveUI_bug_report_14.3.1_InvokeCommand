using System;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Working_xamarinforms_example_ReactiveUI_14_1_1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var command = ReactiveCommand.Create<string>(id => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.Label.Text = $"Command executed, execution ID: {id}";
                });
            });


            // NOTE: This code works just fine

            //Observable.FromEventPattern(
            //    h => this.Button.Clicked += h,
            //    h => this.Button.Clicked -= h)
            //    .Select(_ => Guid.NewGuid().ToString())
            //    .InvokeCommand(command);

            // This code works
            Observable.FromEventPattern(
                h => this.Button.Clicked += h,
                h => this.Button.Clicked -= h)
                .Subscribe(_ => {
                    Observable.Return(Guid.NewGuid().ToString())
                              .InvokeCommand(command);

                });
        }
    }
}
