using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurConv
{
    public partial class App : Application
    {

        MainViewModel mvm;
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            mvm = new MainViewModel();
            MainPage = new MainPage(mvm);
        }

        protected override void OnStart()
        {
            //if (Application.Current.Properties.ContainsKey("Date"))
            //{
            //    mvm.Date = (string)Application.Current.Properties["Date"];
            //    mvm.result = (MainViewModel.Root)Application.Current.Properties["Cur"];
            //    mvm.curNames = (System.Collections.ObjectModel.ObservableCollection<MainViewModel.Cur>)Application.Current.Properties["curNames"];
            //    //mvm.setCurNameSelector();
            //    mvm.selectedCur1 = (MainViewModel.Cur)Application.Current.Properties["selectedCur1"];
            //    mvm.selectedCur2 = (MainViewModel.Cur)Application.Current.Properties["selectedCur2"];
            //    mvm.curRes1 = (string)Application.Current.Properties["curRes1"];
            //    mvm.curRes2 = (string)Application.Current.Properties["curRes2"];
                
            //}
            
        }

        protected override void OnSleep()
        {
            //Application.Current.Properties["Date"] = mvm.Date;
            //Application.Current.Properties["Cur"] = mvm.result;
            //Application.Current.Properties["curNames"] = mvm.curNames;
            //Application.Current.Properties["curRes1"] = mvm.curRes1;
            //Application.Current.Properties["curRes2"] = mvm.curRes2;
            //Application.Current.Properties["selectedCur1"] = mvm.selectedCur1;
            //Application.Current.Properties["selectedCur2"] = mvm.selectedCur2;
        }

        protected override void OnResume()
        {
            //if (Application.Current.Properties.ContainsKey("Date"))
            //{
            //    mvm.Date = (string)Application.Current.Properties["Date"];
            //    mvm.result = (MainViewModel.Root)Application.Current.Properties["Cur"];
            //    mvm.curNames = (System.Collections.ObjectModel.ObservableCollection<MainViewModel.Cur>)Application.Current.Properties["curNames"];
            //    //mvm.setCurNameSelector();
            //    mvm.selectedCur1 = (MainViewModel.Cur)Application.Current.Properties["selectedCur1"];
            //    mvm.selectedCur2 = (MainViewModel.Cur)Application.Current.Properties["selectedCur2"];
            //    mvm.curRes1 = (string)Application.Current.Properties["curRes1"];
            //    mvm.curRes2 = (string)Application.Current.Properties["curRes2"];
            //}
        }
    }
}
