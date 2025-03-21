// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text;

namespace System.Management.Automation
{
    public enum OutputRendering
    {
        Host = 0,
        PlainText = 1,
        Ansi = 2,
    }

    public enum ProgressView
    {
        Minimal = 0,
        Classic = 1,
    }

    public sealed class PSStyle
    {
        public sealed class ForegroundColor
        {
            public string Black { get; } = "\x1b[30m";
            public string Red { get; } = "\x1b[31m";
            public string Green { get; } = "\x1b[32m";
            public string Yellow { get; } = "\x1b[33m";
            public string Blue { get; } = "\x1b[34m";
            public string Magenta { get; } = "\x1b[35m";
            public string Cyan { get; } = "\x1b[36m";
            public string White { get; } = "\x1b[37m";
            public string BrightBlack { get; } = "\x1b[90m";
            public string BrightRed { get; } = "\x1b[91m";
            public string BrightGreen { get; } = "\x1b[92m";
            public string BrightYellow { get; } = "\x1b[93m";
            public string BrightBlue { get; } = "\x1b[94m";
            public string BrightMagenta { get; } = "\x1b[95m";
            public string BrightCyan { get; } = "\x1b[96m";
            public string BrightWhite { get; } = "\x1b[97m";

            public string FromRgb(byte red, byte green, byte blue)
            {
                return $"\x1b[38;2;{red};{green};{blue}m";
            }

            public string FromRgb(int rgb)
            {
                byte red, green, blue;
                blue = (byte)(rgb & 0xFF);
                rgb >>= 8;
                green = (byte)(rgb & 0xFF);
                rgb >>= 8;
                red = (byte)(rgb & 0xFF);
                return FromRgb(red, green, blue);
            }

            public string FromConsoleColor(ConsoleColor color)
            {
                return MapForegroundColorToEscapeSequence(color);
            }
        }

        public sealed class BackgroundColor
        {
            public string Black { get; } = "\x1b[40m";
            public string Red { get; } = "\x1b[41m";
            public string Green { get; } = "\x1b[42m";
            public string Yellow { get; } = "\x1b[43m";
            public string Blue { get; } = "\x1b[44m";
            public string Magenta { get; } = "\x1b[45m";
            public string Cyan { get; } = "\x1b[46m";
            public string White { get; } = "\x1b[47m";
            public string BrightBlack { get; } = "\x1b[100m";
            public string BrightRed { get; } = "\x1b[101m";
            public string BrightGreen { get; } = "\x1b[102m";
            public string BrightYellow { get; } = "\x1b[103m";
            public string BrightBlue { get; } = "\x1b[104m";
            public string BrightMagenta { get; } = "\x1b[105m";
            public string BrightCyan { get; } = "\x1b[106m";
            public string BrightWhite { get; } = "\x1b[107m";

            public string FromRgb(byte red, byte green, byte blue)
            {
                return $"\x1b[48;2;{red};{green};{blue}m";
            }

            public string FromRgb(int rgb)
            {
                byte red, green, blue;
                blue = (byte)(rgb & 0xFF);
                rgb >>= 8;
                green = (byte)(rgb & 0xFF);
                rgb >>= 8;
                red = (byte)(rgb & 0xFF);
                return FromRgb(red, green, blue);
            }

            public string FromConsoleColor(ConsoleColor color)
            {
                return MapBackgroundColorToEscapeSequence(color);
            }
        }

        public sealed class ProgressConfiguration
        {
            public string Style
            {
                get => _style;
                set => _style = ValidateNoContent(value);
            }
            private string _style = "\x1b[33;1m";

            public int MaxWidth
            {
                get => _maxWidth;
                set
                {
                    if (value < 18)
                    {
                        throw new ArgumentOutOfRangeException(nameof(MaxWidth), "The MaxWidth for the Progress rendering must be at least 18 to render correctly.");
                    }
                    _maxWidth = value;
                }
            }
            private int _maxWidth = 120;

            public ProgressView View { get; set; } = ProgressView.Minimal;
            public bool UseOSCIndicator { get; set; } = false;
        }

        public sealed class FormattingData
        {
            public string FormatAccent
            {
                get => _formatAccent;
                set => _formatAccent = ValidateNoContent(value);
            }
            private string _formatAccent = "\x1b[32;1m";

            public string TableHeader
            {
                get => _tableHeader;
                set => _tableHeader = ValidateNoContent(value);
            }
            private string _tableHeader = "\x1b[32;1m";

            public string CustomTableHeaderLabel
            {
                get => _customTableHeaderLabel;
                set => _customTableHeaderLabel = ValidateNoContent(value);
            }
            private string _customTableHeaderLabel = "\x1b[32;1;3m";

            public string ErrorAccent
            {
                get => _errorAccent;
                set => _errorAccent = ValidateNoContent(value);
            }
            private string _errorAccent = "\x1b[36;1m";

            public string Error
            {
                get => _error;
                set => _error = ValidateNoContent(value);
            }
            private string _error = "\x1b[31;1m";

            public string Warning
            {
                get => _warning;
                set => _warning = ValidateNoContent(value);
            }
            private string _warning = "\x1b[33;1m";

            public string Verbose
            {
                get => _verbose;
                set => _verbose = ValidateNoContent(value);
            }
            private string _verbose = "\x1b[33;1m";

            public string Debug
            {
                get => _debug;
                set => _debug = ValidateNoContent(value);
            }
            private string _debug = "\x1b[33;1m";

            public string FeedbackName
            {
                get => _feedbackName;
                set => _feedbackName = ValidateNoContent(value);
            }
            private string _feedbackName = "\x1b[33m";

            public string FeedbackText
            {
                get => _feedbackText;
                set => _feedbackText = ValidateNoContent(value);
            }
            private string _feedbackText = "\x1b[96m";

            public string FeedbackAction
            {
                get => _feedbackAction;
                set => _feedbackAction = ValidateNoContent(value);
            }
            private string _feedbackAction = "\x1b[97m";
        }

        public sealed class FileInfoFormatting
        {
            public string Directory
            {
                get => _directory;
                set => _directory = ValidateNoContent(value);
            }
            private string _directory = "\x1b[44;1m";

            public string SymbolicLink
            {
                get => _symbolicLink;
                set => _symbolicLink = ValidateNoContent(value);
            }
            private string _symbolicLink = "\x1b[36;1m";

            public string Executable
            {
                get => _executable;
                set => _executable = ValidateNoContent(value);
            }
            private string _executable = "\x1b[32;1m";

            public sealed class FileExtensionDictionary
            {
                private static string ValidateExtension(string extension)
                {
                    if (!extension.StartsWith("."))
                    {
                        throw new ArgumentException("When adding or removing extensions, the extension must start with a period.");
                    }
                    return extension;
                }
                private readonly Dictionary<string, string> _extensionDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                public void Add(string extension, string decoration)
                {
                    _extensionDictionary.Add(ValidateExtension(extension), ValidateNoContent(decoration));
                }

                internal void AddWithoutValidation(string extension, string decoration)
                {
                    _extensionDictionary.Add(extension, decoration);
                }

                public void Remove(string extension)
                {
                    _extensionDictionary.Remove(ValidateExtension(extension));
                }

                public void Clear()
                {
                    _extensionDictionary.Clear();
                }

                public string this[string extension]
                {
                    get
                    {
                        return _extensionDictionary[ValidateExtension(extension)];
                    }
                    set
                    {
                        _extensionDictionary[ValidateExtension(extension)] = ValidateNoContent(value);
                    }
                }

                public bool ContainsKey(string extension)
                {
                    if (string.IsNullOrEmpty(extension))
                    {
                        return false;
                    }
                    return _extensionDictionary.ContainsKey(ValidateExtension(extension));
                }

                public IEnumerable<string> Keys
                {
                    get
                    {
                        return _extensionDictionary.Keys;
                    }
                }
            }

            public FileExtensionDictionary Extension { get; }

            public FileInfoFormatting()
            {
                Extension = new FileExtensionDictionary();
                Extension.AddWithoutValidation(".zip", "\x1b[31;1m");
                Extension.AddWithoutValidation(".tgz", "\x1b[31;1m");
                Extension.AddWithoutValidation(".gz", "\x1b[31;1m");
                Extension.AddWithoutValidation(".tar", "\x1b[31;1m");
                Extension.AddWithoutValidation(".nupkg", "\x1b[31;1m");
                Extension.AddWithoutValidation(".cab", "\x1b[31;1m");
                Extension.AddWithoutValidation(".7z", "\x1b[31;1m");
                Extension.AddWithoutValidation(".ps1", "\x1b[33;1m");
                Extension.AddWithoutValidation(".psd1", "\x1b[33;1m");
                Extension.AddWithoutValidation(".psm1", "\x1b[33;1m");
                Extension.AddWithoutValidation(".ps1xml", "\x1b[33;1m");
            }
        }

        public OutputRendering OutputRendering { get; set; } = OutputRendering.Host;
        public string Reset { get; } = "\x1b[0m";
        public string BlinkOff { get; } = "\x1b[25m";
        public string Blink { get; } = "\x1b[5m";
        public string BoldOff { get; } = "\x1b[22m";
        public string Bold { get; } = "\x1b[1m";
        public string DimOff { get; } = "\x1b[22m";
        public string Dim { get; } = "\x1b[2m";
        public string Hidden { get; } = "\x1b[8m";
        public string HiddenOff { get; } = "\x1b[28m";
        public string Reverse { get; } = "\x1b[7m";
        public string ReverseOff { get; } = "\x1b[27m";
        public string ItalicOff { get; } = "\x1b[23m";
        public string Italic { get; } = "\x1b[3m";
        public string UnderlineOff { get; } = "\x1b[24m";
        public string Underline { get; } = "\x1b[4m";
        public string StrikethroughOff { get; } = "\x1b[29m";
        public string Strikethrough { get; } = "\x1b[9m";

        public string FormatHyperlink(string text, Uri link)
        {
            return $"\x1b]8;;{link}\x1b\\{text}\x1b]8;;\x1b\\";
        }

        public FormattingData Formatting { get; }
        public ProgressConfiguration Progress { get; }
        public ForegroundColor Foreground { get; }
        public BackgroundColor Background { get; }
        public FileInfoFormatting FileInfo { get; }

        private static readonly PSStyle s_psstyle = new PSStyle();

        private PSStyle()
        {
            Formatting = new FormattingData();
            Progress = new ProgressConfiguration();
            Foreground = new ForegroundColor();
            Background = new BackgroundColor();
            FileInfo = new FileInfoFormatting();
        }

        private static string ValidateNoContent(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException();
            StringBuilder content = new StringBuilder();
            int state = 0;

            foreach (char c in text)
            {
                switch (state)
                {
                    case 0:
                        if (c == 27)
                        {
                            state = 1;
                        }
                        else
                        {
                            content.Append(c);
                        }
                        break;
                    case 1:
                        if (c == '[')
                        {
                            state = 2;
                        }
                        else
                        {
                            state = 0;
                            content.Append(c);
                        }
                        break;
                    default:
                        if (char.IsLetter(c))
                        {
                            state = 0;
                        }
                        else
                        {
                            if (c != ';' && !char.IsDigit(c))
                            {
                                content.Append(c);
                                state = 0;
                            }
                        }
                        break;
                }
            }

            if (content.Length > 0)
            {
                throw new ArgumentException($"The specified string contains printable content when it should only contain ANSI escape sequences: {content}");
            }

            if (state != 0)
            {
                throw new ArgumentException($"The specified string contains invalid ANSI escape sequences");
            }

            return text;
        }

        public static PSStyle Instance
        {
            get
            {
                return s_psstyle;
            }
        }

        private static readonly string[] BackgroundColorMap =
            {
                "\x1b[40m",
                "\x1b[44m",
                "\x1b[42m",
                "\x1b[46m",
                "\x1b[41m",
                "\x1b[45m",
                "\x1b[43m",
                "\x1b[47m",
                "\x1b[100m",
                "\x1b[104m",
                "\x1b[102m",
                "\x1b[106m",
                "\x1b[101m",
                "\x1b[105m",
                "\x1b[103m",
                "\x1b[107m",
            };
        private static readonly string[] ForegroundColorMap =
            {
                "\x1b[30m",
                "\x1b[34m",
                "\x1b[32m",
                "\x1b[36m",
                "\x1b[31m",
                "\x1b[35m",
                "\x1b[33m",
                "\x1b[37m",
                "\x1b[90m",
                "\x1b[94m",
                "\x1b[92m",
                "\x1b[96m",
                "\x1b[91m",
                "\x1b[95m",
                "\x1b[93m",
                "\x1b[97m",
            };

        internal static string MapColorToEscapeSequence(ConsoleColor color, bool isBackground)
        {
            int index = (int)color;
            if (index < 0 || index >= ForegroundColorMap.Length)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(color));
            }
            return (isBackground ? BackgroundColorMap : ForegroundColorMap)[index];
        }

        public static string MapForegroundColorToEscapeSequence(ConsoleColor foregroundColor) => MapColorToEscapeSequence(foregroundColor, isBackground: false);
        public static string MapBackgroundColorToEscapeSequence(ConsoleColor backgroundColor) => MapColorToEscapeSequence(backgroundColor, isBackground: true);

        public static string MapColorPairToEscapeSequence(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            int foreIndex = (int)foregroundColor;
            int backIndex = (int)backgroundColor;
            if (foreIndex < 0 || foreIndex >= ForegroundColorMap.Length)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(foregroundColor));
            }
            if (backIndex < 0 || backIndex >= ForegroundColorMap.Length)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(backgroundColor));
            }
            string foreground = ForegroundColorMap[foreIndex];
            string background = BackgroundColorMap[backIndex];
            return string.Concat(
                foreground.Substring(0, foreground.Length - 1),
                ";",
                background.Substring(2));
        }
    }
}
