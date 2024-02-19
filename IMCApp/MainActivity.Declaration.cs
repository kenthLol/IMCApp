using AndroidX.CardView.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCApp;

public partial class MainActivity
{
    private bool isMaleSelected = false;
    private bool isFemaleSelected = false;
    private int currentWeight = 60;
    private int currentHeight = 120;
    private int currentAge = 20;

    const string IMC_KEY = "IMC_RESULT";

    private CardView viewMale = default!;
    private CardView viewFemale = default!;

    private TextView tvHeight = default!;
    private RangeSlider rsHeight = default!;

    private FloatingActionButton btnSubtractWeight = default!;
    private FloatingActionButton btnAddWeight = default!;
    private TextView tvWeight = default!;

    private FloatingActionButton btnSubtractAge = default!;
    private FloatingActionButton btnAddAge = default!;
    private TextView tvAge = default!;

    private Button btnCalculate = default!;
}
