﻿using System;
using Xamarin.Forms;
using System.Reflection;

namespace FFImageLoading.Forms
{
	/// <summary>
	/// Embedded resource image source.
	/// eg. resource://YourProject.Resource.Resource.png
	/// eg. resource://YourProject.Resource.Resource.png?assembly=[FULL_ASSEMBLY_NAME]
	/// </summary>
    [Preserve(AllMembers = true)]
	public class EmbeddedResourceImageSource : ImageSource
    {
        public EmbeddedResourceImageSource(Uri uri)
        {
            var text = uri.OriginalString;

            if (string.IsNullOrWhiteSpace(uri.Query))
            {
                var assemblyName = Application.Current?.GetType()?.GetTypeAssemblyFullName();
                Uri = new Uri(assemblyName == null ? text : $"{text}?assembly={Uri.EscapeUriString(assemblyName)}");
            }
            else if (!uri.Query.Contains("assembly=", StringComparison.OrdinalIgnoreCase))
            {
                var assemblyName = Application.Current?.GetType()?.GetTypeAssemblyFullName();
                Uri = new Uri(assemblyName == null ? text : $"{text}?assembly={Uri.EscapeUriString(assemblyName)}");
            }
            else
            {
                Uri = uri;
            }
        }

        public EmbeddedResourceImageSource(string resourceName, Assembly assembly)
        {
            Uri = new Uri($"resource://{resourceName}?assembly={Uri.EscapeUriString(assembly.FullName)}");
        }

        public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(Uri), typeof(EmbeddedResourceImageSource), default(Uri));

        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public override string ToString()
		{
			return $"EmbeddedResource: {Uri}";
		}
    }
}