using FFImageLoading.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NativeAndForms.Extensions
{
    [ContentProperty("Source")]
    public class EmbeddedImageResourceExtension : IMarkupExtension
    {
        public Uri Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            var imageSource = new EmbeddedResourceImageSource(Source);
            // Do your translation lookup here, using whatever method you require
            //var imageSource = MultiResourceImageSource.FromMultiResource(Source);

            return imageSource;
        }
    }

}
