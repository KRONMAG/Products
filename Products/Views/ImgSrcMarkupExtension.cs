using System;
using System.Windows.Markup;

namespace Products.Views
{
    public class ImgSrcMarkupExtension : MarkupExtension
    {
        private string source;

        public ImgSrcMarkupExtension(string source)
        {
            this.source = source;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return $"{Environment.CurrentDirectory}/{source}";
        }
    }
}
