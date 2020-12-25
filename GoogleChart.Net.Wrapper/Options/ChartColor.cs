using System.Drawing;

namespace GoogleChart.Net.Wrapper.Options
{
    public class ChartColor
    {
        private ChartColor(string htmlValue)
        {
            HtmlValue = htmlValue;
        }
        public string HtmlValue { get; }

        public static implicit operator ChartColor(string value) => new ChartColor(value);
        public static implicit operator ChartColor(Color color) => new ChartColor("#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
    }
}
