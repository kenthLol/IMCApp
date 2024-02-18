using Android.Views;
using AndroidX.CardView.Widget;
using AndroidX.Core.Content;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Slider;
using static Android.Renderscripts.Sampler;

namespace IMCApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
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


        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitComponents();
            InitEvents();
            InitUI();
        }

        private void InitComponents()
        {
            viewMale = FindViewById<CardView>(Resource.Id.viewMale)!;
            viewFemale = FindViewById<CardView>(Resource.Id.viewFemale)!;

            tvHeight = FindViewById<TextView>(Resource.Id.tvHeight)!;
            rsHeight = FindViewById<RangeSlider>(Resource.Id.rsHeight)!;

            btnSubtractWeight = FindViewById<FloatingActionButton>(Resource.Id.btnSubtractWeight)!;
            btnAddWeight = FindViewById<FloatingActionButton>(Resource.Id.btnPlusWeight)!;
            tvWeight = FindViewById<TextView>(Resource.Id.tvWeight)!;

            btnSubtractAge = FindViewById<FloatingActionButton>(Resource.Id.btnSubtractAge)!;
            btnAddAge = FindViewById<FloatingActionButton>(Resource.Id.btnPlusAge)!;
            tvAge = FindViewById<TextView>(Resource.Id.tvAge)!;

            btnCalculate = FindViewById<Button>(Resource.Id.btnCalculate)!;
        }

        private void InitEvents()
        {

            viewMale.Click += (sender, e) =>
            {
                ChangeGender();
                SetGenderColor();
            };

            viewFemale.Click += (sender, e) =>
            {
                ChangeGender();
                SetGenderColor();
            };


            btnSubtractWeight.Click += (sender, e) =>
            {
                currentWeight--;
                SetWeight();
            };

            btnAddWeight.Click += (sender, e) =>
            {
                currentWeight++;
                SetWeight();
            };

            rsHeight.AddOnChangeListener(OnChangedListener);

            btnSubtractAge.Click += (sender, e) =>
            {
                currentAge--;
                SetAge();
            };

            btnAddAge.Click += (sender, e) =>
            {
                currentAge++;
                SetAge();
            };

            btnCalculate.Click += (sender, e) =>
            {
                double result = CalculateIMC();
                NavigateToResult(result);
            };
        }

        private void InitUI()
        {
            SetGenderColor();
            SetWeight();
            SetAge();
        }

        private void OnChangedListener(object RangeSlider, float value, bool FromUser)
        {
            currentHeight = (int)value;
            tvHeight.Text = $"{currentHeight} cm";
        }

        private void NavigateToResult(double result)
        {
            var intent = new Android.Content.Intent(this, typeof(ResultImcActivity));
            intent.PutExtra(IMC_KEY, result);
            StartActivity(intent);
        }

        private double CalculateIMC()
        {
            double height = currentHeight / 100.0;
            double imc = currentWeight / (height * height);

            return Math.Round(imc, 2);
        }

        private void SetWeight()
        {
            tvWeight.Text = currentWeight.ToString();
        }

        private void SetAge()
        {
            tvAge.Text = currentAge.ToString();
        }

        private void ChangeGender()
        {
            if (viewMale.Pressed)
            {
                isMaleSelected = true;
                isFemaleSelected = false;
            }
            else
            {
                isMaleSelected = false;
                isFemaleSelected = true;
            }
        }

        private void SetGenderColor()
        {
            viewMale.SetCardBackgroundColor(isMaleSelected ?
                ContextCompat.GetColor(this, Resource.Color.background_component_selected) :
                ContextCompat.GetColor(this, Resource.Color.background_component));

            viewFemale.SetCardBackgroundColor(isFemaleSelected ?
                ContextCompat.GetColor(this, Resource.Color.background_component_selected) :
                ContextCompat.GetColor(this, Resource.Color.background_component));
        }
    }
}