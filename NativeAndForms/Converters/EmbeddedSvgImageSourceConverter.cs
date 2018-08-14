using FFImageLoading.Svg.Forms;
using NativeAndForms.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace NativeAndForms.Converters
{
    public class EmbeddedSvgImageSourceConverter : IValueConverter
    {
        private string sourceString;
        private Assembly assembly;
        private string resourceName;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string source)
            {
                sourceString = source;

                var sourceUri = new Uri(sourceString);

                if (!sourceString.ToString().StartsWith("resource://", StringComparison.OrdinalIgnoreCase))
                        throw new Exception("Only resource:// scheme is supported");

                var parts = sourceUri.OriginalString.Substring(11).Split('?');
                resourceName = parts.First();

                if (parts.Count() > 1)
                {
                    var name = Uri.UnescapeDataString(sourceUri.Query.Substring(10));
                    var assemblyName = new AssemblyName(name);
                    assembly = Assembly.Load(assemblyName);
                }
                
                var svgImageSource = new SvgImageSource(Xamarin.Forms.ImageSource.FromStream(LoadFile), 100, 100, true, null);

                return svgImageSource;
            }

            return null;
        }

        Stream LoadFile()
        {
            if (assembly == null)
            {
                assembly = IntrospectionExtensions.GetTypeInfo(typeof(HomeViewModel)).Assembly;
            }
            Stream stream = assembly.GetManifestResourceStream(resourceName);

            return stream;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
