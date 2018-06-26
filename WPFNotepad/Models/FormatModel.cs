using System;
using System.IO;
using System.Windows;
using System.Windows.Media;


namespace WPFNotepad.Models
{
    /// <summary>
    /// Model for document format properties.
    /// </summary>

    
    public class FormatModel :ObservableObject
    {
        private FontStyle _style;
        public FontStyle Style
        {
            get { return _style; }
            set { OnPropertyChanged(ref _style, value); WriteFormatDoc(); }
        }
        private FontWeight _weight;
        public FontWeight Weight
        {
            get { return _weight; }
            set { OnPropertyChanged(ref _weight, value); WriteFormatDoc(); }
        }

        private FontFamily _family;
        public FontFamily Family
        {
            get { return _family; }
            set
            {
                OnPropertyChanged(ref _family, value); WriteFormatDoc(); }
        }

        private TextWrapping _wrap;
        public TextWrapping Wrap
        {
            get { return _wrap; }
            set
            {
                OnPropertyChanged(ref _wrap, value);
                isWrapped = value == TextWrapping.Wrap ? true : false;
                WriteFormatDoc();
            }
        }

        private bool _isWrapped;
        public bool isWrapped
        {
            get { return _isWrapped; }
            set { OnPropertyChanged(ref _isWrapped, value); WriteFormatDoc(); }
        }

        private double _size;
        public double Size
        {
            get { return _size; }
            set { OnPropertyChanged(ref _size, value); WriteFormatDoc(); }
        }
        private string _configFileSave = AddFilePath();
        public string configFileSave
        {
            get { return _configFileSave; }
            set { OnPropertyChanged(ref _configFileSave, value); WriteFormatDoc(); }
        }


        //---------------------Methods Start Here---------------------
        public static string AddFilePath(string filepath = null)
        {
            try
            {
                if(string.IsNullOrEmpty(filepath))
                {
                    string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "config.cfg");
                    if (File.Exists(destPath))
                    {
                        return File.ReadAllText(destPath);
                    }
                    
                }
                return Path.Combine("%USERPROFILE%", "GKEdit", "formatconfig.cfg");

            }
            catch (FileLoadException f)
            {
                throw f;
            }
        }
        public void ReadFormatDoc()
        {
            this.configFileSave = AddFilePath(this._configFileSave);
            FormatModel formatModel = Methods.XmlSerialization.ReadFromXmlFile<FormatModel>(this.configFileSave);
            this._style = formatModel._style;
            this._weight = formatModel._weight;
            this._family = formatModel._family;
            this._size = formatModel._size;
            this._wrap = formatModel._wrap;
        }
        public void WriteFormatDoc()
        {
            Methods.XmlSerialization.WriteToXmlFile<FormatModel>(this._configFileSave, this, false);
        }

        public FormatModel()
        {
            this._family = new FontFamily(null, "Courier New");
            this._size = Convert.ToDouble(14);
            this._wrap = TextWrapping.NoWrap;
        }
        
    }
}
