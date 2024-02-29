﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTech.ViewModel;

namespace TeleTech.Stores
{
    internal class NavigationStore
    {
        public event Action CurrentViewChanged;
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnCurrentViewChanged();
            }
        }

        private void OnCurrentViewChanged()
        {
            CurrentViewChanged?.Invoke();
        }
    }
}