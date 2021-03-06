﻿using System;
using System.Windows.Data;

namespace Ais.Internal.Dcm.ModernUIV2.Converter
{
    public class UrlToFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string filename = string.Empty;
            if (value != null && !string.IsNullOrWhiteSpace(value as string))
            {
                string url = value as string;
                string filepath = url.Split('?')[0];
                filename = filepath.Substring(filepath.LastIndexOf('/') + 1);
            }
            return filename;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
