using System.Runtime.CompilerServices;
using FibNumb.Consts;

namespace FibNumb;

public partial class MainPage : ContentPage
{
	int count = 0;
	List<Button> BtnArrG=new List<Button>();


    public MainPage()
	{
		InitializeComponent();
        vr.MainP = this;
		vr.MainView = vr.MainP.Content;
	//	BtnGrid.HeightRequest = Layot.Height;
	//	BtnGrid.WidthRequest = Layot.Width;
		LvlSel.WidthRequest=200;
		LvlSel.HeightRequest = 50;
        Button button = new Button();
        button.Text = "testc";
		button.IsEnabled = true;
		button.Pressed += OnCounterClicked;
		// BtnGrid.Add(button, 0, 3);
		BtnGrid.RowSpacing = 1;
		BtnGrid.ColumnSpacing = 1;
		Level.Text = LvlSel.Value.ToString();
		Score.Text = Main.ScoreN.ToString();
		Lives.Text = Main.LivesN.ToString();
		Challenge.Text = Main.NumbToF.ToString();
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
        Page pg = new Page();
		pg.Layout(new Rect(0, 0, this.Width, this.Height));
		vr.MainView.Layout(new Rect(0, 0,100, 20));
		vr.MainP.Layout(new Rect(0, 0, 55, 90));
		Button button = new Button();
		button.Text = "testc";
		
		BtnGrid.Add(button, 0,3);
        count++;
	}
    private void OnSliderValueChanged(object sender, EventArgs e)
    {
        Main.AspectRatioWidth = Math.Round((vr.MainView.Width / vr.MainView.Height)/2,1);
        Main.AspectRatioHeight = Math.Round(vr.MainView.Height / vr.MainView.Width,1);
        uint lvl = (uint)LvlSel.Value;
		if (lvl > Consts.Main.level)
		{
			Methods.LevelSetup.OnSliderValueChanged(lvl);
			GridChangeIncr();
		}
		else
		{
            Methods.LevelSetup.OnSliderValueChanged(lvl);
            GridChangeDecr();
			BtnGrid.Clear();
        }
		Level.Text = Consts.Main.level.ToString();//.level.ToString();
		BtnIncr();
        Score.Text = Main.ScoreN.ToString();
        Lives.Text = Main.LivesN.ToString();
        Challenge.Text = Main.NumbToF.ToString();
    }

	private void OnBtnClick(object sender, EventArgs e)
	{
		if (((Button)sender).Text == Challenge.Text)
		{
			Main.ScoreN += 100;
			((Button)sender).BackgroundColor = Colors.Green;
			((Button)sender).TextColor = Color.FromArgb("#DFD8F7");
        }
		else
		{
			Main.ScoreN -= 100;
            ((Button)sender).BackgroundColor = Colors.Red;
            ((Button)sender).TextColor = Color.FromArgb("#DFD8F7");
        }
        BtnArrG.Remove((Button)sender);
        if (Main.ScoreN < 0) Main.LivesN -= 1;
		Random rnd=new Random();
		if (BtnArrG.Count > 0)
		{
			Main.NumbToF = BtnArrG.Select(x => uint.Parse(x.Text)).ToList()[rnd.Next(BtnArrG.Count)]; //Main.FibNumbs[rnd.Next(Main.FibNumbs.Length/3)];
			Score.Text = Main.ScoreN.ToString();
			Lives.Text = Main.LivesN.ToString();
			Challenge.Text = Main.NumbToF.ToString();
			((Button)sender).IsEnabled = false;
		}
        //App.Current.CloseWindow(this.Window);
    }

	private void GridChangeIncr()
	{
        Grid TmpGr = new Grid();
		for (int i = 0; i < Consts.Main.NmbOfSquares; i++)
		{
			TmpGr.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(Main.SizeOfSquare*Main.AspectRatioWidth, GridUnitType.Auto)));
			TmpGr.RowDefinitions.Add(new RowDefinition(Main.SizeOfSquare*Main.AspectRatioHeight));
		}
        BtnGrid.ColumnDefinitions = TmpGr.ColumnDefinitions;
        BtnGrid.RowDefinitions = TmpGr.RowDefinitions;
    }
	private void GridChangeDecr()
	{
		Grid TmpGr = new Grid();
        for (int i = 0; i < Consts.Main.NmbOfSquares; i++)
        {
            TmpGr.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(Main.SizeOfSquare * Main.AspectRatioWidth, GridUnitType.Auto)));
            TmpGr.RowDefinitions.Add(new RowDefinition(Main.SizeOfSquare * Main.AspectRatioHeight));
        }
        BtnGrid.ColumnDefinitions = TmpGr.ColumnDefinitions;
        BtnGrid.RowDefinitions = TmpGr.RowDefinitions;
    }
    

	public void BtnIncr()
	{
		int Area = (int)Math.Pow(Consts.Main.NmbOfSquares, 2);
        Button[] BtnArrX= new Button[Area];
		for(int i=0;i<BtnArrX.Length;i++)
		{
			BtnArrX[i] = new Button();
            BtnArrX[i].Text = Main.FibNumbs[(int)(Math.Floor((double)i/Main.NmbOfSquares)+(i%Main.NmbOfSquares*2))].ToString();// i.ToString();
			BtnArrX[i].WidthRequest = Main.SizeOfSquare * Main.AspectRatioWidth;
			BtnArrX[i].HeightRequest = Main.SizeOfSquare * Main.AspectRatioHeight;
			BtnArrX[i].TextColor = (Main.Map[i])?Color.FromArgb("#DFD8F7"):Color.FromArgb("#FF6B5814");
			BtnArrX[i].Pressed += OnBtnClick;

        }
		for (int i= 0; i< Consts.Main.NmbOfSquares; i++)
		{

			for (int j = 0; j < Consts.Main.NmbOfSquares; j++)
			{
				BtnGrid.Add(BtnArrX[j+i*Consts.Main.NmbOfSquares], j, i);
				

			}
		}
		BtnArrG = BtnArrX.ToList();
    }

    public void BtnDecr()
    {
        int Area = (int)Math.Pow(Consts.Main.NmbOfSquares, 2);
		int CurArea = BtnGrid.Count();
        Button[] BtnArrX = new Button[Area];
        for (int i = 0; i < BtnArrX.Length; i++)
        {
            BtnArrX[i] = new Button();
            BtnArrX[i].Text = i.ToString();
            BtnArrX[i].WidthRequest = Main.SizeOfSquare * Main.AspectRatioWidth;
            BtnArrX[i].HeightRequest = Main.SizeOfSquare * Main.AspectRatioHeight;
        }
        for (int i = 0; i < Consts.Main.NmbOfSquares; i++)
        {

            for (int j = 0; j < Consts.Main.NmbOfSquares; j++)
            {
                BtnGrid.Add(BtnArrX[j + i * Consts.Main.NmbOfSquares], i, j);

            }
        }
        BtnArrG = BtnArrX.ToList();
    }

}

public static class vr
{
    public static string Tag = "";
    public static MainPage MainP;
    public static View MainView;
}

