using System;
using System.Collections.Generic;
using System.Text;
using MobileProjectJuist3.Models;
using MobileProjectJuist3.Services;

namespace MobileProjectJuist3.ViewModels
{
    class CrystalPageViewModel : BaseViewModel
    {
        #region Fields


        private List<Crystal> crystals = new List<Crystal>();

        #endregion

        #region Constructors
        public CrystalPageViewModel()
        {
            this.InitializeCrystals();

        }

        #endregion
        #region

        public List<Crystal> Crystals
        {
            get
            {
                return this.crystals;

            }
            set
            {

            }
        }

        #endregion
        #region Methods

        private async void InitializeCrystals()
        {
            this.crystals = await CrystalService.GetCrystalsForUserAsync(App.CurrentUser.Id);
            if (crystals.Count <= 0)
            {
                Crystal crystal = new Crystal();
                crystal.Name = "Quartz";
                crystal.UserId = App.CurrentUser.Id;
                await CrystalService.SaveCrystalAsync(crystal);
                this.InitializeCrystals();

            }
        }


        #endregion

    }
}
