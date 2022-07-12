using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsViewClickTester
{
    internal class StartupPage : ContentPage
    {
        class Data
        {
            public string Text { get; set; }
        }
        List<Data> DataItems = new() { new Data { Text = "One" }, new Data { Text = "Two" } , new Data { Text = "Three" } };
        public StartupPage()
        {
            BindingContext = this;
            var collectionView = new CollectionView()
            {
                SelectionMode = SelectionMode.Single,
                Margin = 10,
            };
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                double heightRequest = 45;

                var image = new Image { Source = "ic_open.png", HorizontalOptions = LayoutOptions.Start, HeightRequest = heightRequest, WidthRequest = heightRequest };
                var checkBox = new CheckBox { HorizontalOptions = LayoutOptions.Start };
                var label = new Label { Text = "One", VerticalOptions = LayoutOptions.Center };
                label.SetBinding(Label.TextProperty, "Text");
                var graphicsView = new GraphicsView { Drawable = new MyDrawable() };

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(heightRequest, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.Add(image, 0, 0);
                grid.Add(checkBox, 1, 0);
                grid.Add(graphicsView, 2, 0);
                grid.Add(label, 2, 0);

                return grid;

            });
            collectionView.ItemsSource = DataItems;
            collectionView.SelectionChanged += CollectionView_SelectionChanged;

            Content = collectionView;
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert("Alert", "Clicked", "OK");
        }

        internal class MyDrawable : IDrawable
        {
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.FillColor = Colors.LightBlue.MultiplyAlpha(0.5f);
                canvas.FillRectangle(dirtyRect);
            }
        }
    }
}
