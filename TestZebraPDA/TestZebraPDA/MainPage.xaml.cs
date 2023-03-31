using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestZebraPDA
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        bool isreading = false;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                //scan = DependencyService.Get<IScanner_Zebra>();
                //scan.RegisterReceiver();

                MessagingCenter.Subscribe<App, string>(this, "InitialScan", (Action<App, string>)(async (sender, arg) => {
                    if (isreading)
                    {
                        await OnScanAction(arg);
                    }

                }));

                isreading = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", ex.Message, "확인");
            }


        }
        protected override void OnDisappearing()
        {

            //scan.UnregisterReceiver();
            MessagingCenter.Unsubscribe<App, string>(this, "InitialScan");
            base.OnDisappearing();
        }


        private async Task OnScanAction(string arg)
        {
            isreading = true;


            //UI 변경관련 된 부분은 무조건 Thread 작업을 통한 작업이 필요함.
            Device.BeginInvokeOnMainThread(async () =>
            {
                txtValue.Text = arg.Split(',')[0];
                txtcodesymbol.Text = arg.Split(',')[1];
                await DisplayAlert($"{txtcodesymbol.Text}",$"{txtValue.Text}","확인");
            });


            isreading = false;
        }


       
}

}
