﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using Tiera.Contracts.ViewModels;
using Tiera.Core.Contracts.Services;
using Tiera.Core.Models;

namespace Tiera.ViewModels
{
    public class CustomersViewModel : ObservableObject, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;
        private SampleOrder _selected;

        public SampleOrder Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public CustomersViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            var data = await _sampleDataService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            Selected = SampleItems.First();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
