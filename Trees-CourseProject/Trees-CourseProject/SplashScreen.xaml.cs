using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Trees_CourseProject
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            // Анимация появления окна
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(2); // Продолжительность анимации в секундах
            this.BeginAnimation(Window.OpacityProperty, fadeInAnimation);

            // Ожидание завершения анимации
            await Task.Delay(TimeSpan.FromSeconds(2)); // Длительность анимации плюс дополнительное время

            // Закрытие окна с анимацией исчезновения
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            fadeOutAnimation.From = 1;
            fadeOutAnimation.To = 0;
            fadeOutAnimation.Duration = TimeSpan.FromSeconds(2); // Продолжительность анимации в секундах
            fadeOutAnimation.Completed += (s, args) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }
    }
}
