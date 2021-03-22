using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityMVVM.ViewModel
{
    public class ViewModelReference : MonoBehaviour
    {
        [SerializeField]
        private ViewModelBase _viewModel;
        public ViewModelBase ViewModel => _viewModel;
    }
}