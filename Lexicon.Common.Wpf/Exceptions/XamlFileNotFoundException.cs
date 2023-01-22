﻿using Lexicon.Common.Wpf.Markup;

namespace Lexicon.Common.Wpf.Exceptions;
public class XamlFileNotFoundException : Exception
{
    public XamlFileNotFoundException() : base($"Could not load xaml file from the entry or executing assembly. Consider extending the '{nameof(DynamicXamlLoader)}' class by providing additional assmeblys to look for.")
    {
    }
}
