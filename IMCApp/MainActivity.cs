using Android.Views;
using AndroidX.CardView.Widget;
using AndroidX.Core.Content;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Slider;
using static Android.Renderscripts.Sampler;

namespace IMCApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public partial class MainActivity : Activity
    {
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
            viewMale = FindViewById<CardView>(Resource.Id.view_male)!;
            viewFemale = FindViewById<CardView>(Resource.Id.view_female)!;

            tvHeight = FindViewById<TextView>(Resource.Id.tv_height)!;
            rsHeight = FindViewById<RangeSlider>(Resource.Id.rs_height)!;

            btnSubtractWeight = FindViewById<FloatingActionButton>(Resource.Id.btn_subtract_weight)!;
            btnAddWeight = FindViewById<FloatingActionButton>(Resource.Id.btn_plus_weight)!;
            tvWeight = FindViewById<TextView>(Resource.Id.tv_weight)!;

            btnSubtractAge = FindViewById<FloatingActionButton>(Resource.Id.btn_subtract_age)!;
            btnAddAge = FindViewById<FloatingActionButton>(Resource.Id.btn_plus_age)!;
            tvAge = FindViewById<TextView>(Resource.Id.tv_age)!;

            btnCalculate = FindViewById<Button>(Resource.Id.btn_calculate)!;
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