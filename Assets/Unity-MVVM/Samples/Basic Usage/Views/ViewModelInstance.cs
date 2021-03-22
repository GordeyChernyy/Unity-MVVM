using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMVVM.ViewModel;

public class ViewModelInstance : ViewModelBase
{
    public string label;

    private string _text;
    public string Text
    {
        get=>_text;
        set
        {
            if (value == _text) return;
            _text = value;
            NotifyPropertyChanged(nameof(Text));
        }
    }
    int counter = 0;

    // Update is called once per frame
    void Update()
    {
        counter++;
        Text = label + " : " + counter.ToString();
    }
}
