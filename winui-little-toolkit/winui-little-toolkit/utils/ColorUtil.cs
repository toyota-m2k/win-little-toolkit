using System;
using System.Text.RegularExpressions;

namespace io.github.toyota32k.toolkit.winui.utils;

/**
 * System.Drawing.Color <--> Windows.UI.Color 相互変換
 * コントロールの配色は Windows.UI.Color を使うが System.Drawing.Colorの方がやや高機能なので、相互に変換できるようにしておく。
 */
public static class ColorUtil {
    private static readonly Regex colorReg = new(@"#*(?<a>[0-9a-f]{2})*(?<r>[0-9a-f]{2})(?<g>[0-9a-f]{2})(?<b>[0-9a-f]{2})", RegexOptions.IgnoreCase);
    public static System.Drawing.Color ParseSystemColor(string colorText) {
        var m = colorReg.Match(colorText);
        if (!m.Success) return System.Drawing.Color.Gray;
        var a = m.Groups["a"].Value;
        var r = m.Groups["r"].Value;
        var g = m.Groups["g"].Value;
        var b = m.Groups["b"].Value;
        return !string.IsNullOrEmpty(a)
            ? System.Drawing.Color.FromArgb(Convert.ToByte(a,16), Convert.ToByte(r, 16), Convert.ToByte(g,16), Convert.ToByte(b,16)) 
            : System.Drawing.Color.FromArgb(0xFF, Convert.ToByte(r, 16), Convert.ToByte(g, 16), Convert.ToByte(b, 16));
    }

    public static Windows.UI.Color toWindowsColor(this System.Drawing.Color color) {
        return Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
    public static System.Drawing.Color toSystemColor(this Windows.UI.Color color) {
        return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
    public static Windows.UI.Color ParseWindowsColor(string colorText) {
        // return ParseSystemColor(colorText).toWindowsColor();
        var m = colorReg.Match(colorText);
        if (!m.Success) return System.Drawing.Color.Gray.toWindowsColor();
        var a = m.Groups["a"].Value;
        var r = m.Groups["r"].Value;
        var g = m.Groups["g"].Value;
        var b = m.Groups["b"].Value;
        return !string.IsNullOrEmpty(a)
            ? Windows.UI.Color.FromArgb(Convert.ToByte(a, 16), Convert.ToByte(r, 16), Convert.ToByte(g, 16), Convert.ToByte(b, 16))
            : Windows.UI.Color.FromArgb(0xFF, Convert.ToByte(r, 16), Convert.ToByte(g, 16), Convert.ToByte(b, 16));
    }
}
