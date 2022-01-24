using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Crystals.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {

        public string CatFact { get; set; }

        #region Commands

        public ICommand GetStartedCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel() : base()
        {
            GetStartedCommand = new Command(GetStartedCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void GetStartedCommandHandler()
        {
            _= await GetCatFactAsync();
            await Application.Current.MainPage.DisplayAlert("Cat Fact", CatFact, "Continue");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        #endregion

        public async Task<string> GetCatFactAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://catfact.ninja/fact?max_length=35"),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Result results = JsonConvert.DeserializeObject<Result>(body);
                CatFact = results.fact;
                Console.WriteLine(CatFact);
                OnPropertyChanged(nameof(CatFact));

            }
            return CatFact;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.WriteLine("Changed " + propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public class Result
        {
            public string fact { get; set; }
            public string length { get; set; }
        }

    }
}
