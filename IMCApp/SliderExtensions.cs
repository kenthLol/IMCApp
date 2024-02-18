using Google.Android.Material.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCApp;

public static class ListenerExtensions
{
    private class BaseOnChangeListener(Action<Java.Lang.Object, float, bool> onValueChanged) : Java.Lang.Object, IBaseOnChangeListener
    {
        public void OnValueChange(Java.Lang.Object Slider, float Value, bool FromUser)
        {
            onValueChanged(Slider, Value, FromUser);
        }
    }

    public static void AddOnChangeListener(this RangeSlider RangeSlider, Action<Java.Lang.Object, float, bool> onValueChanged)
    {
        var method = Java.Lang.Class
            .ForName("com.google.android.material.slider.BaseSlider")
            .GetDeclaredMethods()
            .FirstOrDefault(m => m.Name == "addOnChangeListener");

        method?.Invoke(RangeSlider, new BaseOnChangeListener(onValueChanged));
    }
}
