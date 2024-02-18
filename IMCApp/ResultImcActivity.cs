using Android.Content;
using AndroidX.Activity;
using AndroidX.Core.Content;
using Java.Security.Interfaces;
namespace IMCApp;

[Activity(Label = "ResultImcActivity")]
public class ResultImcActivity : Activity
{
    private TextView tvResult = default!;
    private TextView tvDescription = default!;
    private TextView tvIMC = default!;

    private Button btnRecalculate = default!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.activity_result_imc);

        // Create your application here
        double result = Intent!.GetDoubleExtra("IMC_RESULT", 0.0);
        InitComponents();
        InitUI(result);
        InitListeners();
    }

    private void InitListeners()
    {
        btnRecalculate.Click += (sender, e) =>
        {
            Finish();
        };
    }

    private void InitUI(double result)
    {
        tvIMC.Text = result.ToString();
        switch(result)
        {
            case >= 0.00f and <= 18.49f: //Bajo peso
                tvResult.Text = "Bajo Peso";
                tvResult.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.underweight)));
                tvDescription.Text = "Estás por debajo de lo óptimo para tu peso y altura";
                break;
            case >= 18.50f and <= 24.99f: //Peso normal
                tvResult.Text = "Normal";
                tvResult.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.normal)));
                tvDescription.Text = "Estás en lo óptimo para tu peso y altura";
                break;
            case >= 25.00f and <= 29.99f: //Sobrepeso
                tvResult.Text = "Sobrepeso";
                tvResult.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.overweight)));
                tvDescription.Text = "Estás por encima de lo óptimo para tu peso y altura";
                break;
            case >= 30.00f and <= 34.99f: //Obesidad I
                tvResult.Text = "Obesidad";
                tvResult.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.obesity)));
                tvDescription.Text = "Estás muy por encima de lo óptimo para tu peso y altura";
                break;
            default:
                tvIMC.Text = "Error";
                tvResult.Text = "Error";
                tvResult.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.obesity)));
                tvDescription.Text = "Error";
                break;
        }
            
    }

    private void InitComponents()
    {
        tvResult = FindViewById<TextView>(Resource.Id.tvResult)!;
        tvDescription = FindViewById<TextView>(Resource.Id.tvDescription)!;
        tvIMC = FindViewById<TextView>(Resource.Id.tvIMC)!;
        btnRecalculate = FindViewById<Button>(Resource.Id.btnRecalculate)!;
    }
}