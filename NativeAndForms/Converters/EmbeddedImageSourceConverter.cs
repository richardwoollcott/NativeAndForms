using FFImageLoading.Forms;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace NativeAndForms.Converters
{
    public class EmbeddedImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sourceString)
            {
                var sourceUri = new Uri(sourceString);

                //var imageSource = new EmbeddedResourceImageSource(sourceUri);
                //var imageSource = new EmbeddedResourceImageSource(sourceString, typeof(HomeViewModel).Assembly);

                //var imageSource = new FileImageSource() { File = sourceUri.AbsolutePath };
                var imageSource = new FileImageSource() { File = "NativeAndForms.Resources.delay.png" };
                return imageSource;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
