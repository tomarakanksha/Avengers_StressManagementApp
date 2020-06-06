using System;
using Xamarin.Forms;

namespace StressManagement
{
	public partial class LinkToInAppXaml : ContentPage
	{
		public LinkToInAppXaml ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Demonstrates how to load a view for web browsing within an app.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void navButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync (new InAppBrowserXaml ("https://web-chat.global.assistant.watson.cloud.ibm.com/preview.html?region=eu-gb&integrationID=6dbcb2ce-0e69-4d58-a02b-f9f507e61683&serviceInstanceID=f0e8fb68-e82e-489f-96cf-0c996ed4d390"));
		}
	}
}

