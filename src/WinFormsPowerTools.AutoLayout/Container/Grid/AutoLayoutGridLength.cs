namespace WinFormsPowerTools.AutoLayout
{
    public struct AutoLayoutGridLength
    {
        public double Value { get; set; }
        public bool IsAbsolut { get; set; }
        public bool IsAuto { get; set; }
        public bool IsStar { get; set; }

        public static bool TryParse(string value, out AutoLayoutGridLength gridLength)
        {
            gridLength = new AutoLayoutGridLength();

            try
            {
                Parse(ref gridLength, value);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        
        internal static void Parse(ref AutoLayoutGridLength gridLength, string value)
        {
            switch (value)
            {
                case "Auto":
                    gridLength.IsAuto = true;
                    gridLength.IsStar = false;
                    gridLength.IsAbsolut = false;
                    break;
                    
                default:
                    if (value.EndsWith("*"))
                    {
                        gridLength.IsStar = true;
                        gridLength.IsAuto = false;
                        gridLength.IsAbsolut = false;
                        gridLength.Value = double.Parse(value[..^1]);
                    }
                    else
                    {
                        gridLength.IsAbsolut = true;
                        gridLength.Value = double.Parse(value);
                    }
                    break;
            }
        }
    }
}
